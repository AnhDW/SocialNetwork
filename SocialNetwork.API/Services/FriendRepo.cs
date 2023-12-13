using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.Data;
using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Helpers;
using SocialNetwork.API.Services.IServices;

namespace SocialNetwork.API.Services
{
    public class FriendRepo : IFriendRepo
    {
        private readonly DataContext _context;

        public FriendRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<Friend> GetInvitation(int friendId)
        {
            return await _context.Friends.FirstOrDefaultAsync(f => f.FriendId == friendId);
        }

        public async Task<PagedList<FriendRequestDto>> GetInvitations(int friendId)
        {
            var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            var friends = _context.Friends.OrderBy(t=>t.Timestamp).AsQueryable();

            friends = friends.Where(f=>f.FriendId == friendId);
            users = friends.Select(friend =>friend.ConnectedUser);

            var friendUsers = friends.Select(friend => new FriendRequestDto
            {
                Id = friend.UserId,
                UserName = friend.CurrentUser.UserName,
                AvatarUrl = friend.CurrentUser.AvatarUrl,
                Gender = friend.CurrentUser.Gender,
                DateOfBirth = friend.CurrentUser.DateOfBirth
            });

            return await PagedList<FriendRequestDto>.CreateAsync(friendUsers, 1, friendUsers.Count());
        }

        public async Task<Friend> GetUserFriend(int userId, int friendId)
        {
            return await _context.Friends.FindAsync(userId, friendId);
        }

        public async Task<PagedList<FriendDto>> GetUserFriends(FriendsParams friendsParams)
        {
            var users = _context.Users.OrderBy(u=>u.UserName).AsQueryable();
            var friends = _context.Friends.AsQueryable();

            if(friendsParams.Predicate == "connected")
            {
                friends = friends.Where(friend => friend.UserId == friendsParams.UserId);
                users = friends.Select(friend => friend.CurrentUser); 
            }

            if (friendsParams.Predicate == "connectedBy")
            {
                friends = friends.Where(friend => friend.FriendId == friendsParams.FriendId);
                users = friends.Select(friend => friend.ConnectedUser);
            }

            var friendUsers = friends.Select(friend => new FriendDto
            {
                UserId = friend.UserId,
                UserName = friend.CurrentUser.UserName,
                FriendId = friend.FriendId,
                FriendName = friend.ConnectedUser.UserName,
            });

            return await PagedList<FriendDto>.CreateAsync(friendUsers, friendsParams.PageNumber, friendsParams.PageSize);
        }

        public async Task<User> GetUserWithFriends(int userId)
        {
            return await _context.Users
                .Include(x => x.ConnectedUsers)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task Update(Friend friend)
        {
            _context.Friends.Update(friend);
            await _context.SaveChangesAsync();
        }

        void IFriendRepo.Add(Friend friend)
        {
            _context.Friends.Add(friend);
        }

        void IFriendRepo.Delete(Friend friend)
        {
            _context.Friends.Remove(friend);
        }

        void IFriendRepo.Update(Friend friend)
        {
            _context.Friends.Update(friend);
        }
    }
}
