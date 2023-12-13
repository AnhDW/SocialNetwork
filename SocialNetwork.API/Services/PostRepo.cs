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
    public class PostRepo : IPostRepo
    {
        private readonly DataContext _context;
        private readonly IAttachmentSvc _attachmentSvc;
        private readonly IMapper _mapper;

        public PostRepo(DataContext context, IAttachmentSvc attachmentSvc, IMapper mapper)
        {
            _context = context;
            _attachmentSvc = attachmentSvc;
            _mapper = mapper;
        }

        public async Task<PagedList<PostDto>> GetPosts(PostParams postParams)
        {
            var query = _context.Posts
                .Include(a => a.Attachments)
                .Include(l => l.LikePosts).OrderBy(p => p.Id)
                .AsQueryable();

            if(postParams.Content != null)
            {
                query = query.Where(p => p.Content == postParams.Content);
            }

            query = postParams.OrderBy switch
            {
                "create" => query.OrderBy(p => p.CreatedDate),
                _ => query.OrderBy(p => p.LastActive)
            };

            return await PagedList<PostDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<PostDto>(_mapper.ConfigurationProvider),
                postParams.PageNumber,
                postParams.PageSize
                );
            
        }
        public async Task<Post> GetPostById(int postId)
        {
            return await _context.Posts
                .Include(a => a.Attachments)
                .Include(l => l.LikePosts)
                .FirstOrDefaultAsync(p => p.Id == postId);
        }
    }
}
