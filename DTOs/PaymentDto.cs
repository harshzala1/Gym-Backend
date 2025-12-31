namespace Gym.DTOs
{
    // Response DTO - includes MemberName for display
    public class PaymentDto
    {
        public int PaymentID { get; set; }
        public int MemberID { get; set; }
        public string? MemberName { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string TransactionID { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public DateTime MembershipExpiryDate { get; set; }
    }

    // Create DTO - for POST requests
    public class CreatePaymentDto
    {
        public int MemberID { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string TransactionID { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public DateTime MembershipExpiryDate { get; set; }
    }

    // Update DTO - for PUT requests
    public class UpdatePaymentDto
    {
        public int PaymentID { get; set; }
        public int MemberID { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string TransactionID { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public DateTime MembershipExpiryDate { get; set; }
    }
}
