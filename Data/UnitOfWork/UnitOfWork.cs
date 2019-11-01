using Data.Repository;
using Model.Models;
using System;
using System.Threading.Tasks;

namespace Data.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private bool disposed = false;
		protected readonly AppDbContext _context;

		public UnitOfWork(AppDbContext context)
		{
			_context = context ?? throw new ArgumentNullException("DbContext is null on uow!");
			LazyLoading(false);
			ChangeTracking(false);
		}

		public IRepository<T> Repository<T>() where T : class
		{
			return new Repository<T>(_context);
		}

		public int SaveChanges()
		{
			try
			{
				#region WRITE LOG

				//var changes = _context.ChangeTracker.Entries()
				//    .Select(x => new UowChangeLog
				//    {
				//        OriginalValues = x.State != EntityState.Added ? x.OriginalValues.PropertyNames.ToDictionary(c => c, c => x.OriginalValues[c]) : null,
				//        CurrentValues = x.CurrentValues.PropertyNames.ToDictionary(c => c, c => x.CurrentValues[c]),
				//        State = x.State,
				//        DateInserted = DateTime.Now
				//    })
				//    .FirstOrDefault();

				//WriteUOWLogs(changes);

				#endregion WRITE LOG

				return _context.SaveChanges();
			}
			catch (Exception ex)
			{
				// catch DbEntityValidationException errors
				_ = ex.Message;
				throw;
			}
		}

		public async Task<int> SaveChangesAsync()
		{
			try
			{
				return await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_ = ex.Message;
				throw;
			}
		}

		public void ChangeTracking(bool option)
		{
			_context.ChangeTracker.AutoDetectChangesEnabled = option;
		}

		public void LazyLoading(bool status)
		{
			_context.ChangeTracker.AutoDetectChangesEnabled = status;
			_context.ChangeTracker.LazyLoadingEnabled = status;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}

			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
