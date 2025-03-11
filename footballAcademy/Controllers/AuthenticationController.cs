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





        // Login işlemi için POST metodu
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            if (userLoginDTO == null)
            {
                return BadRequest("Geçersiz giriş verisi.");
            }

            // Giriş işlemini AuthenticationService üzerinden gerçekleştir
            var result = await _authenticationService.LoginUser(userLoginDTO);

            // Eğer giriş başarılıysa, başarılı mesajı döndürülür
            if (result.Succeeded)
            {
                return Ok("Giriş başarılı.");
            }

            // Eğer giriş başarısızsa, hata mesajları döndürülür
            return Unauthorized(result.Errors.Select(e => e.Description));
        }
    }
}
