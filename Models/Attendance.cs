using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceID { get; set; }

        public int MemberID { get; set; }
        public Member? Member { get; set; }

        public DateTime CheckInTime { get; set; } = DateTime.Now;
        public string RecordedBy { get; set; }
    }
}
