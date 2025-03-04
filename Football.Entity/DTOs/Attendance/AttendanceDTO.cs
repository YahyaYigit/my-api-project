
namespace Basketball.Entity.DTOs.Attendance
{
    public class AttendanceDTO
    {
        public int Id { get; set; } // Yoklama ID

        public int UserId { get; set; }

        public string? FirstName { get; set; }  // Kullanıcı adı
        public string? LastName { get; set; }   // Kullanıcı soyadı

        public DateTime Date { get; set; } // Yoklama tarihi
        public string? Status { get; set; } // Geldi/Gelmedi bilgisi
    }
}
