using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User> GetAsync(string email, string password = "");
	}
}
