using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Application.Interfaces.Repositories
{
	public interface IFacilityRepository
	{
		Task AddAsync(Facility newFacility);
		Task<IEnumerable<Facility>> GetAllAsync();
		Task<Facility?> GetByIdAsync(Guid id);
		Task SaveChangesAsync();
	}
}
