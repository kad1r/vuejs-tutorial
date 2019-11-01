using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using RazorTable.Dto;
using RazorTable.Generator;
using ServicePattern.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ServicePattern.Service
{
	public class MenuService : Service<Menu>, IMenuService
	{
		private readonly IRepository<Menu> _repository;

		public MenuService(IRepository<Menu> repository) : base(repository)
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

		public async Task<IEnumerable<Menu>> GetAllMenusWithOptions(int page, int size, List<SearchObj> searchParameters, List<SortObj> sortParameters)
		{
			var skip = page == 1 ? 0 : (page - 1) * size;
			var searchQuery = SearchHelper.GenerateFromSearchObj(searchParameters, true);
			var sorting = SortHelper.GenerateFromSortObj(sortParameters);

			return await _repository
				.Query()
				.Where(searchQuery)
				.OrderBy(sorting)
				.Skip(skip)
				.Take(size)
				.ToListAsync();
		}
	}
}
