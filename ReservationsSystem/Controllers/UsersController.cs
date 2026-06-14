using Microsoft.AspNetCore.Mvc;
using ReservationsSystem.Application.DTOs;
using ReservationsSystem.Application.Interfaces.Services;

namespace ReservationsSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUsersService _usersService;

		public UsersController(IUsersService usersService)
		{
			_usersService = usersService;
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		// GET: api/<UsersController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
		{
			var users = await _usersService.GetAllUsersAsync();

			return Ok(new ResponseWrapper<IEnumerable<UserDto>>
			{
				Data = users,
				Message = "Users retrieved successfully",
				Success = true
			});
		}

		// GET api/<UsersController>/5
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<UserDto>> GetUserById(Guid id)
		{
			var user = await _usersService.GetUserByIdAsync(id);

			return Ok(new ResponseWrapper<UserDto>
			{
				Data = user,
				Message = "User retrieved successfully",
				Success = true
			});
		}

		// POST api/<UsersController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto createUserDto)
		{
			var createdUser = await _usersService.CreateAsync(createUserDto);

			return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, new ResponseWrapper<UserDto>
			{
				Data = createdUser,
				Message = "User created successfully",
				Success = true
			});
		}

		// PUT api/<UsersController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<UserDto>> UpdateUser(Guid id, [FromBody] UserDto userDto)
		{
			var updatedUser = await _usersService.UpdateUserAsync(userDto);

			return Ok(new ResponseWrapper<UserDto>
			{
				Data = updatedUser,
				Message = "User updated successfully",
				Success = true
			});
		}

		// DELETE api/<UsersController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> DeleteUser(Guid id)
		{
			await _usersService.DeleteUserAsync(id);

			return NoContent();
		}
	}
}
