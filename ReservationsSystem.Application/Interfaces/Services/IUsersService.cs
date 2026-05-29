using ReservationsSystem.Application.DTOs;

namespace ReservationsSystem.Application.Interfaces.Services
{
	public interface IUsersService
	{
		public Task<UserDto> CreateAsync(CreateUserDto createUserDto);
		public Task DeleteUserAsync(Guid id);
		public Task<IEnumerable<UserDto>> GetAllUsersAsync();
		public Task<UserDto> GetUserByIdAsync(Guid id);
		public Task<UserDto> UpdateUserAsync(UserDto userDto);
	}
}
