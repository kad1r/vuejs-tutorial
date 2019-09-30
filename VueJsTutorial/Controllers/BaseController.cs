using Common.Dto;
using Common.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace VueJsTutorial.Controllers
{
	public class BaseController : Controller
	{
		public string localhost = string.Empty;
		public IHostingEnvironment env;
		public AppSettings AppSettings;

		public BaseController(IHostingEnvironment environment)
		{
			env = environment;
			AppSettings = ConfigurationHelper.GetAppSettings();
		}
	}
}
