using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
	public class UserService
	{
		private readonly IRepository<User> _userRepository;

		public UserService(IRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task CreateUserAsync(User user)
		{
			Guard.Against.Null(user, nameof(user));
			await _userRepository.AddAsync(user);
		}
	}
}
