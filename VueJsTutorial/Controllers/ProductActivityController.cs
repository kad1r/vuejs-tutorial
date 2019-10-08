using Data.Services;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Model.Models;
using Model.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace VueJsTutorial.Controllers
{
	public class ProductActivityController : BaseController
	{
		private readonly IUnitOfWork _uow;
		private readonly ProductService _productService;
		public readonly IConfiguration configuration;
		public readonly IHostingEnvironment env;

		public ProductActivityController(IUnitOfWork uow,/* ProductService productService,*/ IHostingEnvironment environment) : base(environment)
		{
			_uow = uow;
			//_productService = productService;
			env = environment;
		}

		public async Task<IActionResult> Index()
		{
			await _productService.DeleteAllProducts();

			return View();
		}

		public void FormInit(ProductActivityVM model)
		{
			model.AppSettings = AppSettings;
			model.ActivityTypes = _uow.Repository<ActivityType>()
				.QueryNoTracking(x => x.IsActive)
				.Select(x => new SelectListItem
				{
					Text = x.Heading,
					Value = x.Id.ToString()
				}).ToList();
			model.WareHouses = _uow.Repository<WareHouse>()
				.QueryNoTracking(x => x.IsActive)
				.Select(x => new SelectListItem
				{
					Text = x.Heading,
					Value = x.Id.ToString()
				}).ToList();
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
