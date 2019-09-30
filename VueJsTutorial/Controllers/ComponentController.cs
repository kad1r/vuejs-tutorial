using Microsoft.AspNetCore.Mvc;

namespace VueJsTutorial.Controllers
{
	public class ComponentController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult SubListComponent()
		{
			return PartialView();
		}
	}
}
