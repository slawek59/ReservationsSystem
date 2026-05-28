using ReservationsSystem.Application.DTOs;

namespace ReservationsSystem.Application.Interfaces.Services
{
	public interface IUsersService
	{
		public Task<UserDto> CreateAsync(CreateUserDto createUserDto);
		public Task<IEnumerable<UserDto>> GetAllUsersAsync();
		public Task<UserDto> GetUserByIdAsync(Guid id);
	}
}
