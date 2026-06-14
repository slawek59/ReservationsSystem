using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Application.Interfaces.Services
{
	public interface ICsvGenerator
	{
		Task<byte[]> GenerateReservationsCsvContent(IEnumerable<Reservation> reservations);
	}
}
