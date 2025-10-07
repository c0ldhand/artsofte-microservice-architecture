using CoreLib.DTO;
using CoreLib.Entities;
using CoreLib.Interfaces.Services;
using IdentityService.Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace IdentityService.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize(Roles = "ADMIN")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userSevice) => _userService = userSevice;

        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUsersById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new { message = $"User with ID {id} not found." });
            return Ok(user);
        }

        [HttpPut("{id}")] 
        public async Task<ActionResult> UpdateMe(Guid id, [FromBody] UserDTO userDTO)
        {
            if (id != userDTO.Id)
                return BadRequest(new { message = "ID in URL does not match ID in body." });
            await _userService.UpdateUserAsync(userDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
