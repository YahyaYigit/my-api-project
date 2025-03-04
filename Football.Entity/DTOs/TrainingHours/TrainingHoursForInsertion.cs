using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basketball.Entity.DTOs.TrainingHours
{
    public class TrainingHoursForInsertion
    {
        public string? TrainingDate { get; set; } // Antrenman günü
        public TimeSpan TrainingTime { get; set; }

        public int CategoryGroupsId { get; set; }  // Kategori grubu ID'si
    }
}
