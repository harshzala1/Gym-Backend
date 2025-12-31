using FluentValidation;
using Gym.DTOs;

namespace Gym.Validators
{
    public class CreateTrainerValidator : AbstractValidator<CreateTrainerDto>
    {
        public CreateTrainerValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is required.");

            RuleFor(x => x.Mobile)
                .NotEmpty().WithMessage("Mobile number is required.")
                .Matches(@"^\d{10}$").WithMessage("Mobile number must be 10 digits.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0.");

            RuleFor(x => x.ShiftTiming)
                 .NotEmpty().WithMessage("Shift Timing is required.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        }
    }

    public class UpdateTrainerValidator : AbstractValidator<UpdateTrainerDto>
    {
        public UpdateTrainerValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is required.");

            RuleFor(x => x.Mobile)
                .Matches(@"^\d{10}$").WithMessage("Mobile number must be 10 digits.")
                .When(x => !string.IsNullOrEmpty(x.Mobile));

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0.");
        }
    }
}
