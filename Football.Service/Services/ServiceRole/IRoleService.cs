using Basketball.Entity.DTOs.Role;
using Microsoft.AspNetCore.Identity;

namespace Basketball.Service.Services.ServiceRole
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync(); // Tüm rolleri getir
        Task<RoleDto?> GetRoleByIdAsync(int id); // ID'ye göre rol getir
        Task<IdentityResult> InsertRoleAsync(RoleForInsertion roleForInsertion); // Yeni rol oluştur
        Task<IdentityResult> UpdateRoleAsync(RoleForUpdate roleForUpdate); // Rol güncelle
        Task<IdentityResult> DeleteRoleAsync(int id); // Rolü soft delete yap
    }
}
