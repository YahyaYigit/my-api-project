using Basketball.Entity.DTOs.LoginDTO;
using Basketball.Entity.DTOs.User;


namespace Basketball.Service.Services.ServiceAuthentication
{
    public interface IAuthenticationService
    {
        Task<UserDTO> RegisterUser(UserRegisterDTO userRegisterDTO);
        Task<LoginDTO> LoginUser(UserLoginDTO userLoginDTO);
    }
}
