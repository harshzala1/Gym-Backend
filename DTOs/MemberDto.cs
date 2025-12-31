namespace Gym.DTOs
{
    // Response DTO - excludes sensitive data like PasswordHash
    public class MemberDto
    {
        public int MemberID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal WeightKg { get; set; }
        public decimal HeightCm { get; set; }
        public decimal BMI { get; set; }
        public DateTime JoiningDate { get; set; }
        public int MembershipID { get; set; }
        public string? MembershipName { get; set; }
        public int TrainerID { get; set; }
        public string? TrainerName { get; set; }
        public int DietPlanID { get; set; }
        public string? DietPlanName { get; set; }
        public string Username { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public string AccountStatus { get; set; } = string.Empty;
    }

    // Create DTO - for POST requests
    public class CreateMemberDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal WeightKg { get; set; }
        public decimal HeightCm { get; set; }
        public decimal BMI { get; set; }
        public DateTime JoiningDate { get; set; } = DateTime.Now;
        public int MembershipID { get; set; }
        public int TrainerID { get; set; }
        public int DietPlanID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        public string AccountStatus { get; set; } = "Active";
    }

    // Update DTO - for PUT requests
    public class UpdateMemberDto
    {
        public int MemberID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal WeightKg { get; set; }
        public decimal HeightCm { get; set; }
        public decimal BMI { get; set; }
        public DateTime JoiningDate { get; set; }
        public int MembershipID { get; set; }
        public int TrainerID { get; set; }
        public int DietPlanID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Password { get; set; } // Optional - only update if provided
        public bool IsAdmin { get; set; }
        public string AccountStatus { get; set; } = string.Empty;
    }
}
