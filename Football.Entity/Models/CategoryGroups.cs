using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; // Bu namespace'i ekleyin

namespace Basketball.Entity.Models
{
    public class CategoryGroups
    {
        public int Id { get; set; }
        public string Age { get; set; } = null!;
        public bool IsDeleted { get; set; }

        // Kullanıcılarla ilişki (Bir kategori grubunda birden fazla kullanıcı olabilir)
        [InverseProperty("CategoryGroups")]
        [JsonIgnore] // Bu property JSON'a dahil edilmeyecek, dolayısıyla döngü kesilmiş olacak.
        public ICollection<User> Users { get; set; } = new List<User>();

        // TrainingHours ile ilişki (Bir kategori grubunda birden fazla antrenman saati olabilir)
        public ICollection<TrainingHours> TrainingHours { get; set; } = new List<TrainingHours>();
    }
}
