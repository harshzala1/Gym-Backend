using FluentValidation;
using Gym.Data;
using Gym.DTOs;
using Gym.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        #region Configuration Fields
        private readonly GymDbContext _context;
        private readonly IValidator<CreateAttendanceDto> _createValidator;

        public AttendanceController(GymDbContext context,
             IValidator<CreateAttendanceDto> createValidator)
        {
            _context = context;
            _createValidator = createValidator;
        }
        #endregion

        #region GetAllAttendance
        [HttpGet]
        public IActionResult GetAttendance()
        {
            var attendanceList = _context.Attendance
                .Include(a => a.Member)
                .Select(a => new AttendanceDto
                {
                    AttendanceID = a.AttendanceID,
                    MemberID = a.MemberID,
                    MemberName = a.Member != null ? a.Member.FullName : null,
                    CheckInTime = a.CheckInTime,
                    RecordedBy = a.RecordedBy
                })
                .ToList();
            return Ok(attendanceList);
        }
        #endregion

        #region GetAttendanceById
        [HttpGet("{id}")]
        public IActionResult GetAttendanceById(int id)
        {
            var attendance = _context.Attendance
                .Include(a => a.Member)
                .Where(a => a.AttendanceID == id)
                .Select(a => new AttendanceDto
                {
                    AttendanceID = a.AttendanceID,
                    MemberID = a.MemberID,
                    MemberName = a.Member != null ? a.Member.FullName : null,
                    CheckInTime = a.CheckInTime,
                    RecordedBy = a.RecordedBy
                })
                .FirstOrDefault();

            if (attendance == null)
            {
                return NotFound();
            }
            return Ok(attendance);
        }
        #endregion

        #region DeleteAttendanceById
        [HttpDelete("{id}")]
        public IActionResult DeleteAttendanceById(int id)
        {
            var attendance = _context.Attendance.Find(id);

            if (attendance == null)
            {
                return NotFound();
            }
            _context.Attendance.Remove(attendance);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertAttendance
        [HttpPost]
        public async Task<IActionResult> InsertAttendance(CreateAttendanceDto dto)
        {
            // Fluent Validation
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var attendance = new Attendance
            {
                MemberID = dto.MemberID,
                CheckInTime = dto.CheckInTime,
                RecordedBy = dto.RecordedBy
            };

            _context.Attendance.Add(attendance);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAttendanceById), new { id = attendance.AttendanceID }, new AttendanceDto
            {
                AttendanceID = attendance.AttendanceID,
                MemberID = attendance.MemberID,
                CheckInTime = attendance.CheckInTime,
                RecordedBy = attendance.RecordedBy
            });
        }
        #endregion

        #region UpdateAttendance
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttendance(int id, UpdateAttendanceDto dto)
        {
            if (id != dto.AttendanceID)
            {
                return BadRequest();
            }

            var existingAttendance = await _context.Attendance.FindAsync(id);
            if (existingAttendance == null)
            {
                return NotFound();
            }
            existingAttendance.MemberID = dto.MemberID;
            existingAttendance.CheckInTime = dto.CheckInTime;
            existingAttendance.RecordedBy = dto.RecordedBy;

            _context.Attendance.Update(existingAttendance);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion
    }
}
