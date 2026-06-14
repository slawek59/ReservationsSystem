using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Application.Interfaces.Repositories
{
	public interface IFacilityRepository
	{
		Task AddAsync(Facility newFacility);
		Task<bool> ExistsByNameAndLocationAsync(string name, string location);
		Task<IEnumerable<Facility>> GetAllAsync();
		Task<Facility?> GetByIdAsync(Guid id);
		Task SaveChangesAsync();
	}
}
