using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.Data;
using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Extensions;
using SocialNetwork.API.Helpers;
using SocialNetwork.API.Services.IServices;

namespace SocialNetwork.API.Services
{
    public class LikePostRepo : ILikePostRepo
    {
        private readonly DataContext _context;

        public LikePostRepo(DataContext context)
        {
            _context = context;
        }

        public async Task Delete(LikePost likePost)
        {
            _context.LikePosts.Remove(likePost);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetPostWithLikes(int userId)
        {
            //curentUser đã thích những post nào?
            return await _context.Users
                .Include(x=>x.LikePosts)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<LikePost> GetUserLike(int userId, int postId)
        {
            return await _context.LikePosts.FindAsync(userId, postId);
        }

        public async Task<PagedList<InteractWithPostDto>> GetUserLikes(LikesParams likesParams)
        {
            var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            var posts = _context.Posts.AsQueryable();
            var likes = _context.LikePosts.AsQueryable();
            //đã thích những post nào
            if (likesParams.Predicate == "liked")
            {
                likes = likes.Where(like => like.UserId == likesParams.UserId);
                posts = likes.Select(like => like.Post);
            }
            //post được thích bởi ai
            if (likesParams.Predicate == "likedBy")
            {
                likes = likes.Where(like => like.PostId == likesParams.PostId);
                users = likes.Select(like => like.User);
            }

            var likedUsers = likes.Select(like => new InteractWithPostDto
            {
                PostId = like.PostId, 
                UserId = like.UserId,
                UserName = like.User.UserName,
                KnownAs = like.User.KnownAs,
                Age = like.User.DateOfBirth.CacuateAge(),
                Timestamp = like.Timestamp
            });

            return await PagedList<InteractWithPostDto>.CreateAsync(likedUsers, likesParams.PageNumber, likesParams.PageSize);
        }

        public async Task Update(LikePost likePost)
        {
            _context.LikePosts.Update(likePost);
            await _context.SaveChangesAsync();
        }
    }
}
