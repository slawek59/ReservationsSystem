namespace ReservationsSystem.Application.DTOs
{
	public class FileResponseDto
	{
		public byte[] Content { get; set; } = null!;
		public string FileName { get; set; } = null!;
	}
}
