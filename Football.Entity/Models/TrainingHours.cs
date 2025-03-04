using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basketball.Entity.Models
{
    public class TrainingHours
    {
        public int Id { get; set; }
        public string? TrainingDate { get; set; }  // Antrenman tarihi
        public TimeSpan TrainingTime { get; set; }  // Antrenman saati

        // Kategori grubunun ID'sini tutar
        [ForeignKey("CategoryGroup")]
        public int CategoryGroupsId { get; set; }

        // CategoryGroups ile ilişki (Kategori grubu bilgilerini alabilirsiniz)
        public CategoryGroups CategoryGroup { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
