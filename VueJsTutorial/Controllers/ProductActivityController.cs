using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ViewModels;

namespace VueJsTutorial.Controllers
{
	public class ProductActivityController : Controller
	{
		private readonly IUnitOfWork _uow;

		public ProductActivityController()
		{
			_uow = new UnitOfWork(new AppDbContext());
		}

		public ProductActivityController(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public async Task<IActionResult> Index()
		{
			return View();
		}

		public async void FormInit(ProductActivityVM model)
		{
			model.ActivityTypes = _uow.Repository<ActivityType>().Query(x => x.IsActive).ToList();
			model.WareHouses = _uow.Repository<WareHouse>().Query(x => x.IsActive).ToList();
		}

		public async Task<IActionResult> Form()
		{
			var vm = new ProductActivityVM();

			FormInit(vm);

			return View(await Task.FromResult(vm));
		}

		[HttpPost]
		public async Task<IActionResult> Form(ProductActivityVM model, IFormCollection formCollection)
		{
			if (ModelState.IsValid)
			{

			}

			return View();
		}
	}
}