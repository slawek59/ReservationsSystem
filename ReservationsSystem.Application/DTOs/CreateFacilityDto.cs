using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Application.DTOs
{
	public class CreateFacilityDto
	{
		public string Name { get; set; } = string.Empty;
		public FacilityType Type { get; set; }
		public string Location { get; set; } = string.Empty;
		public int Capacity { get; set; }
		public bool IsActive { get; set; }
	}
}
