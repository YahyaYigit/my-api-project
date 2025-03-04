using Basketball.Entity.DTOs.User;
using Basketball.Entity.Models;
using Football.DataAcces.Data;
using Microsoft.AspNetCore.Identity;

namespace Basketball.Service.Services.ServiceAuthentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SampleDBContext _context;

        public AuthenticationService(UserManager<User> userManager, RoleManager<Role> roleManager, SampleDBContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;

        }


        public async Task<IdentityResult> RegisterUser(UserRegisterDTO userRegisterDTO)
        {
            var user = new User
            {
                FirstName = userRegisterDTO.FirstName,
                LastName = userRegisterDTO.LastName,
                UserName = userRegisterDTO.Email, // Kullanıcı adı olarak e-posta kullanılıyor
                Email = userRegisterDTO.Email!,
                BirtDay = userRegisterDTO.BirthDay,
                Address = userRegisterDTO.Address,
                PhoneNumber = userRegisterDTO.PhoneNumber,
                MotherName = userRegisterDTO.MotherName,
                MotherPhoneNumber = userRegisterDTO.MotherPhoneNumber,
                FatherName = userRegisterDTO.FatherName,
                FatherPhoneNumber = userRegisterDTO.FatherPhoneNumber,
            };

            // Şifreyi hashleyerek kaydet
            var result = await _userManager.CreateAsync(user, userRegisterDTO.Password!);

            if (!result.Succeeded)
            {
                // Hata durumunda, hataları dön
                return IdentityResult.Failed(result.Errors.ToArray());
            }

            // Kullanıcı başarılı bir şekilde oluşturulduysa, varsayılan bir rol ekle
            var roleName = "User";

            // Eğer "User" rolü veritabanında yoksa oluştur
            var isRoleInDb = await _roleManager.RoleExistsAsync(roleName);

            if (!isRoleInDb)
            {
                var role = new Role { Name = roleName, IsDeleted = false };
                var roleResult = await _roleManager.CreateAsync(role);
                if (!roleResult.Succeeded)
                {
                    return IdentityResult.Failed(roleResult.Errors.ToArray());
                }
            }

            // Kullanıcıyı Role ekle
            var roleAddResult = await _userManager.AddToRoleAsync(user, roleName);

            if (!roleAddResult.Succeeded)
            {
                return IdentityResult.Failed(roleAddResult.Errors.ToArray());
            }

            // Değişiklikleri kaydet
            await _context.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> LoginUser(UserLoginDTO userLoginDTO)
        {
            // Kullanıcıyı email ile bul
            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email!);

            // Kullanıcı yoksa
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Kullanıcı bulunamadı." }); // Başarısız giriş
            }

            // Şifreyi doğrula
            var passwordCheck = await _userManager.CheckPasswordAsync(user, userLoginDTO.Password!);

            if (!passwordCheck)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Şifre hatalı." }); // Giriş başarısız
            }

            // Kullanıcının rollerini al
            var roles = await _userManager.GetRolesAsync(user);

            // Kullanıcının rolü "Admin" ise
            if (roles.Contains("Admin"))
            {
                return IdentityResult.Success; // Admin girişi başarılı
            }
            // Kullanıcının rolü "User" ise
            else if (roles.Contains("User"))
            {
                return IdentityResult.Success; // Normal kullanıcı girişi başarılı
            }

            // Eğer rol bulunmazsa
            return IdentityResult.Failed(new IdentityError { Description = "Geçersiz rol." });
        }





    }

}