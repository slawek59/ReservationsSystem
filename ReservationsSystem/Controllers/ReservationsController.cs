using Microsoft.AspNetCore.Mvc;
using ReservationsSystem.Application.DTOs;
using ReservationsSystem.Application.Interfaces.Services;
using ReservationsSystem.Infra.Files;

namespace ReservationsSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReservationsController : ControllerBase
	{
		private readonly IReservationsService _reservationsService;
		private readonly IFileService _fileService;

		public ReservationsController(IReservationsService reservationsService, IFileService fileService)
		{
			_reservationsService = reservationsService;
			_fileService = fileService;
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		// GET: api/<UsersController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAllReservations()
		{
			var reservations = await _reservationsService.GetAllReservationsAsync();

			return Ok(reservations);
		}

		// GET api/<UsersController>/5
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<UserDto>> GetReservationById(Guid id)
		{
			var reservation = await _reservationsService.GetReservationByIdAsync(id);

			return Ok(reservation);
		}

		// POST api/<UsersController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<UserDto>> CreateReservation([FromBody] CreateReservationDto createReservationDto)
		{
			var createdReservation = await _reservationsService.CreateAsync(createReservationDto);

			return CreatedAtAction(nameof(GetReservationById), new { id = createdReservation.Id }, createdReservation);
		}

		// PUT api/<UsersController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ReservationDto>> UpdateReservation(Guid id, [FromBody] ReservationDto reservationDto)
		{
			var updatedReservation = await _reservationsService.UpdateReservationAsync(reservationDto);

			return Ok(updatedReservation);
		}

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
