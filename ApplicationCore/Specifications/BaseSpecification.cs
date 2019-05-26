using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications
{
	public class BaseSpecification<T> : ISpecification<T>
	{
		public Expression<Func<T, bool>> Criteria { get; private set; }
		public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; private set; }
		public Expression<Func<T, object>> OrderByDescending { get; private set; }

		public int Take { get; private set; }
		public int Skip { get; private set; }
		public bool IsPagingEnabled { get; private set; } = false;

		public virtual void AddCriteria(Expression<Func<T, bool>> criteria)
		{
			Criteria = criteria;
		}

		public virtual void AddInclude(Expression<Func<T, object>> includeExpression)
		{
			Includes.Add(includeExpression);
		}

		public virtual void ApplyPaging(int skip, int take)
		{
			Skip = skip;
			Take = take;
			IsPagingEnabled = true;
		}

		public virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
		{
			OrderBy = orderByExpression;
		}

		public virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
		{
			OrderByDescending = orderByDescendingExpression;
		}
	}
}
