using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Helpers;

namespace SocialNetwork.API.Services.IServices
{
    public interface IFriendRepo
    {
        Task<Friend> GetUserFriend(int userId, int friendId);
        Task<Friend> GetInvitation(int friendId);
        Task<User> GetUserWithFriends(int userId);
        Task<PagedList<FriendRequestDto>> GetInvitations(int friendId);
        Task<PagedList<FriendDto>> GetUserFriends(FriendsParams friendsParams);

        void Add(Friend friend);
        void Update(Friend friend);
        void Delete(Friend friend);
    }
}
