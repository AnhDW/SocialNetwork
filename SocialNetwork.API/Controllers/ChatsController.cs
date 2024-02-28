using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Extensions;
using SocialNetwork.API.Helpers;
using SocialNetwork.API.Services.IServices;

namespace SocialNetwork.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ValidateToken]
    public class ChatsController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IChatRepo _chatRepo;
        private readonly IMapper _mapper;

        public ChatsController(IUserRepo userRepo, IChatRepo chatRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _chatRepo = chatRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ChatDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetUsername();
            if (username == createMessageDto.RecipientUsername.ToLower()) return BadRequest("You cannot sent message to yourself");

            var sender = await _userRepo.GetByUsernameAsync(username);
            var receiver = await _userRepo.GetByUsernameAsync(createMessageDto.RecipientUsername);

            if (receiver == null) return NotFound();

            var chat = new Chat
            {
                Sender = sender,
                SenderName = username,
                Receiver = receiver,
                ReceiverName = createMessageDto.RecipientUsername,
                Content = createMessageDto.Content,
            };

            _chatRepo.AddChat(chat);

            if(await _chatRepo.SaveAllAsync()) return Ok(_mapper.Map<ChatDto>(chat));
            return BadRequest("Failed to sent message");
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<ChatDto>>> GetMessagesForUser([FromQuery] ChatParams chatParams)
        {
            chatParams.Username = User.GetUsername();

            var messages = await _chatRepo.GetChatsForUser(chatParams);

            Response.AddPaginationHeader(new PaginationHeader(
                chatParams.PageNumber, chatParams.PageSize, messages.TotalCount, messages.TotalPages));

            return messages;
        }

        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<ChatDto>>> GetMessageThread(string username)
        {
            var currentUsername = User.GetUsername();

            return Ok(await _chatRepo.GetChatsThread(currentUsername, username));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var username = User.GetUsername();
            var message = await _chatRepo.GetChat(id);

            if (message.SenderName != username && message.ReceiverName != username)
                return Unauthorized();

            if (message.SenderName == username) message.SenderDeleted = true;
            if (message.ReceiverName == username) message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted)
            {
                _chatRepo.DeleteChat(message);
            }

            if (await _chatRepo.SaveAllAsync()) return Ok();

            return BadRequest("Problem deleting message");
        }
    }
}
