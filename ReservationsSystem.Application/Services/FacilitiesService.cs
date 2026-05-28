using ReservationsSystem.Application.DTOs;
using ReservationsSystem.Application.Interfaces.Services;

namespace ReservationsSystem.Application.Services
{
	public class FacilitiesService : IFacilitiesService
	{
		public FacilitiesService()
		{
			
		}

		public Task<FacilityDto> CreateAsync(CreateFacilityDto createFacilityDto)
		{
			throw new NotImplementedException();
		}

		public Task<List<FacilityDto>> GetAllFacilitiesAsync()
		{
			throw new NotImplementedException();
		}

		public Task<FacilityDto> GetFacilityByIdAsync(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
