using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using RazorTable.Dto;

namespace Data.Services
{
	public class MenuService : Repository<Menu>, IMenuService
	{
		private readonly Repository<Menu> _repository;

		public MenuService(Repository<Menu> repository, AppDbContext context) : base(context)
		{
			_repository = repository;
		}

		public override void Add(Menu menu)
		{
			_repository.Add(menu);
		}

		public async Task<IEnumerable<Menu>> GetAllMenus()
		{
			return await _repository.Query().ToListAsync();
		}

		public async Task<IEnumerable<Menu>> GetAllMenusWithOptions(int page, List<SearchObj> searchParameters, List<SortObj> sortParameters)
		{
			return await _repository.Query().ToListAsync();
		}
	}
}
