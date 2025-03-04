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
                return BadRequest(ModelState);
            }

            // Kullanıcı kaydını gerçekleştir
            var result = await _authenticationService.RegisterUser(userRegisterDTO);


           

            if (result.Succeeded)
            {
                return Ok(new { Message = "Kullanıcı başarıyla kaydedildi." });
            }
            else
            {
                // Hata mesajlarını dön
                return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
            }
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
