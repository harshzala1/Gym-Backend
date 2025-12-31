namespace Gym.DTOs
{
    // Response DTO
    public class MembershipDto
    {
        public int MembershipID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DurationMonths { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    // Create DTO - for POST requests
    public class CreateMembershipDto
    {
        public string Name { get; set; } = string.Empty;
        public int DurationMonths { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    // Update DTO - for PUT requests
    public class UpdateMembershipDto
    {
        public int MembershipID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DurationMonths { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
