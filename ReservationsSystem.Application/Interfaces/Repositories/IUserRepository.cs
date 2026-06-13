
using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Application.Interfaces.Repositories
{
	public interface IUserRepository
	{
		Task AddAsync(User newUser);
		Task<IEnumerable<User>> GetAllAsync();
		Task<User?> GetByIdAsync(Guid id);
		Task SaveChangesAsync();
	}
}
