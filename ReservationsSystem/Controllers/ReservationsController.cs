using Microsoft.AspNetCore.Mvc;
using ReservationsSystem.Application.DTOs;
using ReservationsSystem.Application.Interfaces.Services;

namespace ReservationsSystem.API.Controllers
{
	/// <summary>
	/// Manages operations related to reservations.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class ReservationsController : ControllerBase
	{
		private readonly IReservationsService _reservationsService;
		private readonly IFileService _fileService;

		/// <summary>
		/// Constructor for the ReservationsController.
		/// </summary>
		/// <param name="reservationsService">The service for managing reservations.</param>
		/// <param name="fileService">The service for managing file operations.</param>
		public ReservationsController(IReservationsService reservationsService, IFileService fileService)
		{
			_reservationsService = reservationsService;
			_fileService = fileService;
		}

		/// <summary>
		/// Retrieves all reservations.
		/// </summary>
		/// <returns>Collection of reservations.</returns>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		// GET: api/<UsersController>
		[HttpGet]
		public async Task<ActionResult<ResponseWrapper<IEnumerable<ReservationDto>>>> GetAllReservations()
		{
			var reservations = await _reservationsService.GetAllReservationsAsync();

			return Ok(new ResponseWrapper<IEnumerable<ReservationDto>>
			{
				Data = reservations,
				Message = "Reservations retrieved successfully",
				Success = true
			});
		}

		/// <summary>
		/// Retrieves a specific reservation by its ID.
		/// </summary>
		/// <param name="id">The ID of the reservation to retrieve.</param>
		/// <returns>The requested reservation.</returns>
		// GET api/<UsersController>/5
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ResponseWrapper<ReservationDto>>> GetReservationById(Guid id)
		{
			var reservation = await _reservationsService.GetReservationByIdAsync(id);

			return Ok(new ResponseWrapper<ReservationDto>
			{
				Data = reservation,
				Message = "Reservation retrieved successfully",
				Success = true
			});
		}

		/// <summary>
		/// Creates a new reservation.
		/// </summary>
		/// <param name="createReservationDto">The reservation data to create.</param>
		/// <returns>The created reservation.</returns>
		// POST api/<UsersController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ResponseWrapper<ReservationDto>>> CreateReservation([FromBody] CreateReservationDto createReservationDto)
		{
			var createdReservation = await _reservationsService.CreateAsync(createReservationDto);

			return CreatedAtAction(nameof(GetReservationById), new { id = createdReservation.Id }, new ResponseWrapper<ReservationDto>
			{
				Data = createdReservation,
				Message = "Reservation created successfully",
				Success = true
			});
		}

		/// <summary>
		/// Updates an existing reservation.
		/// </summary>
		/// <param name="id">The ID of the reservation to update.</param>
		/// <param name="reservationDto">The reservation data to update.</param>
		/// <returns>The updated reservation.</returns>
		// PUT api/<UsersController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ResponseWrapper<ReservationDto>>> UpdateReservation(Guid id, [FromBody] ReservationDto reservationDto)
		{
			var updatedReservation = await _reservationsService.UpdateReservationAsync(reservationDto);

			return Ok(new ResponseWrapper<ReservationDto>
			{
				Data = updatedReservation,
				Message = "Reservation updated successfully",
				Success = true
			});
		}

		/// <summary>
		/// Deletes a reservation by its ID.
		/// </summary>
		/// <param name="id">The ID of the reservation to delete.</param>
		/// <returns>No content if the deletion is successful.</returns>
		// DELETE api/<UsersController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> DeleteReservation(Guid id)
		{
			await _reservationsService.DeleteReservationAsync(id);

			return NoContent();
		}

		/// <summary>
		/// Exports reservation data to a CSV file.
		/// </summary>
		/// <returns>A CSV file containing reservation data.</returns>
		[HttpGet("export")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> ExportReservations()
		{
			var file = await _fileService.GetReservationFileAsync();

			return File(file.Content, "text/csv", file.FileName);
		}
	}
}
