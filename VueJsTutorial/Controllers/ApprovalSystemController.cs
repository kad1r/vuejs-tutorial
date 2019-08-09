using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Models;
using Model.ViewModels;

namespace VueJsTutorial.Controllers
{
	public class ApprovalSystemController : Controller
	{
		private IUnitOfWork _uow;
		//private readonly IRepository<ApprovalSystems> _approvalSystemRepository;
		//private readonly IRepository<ApprovalSystemTypes> _approvalSystemTypeRepository;
		//private readonly IRepository<ApproverTypes> _approverTypeRepository;
		//private readonly IRepository<CommodityHierarchys> _commodityTypeRepository;
		//private readonly IRepository<Departments> _departmentRepository;

		public ApprovalSystemController()
		{
			//_uow = new UnitOfWork(new AppDbContext());
		}

		public IActionResult Index()
		{
			return View();
		}

		public void FormInit()
		{

		}

		public IActionResult Form(int id)
		{
			//var vm = new ApprovalSystemVM
			//{
			//	//ApprovalSystem = _uow.Repository<ApprovalSystems>().Query(x => x.Id == id).Include(x => x.ApprovalSystemDetails).FirstOrDefault()
			//	ApprovalSystemTypes = _approvalSystemTypeRepository.Query()
			//		.Select(x => new SelectListItem { Text = x.Type, Value = x.Id.ToString() }).ToList(),
			//	ApproverTypes = _approverTypeRepository.Query()
			//		.Select(x => new SelectListItem { Text = x.Heading, Value = x.Id.ToString() }).ToList(),
			//	CommodityClass = _commodityTypeRepository.Query(x => x.CommodityHierarchyTypeId == 2 && x.IsActive == true)
			//		.Select(x => new SelectListItem { Text = x.Heading, Value = x.Id.ToString() }).ToList(),
			//	CommodityFamily = _commodityTypeRepository.Query(x => x.CommodityHierarchyTypeId == 1 && x.IsActive == true)
			//		.Select(x => new SelectListItem { Text = x.Heading, Value = x.Id.ToString() }).ToList(),
			//	Departments = _departmentRepository.Query(x => x.IsActive == true)
			//		.Select(x => new SelectListItem { Text = x.Heading, Value = x.Id.ToString() }).ToList(),
			//};

			//return View(vm);
			return View();
		}

		[HttpPost]
		public IActionResult Form(IFormCollection collection)
		{
			return View();
		}
	}
}