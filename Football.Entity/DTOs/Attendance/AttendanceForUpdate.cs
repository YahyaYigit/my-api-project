using Basketball.Entity.Enums;


namespace Basketball.Entity.DTOs.Attendance
{
    public class AttendanceForUpdate
    {
        public int Id { get; set; } // Güncellenecek kaydı belirlemek için
        public int UserId { get; set; }
        public DateTime Date { get; set; } // Yeni tarih
        public AttendanceEnums Status { get; set; } // Geldi/Gelmedi durumu
        public bool IsDeleted { get; set; }
    }
}
