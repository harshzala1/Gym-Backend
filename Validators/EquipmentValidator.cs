using FluentValidation;
using Gym.DTOs;

namespace Gym.Validators
{
    public class EquipmentValidator : AbstractValidator<CreateEquipmentDto>
    {
        public EquipmentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Equipment Name is required.");

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0).WithMessage("Cost cannot be negative.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.");
            
             // PurchaseDate is nullable, so typically we check if it has value before applying other rules if strictly needed,
             // or just let it be if it's optional.
             RuleFor(x => x.PurchaseDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Purchase Date cannot be in the future.")
                .When(x => x.PurchaseDate.HasValue);
        }
    }
}
