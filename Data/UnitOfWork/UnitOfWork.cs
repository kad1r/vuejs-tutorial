using Data.Repository;
using Model.ViewModels;
using System;
using System.Threading.Tasks;

namespace Data.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		protected string connectionString;
		private Model.Models.AppDbContext _context;
		private UnitOfWork uow;
		private bool disposed = false;

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

		public UnitOfWork(Model.Models.AppDbContext context)
		{
			if (context == null)
			{
				context = new Model.Models.AppDbContext();
				context = _context;
				uow = new UnitOfWork(_context);
			}

			_context = context;
		}

		public Model.Models.AppDbContext DbContext
		{
			get
			{
				if (_context == null)
				{
					_context = new Model.Models.AppDbContext();
				}

				return _context;
			}
		}

		public IRepository<T> Repository<T>() where T : class
		{
			if (uow == null)
			{
				uow = new UnitOfWork(_context);
			}

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
				var msg = ex.Message;
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
				var msg = ex.Message;
				throw;
			}
		}

		public void ChangeTracking(bool option)
		{
			DbContext.ChangeTracker.AutoDetectChangesEnabled = option;
		}

		public void LazyLoading(bool status)
		{
			DbContext.ChangeTracker.AutoDetectChangesEnabled = status;
			DbContext.ChangeTracker.LazyLoadingEnabled = status;
		}
	}
}
