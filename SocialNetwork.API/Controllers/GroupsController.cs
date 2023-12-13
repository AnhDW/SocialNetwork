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
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepo _groupRepo;
        private readonly IMapper _mapper;

        public GroupsController(IGroupRepo groupRepo, IMapper mapper)
        {
            _groupRepo = groupRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<GroupDto>>> GetGroups([FromQuery] GroupParams groupParams)
        {
            var groups = await _groupRepo.GetGroups(groupParams);

            Response.AddPaginationHeader(
                new PaginationHeader(groups.CurrentPage, groups.PageSize, groups.TotalCount, groups.TotalPages));

            return Ok(groups);
        }

        [HttpGet("{groupId}")]
        public async Task<ActionResult<GroupDto>> GetGroupById(int groupId)
        {
            var group = await _groupRepo.GetGroup(groupId);
            return _mapper.Map<GroupDto>(group);
        }

        
    }
}
