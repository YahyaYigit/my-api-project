﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basketball.Entity.Models
{
    public class User : IdentityUser<int>
    {
        // CategoryGroups ile ilişki
        [ForeignKey("CategoryGroups")]
        public int? CategoryGroupsId { get; set; }  // Nullable olmalı
        public CategoryGroups? CategoryGroups { get; set; }  // CategoryGroups ile ilişki

        // Kullanıcı birden fazla Dues kaydına sahip olabilir, bu nedenle ICollection kullanılıyor
        public ICollection<Dues> Dues { get; set; } = new List<Dues>();

        // Her bir kullanıcı, birden fazla Attendance kaydına sahip olabilir.
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

        public bool IsAdmin { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirtDay { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MotherName { get; set; }
        public string? MotherPhoneNumber { get; set; }
        public string? FatherName { get; set; }
        public string? FatherPhoneNumber { get; set; }
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

        public bool IsDeleted { get; set; }
    }
}