namespace ReservationsSystem.Application.DTOs
{
	public class UserDto
	{
		public Guid Id { get; set; }
		public string Email { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public bool IsActive { get; set; }
		public ICollection<Guid> Reservations { get; set; } = new List<Guid>();
	}
}
