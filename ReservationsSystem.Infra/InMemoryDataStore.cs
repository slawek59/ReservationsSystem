using ReservationsSystem.Application.Interfaces.Repositories;
using ReservationsSystem.Domain.Entities;
using System.Resources;

namespace ReservationsSystem.Infra
{
	public class InMemoryDataStore : IInMemoryDataStore
	{
		public List<User> Users { get; set; } = new List<User>
		{
			new User
			{
				Id = Guid.NewGuid(),
				FirstName = "firstName1",
				LastName = "lastName1",
				Email = "email1",
				Phone = "phone1",
				CreatedAt = DateTime.Now,
			},
			new User
			{
				Id = Guid.NewGuid(),
				FirstName = "firstName2",
				LastName = "lastName2",
				Email = "email2",
				Phone = "phone2",
				CreatedAt = DateTime.Now,
			}
		};
		public List<Facility> Facilities { get; set; } = new List<Facility>
		{
			new Facility
			{
				Id = Guid.NewGuid(),
				Name = "name1",
				Type = FacilityType.TenisCourt,
				Location = "location1",
				Capacity = 100,
				IsActive = true,
				CreatedAt = DateTime.Now
			},
			new Facility
			{
				Id = Guid.NewGuid(),
				Name = "name1",
				Type = FacilityType.Gym,
				Location = "location2",
				Capacity = 200,
				IsActive = false,
				CreatedAt = DateTime.Now
			}
		};
		public List<Reservation> Reservations { get; set; } = new List<Reservation>();
	}
}
