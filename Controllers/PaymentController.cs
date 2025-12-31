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
    public class PaymentController : ControllerBase
    {
        #region Configuration Fields
        private readonly GymDbContext _context;
        private readonly IValidator<CreatePaymentDto> _createValidator;

        public PaymentController(GymDbContext context,
             IValidator<CreatePaymentDto> createValidator)
        {
            _context = context;
            _createValidator = createValidator;
        }
        #endregion

        #region GetAllPayments
        [HttpGet]
        public IActionResult GetPayments()
        {
            var payments = _context.Payments
                .Include(p => p.Member)
                .Select(p => new PaymentDto
                {
                    PaymentID = p.PaymentID,
                    MemberID = p.MemberID,
                    MemberName = p.Member != null ? p.Member.FullName : null,
                    AmountPaid = p.AmountPaid,
                    PaymentMethod = p.PaymentMethod,
                    TransactionID = p.TransactionID,
                    PaymentDate = p.PaymentDate,
                    MembershipExpiryDate = p.MembershipExpiryDate
                })
                .ToList();
            return Ok(payments);
        }
        #endregion

        #region GetPaymentById
        [HttpGet("{id}")]
        public IActionResult GetPaymentById(int id)
        {
            var payment = _context.Payments
                .Include(p => p.Member)
                .Where(p => p.PaymentID == id)
                .Select(p => new PaymentDto
                {
                    PaymentID = p.PaymentID,
                    MemberID = p.MemberID,
                    MemberName = p.Member != null ? p.Member.FullName : null,
                    AmountPaid = p.AmountPaid,
                    PaymentMethod = p.PaymentMethod,
                    TransactionID = p.TransactionID,
                    PaymentDate = p.PaymentDate,
                    MembershipExpiryDate = p.MembershipExpiryDate
                })
                .FirstOrDefault();

            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }
        #endregion

        #region DeletePaymentById
        [HttpDelete("{id}")]
        public IActionResult DeletePaymentById(int id)
        {
            var payment = _context.Payments.Find(id);

            if (payment == null)
            {
                return NotFound();
            }
            _context.Payments.Remove(payment);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertPayment
        [HttpPost]
        public async Task<IActionResult> InsertPayment(CreatePaymentDto dto)
        {
            // Fluent Validation
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var payment = new Payment
            {
                MemberID = dto.MemberID,
                AmountPaid = dto.AmountPaid,
                PaymentMethod = dto.PaymentMethod,
                TransactionID = dto.TransactionID,
                PaymentDate = dto.PaymentDate,
                MembershipExpiryDate = dto.MembershipExpiryDate
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.PaymentID }, new PaymentDto
            {
                PaymentID = payment.PaymentID,
                MemberID = payment.MemberID,
                AmountPaid = payment.AmountPaid,
                PaymentMethod = payment.PaymentMethod,
                TransactionID = payment.TransactionID,
                PaymentDate = payment.PaymentDate,
                MembershipExpiryDate = payment.MembershipExpiryDate
            });
        }
        #endregion

        #region UpdatePayment
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, UpdatePaymentDto dto)
        {
            if (id != dto.PaymentID)
            {
                return BadRequest();
            }

            var existingPayment = await _context.Payments.FindAsync(id);
            if (existingPayment == null)
            {
                return NotFound();
            }
            existingPayment.MemberID = dto.MemberID;
            existingPayment.AmountPaid = dto.AmountPaid;
            existingPayment.PaymentMethod = dto.PaymentMethod;
            existingPayment.TransactionID = dto.TransactionID;
            existingPayment.PaymentDate = dto.PaymentDate;
            existingPayment.MembershipExpiryDate = dto.MembershipExpiryDate;

            _context.Payments.Update(existingPayment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion
    }
}
