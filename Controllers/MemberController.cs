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
    public class MemberController : ControllerBase
    {
        #region Configuration Fields
        private readonly GymDbContext _context;
        private readonly IValidator<CreateMemberDto> _createValidator;
        private readonly IValidator<UpdateMemberDto> _updateValidator;

        public MemberController(GymDbContext context, 
            IValidator<CreateMemberDto> createValidator,
            IValidator<UpdateMemberDto> updateValidator)
        {
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        #endregion

        #region GetAllMembers
        [HttpGet]
        public IActionResult GetMembers()
        {
            var members = _context.Members
                .Include(m => m.Membership)
                .Include(m => m.Trainer)
                .Include(m => m.DietPlan)
                .Select(m => new MemberDto
                {
                    MemberID = m.MemberID,
                    FullName = m.FullName,
                    Mobile = m.Mobile,
                    Email = m.Email,
                    Age = m.Age,
                    WeightKg = m.WeightKg,
                    HeightCm = m.HeightCm,
                    BMI = m.BMI,
                    JoiningDate = m.JoiningDate,
                    MembershipID = m.MembershipID,
                    MembershipName = m.Membership != null ? m.Membership.Name : null,
                    TrainerID = m.TrainerID,
                    TrainerName = m.Trainer != null ? m.Trainer.FullName : null,
                    DietPlanID = m.DietPlanID,
                    DietPlanName = m.DietPlan != null ? m.DietPlan.DietName : null,
                    Username = m.Username,
                    IsAdmin = m.IsAdmin,
                    AccountStatus = m.AccountStatus
                })
                .ToList();
            return Ok(members);
        }
        #endregion

        #region GetMemberById
        [HttpGet("{id}")]
        public IActionResult GetMemberById(int id)
        {
            var member = _context.Members
                .Include(m => m.Membership)
                .Include(m => m.Trainer)
                .Include(m => m.DietPlan)
                .Where(m => m.MemberID == id)
                .Select(m => new MemberDto
                {
                    MemberID = m.MemberID,
                    FullName = m.FullName,
                    Mobile = m.Mobile,
                    Email = m.Email,
                    Age = m.Age,
                    WeightKg = m.WeightKg,
                    HeightCm = m.HeightCm,
                    BMI = m.BMI,
                    JoiningDate = m.JoiningDate,
                    MembershipID = m.MembershipID,
                    MembershipName = m.Membership != null ? m.Membership.Name : null,
                    TrainerID = m.TrainerID,
                    TrainerName = m.Trainer != null ? m.Trainer.FullName : null,
                    DietPlanID = m.DietPlanID,
                    DietPlanName = m.DietPlan != null ? m.DietPlan.DietName : null,
                    Username = m.Username,
                    IsAdmin = m.IsAdmin,
                    AccountStatus = m.AccountStatus
                })
                .FirstOrDefault();

            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }
        #endregion

        #region DeleteMemberById
        [HttpDelete("{id}")]
        public IActionResult DeleteMemberById(int id)
        {
            var member = _context.Members.Find(id);

            if (member == null)
            {
                return NotFound();
            }
            _context.Members.Remove(member);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertMember
        [HttpPost]
        public async Task<IActionResult> InsertMember(CreateMemberDto dto)
        {
            // Fluent Validation
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var member = new Member
            {
                FullName = dto.FullName,
                Mobile = dto.Mobile,
                Email = dto.Email,
                Age = dto.Age,
                WeightKg = dto.WeightKg,
                HeightCm = dto.HeightCm,
                BMI = dto.BMI,
                JoiningDate = dto.JoiningDate,
                MembershipID = dto.MembershipID,
                TrainerID = dto.TrainerID,
                DietPlanID = dto.DietPlanID,
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IsAdmin = dto.IsAdmin,
                AccountStatus = dto.AccountStatus
            };

            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMemberById), new { id = member.MemberID }, new MemberDto
            {
                MemberID = member.MemberID,
                FullName = member.FullName,
                Mobile = member.Mobile,
                Email = member.Email,
                Age = member.Age,
                WeightKg = member.WeightKg,
                HeightCm = member.HeightCm,
                BMI = member.BMI,
                JoiningDate = member.JoiningDate,
                MembershipID = member.MembershipID,
                TrainerID = member.TrainerID,
                DietPlanID = member.DietPlanID,
                Username = member.Username,
                IsAdmin = member.IsAdmin,
                AccountStatus = member.AccountStatus
            });
        }
        #endregion

        #region UpdateMember
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, UpdateMemberDto dto)
        {
            if (id != dto.MemberID)
            {
                return BadRequest();
            }

            // Fluent Validation
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var existingMember = await _context.Members.FindAsync(id);
            if (existingMember == null)
            {
                return NotFound();
            }
            existingMember.FullName = dto.FullName;
            existingMember.Mobile = dto.Mobile;
            existingMember.Email = dto.Email;
            existingMember.Age = dto.Age;
            existingMember.WeightKg = dto.WeightKg;
            existingMember.HeightCm = dto.HeightCm;
            existingMember.BMI = dto.BMI;
            existingMember.JoiningDate = dto.JoiningDate;
            existingMember.MembershipID = dto.MembershipID;
            existingMember.TrainerID = dto.TrainerID;
            existingMember.DietPlanID = dto.DietPlanID;
            existingMember.Username = dto.Username;
            
            // Only update password if provided
            if (!string.IsNullOrEmpty(dto.Password))
            {
                existingMember.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }
            
            existingMember.IsAdmin = dto.IsAdmin;
            existingMember.AccountStatus = dto.AccountStatus;

            _context.Members.Update(existingMember);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion
    }
}
