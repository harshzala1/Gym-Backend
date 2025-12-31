using FluentValidation;
using Gym.DTOs;

namespace Gym.Validators
{
    public class RequestValidator : AbstractValidator<RequestDto>
    {
        public RequestValidator()
        {
            RuleFor(x => x.MemberID)
                .GreaterThan(0).WithMessage("Valid Member ID is required.");

            RuleFor(x => x.TrainerID)
                .GreaterThan(0).WithMessage("Valid Trainer ID is required.");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Subject is required.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.");
        }
    }
}
