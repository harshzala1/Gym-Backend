namespace Gym.DTOs
{
    // Response DTO - includes MemberName for display
    public class AttendanceDto
    {
        public int AttendanceID { get; set; }
        public int MemberID { get; set; }
        public string? MemberName { get; set; }
        public DateTime CheckInTime { get; set; }
        public string RecordedBy { get; set; } = string.Empty;
    }

    // Create DTO - for POST requests
    public class CreateAttendanceDto
    {
        public int MemberID { get; set; }
        public DateTime CheckInTime { get; set; } = DateTime.Now;
        public string RecordedBy { get; set; } = string.Empty;
    }

    // Update DTO - for PUT requests
    public class UpdateAttendanceDto
    {
        public int AttendanceID { get; set; }
        public int MemberID { get; set; }
        public DateTime CheckInTime { get; set; }
        public string RecordedBy { get; set; } = string.Empty;
    }
}
