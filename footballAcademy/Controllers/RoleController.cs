using Basketball.Entity.DTOs.Role;
using Basketball.Service.Services.ServiceRole;
using Microsoft.AspNetCore.Mvc;

namespace Basketball.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // Tüm rolleri getir
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        // ID'ye göre rol getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
                return NotFound("Role not found.");
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> InsertRole([FromBody] RoleForInsertion roleForInsertion)
        {
            if (roleForInsertion == null)
                return BadRequest("Invalid role data.");

            // Rolü oluştur
            var result = await _roleService.InsertRoleAsync(roleForInsertion);

            if (result.Succeeded)
            {
                // RoleDTO'yu doğru şekilde kullanıyoruz
                var role = new RoleDto
                {
                    Name = roleForInsertion.Name
                    // Burada başka alanlar varsa onları da ekleyebilirsiniz (örneğin Id vs.)
                };

                return CreatedAtAction(nameof(GetRoleById), new { id = role.Name }, role);
            }

            return BadRequest(result.Errors);
        }





        // Rol güncelle
        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] RoleForUpdate roleForUpdate)
        {
            if (roleForUpdate == null)
                return BadRequest("Invalid role data.");

            var result = await _roleService.UpdateRoleAsync(roleForUpdate);
            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result.Errors);
        }

        // Rolü sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            if (result.Succeeded)
                return NoContent();

            return BadRequest(result.Errors);
        }
    }
}
