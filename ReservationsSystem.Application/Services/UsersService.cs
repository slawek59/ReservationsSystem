using ReservationsSystem.Application.DTOs;
using ReservationsSystem.Application.Interfaces.Repositories;
using ReservationsSystem.Application.Interfaces.Services;
using ReservationsSystem.Domain.Entities;

namespace ReservationsSystem.Application.Services
{
	public class UsersService : IUsersService
	{
		private readonly IInMemoryDataStore _dataStore;

		public UsersService(IInMemoryDataStore dataStore)
		{
			_dataStore = dataStore;
		}

		public async Task<UserDto> CreateAsync(CreateUserDto createUserDto)
		{
			var newUser = new User
			{
				Id = Guid.NewGuid(),
				FirstName = createUserDto.FirstName,
				LastName = createUserDto.LastName,
				Email = createUserDto.Email,
				Phone = createUserDto.Phone,
				CreatedAt = DateTime.UtcNow,
			};

			_dataStore.Users.Add(newUser);

			return new UserDto
			{
				Id = newUser.Id,
				Email = newUser.Email,
				Phone = newUser.Phone
			};
		}

		public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
		{
			var users = _dataStore.Users;

			return users.Select(
				u => new UserDto
				{
					Id = u.Id,
					Email = u.Email,
					Phone = u.Phone,
					Reservations = u.Reservations.Select(r => r.Id).ToList(),
				}).ToList();
		}

		public async Task<UserDto> GetUserByIdAsync(Guid id)
		{
			var user = _dataStore.Users.FirstOrDefault(u => u.Id == id);

			return new UserDto
			{
				Id = user.Id,
				Email = user.Email,
				Phone = user.Phone,
				Reservations = user.Reservations.Select(r => r.Id).ToList(),
			};
		}
	}
}
