using ReservationsSystem.Application.DTOs;

namespace ReservationsSystem.Application.Interfaces.Services
{
	public interface IReservationsService
	{
		public Task<ReservationDto> CreateAsync(CreateReservationDto createReservationDto);
		public Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
		public Task<ReservationDto> GetReservationByIdAsync(Guid id);
	}
}
