using FluentValidation;
using ReservationsSystem.Application.DTOs;

namespace ReservationsSystem.Application.Validators.User
{
	public class CreateUserDataValidator : AbstractValidator<CreateUserDto>
	{
		public CreateUserDataValidator()
		{
			RuleFor(x => x.FirstName)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.WithMessage("First name is required.")
				.Must(x => !string.IsNullOrWhiteSpace(x))
				.WithMessage("First name cannot contain only whitespace.")
				.Length(2, 100)
				.WithMessage("First name must be between 2 and 100 characters long.");

			RuleFor(x => x.LastName)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.WithMessage("Last name is required.")
				.Must(x => !string.IsNullOrWhiteSpace(x))
				.WithMessage("Last name cannot contain only whitespace.")
				.Length(2, 100)
				.WithMessage("Last name must be between 2 and 100 characters long.");

			RuleFor(x => x.Email)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.WithMessage("Email address is required.")
				.Must(x => !string.IsNullOrWhiteSpace(x))
				.WithMessage("Email address cannot contain only whitespace.")
				.EmailAddress()
				.WithMessage("Email address must be a valid email format.");

			RuleFor(x => x.Phone)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.WithMessage("Phone number is required.")
				.Must(x => !string.IsNullOrWhiteSpace(x))
				.WithMessage("Phone number cannot contain only whitespace.");
		}
	}
}
