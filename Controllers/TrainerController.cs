using FluentValidation;
using Gym.Data;
using Gym.DTOs;
using Gym.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        #region Configuration Fields
        private readonly GymDbContext _context;
        private readonly IValidator<CreateTrainerDto> _createValidator;
        private readonly IValidator<UpdateTrainerDto> _updateValidator;

        public TrainerController(GymDbContext context,
            IValidator<CreateTrainerDto> createValidator,
            IValidator<UpdateTrainerDto> updateValidator)
        {
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        #endregion

        #region GetAllTrainers
        [HttpGet]
        public IActionResult GetTrainers()
        {
            var trainers = _context.Trainers
                .Select(t => new TrainerDto
                {
                    TrainerID = t.TrainerID,
                    FullName = t.FullName,
                    Mobile = t.Mobile,
                    Email = t.Email,
                    Salary = t.Salary,
                    ShiftTiming = t.ShiftTiming,
                    Username = t.Username,
                    IsActive = t.IsActive
                })
                .ToList();
            return Ok(trainers);
        }
        #endregion

        #region GetTrainerById
        [HttpGet("{id}")]
        public IActionResult GetTrainerById(int id)
        {
            var trainer = _context.Trainers
                .Where(t => t.TrainerID == id)
                .Select(t => new TrainerDto
                {
                    TrainerID = t.TrainerID,
                    FullName = t.FullName,
                    Mobile = t.Mobile,
                    Email = t.Email,
                    Salary = t.Salary,
                    ShiftTiming = t.ShiftTiming,
                    Username = t.Username,
                    IsActive = t.IsActive
                })
                .FirstOrDefault();

            if (trainer == null)
            {
                return NotFound();
            }
            return Ok(trainer);
        }
        #endregion

        #region DeleteTrainerById
        [HttpDelete("{id}")]
        public IActionResult DeleteTrainerById(int id)
        {
            var trainer = _context.Trainers.Find(id);

            if (trainer == null)
            {
                return NotFound();
            }
            _context.Trainers.Remove(trainer);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertTrainer
        [HttpPost]
        public async Task<IActionResult> InsertTrainer(CreateTrainerDto dto)
        {
            // Fluent Validation
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var trainer = new Trainer
            {
                FullName = dto.FullName,
                Mobile = dto.Mobile,
                Email = dto.Email,
                Salary = dto.Salary,
                ShiftTiming = dto.ShiftTiming,
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IsActive = dto.IsActive
            };

            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTrainerById), new { id = trainer.TrainerID }, new TrainerDto
            {
                TrainerID = trainer.TrainerID,
                FullName = trainer.FullName,
                Mobile = trainer.Mobile,
                Email = trainer.Email,
                Salary = trainer.Salary,
                ShiftTiming = trainer.ShiftTiming,
                Username = trainer.Username,
                IsActive = trainer.IsActive
            });
        }
        #endregion

        #region UpdateTrainer
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainer(int id, UpdateTrainerDto dto)
        {
            if (id != dto.TrainerID)
            {
                return BadRequest();
            }

            // Fluent Validation
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var existingTrainer = await _context.Trainers.FindAsync(id);
            if (existingTrainer == null)
            {
                return NotFound();
            }
            existingTrainer.FullName = dto.FullName;
            existingTrainer.Mobile = dto.Mobile;
            existingTrainer.Email = dto.Email;
            existingTrainer.Salary = dto.Salary;
            existingTrainer.ShiftTiming = dto.ShiftTiming;
            existingTrainer.Username = dto.Username;
            
            // Only update password if a new one is provided
            if (!string.IsNullOrEmpty(dto.Password))
            {
                existingTrainer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            existingTrainer.IsActive = dto.IsActive;

            _context.Trainers.Update(existingTrainer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion
    }
}
