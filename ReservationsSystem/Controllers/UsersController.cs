using Microsoft.AspNetCore.Mvc;
using ReservationsSystem.Application.DTOs;
using ReservationsSystem.Application.Interfaces.Services;
using ReservationsSystem.Application.Services;
using System;

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

			return Ok(users);
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

			return Ok(user);
		}

		// POST api/<UsersController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto createUserDto)
		{
			var createdUser = await _usersService.CreateAsync(createUserDto);

			return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
		}

		// PUT api/<UsersController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<UserDto>> UpdateUser(Guid id, [FromBody] UserDto userDto)
		{
			var updatedUser = _usersService.UpdateUserAsync(userDto);

			return Ok(updatedUser);
		}

		// DELETE api/<UsersController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> DeleteUser(Guid id)
		{
			_usersService.DeleteUserAsync(id);

			return NoContent();
		}
	}
}
