using FluentValidation;
using ReservationsSystem.Application.DTOs;

namespace ReservationsSystem.Application.Validators.Facility
{
	public class CreateFacilityDataValidator : AbstractValidator<CreateFacilityDto>
	{
		public CreateFacilityDataValidator()
		{
			RuleFor(x => x.Name)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.WithMessage("Name is required.")
				.Must(x =>
				{
					var trimmedLength = x.Trim().Length;
					return trimmedLength >= 2 && trimmedLength <= 100;
				})
				.WithMessage("Name must be between 2 and 100 characters long.");

			RuleFor(x => x.Type)
				.IsInEnum()
				.WithMessage("Invalid facility type.");

			RuleFor(x => x.Location)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.WithMessage("Location is required.")
				.Must(x =>
				{
					var trimmedLength = x.Trim().Length;
					return trimmedLength >= 2 && trimmedLength <= 100;
				})
				.WithMessage("Location must be between 2 and 100 characters long.");

			RuleFor(x => x.Capacity)
				.GreaterThanOrEqualTo(10)
				.WithMessage("Capacity must be at least 10.");
		}
	}
}
