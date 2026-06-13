using Microsoft.EntityFrameworkCore;
using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Infra.Persistence
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Facility> Facilities { get; set; }
		public DbSet<Reservation> Reservations { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

		}
	}
}
