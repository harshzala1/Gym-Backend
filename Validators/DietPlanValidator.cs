using FluentValidation;
using Gym.DTOs;

namespace Gym.Validators
{
    public class DietPlanValidator : AbstractValidator<DietPlanDto>
    {
        public DietPlanValidator()
        {
            RuleFor(x => x.DietName)
                .NotEmpty().WithMessage("Diet Name is required.");

            RuleFor(x => x.MondayPlan).NotEmpty().WithMessage("Monday plan is required.");
            RuleFor(x => x.TuesdayPlan).NotEmpty().WithMessage("Tuesday plan is required.");
            RuleFor(x => x.WednesdayPlan).NotEmpty().WithMessage("Wednesday plan is required.");
            RuleFor(x => x.ThursdayPlan).NotEmpty().WithMessage("Thursday plan is required.");
            RuleFor(x => x.FridayPlan).NotEmpty().WithMessage("Friday plan is required.");
            RuleFor(x => x.SaturdayPlan).NotEmpty().WithMessage("Saturday plan is required.");
            RuleFor(x => x.SundayPlan).NotEmpty().WithMessage("Sunday plan is required.");
        }
    }
}
