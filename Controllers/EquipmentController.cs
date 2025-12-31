using FluentValidation;
using Gym.Data;
using Gym.DTOs;
using Gym.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        #region Configuration Fields
        private readonly GymDbContext _context;
        private readonly IValidator<CreateEquipmentDto> _createValidator;

        public EquipmentController(GymDbContext context,
             IValidator<CreateEquipmentDto> createValidator)
        {
            _context = context;
            _createValidator = createValidator;
        }
        #endregion

        #region GetAllEquipment
        [HttpGet]
        public IActionResult GetEquipment()
        {
            var equipmentList = _context.Equipment
                .Select(e => new EquipmentDto
                {
                    EquipmentID = e.EquipmentID,
                    Name = e.Name,
                    PurchaseDate = e.PurchaseDate,
                    Cost = e.Cost,
                    WarrantyExpiryDate = e.WarrantyExpiryDate,
                    NextMaintenanceDate = e.NextMaintenanceDate,
                    Status = e.Status
                })
                .ToList();
            return Ok(equipmentList);
        }
        #endregion

        #region GetEquipmentById
        [HttpGet("{id}")]
        public IActionResult GetEquipmentById(int id)
        {
            var equipment = _context.Equipment
                .Where(e => e.EquipmentID == id)
                .Select(e => new EquipmentDto
                {
                    EquipmentID = e.EquipmentID,
                    Name = e.Name,
                    PurchaseDate = e.PurchaseDate,
                    Cost = e.Cost,
                    WarrantyExpiryDate = e.WarrantyExpiryDate,
                    NextMaintenanceDate = e.NextMaintenanceDate,
                    Status = e.Status
                })
                .FirstOrDefault();

            if (equipment == null)
            {
                return NotFound();
            }
            return Ok(equipment);
        }
        #endregion

        #region DeleteEquipmentById
        [HttpDelete("{id}")]
        public IActionResult DeleteEquipmentById(int id)
        {
            var equipment = _context.Equipment.Find(id);

            if (equipment == null)
            {
                return NotFound();
            }
            _context.Equipment.Remove(equipment);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertEquipment
        [HttpPost]
        public async Task<IActionResult> InsertEquipment(CreateEquipmentDto dto)
        {
            // Fluent Validation
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var equipment = new Equipment
            {
                Name = dto.Name,
                PurchaseDate = dto.PurchaseDate,
                Cost = dto.Cost,
                WarrantyExpiryDate = dto.WarrantyExpiryDate,
                NextMaintenanceDate = dto.NextMaintenanceDate,
                Status = dto.Status
            };

            _context.Equipment.Add(equipment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEquipmentById), new { id = equipment.EquipmentID }, new EquipmentDto
            {
                EquipmentID = equipment.EquipmentID,
                Name = equipment.Name,
                PurchaseDate = equipment.PurchaseDate,
                Cost = equipment.Cost,
                WarrantyExpiryDate = equipment.WarrantyExpiryDate,
                NextMaintenanceDate = equipment.NextMaintenanceDate,
                Status = equipment.Status
            });
        }
        #endregion

        #region UpdateEquipment
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEquipment(int id, UpdateEquipmentDto dto)
        {
            if (id != dto.EquipmentID)
            {
                return BadRequest();
            }

            var existingEquipment = await _context.Equipment.FindAsync(id);
            if (existingEquipment == null)
            {
                return NotFound();
            }
            existingEquipment.Name = dto.Name;
            existingEquipment.PurchaseDate = dto.PurchaseDate;
            existingEquipment.Cost = dto.Cost;
            existingEquipment.WarrantyExpiryDate = dto.WarrantyExpiryDate;
            existingEquipment.NextMaintenanceDate = dto.NextMaintenanceDate;
            existingEquipment.Status = dto.Status;

            _context.Equipment.Update(existingEquipment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion
    }
}
