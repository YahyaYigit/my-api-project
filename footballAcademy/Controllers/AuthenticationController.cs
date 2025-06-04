using Basketball.Entity.DTOs.User;
using Basketball.Service.Services.ServiceAuthentication;
using Microsoft.AspNetCore.Mvc;

namespace Basketball.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        // Kullanıcı kaydı
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDTO userRegisterDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Model validasyonu başarısızsa hata döner
            }

            // Kullanıcı kaydını yap ve UserDTO döndür
            var userDTO = await _authenticationService.RegisterUser(userRegisterDTO);

            if (userDTO == null)
            {
                // Kullanıcı oluşturulamadıysa hata mesajı döndür
                return BadRequest(new { Message = "Kullanıcı kaydı sırasında bir hata oluştu." });
            }

            // Başarı mesajı ve UserDTO ile dönüş
            return Ok(new { Message = "Kullanıcı başarıyla kaydedildi.", User = userDTO });
        }





        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            if (userLoginDTO == null)
            {
                return BadRequest("Geçersiz giriş verisi.");
            }

            // Giriş işlemini AuthenticationService üzerinden gerçekleştir
            var loginDto = await _authenticationService.LoginUser(userLoginDTO);

            // Kullanıcı bulunamadıysa veya şifre hatalıysa exception fırlatılmıştır
            if (loginDto == null)
            {
                return Unauthorized("Kullanıcı bulunamadı veya şifre hatalı.");
            }

            // Başarılı girişte hem admin durumu hem kullanıcı bilgisi birlikte dönülüyor
            return Ok(new
            {
                message = "Giriş başarılı",
                isAdmin = loginDto.IsAdmin,
                user = loginDto.User
            });
        }

    }
}
