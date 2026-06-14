using Microsoft.EntityFrameworkCore;
using ReservationsSystem.Application.Interfaces.Repositories;
using ReservationsSystem.Domain.Entities;
using ReservationsSystem.Infra.Persistence;

namespace ReservationsSystem.Infra.Repositories
{
	public class FacilityRepository : IFacilityRepository
	{
		private readonly AppDbContext _context;
		public FacilityRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Facility newFacility)
		{
			await _context.Facilities.AddAsync(newFacility);
		}

		public async Task<bool> ExistsByNameAndLocationAsync(string name, string location)
		{
			return await _context.Facilities.AnyAsync(f =>
				f.Name == name &&
				f.Location == location
			);
		}

		public async Task<IEnumerable<Facility>> GetAllAsync()
		{
			return await _context.Facilities
				.AsNoTracking()
				.Include(f => f.Reservations)
				.ToListAsync();
		}

		public async Task<Facility?> GetByIdAsync(Guid id)
		{
			return await _context.Facilities
				.Include(f => f.Reservations)
				.FirstOrDefaultAsync(f => f.Id == id);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}

