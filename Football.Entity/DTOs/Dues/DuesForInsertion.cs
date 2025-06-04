namespace Basketball.Entity.DTOs.Dues
{
    public class DuesForInsertion
    {
        public int UserId { get; set; }
        public decimal Fee { get; set; }
        public string PaymentType { get; set; } = null!;
        public int Month { get; set; }
        public int Year { get; set; }
    }
}