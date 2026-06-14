using ReservationsSystem.Application.DTOs;
using ReservationsSystem.Application.Interfaces.Repositories;
using ReservationsSystem.Application.Interfaces.Services;

namespace ReservationsSystem.Infra.Files
{
	public class FileService : IFileService
	{
		private readonly IReservationRepository _reservationRepository;
		private readonly ICsvGenerator _csvGenerator;

		public FileService(IReservationRepository reservationRepository, ICsvGenerator csvGenerator)
		{
			_reservationRepository = reservationRepository;
			_csvGenerator = csvGenerator;
		}

		public async Task<FileResponseDto> GetReservationFileAsync()
		{
			var reservations = await _reservationRepository.GetAllAsync();

			var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

			var fileName = $"reservations_{timestamp}.csv";

			var csvContent = await _csvGenerator.GenerateReservationsCsvContent(reservations);

			return new FileResponseDto { Content = csvContent, FileName = fileName };
		}
	}
}
