using FluentValidation;
using ReservationsSystem.Application.DTOs;

namespace ReservationsSystem.Application.Validators.Reservation
{
	public class UpdateReservationDataValidator : AbstractValidator<ReservationDto>
	{
		public UpdateReservationDataValidator()
		{
			RuleFor(x => x.Id)
					.NotEmpty()
					.WithMessage("Reservation ID is required.");

			RuleFor(x => x.Status)
					.IsInEnum()
					.WithMessage("Invalid reservation status.");
		}
	}
}
