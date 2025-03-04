using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basketball.Entity.DTOs.CategoryGroups
{
    public class CategoryGroupsForUpdate
    {
        public int Id { get; set; }
        public string Age { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
