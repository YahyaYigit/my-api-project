
namespace Basketball.Entity.DTOs.Dues
{
    public class DuesDTO
    {
        public int Id { get; set; }
        public decimal Fee { get; set; }
        public string PaymentType { get; set; } = null!;
        public int Month { get; set; }
        public int Year { get; set; }
        public bool IsDeleted { get; set; }

        // İlişkili User nesnelerinin FirstName ve LastName özellikleri
        public List<string> FirstNames { get; set; } = new List<string>();
        public List<string> LastNames { get; set; } = new List<string>();

      

    }
}
