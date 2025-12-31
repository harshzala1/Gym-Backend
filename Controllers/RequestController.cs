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
    public class RequestController : ControllerBase
    {
        #region Configuration Fields
        private readonly GymDbContext _context;
        private readonly IValidator<CreateRequestDto> _createValidator;
        private readonly IValidator<UpdateRequestDto> _updateValidator;

        public RequestController(GymDbContext context,
            IValidator<CreateRequestDto> createValidator,
            IValidator<UpdateRequestDto> updateValidator)
        {
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        #endregion

        #region GetAllRequests
        [HttpGet]
        public IActionResult GetRequests()
        {
            var requests = _context.Requests
                .Include(r => r.Member)
                .Include(r => r.Trainer)
                .Select(r => new RequestDto
                {
                    RequestID = r.RequestID,
                    MemberID = r.MemberID,
                    MemberName = r.Member != null ? r.Member.FullName : null,
                    TrainerID = r.TrainerID,
                    TrainerName = r.Trainer != null ? r.Trainer.FullName : null,
                    Subject = r.Subject,
                    Message = r.Message,
                    Status = r.Status,
                    CreatedAt = r.CreatedAt
                })
                .ToList();
            return Ok(requests);
        }
        #endregion

        #region GetRequestById
        [HttpGet("{id}")]
        public IActionResult GetRequestById(int id)
        {
            var request = _context.Requests
                .Include(r => r.Member)
                .Include(r => r.Trainer)
                .Where(r => r.RequestID == id)
                .Select(r => new RequestDto
                {
                    RequestID = r.RequestID,
                    MemberID = r.MemberID,
                    MemberName = r.Member != null ? r.Member.FullName : null,
                    TrainerID = r.TrainerID,
                    TrainerName = r.Trainer != null ? r.Trainer.FullName : null,
                    Subject = r.Subject,
                    Message = r.Message,
                    Status = r.Status,
                    CreatedAt = r.CreatedAt
                })
                .FirstOrDefault();

            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }
        #endregion

        #region DeleteRequestById
        [HttpDelete("{id}")]
        public IActionResult DeleteRequestById(int id)
        {
            var request = _context.Requests.Find(id);

            if (request == null)
            {
                return NotFound();
            }
            _context.Requests.Remove(request);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertRequest
        [HttpPost]
        public async Task<IActionResult> InsertRequest(CreateRequestDto dto)
        {
            // Fluent Validation
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var request = new Request
            {
                MemberID = dto.MemberID,
                TrainerID = dto.TrainerID,
                Subject = dto.Subject,
                Message = dto.Message,
                Status = dto.Status,
                CreatedAt = DateTime.Now
            };

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRequestById), new { id = request.RequestID }, new RequestDto
            {
                RequestID = request.RequestID,
                MemberID = request.MemberID,
                TrainerID = request.TrainerID,
                Subject = request.Subject,
                Message = request.Message,
                Status = request.Status,
                CreatedAt = request.CreatedAt
            });
        }
        #endregion

        #region UpdateRequest
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(int id, UpdateRequestDto dto)
        {
            if (id != dto.RequestID)
            {
                return BadRequest();
            }

            // Fluent Validation
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var existingRequest = await _context.Requests.FindAsync(id);
            if (existingRequest == null)
            {
                return NotFound();
            }
            existingRequest.MemberID = dto.MemberID;
            existingRequest.TrainerID = dto.TrainerID;
            existingRequest.Subject = dto.Subject;
            existingRequest.Message = dto.Message;
            existingRequest.Status = dto.Status;

            _context.Requests.Update(existingRequest);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion
    }
}
