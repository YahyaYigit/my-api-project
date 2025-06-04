using Basketball.Entity.DTOs.LoginDTO;
using Basketball.Entity.DTOs.User;
using Basketball.Entity.Models;
using Football.DataAcces.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
                AcceptedKVKK = userRegisterDTO.AcceptedKVKK,
                AcceptedImportant =userRegisterDTO.AcceptedImportant,
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
                IsAcceptedMotherWhatsappGroup = user.IsAcceptedMotherWhatsappGroup,
                AcceptedKVKK = user.AcceptedKVKK,
                AcceptedImportant = user.AcceptedImportant
            };

            return userDTO; // Kullanıcı başarıyla kaydedildi, UserDTO döndürülüyor
        }








        public async Task<LoginDTO> LoginUser(UserLoginDTO userLoginDTO)
        {
            var emailLower = userLoginDTO.Email!.ToLower();

            var user = await _context.Users
                .Include(u => u.CategoryGroups)
                .Include(u => u.Dues)
                .FirstOrDefaultAsync(u => u.Email!.ToLower() == emailLower);

            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            if (user.IsDeleted)
            {
                return null!;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, userLoginDTO.Password!);
            if (!passwordCheck)
            {
                throw new Exception("Şifre hatalı.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var userDTO = new UserDTO
            {
                Id = user.Id,
                IsAdmin = user.IsAdmin,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CategoryGroupsId = user.CategoryGroups?.Id ?? 0,
                CategoryGroups = user.CategoryGroups?.Age ?? "Bilgi Yok",
                BirtDay = user.BirtDay,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                MotherName = user.MotherName,
                MotherPhoneNumber = user.MotherPhoneNumber,
                FatherName = user.FatherName,
                FatherPhoneNumber = user.FatherPhoneNumber,
                Email = user.Email,
                TcNo = user.TcNo,
                BirthPlace = user.BirthPlace,
                School = user.School,
                Height = user.Height,
                Weight = user.Weight,
                HealthProblem = user.HealthProblem,
                IsAcceptedWhatsappGroup = user.IsAcceptedWhatsappGroup,
                IsAcceptedMotherWhatsappGroup = user.IsAcceptedMotherWhatsappGroup,
                IsAcceptedFatherWhatsappGroup = user.IsAcceptedFatherWhatsappGroup,
                AcceptedKVKK = user.AcceptedKVKK,
                AcceptedImportant = user.AcceptedImportant,

                // MonthlyFees sözlüğünü dolduruyoruz

                MonthlyFees = user.Dues != null ? user.Dues.ToDictionary(

                        d => $"{d.Month}-{d.Year}", // Anahtar: Ay-Yıl formatında

                        d => $"{d.PaymentType} - {d.Fee} TL") // Değer: PaymentType ve Fee bilgisi

                        : new Dictionary<string, string>()

            };

            return new LoginDTO
            {
                IsAdmin = roles.Contains("Admin"),
                User = userDTO
            };
        }











    }

}