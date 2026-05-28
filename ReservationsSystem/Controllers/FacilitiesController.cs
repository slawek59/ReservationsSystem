using Microsoft.AspNetCore.Mvc;
using ReservationsSystem.Application.DTOs;
using ReservationsSystem.Application.Interfaces.Services;

namespace ReservationsSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FacilitiesController : ControllerBase
	{
		private readonly IFacilitiesService _facilitiesService;

		public FacilitiesController(IFacilitiesService facilitiesService)
		{
			_facilitiesService = facilitiesService;
		}

		// GET: api/<FacilitiesController>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<IEnumerable<FacilityDto>>> GetAllFacilities()
		{
			var allFacilities = await _facilitiesService.GetAllFacilitiesAsync();

			return Ok(allFacilities);
		}

		// GET api/<FacilitiesController>/5
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<FacilityDto>> GetFacilityById(Guid id)
		{
			var facility = await _facilitiesService.GetFacilityByIdAsync(id);

			return Ok(facility);
		}

		// POST api/<FacilitiesController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<FacilityDto>> CreateFacility([FromBody] CreateFacilityDto createFacilityDto)
		{
			var newFacility = await _facilitiesService.CreateAsync(createFacilityDto);

			return CreatedAtAction(nameof(GetFacilityById), new { id = newFacility.Id }, newFacility);
		}

		// PUT api/<FacilitiesController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<FacilitiesController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
