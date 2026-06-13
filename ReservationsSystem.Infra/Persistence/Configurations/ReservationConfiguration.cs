using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Infra.Persistence.Configurations
{
	public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
	{
		public void Configure(EntityTypeBuilder<Reservation> entity)
		{
			entity.HasKey(r => r.Id);

			entity.Property(r => r.StartTime)
				.IsRequired();

			entity.Property(r => r.EndTime)
				.IsRequired();

			entity.Property(r => r.Status)
				.IsRequired();

			entity.Property(r => r.CreatedAt)
				.IsRequired();

			entity.HasOne(r => r.User)
				.WithMany(u => u.Reservations)
				.HasForeignKey(r => r.UserId);

			entity.HasOne(r => r.Facility)
				.WithMany(f => f.Reservations)
				.HasForeignKey(r => r.FacilityId);
		}
	}
}
