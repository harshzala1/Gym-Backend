using FluentValidation;
using Gym.DTOs;

namespace Gym.Validators
{
    public class AttendanceValidator : AbstractValidator<CreateAttendanceDto>
    {
        public AttendanceValidator()
        {
            RuleFor(x => x.MemberID)
                .GreaterThan(0).WithMessage("Valid Member ID is required.");

            RuleFor(x => x.CheckInTime)
                .NotEmpty().WithMessage("CheckIn Time is required.");

            RuleFor(x => x.RecordedBy)
                .NotEmpty().WithMessage("Recorded By is required.");
        }
    }
}
