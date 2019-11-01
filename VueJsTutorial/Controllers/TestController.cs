using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VueJsTutorial.Models;
using System.Diagnostics;

namespace VueJsTutorial.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HelloWorld()
        {
            ViewData["Message"] = string.Empty;

            return View();
        }

      
        [HttpGet]
        public IActionResult EmployeeIndex()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}