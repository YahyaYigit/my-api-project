using System.ComponentModel.DataAnnotations.Schema;

namespace Basketball.Entity.Models
{
    public class Dues
    {
        public int Id { get; set; }
        public decimal Fee { get; set; }  // Ücret decimal olmalı
        public string PaymentType { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public int UserId { get; set; }

        // CategoryGroups ile ilişki (Kategori grubu bilgilerini alabilirsiniz)
        [InverseProperty("Dues")]
        public User Users { get; set; } = null!;

        // Ay ve yıl bilgilerini int olarak ekliyoruz:
        public int Month { get; set; }         // 1 ile 12 arasında bir değer (örn. 1: Ocak, 2: Şubat, vb.)
        public int Year { get; set; }          // Ödeme yapılan yıl


    }

}