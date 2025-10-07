using CoreLib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDTO?> GetUserByIdAsync(Guid id);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task UpdateUserAsync(UserDTO dto);
        Task DeleteUserAsync(Guid id);
    }
}
