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
    public class PostsController : ControllerBase
    {
        private readonly IPostRepo _postRepo;
        private readonly IMapper _mapper;
        public PostsController(IPostRepo postRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<PostDto>>> GetPosts([FromQuery] PostParams postParams)
        {
            var posts = await _postRepo.GetPosts(postParams);

            Response.AddPaginationHeader(
            new PaginationHeader(posts.CurrentPage, posts.PageSize, posts.TotalCount, posts.TotalPages));
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPosts(int id)
        {
            var post = await _postRepo.GetPostById(id);
            return Ok(_mapper.Map<PostDto>(post));
        }
    }
}
