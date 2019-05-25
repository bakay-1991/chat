using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data
{
	public class SpecificationEvaluator<T> where T : BaseEntity
	{
		public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
		{
			IQueryable<T> query = inputQuery;

			if (specification.Criteria != null)
			{
				query = query.Where(specification.Criteria);
			}

			query = specification.Includes.Aggregate(query,
									(current, include) => current.Include(include));


			// Apply ordering if expressions are set
			//if (specification.OrderBy != null)
			//{
			//	query = query.OrderBy(specification.OrderBy);
			//}
			//else
			if (specification.OrderByDescending != null)
			{
				query = query.OrderByDescending(specification.OrderByDescending);
			}

			//if (specification.GroupBy != null)
			//{
			//	query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
			//}

			// Apply paging if enabled
			if (specification.IsPagingEnabled)
			{
				query = query.Skip(specification.Skip)
							 .Take(specification.Take);
			}
			return query;
		}
	}
}
