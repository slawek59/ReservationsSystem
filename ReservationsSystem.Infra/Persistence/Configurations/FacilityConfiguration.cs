using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Infra.Persistence.Configurations
{
	public class FacilityConfiguration : IEntityTypeConfiguration<Facility>
	{
		public void Configure(EntityTypeBuilder<Facility> entity)
		{
			entity.HasKey(f => f.Id);

			entity.Property(f => f.Name)
				.IsRequired()
				.HasMaxLength(100);

			entity.Property(f => f.Location)
				.IsRequired()
				.HasMaxLength(200);

			entity.Property(f => f.Capacity)
				.IsRequired();

			entity.Property(f => f.Type)
				.IsRequired();

			entity.Property(f => f.CreatedAt)
				.IsRequired();

			entity.HasIndex(f => new
			{
				f.Name,
				f.Location
			}).IsUnique();
		}
	}
}
