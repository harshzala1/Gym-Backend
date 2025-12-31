using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public class Trainer
    {
        [Key]
        public int TrainerID { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Mobile { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public string ShiftTiming { get; set; }

        [Required]
        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Member>? Members { get; set; }
    }
}
