

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basketball.Entity.Models
{
    public class User : IdentityUser<int>
    { 

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [ForeignKey("CategoryGroups")]
        public int? CategoryGroupsId { get; set; } // Nullable olmalı!
        [InverseProperty("Users")]
        public CategoryGroups? CategoryGroups { get; set; }

        [ForeignKey("Dues")]
        public int? DuesId { get; set; } // Nullable olmalı!
        [InverseProperty("Users")]
        public Dues? Dues { get; set; }

        // Her bir kullanıcı, birden fazla Attendance kaydına sahip olabilir.
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

        public DateTime BirtDay { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MotherName { get; set; }
        public string? MotherPhoneNumber { get; set; }
        public string? FatherName { get; set; }
        public string? FatherPhoneNumber { get; set; }


        public ICollection<Role>Roles { get; set; } =new List<Role>();

        public bool IsDeleted { get; set; }
    }

}
