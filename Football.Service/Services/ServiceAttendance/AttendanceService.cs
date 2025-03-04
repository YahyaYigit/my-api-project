using Basketball.Entity.DTOs.Attendance;
using Basketball.Entity.Models;
using Football.DataAcces.Data;
using Microsoft.EntityFrameworkCore;

namespace Basketball.Service.Services.ServiceAttendance
{
    public class AttendanceService : IAttendanceService
    {
        private readonly SampleDBContext _context;

        public AttendanceService(SampleDBContext context)
        {
            _context = context;
        }

        public Attendance CreateAttendance(AttendanceForInsertion attendanceForInsertion)
        {
            // CategoryGroupsId doğrulaması
            var user = _context.Users.FirstOrDefault(r => r.Id == attendanceForInsertion.UserId);
            if (user == null)
            {
                throw new ArgumentException("Geçersiz CategoryGroupsId.");
            }

            var attendance = new Attendance
            {
                UserId = attendanceForInsertion.UserId,
                Users = user,

                Status = attendanceForInsertion.Status,
                Date = attendanceForInsertion.Date,

            };

            _context.Attendances.Add(attendance); // Yeni kategori ekle
            _context.SaveChanges(); // Değişiklikleri kaydet

            return attendance;
        }

        public void DeleteAttendance(int id)
        {
            var attendance = GetAttendanceById(id);
            if (attendance == null)
            {
                throw new KeyNotFoundException($"attendance ID {id} bulunamadı."); // Hata kontrolü
            }

            attendance.IsDeleted = true; // Silinmiş olarak işaretle
            _context.SaveChanges(); // Değişiklikleri kaydet
        }

        public IEnumerable<Attendance> GetAllAttendance()
        {
            return _context.Attendances
               .Include(f => f.Users)//CategoryGroupu dahil et
               .Where(c => !c.IsDeleted)
               .ToList();
        }

        public Attendance GetAttendanceById(int id)
        {
            var attendance = _context.Attendances
             .Include(r => r.Users)// İlişkili CategoryGroupu dahil et
             .FirstOrDefault(c => c.Id == id && !c.IsDeleted); // Silinmemiş kategori bul

            if (attendance == null)
            {
                throw new Exception("Attendance bulunamadı");
            }

            return attendance;
        }

        public Attendance UpdateAttendance(AttendanceForUpdate attendanceForUpdate)
        {
            // Attendance var mı diye kontrol et
            var existingAttendance = _context.Attendances.FirstOrDefault(t => t.Id == attendanceForUpdate.Id);

            if (existingAttendance == null)
            {
                throw new KeyNotFoundException($"Attendance ID {attendanceForUpdate.Id} bulunamadı.");
            }

            // UserId'yi güncelleme kontrolü
            if (attendanceForUpdate.UserId != existingAttendance.UserId)
            {
                var user = _context.Users.FirstOrDefault(r => r.Id == attendanceForUpdate.UserId);
                if (user == null)
                {
                    throw new ArgumentException("Geçersiz UserId.");
                }
                existingAttendance.UserId = attendanceForUpdate.UserId;
            }

            // Diğer alanları güncelle
            existingAttendance.Date = attendanceForUpdate.Date;
            existingAttendance.Status = attendanceForUpdate.Status;
            existingAttendance.IsDeleted = attendanceForUpdate.IsDeleted;

            _context.SaveChanges(); // Değişiklikleri kaydet

            return existingAttendance; // Güncellenmiş veriyi geri döndür
        }
    }
}
