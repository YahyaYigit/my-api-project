using Basketball.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
