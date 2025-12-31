using FluentValidation;
using Gym.DTOs;

namespace Gym.Validators
{
    public class CreateMemberValidator : AbstractValidator<CreateMemberDto>
    {
        public CreateMemberValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is required.")
                .MaximumLength(100).WithMessage("Full Name cannot exceed 100 characters.");

            RuleFor(x => x.Mobile)
                .NotEmpty().WithMessage("Mobile number is required.")
                .Matches(@"^\d{10}$").WithMessage("Mobile number must be 10 digits.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Age)
                .InclusiveBetween(10, 100).WithMessage("Age must be between 10 and 100.");

            RuleFor(x => x.WeightKg)
                .GreaterThan(0).WithMessage("Weight must be greater than 0.");
            
            RuleFor(x => x.HeightCm)
                .GreaterThan(0).WithMessage("Height must be greater than 0.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        }
    }

    public class UpdateMemberValidator : AbstractValidator<UpdateMemberDto>
    {
        public UpdateMemberValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is required.")
                .MaximumLength(100).WithMessage("Full Name cannot exceed 100 characters.");

            RuleFor(x => x.Mobile)
                .Matches(@"^\d{10}$").WithMessage("Mobile number must be 10 digits.")
                .When(x => !string.IsNullOrEmpty(x.Mobile));

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Age)
                .InclusiveBetween(10, 100).WithMessage("Age must be between 10 and 100.");
        }
    }
}
