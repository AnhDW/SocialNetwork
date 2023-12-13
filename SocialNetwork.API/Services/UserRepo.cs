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
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(p => p.Posts)
                .Include(l => l.LikePosts)
                .Include(g=>g.Groups)
                .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<PersonalPageDto> GetPersonalPageAsync(string username)
        {
            return await _context.Users.Include(p => p.Posts)
                .Where(u => u.UserName == username)
                .ProjectTo<PersonalPageDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<PagedList<PersonalPageDto>> GetPersonalPagesAsync(UserParams userParams)
        {
            var query = _context.Users.AsQueryable();

            query = query.Where(u => u.UserName != userParams.CurrentUsername);
            //query = query.Where(u => u.Gender == userParams.Gender);

            var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
            var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

            query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);

            query = userParams.OrderBy switch
            {
                "create" => query.OrderByDescending(u => u.CreatedDate),
                _ => query.OrderByDescending(u => u.LastActive),
            };

            return await PagedList<PersonalPageDto>
                .CreateAsync(
                query.AsNoTracking().ProjectTo<PersonalPageDto>(_mapper.ConfigurationProvider),
                userParams.PageNumber,
                userParams.PageSize
                );
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
