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
    public class CommentsController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly ICommentRepo _commentRepo;
        private readonly IPostRepo _postRepo;
        private readonly IMapper _mapper;

        public CommentsController(IUserRepo userRepo, IPostRepo postRepo, IMapper mapper, ICommentRepo commentRepo)
        {
            _userRepo = userRepo;
            _postRepo = postRepo;
            _mapper = mapper;
            _commentRepo = commentRepo;
        }

        [HttpPost("add-comments-to-posts/{postId}")]
        public async Task<ActionResult> AddComment(int postId, [FromBody] string? content)
        {
            var sourceUserId = User.GetUserId();
            var Comment = await _postRepo.GetPostById(postId);
            var sourceUser = await _commentRepo.GetPostWithComments(sourceUserId);

            if(Comment == null) return NotFound();
            if (sourceUserId == Comment.UserId) return BadRequest("You cannot like post yourself");


            if (content == null) return BadRequest("What is your reaction to this post?");
                
            var userComment = new Comment
            {
                UserId = sourceUserId,
                PostId = postId,
                Content = content
            };

            sourceUser.Comments.Add(userComment);

            if(await _userRepo.SaveAllAsync()) return Ok("You commented this post");

            return BadRequest("Fail to comment user");
        }

        //[HttpGet]
        //public async Task<ActionResult<PagedList<InteractWithPostDto>>> GetUserComments([FromQuery] CommentsParams commentsParams)
        //{
        //    commentsParams.UserId = User.GetUserId();
        //    var users = await _commentRepo.GetUserComments(commentsParams);
        //    Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));

        //    return Ok(users);
        //}

        [HttpGet]
        public async Task<ActionResult<PagedList<CommentDto>>> GetComments([FromQuery]CommentsParams commentsParams)
        {
            var comments = await _commentRepo.GetComments(commentsParams);
            Response.AddPaginationHeader(new PaginationHeader(comments.CurrentPage, comments.PageSize, comments.TotalCount, comments.TotalPages));
            return Ok(comments);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateComment([FromBody]CommentDto commentDto)
        {
            var sourceUserId = User.GetUserId();
            var commentExists = await _commentRepo.GetUserComment(sourceUserId, commentDto.PostId, commentDto.Timestamp);
            if(commentExists == null) return NotFound();
            if (commentDto.Content == null) return NoContent();
            commentExists.Content = commentDto.Content;

            await _commentRepo.Update(commentExists);

            return Ok("Update successful");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteComment([FromBody] CommentDto commentDto)
        {
            var sourceUserId = User.GetUserId();
            var commentExists = await _commentRepo.GetUserComment(sourceUserId, commentDto.PostId, commentDto.Timestamp);
            if (commentExists == null) return NotFound();

            await _commentRepo.Delete(commentExists);

            return Ok("Delete successful");
        }
    }
}
