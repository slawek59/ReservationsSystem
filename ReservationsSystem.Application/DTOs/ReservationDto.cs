using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Application.DTOs
{
	public class ReservationDto
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; } = null!;
		public Guid FacilityId { get; set; }
		public Facility Facility { get; set; } = null!;
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public ReservationStatus Status { get; set; }
	}
}
