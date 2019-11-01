using Data.Repository;
using Model.Models;
using RazorTable.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
	public interface IMenuService : IRepository<Menu>
	{
		Task<IEnumerable<Menu>> GetAllMenus();

		Task<IEnumerable<Menu>> GetAllMenusWithOptions(int page, List<SearchObj> searchParameters, List<SortObj> sortParameters);
	}
}
