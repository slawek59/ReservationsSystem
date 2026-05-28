using ReservationsSystem.Application.DTOs;

namespace ReservationsSystem.Application.Interfaces.Services
{
	public interface IFacilitiesService
	{
		public Task<FacilityDto> CreateAsync(CreateFacilityDto createFacilityDto);
		public Task<List<FacilityDto>> GetAllFacilitiesAsync();
		public Task<FacilityDto> GetFacilityByIdAsync(Guid id);
	}
}