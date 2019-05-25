using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
	public interface IRepository<T> where T : BaseEntity
	{
		Task<T> GetByIdAsync(Guid id);
		Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
		Task<IReadOnlyList<T>> ListAllAsync();
		Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
		Task<T> AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
		Task<int> CountAsync(ISpecification<T> spec);
	}
}
