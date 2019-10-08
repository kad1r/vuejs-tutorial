using Common.Dto;
using Common.Helpers;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System.Threading.Tasks;

namespace VueJsTutorial.ViewComponents
{
	[ViewComponent(Name = "ToolbarComponent")]
	public class ToolbarViewComponent : ViewComponent
	{
		private readonly IUnitOfWork _uow;

		public ToolbarViewComponent(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var vm = new ToolbarVM
			{
				//TODO: get menu_id from url
				MenuId = UrlHelper.GetMenuFromUrl(HttpContext.Request)
			};

			if (HttpContext.User.Identity.IsAuthenticated)
			{
				var menuAuthorization = await _uow.Repository<MenuAuthorization>()
					.Query(x => x.MenuId == vm.MenuId)
					.FirstOrDefaultAsync();

				if (menuAuthorization != null)
				{
					vm.CanCreate = menuAuthorization.CanCreate;
					vm.CanDelete = menuAuthorization.CanDelete;
					vm.CanEdit = menuAuthorization.CanEdit;
					vm.CanExport = menuAuthorization.CanRead;
					vm.CanRead = menuAuthorization.CanRead;
					vm.CanSave = menuAuthorization.CanCreate;
				}
			}

			return View(vm);
		}
	}
}
