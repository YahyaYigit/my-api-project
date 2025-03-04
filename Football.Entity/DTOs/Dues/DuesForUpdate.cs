using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basketball.Entity.DTOs.Dues
{
    public class DuesForUpdate
    {
        public int Id { get; set; }
        public decimal Fee { get; set; }  // Ücret decimal olmalı
        public string PaymentType { get; set; } = null!;
        public int Month { get; set; }
        public int Year { get; set; }
        public bool IsDeleted { get; set; }
    }
}
