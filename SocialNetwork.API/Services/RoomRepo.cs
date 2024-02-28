using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.Data;
using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Extensions;
using SocialNetwork.API.Helpers;
using SocialNetwork.API.Services.IServices;

namespace SocialNetwork.API.Services
{
    public class RoomRepo : IRoomRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RoomRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Room> GetRoom(int id)
        {
            return await _context.Rooms.Include(r => r.RoomMembers).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<PagedList<InteractWithRoomDto>> GetRooms(RoomParams roomParams)
        {
            var users = _context.Users.AsQueryable();
            var rooms = _context.Rooms.AsQueryable();
            var members = _context.RoomMembers.AsQueryable();

            if(roomParams.Predicate == "joined")
            {
                members = members.Where(member => member.MemberId == roomParams.UserId);
                rooms = members.Select(member => member.Room);
            }

            if(roomParams.Predicate == "joinedBy")
            {
                members = members.Where(member => member.RoomId == roomParams.RoomId);
                users = members.Select(member => member.Member);
            }

            rooms = roomParams.OrderBy switch
            {
                "create" => rooms.OrderBy(room => room.CreatedDate),
                _ => rooms.OrderBy(room => room.LastActive)
            };

            var roomUsers = members.Select(member => new InteractWithRoomDto
            {
                RoomId = member.RoomId,
                RoomName = member.Room.RoomName,
                MemberId = member.MemberId,
                UserName = member.Member.UserName,
                KnownAs = member.Member.KnownAs,
                Age = member.Member.DateOfBirth.CacuateAge(),
                JoinAt = member.JoinAt
            });

            return await PagedList<InteractWithRoomDto>.CreateAsync(
                roomUsers,
                roomParams.PageNumber,
                roomParams.PageSize
                );
        }
    }
}
