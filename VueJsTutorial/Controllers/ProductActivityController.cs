using Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Models;
using Model.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace VueJsTutorial.Controllers
{
	public class ProductActivityController : Controller
	{
		private readonly IUnitOfWork _uow;

		public ProductActivityController(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public IActionResult Index()
		{
			return View();
		}

		public void FormInit(ProductActivityVM model)
		{
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
