using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Services
{
	public interface IBaseService<T> where T : class
	{
		void Add(T entity);
		void AddRange(IEnumerable<T> entities);
		void Update(T entity);
		void Delete(int id);
		void DeleteRange(IEnumerable<int> ids);
		T FindById(int id);
		IQueryable<T> Query(Expression<Func<T, bool>> exp = null);
		IQueryable<T> QueryNoTracking(Expression<Func<T, bool>> exp = null);
		/*
		 * 
		void Insert(T entity);

		void InsertRange(IEnumerable<T> entities);

		T FindById(int id);

		Task<T> FindByIdAsync(int id);

		void Update(T entity);

		void Delete(T entity);

		void Delete(IList<int> list);

		IQueryable<T> Query();

		IQueryable<T> Query(Expression<Func<T, bool>> exp);

		IQueryable<T> QueryNoTracking();

		IQueryable<T> QueryNoTracking(Expression<Func<T, bool>> exp);
		
		 */
	}
}
