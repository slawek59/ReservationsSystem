using ReservationsSystem.Application.DTOs;

namespace ReservationsSystem.Application.Interfaces.Services
{
	public interface IFileService
	{
		Task<FileResponseDto> GetReservationFileAsync();
	}
}
