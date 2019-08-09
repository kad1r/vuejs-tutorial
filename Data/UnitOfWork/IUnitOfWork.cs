using Data.Repository;
using Model.ViewModels;
using System;
using System.Threading.Tasks;

namespace Data.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		Model.Models.AppDbContext DbContext { get; }
		IRepository<T> Repository<T>() where T : class;

		int SaveChanges();
		Task<int> SaveChangesAsync();
		void ChangeTracking(bool option);
		void LazyLoading(bool option);
	}
}