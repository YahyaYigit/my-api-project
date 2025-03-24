

namespace Basketball.Entity.DTOs.TrainingHours
{
    public class TrainingHoursForInsertion
    {
        public string? TrainingDate { get; set; } // Antrenman günü
        public TimeSpan TrainingStartTime { get; set; }  // Antrenman saati
        public TimeSpan TrainingFinishTime { get; set; }  // Antrenman saati

        public int CategoryGroupsId { get; set; }  // Kategori grubu ID'si
    }
}
