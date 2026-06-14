using ReservationsSystem.Application.DTOs;
using ReservationsSystem.Application.Interfaces.Repositories;
using ReservationsSystem.Application.Interfaces.Services;
using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Application.Services
{
	public class FacilitiesService : IFacilitiesService
	{
		private readonly IFacilityRepository _facilityRepository;

		public FacilitiesService(IFacilityRepository facilityRepository)
		{
			_facilityRepository = facilityRepository;
		}

		public async Task<FacilityDto> CreateAsync(CreateFacilityDto createFacilityDto)
		{
			//_logger.LogInformation("Creating new facility with name: {FacilityName}", createFacilityDto.Name);

			var doesFacilityAlreadyExist = await _facilityRepository.ExistsByNameAndLocationAsync(createFacilityDto.Name, createFacilityDto.Location);

			if (doesFacilityAlreadyExist)
			{
				//_logger.LogWarning();
				//throw
			}

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

			await _facilityRepository.AddAsync(newFacility);

			//_logger.LogInformation("Facility created successfully. Facility ID: {FacilityId}", newFacility.Id);

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

		public async Task DeleteFacilityAsync(Guid id)
		{
			//_logger.LogInformation("Deleting facility with ID: {FacilityId}", id);

			var facilityToDelete = await GetExistingFacility(id);

			facilityToDelete.IsActive = false;
			await _facilityRepository.SaveChangesAsync();
		}

		public async Task<List<FacilityDto>> GetAllFacilitiesAsync()
		{
			//_logger.LogInformation("Retrieving all facilities.");

			var facilities = await _facilityRepository.GetAllAsync();

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
			//_logger.LogInformation("Retrieving facility with ID: {FacilityId}", id);

			var facility = await GetExistingFacility(id);

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

		public async Task<FacilityDto> UpdateFacilityAsync(FacilityDto facilityDto)
		{
			//_logger.LogInformation("Updating user with ID: {UserId}", userDto.Id);

			var facilityToUpdate = await GetExistingFacility(facilityDto.Id);

			facilityToUpdate.Name = facilityDto.Name;
			facilityToUpdate.Type = facilityDto.Type;
			facilityToUpdate.Capacity = facilityDto.Capacity;

			//_logger.LogInformation("Saving updated facility.");
			await _facilityRepository.SaveChangesAsync();

			return new FacilityDto
			{
				Id = facilityToUpdate.Id,
				Name = facilityToUpdate.Name,
				Type = facilityToUpdate.Type,
				Location = facilityToUpdate.Location,
				Capacity = facilityToUpdate.Capacity,
				IsActive = facilityToUpdate.IsActive,
				Reservations = facilityToUpdate.Reservations.Select(r => r.Id).ToList()
			};
		}

		private async Task<Facility> GetExistingFacility(Guid id)
		{
			//_logger.LogInformation("Retrieving existing facility with ID: {FacilityId}", id);

			var facility = await _facilityRepository.GetByIdAsync(id);

			if (facility == null)
			{
				//_logger.LogWarning("No facility found with ID: {FacilityId}", id);
				//throw new NotFoundException($"No facility found with ID: {id}");
			}
			return facility;
		}
	}
}
