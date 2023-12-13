using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.Data;
using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Helpers;
using SocialNetwork.API.Services.IServices;

namespace SocialNetwork.API.Services
{
    public class ChatRepo : IChatRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ChatRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddChat(Chat chat)
        {
            _context.Chats.Add(chat);
        }

        public void DeleteChat(Chat chat)
        {
            _context.Chats.Remove(chat);
        }

        public async Task<Chat> GetChat(int id)
        {
            return await _context.Chats.FindAsync(id);
        }

        public async Task<PagedList<ChatDto>> GetChatsForUser(ChatParams chatParams)
        {
            var query = _context.Chats.OrderByDescending(c=>c.CreatedDate).AsQueryable();
            query = chatParams.Container switch
            {
                "Inbox" => query.Where(u => u.ReceiverName == chatParams.Username
                    && u.RecipientDeleted == false),
                "Outbox" => query.Where(u => u.SenderName == chatParams.Username
                    && u.SenderDeleted == false),
                _ => query.Where(u => u.ReceiverName == chatParams.Username
                    && u.RecipientDeleted == false && u.DateRead == null)
            };

            var chats = query.ProjectTo<ChatDto>(_mapper.ConfigurationProvider);

            return await PagedList<ChatDto>
                .CreateAsync(chats, chatParams.PageNumber, chatParams.PageSize);
        }

        public async Task<IEnumerable<ChatDto>> GetChatsThread(string currentUsername, string reciptentUsername)
        {
            var chats = await _context.Chats
                            .Include(u=>u.Sender)
                            .Include(u => u.Receiver)
                            .Where(
                                m => m.ReceiverName == currentUsername && m.RecipientDeleted == false &&
                                m.SenderName == reciptentUsername ||
                                m.ReceiverName == reciptentUsername && m.SenderDeleted == false &&
                                m.SenderName == currentUsername
                            ).OrderBy(m => m.CreatedDate).ToListAsync();

            var unreadMessages = chats.Where(m => m.DateRead == null
                && m.ReceiverName == currentUsername).ToList();

            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.DateRead = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<IEnumerable<ChatDto>>(chats);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
