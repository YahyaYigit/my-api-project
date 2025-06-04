

using Basketball.Entity.DTOs.User;

namespace Basketball.Entity.DTOs.LoginDTO
{
    public class LoginDTO
    {
        public bool IsAdmin { get; set; }
        public UserDTO User { get; set; }

    }
}
