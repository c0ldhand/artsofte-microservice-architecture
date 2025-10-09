using CoreLib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Interfaces.Services
{
    public interface IRoleService
    {
        Task<RoleDTO> GetRoleAsync(Guid roleId);
        Task<IEnumerable<RoleDTO>> GetAllRolesAsync();
        Task DeleteRole(Guid roleId);
        Task<RoleDTO> AddRole(CreateRoleRequest request);
        Task UpdateRole(RoleDTO dto);
    }
}
