using Basketball.Entity.Enums;


namespace Basketball.Entity.DTOs.Attendance
{
    public class AttendanceForInsertion
    {
        public int UserId { get; set; } // Hangi kullanıcıya ait olduğu
        public DateTime Date { get; set; } // Yoklamanın alındığı tarih
        public AttendanceEnums Status { get; set; } // Geldi/Gelmedi durumu
    }
}
