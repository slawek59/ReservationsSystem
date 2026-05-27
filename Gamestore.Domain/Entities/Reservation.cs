namespace ReservationsSystem.Domain.Entities
{
	public class Reservation
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; } = null!;
		public Guid FacilityId { get; set; }
		public Facility Facility { get; set; } = null!;
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public ReservationStatus Status { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
