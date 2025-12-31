namespace Gym.DTOs
{
    // Response DTO
    public class DietPlanDto
    {
        public int DietPlanID { get; set; }
        public string DietName { get; set; } = string.Empty;
        public string MondayPlan { get; set; } = string.Empty;
        public string TuesdayPlan { get; set; } = string.Empty;
        public string WednesdayPlan { get; set; } = string.Empty;
        public string ThursdayPlan { get; set; } = string.Empty;
        public string FridayPlan { get; set; } = string.Empty;
        public string SaturdayPlan { get; set; } = string.Empty;
        public string SundayPlan { get; set; } = string.Empty;
    }

    // Create DTO - for POST requests
    public class CreateDietPlanDto
    {
        public string DietName { get; set; } = string.Empty;
        public string MondayPlan { get; set; } = string.Empty;
        public string TuesdayPlan { get; set; } = string.Empty;
        public string WednesdayPlan { get; set; } = string.Empty;
        public string ThursdayPlan { get; set; } = string.Empty;
        public string FridayPlan { get; set; } = string.Empty;
        public string SaturdayPlan { get; set; } = string.Empty;
        public string SundayPlan { get; set; } = string.Empty;
    }

    // Update DTO - for PUT requests
    public class UpdateDietPlanDto
    {
        public int DietPlanID { get; set; }
        public string DietName { get; set; } = string.Empty;
        public string MondayPlan { get; set; } = string.Empty;
        public string TuesdayPlan { get; set; } = string.Empty;
        public string WednesdayPlan { get; set; } = string.Empty;
        public string ThursdayPlan { get; set; } = string.Empty;
        public string FridayPlan { get; set; } = string.Empty;
        public string SaturdayPlan { get; set; } = string.Empty;
        public string SundayPlan { get; set; } = string.Empty;
    }
}
