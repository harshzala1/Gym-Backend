using FluentValidation;
using Gym.Data;
using Gym.DTOs;
using Gym.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietPlanController : ControllerBase
    {
        #region Configuration Fields
        private readonly GymDbContext _context;
        private readonly IValidator<CreateDietPlanDto> _createValidator;
        private readonly IValidator<UpdateDietPlanDto> _updateValidator;

        public DietPlanController(GymDbContext context,
            IValidator<CreateDietPlanDto> createValidator,
            IValidator<UpdateDietPlanDto> updateValidator)
        {
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        #endregion

        #region GetAllDietPlans
        [HttpGet]
        public IActionResult GetDietPlans()
        {
            var dietPlans = _context.DietPlans
                .Select(d => new DietPlanDto
                {
                    DietPlanID = d.DietPlanID,
                    DietName = d.DietName,
                    MondayPlan = d.MondayPlan,
                    TuesdayPlan = d.TuesdayPlan,
                    WednesdayPlan = d.WednesdayPlan,
                    ThursdayPlan = d.ThursdayPlan,
                    FridayPlan = d.FridayPlan,
                    SaturdayPlan = d.SaturdayPlan,
                    SundayPlan = d.SundayPlan
                })
                .ToList();
            return Ok(dietPlans);
        }
        #endregion

        #region GetDietPlanById
        [HttpGet("{id}")]
        public IActionResult GetDietPlanById(int id)
        {
            var dietPlan = _context.DietPlans
                .Where(d => d.DietPlanID == id)
                .Select(d => new DietPlanDto
                {
                    DietPlanID = d.DietPlanID,
                    DietName = d.DietName,
                    MondayPlan = d.MondayPlan,
                    TuesdayPlan = d.TuesdayPlan,
                    WednesdayPlan = d.WednesdayPlan,
                    ThursdayPlan = d.ThursdayPlan,
                    FridayPlan = d.FridayPlan,
                    SaturdayPlan = d.SaturdayPlan,
                    SundayPlan = d.SundayPlan
                })
                .FirstOrDefault();

            if (dietPlan == null)
            {
                return NotFound();
            }
            return Ok(dietPlan);
        }
        #endregion

        #region DeleteDietPlanById
        [HttpDelete("{id}")]
        public IActionResult DeleteDietPlanById(int id)
        {
            var dietPlan = _context.DietPlans.Find(id);

            if (dietPlan == null)
            {
                return NotFound();
            }
            _context.DietPlans.Remove(dietPlan);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertDietPlan
        [HttpPost]
        public async Task<IActionResult> InsertDietPlan(CreateDietPlanDto dto)
        {
            // Fluent Validation
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var dietPlan = new DietPlan
            {
                DietName = dto.DietName,
                MondayPlan = dto.MondayPlan,
                TuesdayPlan = dto.TuesdayPlan,
                WednesdayPlan = dto.WednesdayPlan,
                ThursdayPlan = dto.ThursdayPlan,
                FridayPlan = dto.FridayPlan,
                SaturdayPlan = dto.SaturdayPlan,
                SundayPlan = dto.SundayPlan
            };

            _context.DietPlans.Add(dietPlan);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDietPlanById), new { id = dietPlan.DietPlanID }, new DietPlanDto
            {
                DietPlanID = dietPlan.DietPlanID,
                DietName = dietPlan.DietName,
                MondayPlan = dietPlan.MondayPlan,
                TuesdayPlan = dietPlan.TuesdayPlan,
                WednesdayPlan = dietPlan.WednesdayPlan,
                ThursdayPlan = dietPlan.ThursdayPlan,
                FridayPlan = dietPlan.FridayPlan,
                SaturdayPlan = dietPlan.SaturdayPlan,
                SundayPlan = dietPlan.SundayPlan
            });
        }
        #endregion

        #region UpdateDietPlan
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDietPlan(int id, UpdateDietPlanDto dto)
        {
            if (id != dto.DietPlanID)
            {
                return BadRequest();
            }

            // Fluent Validation
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var existingDietPlan = await _context.DietPlans.FindAsync(id);
            if (existingDietPlan == null)
            {
                return NotFound();
            }
            existingDietPlan.DietName = dto.DietName;
            existingDietPlan.MondayPlan = dto.MondayPlan;
            existingDietPlan.TuesdayPlan = dto.TuesdayPlan;
            existingDietPlan.WednesdayPlan = dto.WednesdayPlan;
            existingDietPlan.ThursdayPlan = dto.ThursdayPlan;
            existingDietPlan.FridayPlan = dto.FridayPlan;
            existingDietPlan.SaturdayPlan = dto.SaturdayPlan;
            existingDietPlan.SundayPlan = dto.SundayPlan;

            _context.DietPlans.Update(existingDietPlan);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion
    }
}
