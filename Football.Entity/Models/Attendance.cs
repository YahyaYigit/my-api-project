using Basketball.Entity.Enums;
using System.ComponentModel.DataAnnotations.Schema;


namespace Basketball.Entity.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } // 'date' küçük harfle olmamalı, büyük harfle düzelttim.
        [ForeignKey("User")]
        public int UserId { get; set; }

        // CategoryGroups ile ilişki (Kategori grubu bilgilerini alabilirsiniz)
        [InverseProperty("Attendances")]
        public User Users { get; set; } = null!;


        public AttendanceEnums Status { get; set; } // 'enums' yerine daha anlamlı bir isim olan 'Status' kullanıldı.

        public bool IsDeleted { get; set; } // 'IsDelete' yerine 'IsDeleted' daha uygun.
    }
}
