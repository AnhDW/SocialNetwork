using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class LikePostsController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly ILikePostRepo _likePostRepo;
        private readonly IPostRepo _postRepo;

        public LikePostsController(IUserRepo userRepo, IPostRepo postRepo, ILikePostRepo likePostRepo)
        {
            _userRepo = userRepo;
            _postRepo = postRepo;
            _likePostRepo = likePostRepo;
        }

        [HttpPost("{postId}")]
        public async Task<ActionResult> AddLike(int postId, [FromBody] string? reationType)
        {
            var sourceUserId = User.GetUserId();
            var likePost = await _postRepo.GetPostById(postId);
            var sourceUser = await _likePostRepo.GetPostWithLikes(sourceUserId);

            if(likePost == null) return NotFound();
            if (sourceUserId == likePost.UserId) return BadRequest("You cannot like post yourself");

            var userLike = await _likePostRepo.GetUserLike(sourceUserId, postId);
            if(userLike != null && reationType == null)
            {
                await _likePostRepo.Delete(userLike);
                return Ok("You unlike this post");
            }

            if(userLike != null && reationType != null && reationType != userLike.ReationType)
            {
                await _likePostRepo.Update(userLike);
                return Ok("You " + reationType + " this post");
            }

            if (reationType == null) return BadRequest("What is your reaction to this post?");
                
            userLike = new LikePost
            {
                UserId = sourceUserId,
                PostId = postId,
                ReationType = reationType
            };

            sourceUser.LikePosts.Add(userLike);

            if(await _userRepo.SaveAllAsync()) return Ok("You " + reationType + " this post");

            return BadRequest("Fail to like user");
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<InteractWithPostDto>>> GetUserLikes([FromQuery] LikesParams likesParams)
        {
            likesParams.UserId = User.GetUserId();
            var users = await _likePostRepo.GetUserLikes(likesParams);
            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));

            return Ok(users);
        }
    }
}
