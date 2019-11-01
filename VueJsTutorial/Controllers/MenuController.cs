using Data.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using RazorTable.Dto;
using ServicePattern.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VueJsTutorial.Models;

namespace VueJsTutorial.Controllers
{
	public class MenuController : BaseController
	{
		private readonly IUnitOfWork _uow;
		private readonly MenuService _menuService;

		public MenuController(IUnitOfWork uow, MenuService menuService, IHostingEnvironment environment)
			: base(environment)
		{
			_uow = uow;
			_menuService = menuService;
		}

		public async Task<IActionResult> Index(int page = 1, List<SearchObj> searchParams = null, List<SortObj> sortParams = null)
		{
			var menuViewModel = new MenuViewModel
			{
				List = await _menuService.GetAllMenusWithOptions(page, pageSize, searchParams, sortParams)
			};

			return View(menuViewModel);
		}

		public async void FormInit(MenuViewModel viewModel)
		{

		}

		public async Task<IActionResult> Form(int? id)
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Form(MenuViewModel model)
		{
			if (ModelState.IsValid)
			{

			}

			return View();
		}
	}
}
