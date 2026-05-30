namespace ReservationsSystem.Domain.Entities
{
	public class Facility
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public FacilityType Type { get; set; }
		public string Location { get; set; } = string.Empty;
		public int Capacity { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedAt { get; set; }
		public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
	}
}
