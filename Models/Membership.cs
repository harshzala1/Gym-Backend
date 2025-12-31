using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public class Membership
    {
        [Key]
        public int MembershipID { get; set; }

        [Required]
        public string Name { get; set; }

        public int DurationMonths { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public ICollection<Member>? Members { get; set; }
    }
}
