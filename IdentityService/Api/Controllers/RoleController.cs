using CoreLib.DTO;
using CoreLib.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace IdentityService.Api.Controllers
{
    [ApiController]
    [Route("api/roles")]
    [Authorize(Roles = "ADMIN")]
    public class RoleController : ControllerBase

    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService) => _roleService = roleService;


        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var role = await _roleService.GetRoleAsync(id);
            return Ok(role);
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Role name cannot be empty.");

            var role = await _roleService.AddRole(request);
            return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, role);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] RoleDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Role ID mismatch.");

            await _roleService.UpdateRole(dto);
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            await _roleService.DeleteRole(id);
            return NoContent();
        }

    }
}
