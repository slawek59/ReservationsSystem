using Microsoft.EntityFrameworkCore;
using ReservationsSystem.Application.Interfaces.Repositories;
using ReservationsSystem.Domain.Entities;
using ReservationsSystem.Infra.Persistence;

namespace ReservationsSystem.Infra.Repositories
{
	public class ReservationRepository : IReservationRepository
	{
		private readonly AppDbContext _context;

		public ReservationRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Reservation newReservation)
		{
			await _context.Reservations.AddAsync(newReservation);
		}

		public async Task<IEnumerable<Reservation>> GetAllAsync()
		{
			return await _context.Reservations
				.AsNoTracking()
				.Include(r => r.User)
				.Include(r => r.Facility)
				.ToListAsync();
		}

		public async Task<Reservation?> GetByIdAsync(Guid id)
		{
			return await _context.Reservations
				.Include(r => r.User)
				.Include(r => r.Facility)
				.FirstOrDefaultAsync(r => r.Id == id);
		}

		public async Task<int> GetReservationsCountForUserAsync(Guid userId)
		{
			return await _context.Reservations
				.CountAsync(r => r.UserId == userId &&
				r.Status != ReservationStatus.Cancelled);
		}

		public async Task<bool> HasOverlappingReservationAsync(Guid facilityId, DateTime startTime, DateTime endTime)
		{
			return await _context.Reservations.AnyAsync(r =>
			
				r.FacilityId == facilityId &&
				r.Status != ReservationStatus.Cancelled &&
				r.StartTime < endTime &&
				r.EndTime > startTime
			);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}