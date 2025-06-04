using System.Text.Json.Serialization;

namespace Basketball.Entity.DTOs.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
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
        public string? TcNo { get; set; }
        public string? BirthPlace { get; set; }
        public string? School { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? HealthProblem { get; set; }
        public bool IsAcceptedWhatsappGroup { get; set; }
        public bool IsAcceptedMotherWhatsappGroup { get; set; }
        public bool IsAcceptedFatherWhatsappGroup { get; set; }

        public bool AcceptedKVKK { get; set; }
        public bool AcceptedImportant { get; set; }

        // Kullanıcının ödeme bilgilerini ay ve yıl bazında tutacak sözlük
        public Dictionary<string, string> MonthlyFees { get; set; } = new Dictionary<string, string>();
        public bool IsDeleted { get; set; }
    }

}