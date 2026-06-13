
using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Application.Interfaces.Repositories
{
	public interface IReservationRepository
	{
		Task AddAsync(Reservation newReservation);
		Task<IEnumerable<Reservation>> GetAllAsync();
		Task<Reservation?> GetByIdAsync(Guid id);
		Task<int> GetReservationsCountForUserAsync(Guid userId);
		Task<bool> HasOverlappingReservationAsync(
			Guid facilityId, 
			DateTime startTime, 
			DateTime endTime);
		Task SaveChangesAsync();
	}
}
