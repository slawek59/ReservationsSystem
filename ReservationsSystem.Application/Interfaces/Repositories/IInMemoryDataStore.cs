using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Application.Interfaces.Repositories
{
	public interface IInMemoryDataStore
	{
		List<Facility> Facilities { get; set; }
		List<Reservation> Reservations { get; set; }
		List<User> Users { get; set; }

	}
}
