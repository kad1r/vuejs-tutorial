using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ServicePattern.Interface
{
	public interface IService<T> where T : class
	{
		void Add(T entity);

		void AddRange(IQueryable<T> entities);

		void Delete(int id);

		void DeleteRange(IQueryable<T> entities);

		T FindById(int id);

		Task<T> FindByIdAsync(int id);

		IQueryable<T> Query(Expression<Func<T, bool>> exp = null);

		IQueryable<T> QueryNoTracking(Expression<Func<T, bool>> exp = null);

		void Update(T original, T updated);
	}
}
