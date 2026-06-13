using ReservationsSystem.Application.DTOs;
using ReservationsSystem.Application.Interfaces.Repositories;
using ReservationsSystem.Application.Interfaces.Services;
using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Application.Services
{
	public class ReservationsService : IReservationsService
	{
		private readonly IReservationRepository _reservationRepository;
		private readonly IUserRepository _userRepository;
		private readonly IFacilityRepository _facilityRepository;

		public ReservationsService(IReservationRepository reservationRepository, IUserRepository userRepository, IFacilityRepository facilityRepository)
		{
			_reservationRepository = reservationRepository;
			_userRepository = userRepository;
			_facilityRepository = facilityRepository;
		}

		public async Task<ReservationDto> CreateAsync(CreateReservationDto createReservationDto)
		{
			//_logger.LogInformation("Creating new reservation.);

			var user = await _userRepository.GetByIdAsync(createReservationDto.UserId);

			if (user == null)
			{
				//_logger.LogWarning("No user found with ID: {UserId}", id);
				//throw new NotFoundException($"No user found with ID: {id}");
			}

			var facility = await _facilityRepository.GetByIdAsync(createReservationDto.FacilityId);

			if (facility == null)
			{
				//_logger.LogWarning("No facility found with ID: {FacilityId}", id);
				//throw new NotFoundException($"No facility found with ID: {id}");
			}

			var newReservation = new Reservation
			{
				Id = Guid.NewGuid(),
				UserId = createReservationDto.UserId,
				User = user,
				FacilityId = createReservationDto.FacilityId,
				Facility = facility,
				StartTime = createReservationDto.StartTime,
				EndTime = createReservationDto.EndTime,
				Status = ReservationStatus.Pending,
				CreatedAt = DateTime.UtcNow,
			};

			await _reservationRepository.AddAsync(newReservation);

			await _reservationRepository.SaveChangesAsync();

			//_logger.LogInformation("Reservation created successfully. User ID: {UserId}", newReservation.UserId);

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

		public async Task DeleteReservationAsync(Guid id)
		{
			//_logger.LogInformation("Deleting reservation with ID: {ReservationId}", id);

			var reservationToDelete = await GetExistingReservation(id);

			reservationToDelete.Status = ReservationStatus.Cancelled;
			await _reservationRepository.SaveChangesAsync();

		}

		public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
		{
			//_logger.LogInformation("Retrieving all reservations.");

			var reservations = await _reservationRepository.GetAllAsync();

			return reservations.Select(r => new ReservationDto
			{
				Id = r.Id,
				UserId = r.UserId,
				FacilityId = r.FacilityId,
				StartTime = r.StartTime,
				EndTime = r.EndTime,
				Status = r.Status,
			});
		}

		public async Task<ReservationDto> GetReservationByIdAsync(Guid id)
		{
			//_logger.LogInformation("Retrieving reservation with ID: {ReservationId}", id);

			var reservation = await GetExistingReservation(id);

			return new ReservationDto
			{
				Id = reservation.Id,
				UserId = reservation.UserId,
				FacilityId = reservation.FacilityId,
				StartTime = reservation.StartTime,
				EndTime = reservation.EndTime,
				Status = reservation.Status,
			};
		}

		public async Task<ReservationDto> UpdateReservationAsync(ReservationDto reservationDto)
		{
			//_logger.LogInformation("Updating reservation with ID: {ReservationId}", reservationDto.Id);

			var reservationToUpdate = await GetExistingReservation(reservationDto.Id);

			reservationToUpdate.Status = reservationDto.Status;

			//_logger.LogInformation("Saving updated reservation.");
			await _reservationRepository.SaveChangesAsync();

			return new ReservationDto
			{
				Id = reservationToUpdate.Id,
				UserId = reservationToUpdate.UserId,
				FacilityId = reservationToUpdate.FacilityId,
				StartTime = reservationToUpdate.StartTime,
				EndTime = reservationToUpdate.EndTime,
				Status = reservationToUpdate.Status,
			};
		}

		private async Task<Reservation> GetExistingReservation(Guid id)
		{
			//_logger.LogInformation("Retrieving existing reservation with ID: {ReservationId}", id);

			var reservation = await _reservationRepository.GetByIdAsync(id);

			if (reservation == null)
			{
				//_logger.LogWarning("No reservation found with ID: {ReservationId}", id);
				//throw new NotFoundException($"No reservation found with ID: {id}");
			}
			return reservation;
		}
	}
}
