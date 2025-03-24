

namespace Basketball.Entity.DTOs.TrainingHours
{
    public class TrainingHoursForUpdate
    {
        public int Id { get; set; }
        public string? TrainingDate { get; set; } // Güncelleme için tarih
        public TimeSpan TrainingStartTime { get; set; }  // Antrenman saati
        public TimeSpan TrainingFinishTime { get; set; }  // Antrenman saati

        public int CategoryGroupsId { get; set; }  // Kategori grubu ID'si

        public bool IsDeleted { get; set; }
    }
}
