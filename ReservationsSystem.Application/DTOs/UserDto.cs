using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Application.DTOs
{
	public class UserDto
	{
		public Guid Id { get; set; }
		public string Email { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
	}
}
