using Data.Repository;
using Data.Services;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Models;
using Model.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorTable.Dto;

namespace VueJsTutorial.Controllers
{
	public class ProductActivityController : BaseController
	{
		private readonly IUnitOfWork _uow;
		private readonly ProductService _productService;
		public readonly Repository<Product> productRepository;
		public readonly IConfiguration configuration;
		public readonly IHostingEnvironment env;

		public ProductActivityController(IUnitOfWork uow,/* ProductService productService, */IHostingEnvironment environment) : base(environment)
		{
			_uow = uow;
			//_productService = productService;
			//env = environment;
		}

		public async Task<IActionResult> Index(int page = 1, IList<SearchObj> searchParams = null, IList<SortObj> sortParams = null)
		{
			await _productService.DeleteAllProducts();

			return View();
		}

		public async Task FormInit(ProductActivityVM model)
		{
			model.AppSettings = AppSettings;
			model.List = await _uow.Repository<ProductActivity>()
				.QueryNoTracking()
				.ToListAsync();
			model.ActivityTypes = await _uow.Repository<ActivityType>()
				.QueryNoTracking(x => x.IsActive)
				.Select(x => new SelectListItem
				{
					Text = x.Heading,
					Value = x.Id.ToString()
				}).ToListAsync();
			model.WareHouses = await _uow.Repository<WareHouse>()
				.QueryNoTracking(x => x.IsActive)
				.Select(x => new SelectListItem
				{
					Text = x.Heading,
					Value = x.Id.ToString()
				}).ToListAsync();
		}

		public async Task<IActionResult> Form()
		{
			var vm = new ProductActivityVM();

			await FormInit(vm);

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
