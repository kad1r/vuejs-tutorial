using Data.Repository;
using Model.Models;
using ServicePattern.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServicePattern.Service
{
	public abstract class Service<T> : IService<T> where T : class
	{
		private readonly IRepository<T> _repository;

		public Service(IRepository<T> repository)
		{
			_repository = repository;
		}

		public virtual void Add(T entity) => _repository.Add(entity);

		public void AddRange(IQueryable<T> entities) => _repository.AddRange(entities);

		public void Delete(int id) => _repository.Delete(id);

		public void DeleteRange(IQueryable<T> entities) => _repository.DeleteRange(entities);
		
		public T FindById(int id) => _repository.FindById(id);

		public async Task<T> FindByIdAsync(int id) => await _repository.FindByIdAsync(id);

		public IQueryable<T> Query(Expression<Func<T, bool>> exp = null) => exp != null ? _repository.Query(exp) : _repository.Query();

		public IQueryable<T> QueryNoTracking(Expression<Func<T, bool>> exp = null) => exp != null ? _repository.QueryNoTracking(exp) : _repository.QueryNoTracking();

		public void Update(T original, T updated) => _repository.QuickUpdate(original, updated);
	}
}
