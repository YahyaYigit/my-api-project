
using System.Text.Json.Serialization;

namespace Basketball.Entity.DTOs.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CategoryGroups { get; set; }
        public int CategoryGroupsId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? DuesId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Dues { get; set; }
        public DateTime BirtDay { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MotherName { get; set; }
        public string? MotherPhoneNumber { get; set; }
        public string? FatherName { get; set; }
        public string? FatherPhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        // Kullanıcının ödeme bilgilerini ay ve yıl bazında tutacak sözlük
        public Dictionary<string, string> MonthlyFees { get; set; } = new Dictionary<string, string>();
    }

}
