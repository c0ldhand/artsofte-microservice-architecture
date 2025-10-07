using CoreLib.Entities;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Repositories;
using IdentityService.Dal.EF;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Dal.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly DBContext _context;

        public RoleRepository(DBContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Role?> GetRoleById(Guid RoleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == RoleId);
        }
    }
}
