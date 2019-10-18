using Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using VueJsTutorial.Models;
using Logging;
using Model.Models;
using System.Linq.Expressions;
using Data.UnitOfWork;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace VueJsTutorial.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUnitOfWork _uow;
		private readonly Logger logger;

		public HomeController(IUnitOfWork uow)
		{
			_uow = uow;
		}

		//[AjaxOnly]
		public IActionResult Index()
		{
			/*
			Expression<Func<Menu, bool>> exp = x => x.IsActive == true;

			if (true)
			{
				Expression<Func<Menu, bool>> exp1 = c => c.MenuAuthorizations.Any(z => z.RoleId == 1);

				var invokedExpression = Expression.Invoke(exp1, exp.Parameters.Cast<Expression>());
				exp = Expression.Lambda<Func<Menu, bool>>(Expression.AndAlso(exp.Body, invokedExpression), exp.Parameters);
			}

			var products = _uow.Repository<Menu>()
				.Query(exp)
				.Include(x => x.MenuAuthorizations)
				.ToList();
			*/

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
