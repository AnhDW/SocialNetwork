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
    public class GroupRepo : IGroupRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GroupRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Group> GetGroup(int id)
        {
            return await _context.Groups.Include(g => g.GroupMembers).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<PagedList<GroupDto>> GetGroups(GroupParams groupParams)
        {
            var query = _context.Groups.OrderBy(g => g.GroupName).AsQueryable();

            if(groupParams.GroupName != null)
            {
                query = query.Where(u => u.GroupName == groupParams.GroupName);
            }

            query = groupParams.OrderBy switch
            {
                "create" => query.OrderByDescending(g => g.CreatedDate),
                _ => query.OrderByDescending(g=>g.LastActive)
            };

            return await PagedList<GroupDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<GroupDto>(_mapper.ConfigurationProvider),
                groupParams.PageNumber,
                groupParams.PageSize
                );
        }
    }
}
