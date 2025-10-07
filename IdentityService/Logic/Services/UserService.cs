using CoreLib.DTO;
using CoreLib.Interfaces.Repositories;
using CoreLib.Interfaces.Services;


namespace IdentityService.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(user => new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.RoleName.Name
            });
        }

        public async Task<UserDTO?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Role = user.RoleName.Name
            };
        }

        public async Task UpdateUserAsync(UserDTO dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.Id) ?? throw new Exception("User not found");
            user.Name = dto.Name;
            user.Updated = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);
        }

       
    }
}
