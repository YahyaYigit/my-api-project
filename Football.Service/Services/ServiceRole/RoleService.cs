using Basketball.Entity.DTOs.Role;
using Basketball.Entity.Models;
using Football.DataAcces.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Basketball.Service.Services.ServiceRole
{
    public class RoleService : IRoleService
    {
        private readonly SampleDBContext _context;
        private readonly RoleManager<Role> _roleManager;

        public RoleService(SampleDBContext context, RoleManager<Role> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        // Tüm rolleri getir
        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles
                                           .Where(r => !r.IsDeleted) // Silinmemiş rolleri al
                                           .Select(r => new RoleDto
                                           {
                                               Id = r.Id,
                                               Name = r.Name!, // Null kontrolü önemli!
                                               IsDeleted = r.IsDeleted
                                           })
                                           .ToListAsync();
            return roles;
        }

        // ID'ye göre rol getir
        public async Task<RoleDto?> GetRoleByIdAsync(int id)
        {
            var role = await _roleManager.Roles
                                          .Where(r => r.Id == id && !r.IsDeleted)
                                          .Select(r => new RoleDto
                                          {
                                              Id = r.Id,
                                              Name = r.Name!,
                                              IsDeleted = r.IsDeleted
                                          })
                                          .FirstOrDefaultAsync();
            return role;
        }

        // Yeni rol oluştur
        public async Task<IdentityResult> InsertRoleAsync(RoleForInsertion roleForInsertion)
        {
            if (string.IsNullOrEmpty(roleForInsertion.Name))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Role name cannot be empty." });
            }

            var role = new Role
            {
                Name = roleForInsertion.Name
            };

            var result = await _roleManager.CreateAsync(role);
            return result;
        }

        // Rol güncelle
        public async Task<IdentityResult> UpdateRoleAsync(RoleForUpdate roleForUpdate)
        {
            if (string.IsNullOrEmpty(roleForUpdate.Name))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Role name cannot be empty." });
            }

            var role = await _roleManager.FindByIdAsync(roleForUpdate.Id.ToString());
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Role not found." });
            }

            role.Name = roleForUpdate.Name;
            role.IsDeleted = roleForUpdate.IsDeleted;

            var result = await _roleManager.UpdateAsync(role);
            return result;
        }

        // Rolü soft delete yap
        public async Task<IdentityResult> DeleteRoleAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Role not found." });
            }

            role.IsDeleted = true;  // Soft delete
            var result = await _roleManager.UpdateAsync(role);
            return result;
        }
    }
}
