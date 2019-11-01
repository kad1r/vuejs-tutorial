using Model.Models;
using RazorTable.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicePattern.Interface
{
	public interface IMenuService : IService<Menu>
	{
		// Add custom business logic here
		// Example:
		//IEnumerable<Menu> Search(string keyword);

		Task<IEnumerable<Menu>> GetAllMenus();

		Task<IEnumerable<Menu>> GetAllMenusWithOptions(int page, int size, List<SearchObj> searchParameters, List<SortObj> sortParameters);
	}
}
