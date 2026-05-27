
using ReservationsSystem.Application.DTOs;

namespace ReservationsSystem.API.Controllers
{
	public interface IFacilitiesService
	{
		public Task<FacilityDto> CreateAsync(CreateFacilityDto createFacilityDto);
		public Task<List<FacilityDto>> GetAllFacilitiesAsync();
		public Task<FacilityDto> GetFacilityByIdAsync(int id);
	}
}