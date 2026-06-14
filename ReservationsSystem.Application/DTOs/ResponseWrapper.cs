namespace ReservationsSystem.Application.DTOs
{
	public class ResponseWrapper<T>
	{
		public bool Success { get; set; }
		public string? Message { get; set; }
		public T? Data { get; set; }
	}
}
