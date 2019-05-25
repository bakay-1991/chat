using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
	public class UserRepository : EfRepository<User>, IUserRepository
	{
		public UserRepository(ChatContext dbContext) : base(dbContext)
		{
		}

		public Task<User> GetAsync(string email, string password = "")
		{
			IQueryable<User> query = _dbContext.Users.Where(x => x.Email == email);
			if (!string.IsNullOrEmpty(password))
			{
				query = query.Where(x => x.Password == password);
			}

			return query.FirstOrDefaultAsync();
		}
	}
}
