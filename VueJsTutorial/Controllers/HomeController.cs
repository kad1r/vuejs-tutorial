using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using VueJsTutorial.Models;

namespace VueJsTutorial.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			var vm = new UserVM();
			var list = new List<User>();
			var user = new User
			{
				Age = 35,
				Dob = DateTime.Now.AddYears(-35),
				Email = "avcikadir@gmail.com"
			};

			list.Add(new User { Age = 36, Dob = DateTime.Now.AddYears(-36), Email = "avcikadir@gmail.com" });
			list.Add(new User { Age = 31, Dob = DateTime.Now.AddYears(-31), Email = "cemlema20@gmail.com" });
			list.Add(new User { Age = 30, Dob = DateTime.Now.AddYears(-30), Email = "gokhanergun@gmail.com" });
			list.Add(new User { Age = 28, Dob = DateTime.Now.AddYears(-32), Email = "selcukaksar@gmail.com" });

			vm.User = user;
			vm.List = list;

			return View(vm);
		}

		public JsonResult GetData()
		{
			var vm = new UserVM();
			var list = new List<User>();
			var user = new User
			{
				Age = 35,
				Dob = DateTime.Now.AddYears(-35),
				Email = "avcikadir@gmail.com"
			};

			list.Add(new User { Age = 36, Dob = DateTime.Now.AddYears(-36), Email = "avcikadir@gmail.com" });
			list.Add(new User { Age = 31, Dob = DateTime.Now.AddYears(-31), Email = "cemlema20@gmail.com" });
			list.Add(new User { Age = 30, Dob = DateTime.Now.AddYears(-30), Email = "gokhanergun@gmail.com" });
			list.Add(new User { Age = 28, Dob = DateTime.Now.AddYears(-32), Email = "selcukaksar@gmail.com" });

			vm.User = user;
			vm.List = list;

			return Json(new { result = vm });
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
