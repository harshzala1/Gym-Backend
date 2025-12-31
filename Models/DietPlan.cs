using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public class DietPlan
    {
        [Key]
        public int DietPlanID { get; set; }

        [Required]
        public string DietName { get; set; }

        public string MondayPlan { get; set; }
        public string TuesdayPlan { get; set; }
        public string WednesdayPlan { get; set; }
        public string ThursdayPlan { get; set; }
        public string FridayPlan { get; set; }
        public string SaturdayPlan { get; set; }
        public string SundayPlan { get; set; }

        public ICollection<Member>? Members { get; set; }
    }
}
