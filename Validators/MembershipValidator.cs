using FluentValidation;
using Gym.DTOs;

namespace Gym.Validators
{
    public class MembershipValidator : AbstractValidator<CreateMembershipDto>
    {
        public MembershipValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Membership Name is required.");

            RuleFor(x => x.DurationMonths)
                .GreaterThan(0).WithMessage("Duration must be greater than 0.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");
        }
    }
}
