using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        public int MemberID { get; set; }
        public Member? Member { get; set; }

        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionID { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public DateTime MembershipExpiryDate { get; set; }
    }
}
