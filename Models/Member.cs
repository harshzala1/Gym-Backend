using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public class Member
    {
        [Key]
        public int MemberID { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Mobile { get; set; }
        public string Email { get; set; }

        public int Age { get; set; }
        public decimal WeightKg { get; set; }
        public decimal HeightCm { get; set; }
        public decimal BMI { get; set; }

        public DateTime JoiningDate { get; set; } = DateTime.Now;

        public int MembershipID { get; set; }
        public Membership? Membership { get; set; }

        public int TrainerID { get; set; }
        public Trainer? Trainer { get; set; }

        public int DietPlanID { get; set; }
        public DietPlan? DietPlan { get; set; }

        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; } = false;
        public string AccountStatus { get; set; } = "Active";

        public ICollection<Attendance>? Attendance { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<Request>? Requests { get; set; }
    }
}
