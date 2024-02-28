using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Helpers;

namespace SocialNetwork.API.Services.IServices
{
    public interface ICommentRepo
    {
        Task<PagedList<CommentDto>> GetComments(CommentsParams commentsParams);
        Task<Comment> GetUserComment(int userId, int postId, DateTime timestamp);
        Task<User> GetPostWithComments(int userId); //đã bình luận những post nào
        Task<PagedList<InteractWithPostDto>> GetUserComments(CommentsParams commentParams);
        Task Update(Comment comment);
        Task Delete(Comment comment);
    }
}
