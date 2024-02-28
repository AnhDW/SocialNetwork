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
    public class RoomChatRepo : IRoomChatRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RoomChatRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddMessage(RoomChat roomChat)
        {
            _context.RoomChats.Add(roomChat);
        }

        public void DeleteMessage(RoomChat roomChat)
        {
            _context.RoomChats.Remove(roomChat);
        }

        public async Task<RoomChat> GetMessageInRoomChat(int id)
        {
            return await _context.RoomChats.FindAsync(id);
        }

        public async Task<PagedList<RoomChatDto>> GetRoomChats(RoomChatParams roomChatParams)
        {
            var query = _context.RoomChats.OrderByDescending(r=>r.Timestamp).AsQueryable();

            query = query.Where(r => r.RoomId == roomChatParams.RoomId);

            var roomChats = query.ProjectTo<RoomChatDto>(_mapper.ConfigurationProvider);

            return await PagedList<RoomChatDto>.CreateAsync(roomChats, roomChatParams.PageNumber, roomChatParams.PageSize);
        }

        //public async Task<PagedList<RoomChatDto>> GetUserLikes(RoomChatParams roomChatParams)
        //{
        //    var query = _context.RoomChats.OrderByDescending(rc => rc.Timestamp).AsQueryable();
        //    query = roomChatParams.Container switch
        //    {
        //        "Inbox" => query.Where(u => u.Room.RoomMembers.FirstOrDefault(rm => rm.Member.UserName == roomChatParams.Username).Member.UserName == roomChatParams.Username),
        //        "Outbox" => query.Where(),
        //    };
        //}

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
