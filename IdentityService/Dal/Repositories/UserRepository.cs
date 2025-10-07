using CoreLib.Entities;
using CoreLib.Interfaces.Repositories;
using IdentityService.Dal.EF;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Dal.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DBContext _context;

        public UserRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.RoleId)
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}
