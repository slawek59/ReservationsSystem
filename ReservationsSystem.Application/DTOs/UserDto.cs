using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Application.DTOs
{
	public class UserDto
	{
		public Guid Id { get; set; }
		public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
	}
}
