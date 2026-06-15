using Microsoft.Extensions.Logging;
using ReservationsSystem.Application.DTOs;
using ReservationsSystem.Application.Exceptions;
using ReservationsSystem.Application.Interfaces.Repositories;
using ReservationsSystem.Application.Interfaces.Services;
using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Application.Services
{
	public class UsersService : IUsersService
	{
		private readonly IUserRepository _userRepository;
		private readonly ILogger<UsersService> _logger;

		public UsersService(IUserRepository userRepository, ILogger<UsersService> logger)
		{
			_userRepository = userRepository;
			_logger = logger;
		}

		public async Task<UserDto> CreateAsync(CreateUserDto createUserDto)
		{
			_logger.LogInformation("Creating new user with name: {UserName}", createUserDto.FirstName);

			var newUser = new User
			{
				Id = Guid.NewGuid(),
				FirstName = createUserDto.FirstName,
				LastName = createUserDto.LastName,
				Email = createUserDto.Email,
				Phone = createUserDto.Phone,
				IsActive = true,
				CreatedAt = DateTime.UtcNow,
			};

			await _userRepository.AddAsync(newUser);

			await _userRepository.SaveChangesAsync();

			_logger.LogInformation("User created successfully. User ID: {UserId}", newUser.Id);

			return new UserDto
			{
				Id = newUser.Id,
				Email = newUser.Email,
				Phone = newUser.Phone
			};
		}

		public async Task DeleteUserAsync(Guid id)
		{
			_logger.LogInformation("Deleting user with ID: {UserId}", id);

			var userToDelete = await GetExistingUser(id);

			userToDelete.IsActive = false;
			await _userRepository.SaveChangesAsync();
		}

		public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
		{
			_logger.LogInformation("Retrieving all users.");

			var allUsers = await _userRepository.GetAllAsync();

			return allUsers.Select(
				u => new UserDto
				{
					Id = u.Id,
					Email = u.Email,
					Phone = u.Phone,
					IsActive = u.IsActive,
					Reservations = u.Reservations.Select(r => r.Id).ToList(),
				}).ToList();
		}

		public async Task<UserDto> GetUserByIdAsync(Guid id)
		{
			_logger.LogInformation("Retrieving user with ID: {UserId}", id);

			var userEntity = await GetExistingUser(id);

			return new UserDto
			{
				Id = userEntity.Id,
				Email = userEntity.Email,
				Phone = userEntity.Phone,
				IsActive = userEntity.IsActive,
				Reservations = userEntity.Reservations.Select(r => r.Id).ToList(),
			};
		}

		public async Task<UserDto> UpdateUserAsync(UserDto userDto)
		{
			_logger.LogInformation("Updating user with ID: {UserId}", userDto.Id);

			var userToUpdate = await GetExistingUser(userDto.Id);

			userToUpdate.Email = userDto.Email;
			userToUpdate.Phone = userDto.Phone;

			_logger.LogInformation("Saving updated user with ID: {UserId}", userDto.Id);
			await _userRepository.SaveChangesAsync();

			return new UserDto
			{
				Id = userToUpdate.Id,
				Email = userToUpdate.Email,
				Phone = userToUpdate.Phone,
				IsActive = userToUpdate.IsActive,
				Reservations = userToUpdate.Reservations.Select(r => r.Id).ToList()
			};
		}

		private async Task<User> GetExistingUser(Guid id)
		{
			var user = await _userRepository.GetByIdAsync(id);
			
			if (user == null)
			{
				_logger.LogWarning("No user found with ID: {UserId}", id);
				throw new NotFoundException($"No user found with ID: {id}");
			}
			return user;
		}
	}
}
