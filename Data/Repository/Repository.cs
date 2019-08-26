using Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Model.ViewModels;

namespace Data.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly DbContext _context;
		private readonly DbSet<T> _dbSet;
		protected Model.Models.AppDbContext DbContext;

		public Repository(Model.Models.AppDbContext context)
		{
			if (context == null)
			{
				context = new Model.Models.AppDbContext();
			}

			_context = context;
			_dbSet = _context.Set<T>();
		}

		public void Add(T entity)
		{
			_dbSet.Add(entity);
		}

		public void AddRange(IEnumerable<T> list)
		{
			_dbSet.AddRange(list);
		}

		public void Update(T entity)
		{
			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}

		public void QuickUpdate(T original, T updated)
		{
			_dbSet.Attach(original);
			_context.Entry(original).CurrentValues.SetValues(updated);
			_context.Entry(original).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			var entity = FindById(id);

			if (entity == null) return;
			else
			{
				if (entity.GetType().GetProperty("IsDeleted") != null)
				{
					T _entity = entity;
					_entity.GetType().GetProperty("IsDeleted").SetValue(_entity, true);

					this.Update(_entity);
				}
				else
				{
					Delete(entity);
				}
			}
		}

		public void Delete(T entity)
		{
			var dbEntityEntry = _context.Entry(entity);

			if (dbEntityEntry.State != EntityState.Deleted)
			{
				dbEntityEntry.State = EntityState.Deleted;
			}
			else
			{
				_dbSet.Attach(entity);
				_dbSet.Remove(entity);
			}
		}

		public void DeleteRange(IEnumerable<T> list)
		{
			_dbSet.RemoveRange(list);
		}

		public void Attach(T Entity)
		{
			_dbSet.Attach(Entity);
		}

		public void ExecQuery(string query)
		{
			this.DbContext.Database.ExecuteSqlCommand(query);
		}

		public void ChangeState(T entity, EntityState state)
		{
			_context.Entry(entity).State = state;
		}

		public T FindById(int id, bool lazyLoading = false)
		{
			_context.ChangeTracker.AutoDetectChangesEnabled = lazyLoading;
			_context.ChangeTracker.LazyLoadingEnabled = lazyLoading;

			return _dbSet.Find(id);
		}

		public async Task<T> FindByIdAsync(int id, bool lazyLoading = false)
		{
			_context.ChangeTracker.AutoDetectChangesEnabled = lazyLoading;
			_context.ChangeTracker.LazyLoadingEnabled = lazyLoading;

			return await _dbSet.FindAsync(id);
		}

		public T FindById(string id, bool lazyLoading = false)
		{
			_context.ChangeTracker.AutoDetectChangesEnabled = lazyLoading;
			_context.ChangeTracker.LazyLoadingEnabled = lazyLoading;

			return _dbSet.Find(id);
		}

		public async Task<T> FindByIdAsync(string id, bool lazyLoading = false)
		{
			_context.ChangeTracker.AutoDetectChangesEnabled = lazyLoading;
			_context.ChangeTracker.LazyLoadingEnabled = lazyLoading;

			return await _dbSet.FindAsync(id);
		}

		public T GetSingle(Expression<Func<T, bool>> expression, bool lazyLoading = false)
		{
			_context.ChangeTracker.AutoDetectChangesEnabled = true;
			_context.ChangeTracker.LazyLoadingEnabled = lazyLoading;

			if (hasFlag("<IsDeleted>"))
			{
				var parameters = expression.Parameters;
				var checkNotDeleted = Expression.Equal(Expression.PropertyOrField(parameters[0], "IsDeleted"), Expression.Constant(false));
				var originalBody = expression.Body;
				var fullExpr = Expression.And(originalBody, checkNotDeleted);
				var lambda = Expression.Lambda<Func<T, bool>>(fullExpr, parameters);

				return _dbSet.Where(lambda).SingleOrDefault();
			}
			else
			{
				return _dbSet.Where(expression).SingleOrDefault();
			}
		}

		public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool lazyLoading = false)
		{
			_context.ChangeTracker.AutoDetectChangesEnabled = true;
			_context.ChangeTracker.LazyLoadingEnabled = lazyLoading;

			if (hasFlag("<IsDeleted>"))
			{
				var parameters = expression.Parameters;
				var checkNotDeleted = Expression.Equal(Expression.PropertyOrField(parameters[0], "IsDeleted"), Expression.Constant(false));
				var originalBody = expression.Body;
				var fullExpr = Expression.And(originalBody, checkNotDeleted);
				var lambda = Expression.Lambda<Func<T, bool>>(fullExpr, parameters);

				return await _dbSet.Where(lambda).SingleOrDefaultAsync();
			}
			else
			{
				return await _dbSet.Where(expression).SingleOrDefaultAsync();
			}
		}

		public IQueryable<T> QueryAll(Expression<Func<T, bool>> expression)
		{
			_context.ChangeTracker.LazyLoadingEnabled = true;

			if (hasFlag("<IsDeleted>"))
			{
				var parameters = expression.Parameters;
				var checkNotDeleted = Expression.Equal(Expression.PropertyOrField(parameters[0], "IsDeleted"), Expression.Constant(false));
				var originalBody = expression.Body;
				var fullExpr = Expression.And(originalBody, checkNotDeleted);
				var lambda = Expression.Lambda<Func<T, bool>>(fullExpr, parameters);

				return _dbSet.Where(lambda);
			}
			else
			{
				return _dbSet.Where(expression);
			}
		}

		public IQueryable<T> Query()
		{
			_context.ChangeTracker.LazyLoadingEnabled = false;

			if (hasFlag("<IsDeleted>"))
			{
				var argument = Expression.Parameter(typeof(T));
				var left = Expression.Property(argument, "IsDeleted");
				var right = Expression.Constant(false);
				var predicate = Expression.Lambda<Func<T, bool>>(Expression.Equal(left, right), new[] { argument });

				return _dbSet.Where(predicate);
			}
			else
			{
				return _dbSet;
			}
		}

		public IQueryable<T> Query(Expression<Func<T, bool>> expression)
		{
			_context.ChangeTracker.LazyLoadingEnabled = false;

			if (hasFlag("<IsDeleted>"))
			{
				var parameters = expression.Parameters;
				var checkNotDeleted = Expression.Equal(Expression.PropertyOrField(parameters[0], "IsDeleted"), Expression.Constant(false));
				var originalBody = expression.Body;
				var fullExpr = Expression.And(originalBody, checkNotDeleted);
				var lambda = Expression.Lambda<Func<T, bool>>(fullExpr, parameters);

				return _dbSet.Where(lambda);
			}
			else
			{
				return _dbSet.Where(expression);
			}
		}

		public IQueryable<T> QueryNoTracking()
		{
			_context.ChangeTracker.AutoDetectChangesEnabled = false;
			_context.ChangeTracker.LazyLoadingEnabled = false;

			return _dbSet.AsNoTracking();
		}

		public IQueryable<T> QueryNoTracking(Expression<Func<T, bool>> expression)
		{
			_context.ChangeTracker.AutoDetectChangesEnabled = false;
			_context.ChangeTracker.LazyLoadingEnabled = false;

			if (hasFlag("<IsDeleted>"))
			{
				var parameters = expression.Parameters;
				var checkNotDeleted = Expression.Equal(Expression.PropertyOrField(parameters[0], "IsDeleted"), Expression.Constant(false));
				var originalBody = expression.Body;
				var fullExpr = Expression.And(originalBody, checkNotDeleted);
				var lambda = Expression.Lambda<Func<T, bool>>(fullExpr, parameters);

				return _dbSet.AsNoTracking().Where(lambda);
			}
			else
			{
				return _dbSet.AsNoTracking().Where(expression);
			}
		}

		public IQueryable<T> Include(Expression<Func<T, object>> expression)
		{
			_context.ChangeTracker.LazyLoadingEnabled = false;

			return _dbSet.Include(expression);
		}

		public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
		{
			_context.ChangeTracker.LazyLoadingEnabled = false;

			foreach (var include in includes)
			{
				_dbSet.Include(include);
			}

			return _dbSet;
		}

		public virtual void LazyLoading(bool lazyLoadingEnabled, bool proxyCreationEnabled, bool autoDetectChanges)
		{
			_context.ChangeTracker.LazyLoadingEnabled = lazyLoadingEnabled;
			_context.ChangeTracker.AutoDetectChangesEnabled = autoDetectChanges;
		}

		public IList<T> SqlQuery(string query)
		{
			return _dbSet.FromSql<T>(query).ToList();
		}

		public async Task<IList<T>> SqlQueryAsync(string query)
		{
			return await _dbSet.FromSql<T>(query).ToListAsync();
		}

		public IList<dynamic> GetQueryResult(string query)
		{
			return _dbSet.FromSql<dynamic>(query).ToList();
		}

		public async Task<IList<dynamic>> GetQueryResultAsync(string query)
		{
			return await _dbSet.FromSql<dynamic>(query).ToListAsync();
		}

		public DataTable DataTableQuery(string query)
		{
			var list = _dbSet.FromSql(query).ToList();

			return ConvertToDataTable(list);
		}

		public int ExecQuery(string query, params object[] parameters)
		{
			return DbContext.Database.ExecuteSqlCommand("EXEC " + query, parameters);
		}

		public bool hasFlag(string field)
		{
			var hasFlag = false;

			var genericTypeArguments = _dbSet.GetType().GenericTypeArguments;
			//entity.GetType().GetProperty("IsDeleted")

			if (genericTypeArguments.Any())
			{
				var fields = ((System.Reflection.TypeInfo)(_dbSet.GetType().GenericTypeArguments.FirstOrDefault())).DeclaredFields;

				hasFlag = fields.Any(x => x.Name.Contains(field));
			}

			return hasFlag;
		}

		public DataTable ConvertToDataTable<T>(IList<T> data)
		{
			var properties = TypeDescriptor.GetProperties(typeof(T));
			var table = new DataTable();

			foreach (PropertyDescriptor prop in properties)
			{
				table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
			}

			foreach (T item in data)
			{
				var row = table.NewRow();

				foreach (PropertyDescriptor prop in properties)
				{
					row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
				}

				table.Rows.Add(row);
			}

			return table;
		}
	}
}