using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public class Request
    {
        [Key]
        public int RequestID { get; set; }

        public int MemberID { get; set; }
        public Member? Member { get; set; }

        public int TrainerID { get; set; }
        public Trainer? Trainer { get; set; }

        public string Subject { get; set; }
        public string Message { get; set; }

        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
