using FluentValidation;
using Gym.DTOs;

namespace Gym.Validators
{
    public class PaymentValidator : AbstractValidator<CreatePaymentDto>
    {
        public PaymentValidator()
        {
            RuleFor(x => x.MemberID)
                .GreaterThan(0).WithMessage("Valid Member ID is required.");

            RuleFor(x => x.AmountPaid)
                .GreaterThan(0).WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.PaymentDate)
                .NotEmpty().WithMessage("Payment Date is required.");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("Payment Method is required.")
                .Must(x => new[] { "Cash", "Card", "UPI", "Bank Transfer" }.Contains(x))
                .WithMessage("Invalid Payment Method.");
        }
    }
}
