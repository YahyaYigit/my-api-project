using Basketball.Entity.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace Basketball.Service.Services.ServiceAuthentication
{
    public interface IAuthenticationService
    {
        Task<UserDTO> RegisterUser(UserRegisterDTO userRegisterDTO);
        Task<IdentityResult> LoginUser(UserLoginDTO userLoginDTO);
    }
}
