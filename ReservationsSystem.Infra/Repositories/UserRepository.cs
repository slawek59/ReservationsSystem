using Microsoft.EntityFrameworkCore;
using ReservationsSystem.Application.Interfaces.Repositories;
using ReservationsSystem.Domain.Entities;
using ReservationsSystem.Infra.Persistence;

namespace ReservationsSystem.Infra.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(User newUser)
		{
			await _context.Users.AddAsync(newUser);
		}

		public async Task<IEnumerable<User>> GetAllAsync()
		{
			return await _context.Users
				.AsNoTracking()
				.Include(u => u.Reservations)
				.ToListAsync();
		}

		public async Task<User?> GetByIdAsync(Guid id)
		{
			return await _context.Users
				.Include(u => u.Reservations)
				.FirstOrDefaultAsync(u => u.Id == id);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}