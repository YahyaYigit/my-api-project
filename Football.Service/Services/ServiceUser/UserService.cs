using Basketball.Entity.DTOs.User;
using Basketball.Entity.Models;
using Football.DataAcces.Data;
using Microsoft.EntityFrameworkCore;


namespace Basketball.Service.Services.ServiceUser
{
    public class UserService : IUserService
    {
        private readonly SampleDBContext _context;

        public UserService(SampleDBContext context)
        {
            _context = context;
        }

       


        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User ID {id} bulunamadı."); // Hata kontrolü
            }

            user.IsDeleted = true; // Silinmiş olarak işaretle
            _context.SaveChanges(); // Değişiklikleri kaydet
        }

        public IEnumerable<User> GetAllDeleteUsers()
        {
            return _context.Users
                .Include(u => u.Dues) // CategoryGroups ilişkisini dahil et
                .Include(f => f.CategoryGroups) // Kategorileri dahil et    
                .Where(c => c.IsDeleted)        // Sadece silinmiş kullanıcıları getir
                .ToList();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users
                .Include(u => u.Dues) // CategoryGroups ilişkisini dahil et
                .Include(f => f.CategoryGroups) // Kategoriyi dahil et
                .Where(c => !c.IsDeleted)// Sadece silinmemiş kullanıcıları getir
                .ToList();
        }

        public User GetUserById(int id)
        {
            var user = _context.Users
                .Include(u => u.Dues) // CategoryGroups ilişkisini dahil et
               .Include(f => f.CategoryGroups) // Kategoriyi dahil et
               .FirstOrDefault(f => f.Id == id && !f.IsDeleted);  // Silinmemiş Useri bul

            if (user == null)
            {
                throw new Exception("User bulunamadı");
            }

            return user;  // User olarak döndür
        }


        public User UpdateUser(UserForUpdate userForUpdate)
        {
            var existingUser = GetUserById(userForUpdate.Id);

            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User ID {userForUpdate.Id} bulunamadı.");
            }

            // Kullanıcı entity'si takip edilmiyor olabilir, bu yüzden tekrar takip ettirelim
            if (_context.Entry(existingUser).State == EntityState.Detached)
            {
                _context.Users.Attach(existingUser);
            }

            // Güncelleme işlemi
            existingUser.FirstName = userForUpdate.FirstName;
            existingUser.LastName = userForUpdate.LastName;
            existingUser.CategoryGroupsId = userForUpdate.CategoryGroupsId;
            existingUser.BirtDay = userForUpdate.BirtDay;
            existingUser.Address = userForUpdate.Address;
            existingUser.PhoneNumber = userForUpdate.PhoneNumber;
            existingUser.MotherName = userForUpdate.MotherName;
            existingUser.MotherPhoneNumber = userForUpdate.MotherPhoneNumber;
            existingUser.FatherName = userForUpdate.FatherName;
            existingUser.FatherPhoneNumber = userForUpdate.FatherPhoneNumber;
          
            existingUser.Email = userForUpdate.Email;
            existingUser.DuesId = userForUpdate.DuesId;
            existingUser.IsDeleted = userForUpdate.IsDeleted;

            // CategoryGroups güncellemesi
            existingUser.CategoryGroups = _context.CategoryGroups.FirstOrDefault(r => r.Id == userForUpdate.CategoryGroupsId);
            if (existingUser.CategoryGroups == null)
            {
                throw new ArgumentException("Geçersiz CategoryGroupsId.");
            }

            _context.SaveChanges();

            return existingUser;
        }

    }
}
