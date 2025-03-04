
using Basketball.Entity.Models;

namespace Basketball.Service.Services.ServiceUser
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers(); // Tüm User DTO'larını getir
        IEnumerable<User> GetAllDeleteUsers();
        User GetUserById(int id); // ID'ye göre User Getir
        User UpdateUser(Basketball.Entity.DTOs.User.UserForUpdate userForUpdate); // User güncelle
        void DeleteUser(int id); // User sil


    }
}

