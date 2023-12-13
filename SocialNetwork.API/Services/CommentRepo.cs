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
    public class CommentRepo : ICommentRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CommentRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<Comment> GetUserComment(int userId, int postId, DateTime timestamp)
        {
            return await _context.Comments.FindAsync(userId, postId, timestamp);
        }

        public async Task<User> GetPostWithComments(int userId)
        {
            return await _context.Users
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<PagedList<InteractWithPostDto>> GetUserComments(CommentsParams commentsParams)
        {
            var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            var posts = _context.Posts.AsQueryable();
            var comments = _context.Comments.AsQueryable();
            //đã thích những post nào
            if (commentsParams.Predicate == "commented")
            {
                comments = comments.Where(comment => comment.UserId == commentsParams.UserId);
                posts = comments.Select(comment => comment.Post);
            }
            //post được thích bởi ai
            if (commentsParams.Predicate == "commentedBy")
            {
                comments = comments.Where(comment=> comment.PostId == commentsParams.PostId);
                users = comments.Select(comment => comment.User);
            }

            var commentUsers = comments.Select(comment => new InteractWithPostDto
            {
                PostId = comment.PostId,
                UserId = comment.UserId,
                UserName = comment.User.UserName,
                KnownAs = comment.User.KnownAs,
                Age = comment.User.DateOfBirth.CacuateAge(),
                
            });

            return await PagedList<InteractWithPostDto>.CreateAsync(commentUsers, commentsParams.PageNumber, commentsParams.PageSize);
        }

        public async Task Update(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }
    }
}
