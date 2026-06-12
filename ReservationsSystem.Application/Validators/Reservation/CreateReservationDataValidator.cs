using FluentValidation;
using ReservationsSystem.Application.DTOs;

namespace ReservationsSystem.Application.Validators.Reservation
{
	public class CreateReservationDataValidator : AbstractValidator<CreateReservationDto>
	{
		public CreateReservationDataValidator()
		{
			RuleFor(x => x.UserId)
					.NotEmpty()
					.WithMessage("User ID is required.");

			RuleFor(x => x.FacilityId)
					.NotEmpty()
					.WithMessage("Facility ID is required.");

			RuleFor(x => x.StartTime)
					.NotEmpty()
					.WithMessage("Start time is required.");

			RuleFor(x => x.EndTime)
					.NotEmpty()
					.WithMessage("End time is required.");

			RuleFor(x => x.StartTime)
					.LessThan(x => x.EndTime)
					.WithMessage("Start time must be before end time.");
		}
	}
}
