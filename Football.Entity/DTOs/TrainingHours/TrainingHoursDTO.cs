

namespace Basketball.Entity.DTOs.TrainingHours
{
    public class TrainingHoursDTO
    {
        public int Id { get; set; }
        public string? TrainingDate { get; set; }
      
        public TimeSpan TrainingStartTime { get; set; }  // Antrenman saati
        public TimeSpan TrainingFinishTime { get; set; }  // Antrenman saati
        public int CategoryGroupsId { get; set; }  // Kategori grubu ID'si
        public string CategoryGroupName { get; set; } = null!;  // Kategori grubu adı (ör. "U-16")

        // Antrenman tarihinin ve saatinin yanı sıra, bu yaş grubuyla ilişkili kullanıcı bilgilerini de ekleyebilirsiniz.
        public List<string> FirstNames { get; set; } = new List<string>(); // Kullanıcı isimleri
        public List<string> LastNames { get; set; } = new List<string>(); // Kullanıcı soyadları


    }
}
