using Microsoft.EntityFrameworkCore;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repository
{
	public interface IRepository<T> where T : class
	{
		void Add(T entity);
		void AddRange(IEnumerable<T> list);
		void Update(T entity);
		void QuickUpdate(T entity, T TEntity);
		void Delete(T entity);
		void Delete(int id);
		void DeleteRange(IEnumerable<T> list);
		void Attach(T Entity);
		void ExecQuery(string query);
		void ChangeState(T entity, EntityState state);

		T FindById(int id, bool lazyLoading = false);
		Task<T> FindByIdAsync(int id, bool lazyLoading = false);
		T FindById(string id, bool lazyLoading = false);
		Task<T> FindByIdAsync(string id, bool lazyLoading = false);
		T GetSingle(Expression<Func<T, bool>> expression, bool lazyLoading = false);
		Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool lazyLoading = false);
		IQueryable<T> QueryAll(Expression<Func<T, bool>> expression);
		IQueryable<T> Query();
		IQueryable<T> Query(Expression<Func<T, bool>> expression);
		IQueryable<T> QueryNoTracking();
		IQueryable<T> QueryNoTracking(Expression<Func<T, bool>> expression);
		IQueryable<T> Include(Expression<Func<T, object>> expression);
		IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
		IList<T> SqlQuery(string query);
		IList<dynamic> GetQueryResult(string query);
		DataTable DataTableQuery(string query);
		int ExecQuery(string query, params object[] parameters);
	}
}
