namespace Gym.DTOs
{
    // Response DTO - includes MemberName and TrainerName for display
    public class RequestDto
    {
        public int RequestID { get; set; }
        public int MemberID { get; set; }
        public string? MemberName { get; set; }
        public int TrainerID { get; set; }
        public string? TrainerName { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    // Create DTO - for POST requests
    public class CreateRequestDto
    {
        public int MemberID { get; set; }
        public int TrainerID { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
    }

    // Update DTO - for PUT requests
    public class UpdateRequestDto
    {
        public int RequestID { get; set; }
        public int MemberID { get; set; }
        public int TrainerID { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
