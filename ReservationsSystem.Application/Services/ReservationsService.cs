using ReservationsSystem.Application.DTOs;
using ReservationsSystem.Application.Interfaces.Repositories;
using ReservationsSystem.Application.Interfaces.Services;
using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Application.Services
{
	public class ReservationsService : IReservationsService
	{
		private readonly IInMemoryDataStore _dataStore;

		public ReservationsService(IInMemoryDataStore dataStore)
		{
			_dataStore = dataStore;
		}

		public async Task<ReservationDto> CreateAsync(CreateReservationDto createReservationDto)
		{
			var newReservation = new Reservation
			{
				Id = Guid.NewGuid(),
				UserId = createReservationDto.UserId,
				User = _dataStore.Users.FirstOrDefault(u => u.Id == createReservationDto.UserId),
				FacilityId = createReservationDto.FacilityId,
				Facility = _dataStore.Facilities.FirstOrDefault(f => f.Id == createReservationDto.FacilityId),
				StartTime = createReservationDto.StartTime,
				EndTime = createReservationDto.EndTime,
				Status = createReservationDto.Status,
				CreatedAt = DateTime.Now,
			};

			_dataStore.Reservations.Add(newReservation);

			return new ReservationDto
			{
				Id = newReservation.Id,
				UserId = newReservation.UserId,
				FacilityId = newReservation.FacilityId,
				StartTime = newReservation.StartTime,
				EndTime = newReservation.EndTime,
				Status = newReservation.Status,
			};
		}

		public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
		{
			var reservations = _dataStore.Reservations.ToList();

			return reservations.Select(r => new ReservationDto
			{
				Id = r.Id,
				UserId = r.UserId,
				FacilityId= r.FacilityId,
				StartTime = r.StartTime,
				EndTime = r.EndTime,
				Status = r.Status,
			});
		}

		public async Task<ReservationDto> GetReservationByIdAsync(Guid id)
		{
			var reservation = _dataStore.Reservations.FirstOrDefault(r => r.Id == id);

			return new ReservationDto
			{
				Id = reservation.Id,
				UserId= reservation.UserId,
				FacilityId = reservation.FacilityId,
				StartTime = reservation.StartTime,
				EndTime = reservation.EndTime,
				Status = reservation.Status,
			};
		}
	}
}
