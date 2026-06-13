using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Infra.Persistence.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> entity)
		{
			entity.HasKey(u => u.Id);

			entity.Property(u => u.FirstName)
				.IsRequired()
				.HasMaxLength(100);

			entity.Property(u => u.LastName)
				.IsRequired()
				.HasMaxLength(100);

			entity.Property(u => u.Email)
				.IsRequired();

			entity.HasIndex(u => u.Email)
				.IsUnique();

			entity.Property(u => u.Phone)
				.IsRequired();

			entity.HasIndex(u => u.Phone)
				.IsUnique();

			entity.Property(u => u.IsActive)
				.IsRequired();

			entity.Property(u => u.CreatedAt)
				.IsRequired();
		}
	}
}
