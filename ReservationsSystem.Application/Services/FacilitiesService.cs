using ReservationsSystem.Application.DTOs;
using ReservationsSystem.Application.Interfaces.Repositories;
using ReservationsSystem.Application.Interfaces.Services;
using ReservationsSystem.Domain.Entities;
using System.Xml.Linq;

namespace ReservationsSystem.Application.Services
{
	public class FacilitiesService : IFacilitiesService
	{
		private readonly IInMemoryDataStore _dataStore;

		public FacilitiesService(IInMemoryDataStore dataStore)
		{
			_dataStore = dataStore;
		}

		public async Task<FacilityDto> CreateAsync(CreateFacilityDto createFacilityDto)
		{
			var newFacility = new Facility
			{
				Id = Guid.NewGuid(),
				Name = createFacilityDto.Name,
				Type = createFacilityDto.Type,
				Location = createFacilityDto.Location,
				Capacity = createFacilityDto.Capacity,
				IsActive = createFacilityDto.IsActive,
				CreatedAt = DateTime.UtcNow,
			}
			;

			_dataStore.Facilities.Add(newFacility);

			return new FacilityDto
			{
				Id = newFacility.Id,
				Name = newFacility.Name,
				Type = newFacility.Type,
				Location = newFacility.Location,
				Capacity = newFacility.Capacity,
				IsActive = newFacility.IsActive,
			};
		}

		public async Task<List<FacilityDto>> GetAllFacilitiesAsync()
		{
			var facilities = _dataStore.Facilities.ToList();

			return facilities.Select(
				f => new FacilityDto
				{
					Id = f.Id,
					Name = f.Name,
					Type = f.Type,
					Location = f.Location,
					Capacity = f.Capacity,
					IsActive = f.IsActive,
					Reservations = f.Reservations.Select(r => r.Id).ToList(),
				}).ToList();
		}

		public async Task<FacilityDto> GetFacilityByIdAsync(Guid id)
		{
			var facility = _dataStore.Facilities.FirstOrDefault(f => f.Id == id);

			return new FacilityDto
			{
				Id = facility.Id,
				Name = facility.Name,
				Type = facility.Type,
				Location = facility.Location,
				Capacity = facility.Capacity,
				IsActive = facility.IsActive,
				Reservations = facility.Reservations.Select(r => r.Id).ToList(),
			};
		}
	}
}
