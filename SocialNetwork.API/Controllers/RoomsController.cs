using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Dtos;
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
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepo _roomRepo;
        private readonly IMapper _mapper;

        public RoomsController(IRoomRepo roomRepo, IMapper mapper)
        {
            _roomRepo = roomRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<InteractWithRoomDto>>> GetRooms([FromQuery] RoomParams roomParams)
        {
            roomParams.UserId = User.GetUserId();
            var rooms = await _roomRepo.GetRooms(roomParams);

            Response.AddPaginationHeader(
                new PaginationHeader(rooms.CurrentPage, rooms.PageSize, rooms.TotalCount, rooms.TotalPages));
            
            return Ok(rooms);
        }

        [HttpGet("{roomId}")]
        public async Task<ActionResult<RoomDto>> GetRoomById(int roomId)
        {
            var room = await _roomRepo.GetRoom(roomId);
            return _mapper.Map<RoomDto>(room);
        }
    }
}
