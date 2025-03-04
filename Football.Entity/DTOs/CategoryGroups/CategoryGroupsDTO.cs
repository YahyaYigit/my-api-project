using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basketball.Entity.DTOs.CategoryGroups
{
    public class CategoryGroupsDTO
    {

            public int Id { get; set; }
            public string Age { get; set; } = null!; // Yaş grubu
            public List<string> FirstName { get; set; } = new List<string>(); // Kullanıcıların isimleri
            public List<string> LastName { get; set; } = new List<string>(); // Kullanıcıların soyadları

            // Antrenman saatlerini ekleyebilirsiniz
            public List<DateTime> TrainingDates { get; set; } = new List<DateTime>(); // Antrenman günleri
            public List<TimeSpan> TrainingTimes { get; set; } = new List<TimeSpan>(); // Antrenman saatleri
    }
}
