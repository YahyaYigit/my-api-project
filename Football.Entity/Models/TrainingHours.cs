using System.ComponentModel.DataAnnotations.Schema;
using Basketball.Entity.Enums;


namespace Basketball.Entity.Models
{
    public class TrainingHours
    {
        public int Id { get; set; }
        public TrainingHoursEnums TrainingDate { get; set; }  // Antrenman tarihi
        public TimeSpan TrainingStartTime { get; set; }  // Antrenman saati
        public TimeSpan TrainingFinishTime { get; set; }  // Antrenman saati

        // Kategori grubunun ID'sini tutar
        [ForeignKey("CategoryGroup")]
        public int CategoryGroupsId { get; set; }

        // CategoryGroups ile ilişki (Kategori grubu bilgilerini alabilirsiniz)
        public CategoryGroups CategoryGroup { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
