using Microsoft.AspNetCore.Mvc;
using ReservationsSystem.Application.DTOs;
using ReservationsSystem.Application.Interfaces.Services;

namespace ReservationsSystem.API.Controllers
{
	/// <summary>
	/// Manages operations related to users.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUsersService _usersService;


		/// <summary>
		/// Constructor for the UsersController.
		/// </summary>
		/// <param name="usersService">The service for managing users.</param>
		public UsersController(IUsersService usersService)
		{
			_usersService = usersService;
		}

		/// <summary>
		/// Retrieves all users.
		/// </summary>
		/// <returns>Collection of users.</returns>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		// GET: api/<UsersController>
		[HttpGet]
		public async Task<ActionResult<ResponseWrapper<IEnumerable<UserDto>>>> GetAllUsers()
		{
			var users = await _usersService.GetAllUsersAsync();

			return Ok(new ResponseWrapper<IEnumerable<UserDto>>
			{
				Data = users,
				Message = "Users retrieved successfully",
				Success = true
			});
		}

		/// <summary>
		/// Retrieves a specific reservation by its ID.
		/// </summary>
		/// <param name="id">The ID of the user to retrieve.</param>
		/// <returns>The requested user.</returns>
		// GET api/<UsersController>/5
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ResponseWrapper<UserDto>>> GetUserById(Guid id)
		{
			var user = await _usersService.GetUserByIdAsync(id);

			return Ok(new ResponseWrapper<UserDto>
			{
				Data = user,
				Message = "User retrieved successfully",
				Success = true
			});
		}

		/// <summary>
		/// Creates a new user.
		/// </summary>
		/// <param name="createUserDto">The user data to create.</param>
		/// <returns>The created user.</returns>
		// POST api/<UsersController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ResponseWrapper<UserDto>>> CreateUser([FromBody] CreateUserDto createUserDto)
		{
			var createdUser = await _usersService.CreateAsync(createUserDto);

			return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, new ResponseWrapper<UserDto>
			{
				Data = createdUser,
				Message = "User created successfully",
				Success = true
			});
		}

		/// <summary>
		/// Updates an existing user.
		/// </summary>
		/// <param name="id">The ID of the user to update.</param>
		/// <param name="userDto">The user data to update.</param>
		/// <returns>The updated user.</returns>
		// PUT api/<UsersController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ResponseWrapper<UserDto>>> UpdateUser(Guid id, [FromBody] UserDto userDto)
		{
			var updatedUser = await _usersService.UpdateUserAsync(userDto);

			return Ok(new ResponseWrapper<UserDto>
			{
				Data = updatedUser,
				Message = "User updated successfully",
				Success = true
			});
		}

		/// <summary>
		/// Deletes a user by its ID.
		/// </summary>
		/// <param name="id">The ID of the user to delete.</param>
		/// <returns>No content if the deletion is successful.</returns>
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
