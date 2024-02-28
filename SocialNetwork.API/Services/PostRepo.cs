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
                .Include(u => u.User)
                .Include(a => a.Attachments)
                .OrderByDescending(a => a.LikePosts.Count + a.Comments.Count)
                .AsQueryable();

            if(postParams.UserId != null)
                query = query.Where(u=>u.UserId == postParams.UserId);

            if (postParams.Content != null)
            {
                string[] keywords = postParams.Content.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                //query = query.Where(p => keywords.Any(keyword => p.Content.Contains(keyword)));
                query = query.Select(post => new
                {
                    Post = post,
                    MatchedKeywordCount = keywords.Count(keyword => post.Content.Contains(keyword))
                })
                    .Where(item => item.MatchedKeywordCount > 0)
                    .OrderByDescending(item => item.MatchedKeywordCount)
                    .Select(item => item.Post);
            }

            //query = postParams.OrderBy switch
            //{
            //    "create" => query.OrderByDescending(p => p.CreatedDate),
            //    _ => query.OrderByDescending(p => p.LastActive)
            //};

            return await PagedList<PostDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<PostDto>(_mapper.ConfigurationProvider),
                postParams.PageNumber,
                postParams.PageSize
                );
        }


        public async Task<Post> GetPostById(int postId)
        {
            return await _context.Posts
                .Include(u=>u.User)
                .Include(a => a.Attachments)
                .FirstOrDefaultAsync(p => p.Id == postId);
        }
    }
}
