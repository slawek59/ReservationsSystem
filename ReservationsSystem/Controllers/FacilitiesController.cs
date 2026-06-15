using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using ReservationsSystem.Application.DTOs;
using ReservationsSystem.Application.Interfaces.Services;

namespace ReservationsSystem.API.Controllers
{
	/// <summary>
	/// Manages operations related to facilities.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class FacilitiesController : ControllerBase
	{
		private readonly IFacilitiesService _facilitiesService;

		/// <summary>
		/// Constructor for the FacilitiesController.
		/// </summary>
		/// <param name="facilitiesService">The service for managing facilities.</param>
		public FacilitiesController(IFacilitiesService facilitiesService)
		{
			_facilitiesService = facilitiesService;
		}

		/// <summary>
		/// Retrieves all facilities.
		/// </summary>
		/// <returns>Collection of facilities.</returns>
		// GET: api/<FacilitiesController>
		[HttpGet]
		[OutputCache(PolicyName = "Expire60")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ResponseWrapper<IEnumerable<FacilityDto>>>> GetAllFacilities()
		{
			var allFacilities = await _facilitiesService.GetAllFacilitiesAsync();

			return Ok(new ResponseWrapper<IEnumerable<FacilityDto>>
			{
				Data = allFacilities,
				Message = "Facilities retrieved successfully",
				Success = true
			});
		}

		/// <summary>
		/// Retrieves a specific facility by its ID.
		/// </summary>
		/// <param name="id">The ID of the facility to retrieve.</param>
		/// <returns>The requested facility.</returns>
		// GET api/<FacilitiesController>/5
		[HttpGet("{id}")]
		[OutputCache(PolicyName = "Expire60")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ResponseWrapper<FacilityDto>>> GetFacilityById(Guid id)
		{
			var facility = await _facilitiesService.GetFacilityByIdAsync(id);

			return Ok(new ResponseWrapper<FacilityDto>
			{
				Data = facility,
				Message = "Facility retrieved successfully",
				Success = true
			});
		}

		/// <summary>
		/// Creates a new facility.
		/// </summary>
		/// <param name="createFacilityDto">The facility data to create.</param>
		/// <returns>The created facility.</returns>
		// POST api/<FacilitiesController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ResponseWrapper<FacilityDto>>> CreateFacility([FromBody] CreateFacilityDto createFacilityDto)
		{
			var newFacility = await _facilitiesService.CreateAsync(createFacilityDto);

			return CreatedAtAction(nameof(GetFacilityById), new { id = newFacility.Id }, new ResponseWrapper<FacilityDto>
			{
				Data = newFacility,
				Message = "Facility created successfully",
				Success = true
			});
		}

		/// <summary>
		/// Updates an existing facility.
		/// </summary>
		/// <param name="id">The ID of the facility to update.</param>
		/// <param name="facilityDto">The facility data to update.</param>
		/// <returns>The updated facility.</returns>
		// PUT api/<FacilitiesController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ResponseWrapper<FacilityDto>>> UpdateFacility(Guid id, [FromBody] FacilityDto facilityDto)
		{
			var updatedFacility = await _facilitiesService.UpdateFacilityAsync(facilityDto);

			return Ok(new ResponseWrapper<FacilityDto>
			{
				Data = updatedFacility,
				Message = "Facility updated successfully",
				Success = true
			});
		}

		/// <summary>
		/// Deletes a facility by its ID.
		/// </summary>
		/// <param name="id">The ID of the facility to delete.</param>
		/// <returns>No content if the deletion is successful.</returns>
		// DELETE api/<FacilitiesController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _facilitiesService.DeleteFacilityAsync(id);

			return NoContent();
		}
	}
}