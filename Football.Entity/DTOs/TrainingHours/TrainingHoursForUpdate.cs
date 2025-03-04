using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basketball.Entity.DTOs.TrainingHours
{
    public class TrainingHoursForUpdate
    {
        public int Id { get; set; }
        public string? TrainingDate { get; set; } // Güncelleme için tarih
        public TimeSpan TrainingTime { get; set; } // Güncelleme için saat

        public int CategoryGroupsId { get; set; }  // Kategori grubu ID'si

        public bool IsDeleted { get; set; }
    }
}
