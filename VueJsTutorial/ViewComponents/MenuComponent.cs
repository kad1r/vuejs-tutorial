using Common.Helpers;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VueJsTutorial.ViewComponents
{
	[ViewComponent(Name = "MenuComponent")]
	public class MenuComponent : ViewComponent
	{
		private readonly IUnitOfWork _uow;

		public MenuComponent(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var menuList = new List<Menu>();

			if (!HttpContext.User.Identity.IsAuthenticated)
			{
				menuList = await _uow.Repository<Menu>()
					.Query(x => x.IsActive)
					.ToListAsync();
			}

			return View(menuList);
		}
	}
}
