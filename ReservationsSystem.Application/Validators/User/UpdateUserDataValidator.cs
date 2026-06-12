using FluentValidation;
using ReservationsSystem.Application.DTOs;

namespace ReservationsSystem.Application.Validators.User
{
	public class UpdateUserDataValidator : AbstractValidator<UserDto>
	{
		public UpdateUserDataValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty()
				.WithMessage("Id is required.");

			RuleFor(x => x.Email)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.WithMessage("Email address is required.")
				.EmailAddress()
				.WithMessage("Email address must be a valid email format.");

			RuleFor(x => x.Phone)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.WithMessage("Phone number is required.");
		}
	}
}
