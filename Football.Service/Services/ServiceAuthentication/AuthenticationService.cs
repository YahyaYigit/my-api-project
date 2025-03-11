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

        public async Task<UserDTO> RegisterUser(UserRegisterDTO userRegisterDTO)
        {
            var emailLower = userRegisterDTO.Email!.ToLower();
            var normalizedEmail = _userManager.NormalizeEmail(userRegisterDTO.Email);
            var normalizedUserName = _userManager.NormalizeName(userRegisterDTO.Email);

            var user = new User
            {
                FirstName = userRegisterDTO.FirstName,
                LastName = userRegisterDTO.LastName,
                UserName = emailLower,
                Email = emailLower,
                BirtDay = userRegisterDTO.BirthDay,
                Address = userRegisterDTO.Address,
                PhoneNumber = userRegisterDTO.PhoneNumber,
                MotherName = userRegisterDTO.MotherName,
                MotherPhoneNumber = userRegisterDTO.MotherPhoneNumber,
                FatherName = userRegisterDTO.FatherName,
                FatherPhoneNumber = userRegisterDTO.FatherPhoneNumber,
                BirthPlace = userRegisterDTO.BirthPlace,
                HealthProblem = userRegisterDTO.HealthProblem,
                Height = userRegisterDTO.Height,
                Weight = userRegisterDTO.Weight,
                School = userRegisterDTO.School,
                TcNo = userRegisterDTO.TcNo,
                IsAcceptedWhatsappGroup = userRegisterDTO.IsAcceptedWhatsappGroup,
                IsAcceptedFatherWhatsappGroup = userRegisterDTO.IsAcceptedFatherWhatsappGroup,
                IsAcceptedMotherWhatsappGroup = userRegisterDTO.IsAcceptedMotherWhatsappGroup,
                NormalizedEmail = normalizedEmail,
                NormalizedUserName = normalizedUserName
            };

            // Kullanıcıyı oluştur
            var result = await _userManager.CreateAsync(user, userRegisterDTO.Password!);
            if (!result.Succeeded)
            {
                // Hata durumunda null döndürebiliriz
                return null;
            }

            // Varsayılan "User" rolünü ekle
            var roleName = "User";
            var isRoleInDb = await _roleManager.RoleExistsAsync(roleName);
            if (!isRoleInDb)
            {
                var role = new Role { Name = roleName, IsDeleted = false };
                var roleResult = await _roleManager.CreateAsync(role);
                if (!roleResult.Succeeded)
                {
                    // Hata durumunda null döndürebiliriz
                    return null;
                }
            }

            var roleAddResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!roleAddResult.Succeeded)
            {
                // Hata durumunda null döndürebiliriz
                return null;
            }

            await _context.SaveChangesAsync();

            // Kullanıcı başarıyla kaydedildiğinde UserDTO döndür
            var userDTO = new UserDTO
            {
                Id = user.Id, // ID dahil et
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                BirtDay = user.BirtDay,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                MotherName = user.MotherName,
                MotherPhoneNumber = user.MotherPhoneNumber,
                FatherName = user.FatherName,
                FatherPhoneNumber = user.FatherPhoneNumber,
                BirthPlace = user.BirthPlace,
                HealthProblem = user.HealthProblem,
                Height = user.Height,
                Weight = user.Weight,
                School = user.School,
                TcNo = user.TcNo,
                IsAcceptedWhatsappGroup = user.IsAcceptedWhatsappGroup,
                IsAcceptedFatherWhatsappGroup = user.IsAcceptedFatherWhatsappGroup,
                IsAcceptedMotherWhatsappGroup = user.IsAcceptedMotherWhatsappGroup
            };

            return userDTO; // Kullanıcı başarıyla kaydedildi, UserDTO döndürülüyor
        }








        public async Task<IdentityResult> LoginUser(UserLoginDTO userLoginDTO)
        {
            // E-postayı iki farklı şekilde normalize et
            var normalizedEmail = _userManager.NormalizeEmail(userLoginDTO.Email!);
            var emailUpper = userLoginDTO.Email!.ToUpper();

            // İlk olarak NormalizeEmail ile kullanıcıyı bul
            var user = await _userManager.FindByEmailAsync(normalizedEmail);

            // Eğer bulunamazsa ToUpper() ile tekrar dene ama GERÇEK EŞLEŞMEYİ kontrol et
            if (user == null)
            {
                var secondTryUser = await _userManager.FindByEmailAsync(emailUpper);
                if (secondTryUser != null && secondTryUser.Email!.Equals(userLoginDTO.Email, StringComparison.OrdinalIgnoreCase))
                {
                    user = secondTryUser;
                }
            }

            // Kullanıcı yine de bulunamazsa
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Kullanıcı bulunamadı." });
            }

            // Şifreyi doğrula
            var passwordCheck = await _userManager.CheckPasswordAsync(user, userLoginDTO.Password!);
            if (!passwordCheck)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Şifre hatalı." });
            }

            // Kullanıcının rollerini al
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin"))
            {
                return IdentityResult.Success; // Admin girişi başarılı
            }
            else if (roles.Contains("User"))
            {
                return IdentityResult.Success; // Normal kullanıcı girişi başarılı
            }

            return IdentityResult.Failed(new IdentityError { Description = "Geçersiz rol." });
        }











    }

}