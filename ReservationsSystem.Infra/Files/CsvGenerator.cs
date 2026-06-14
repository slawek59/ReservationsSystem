using ReservationsSystem.Application.Interfaces.Services;
using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Infra.Files
{
	public class CsvGenerator : ICsvGenerator
	{
		public async Task<byte[]> GenerateReservationsCsvContent(IEnumerable<Reservation> reservations)
		{
			using var stream = new MemoryStream();
			using var writer = new StreamWriter(stream);

			await writer.WriteLineAsync("Id,UserId,FacilityId,StartTime,EndTime,Status");

			foreach (var reservation in reservations)
			{
				var line = $"{reservation.Id},{reservation.UserId},{reservation.FacilityId},{reservation.StartTime:O},{reservation.EndTime:O},{reservation.Status}";
				await writer.WriteLineAsync(line);
			}

			await writer.FlushAsync();
			return stream.ToArray();
		}	
	}
}
