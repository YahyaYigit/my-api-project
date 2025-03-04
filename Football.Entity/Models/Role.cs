using Microsoft.AspNetCore.Identity;

namespace Basketball.Entity.Models
{
    public class Role : IdentityRole<int>
    {
        public bool IsDeleted { get; set; } = false; // Varsayılan olarak false

    }
}
