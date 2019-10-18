using Common.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
	public class ProductActivityVM : BaseVM
	{
		public IList<ProductActivity> List { get; set; }
		public Product Product { get; set; } = new Product();
		public ProductActivityForJson ProductActivityForJson { get; set; } = new ProductActivityForJson();
		public IList<Product> Products { get; set; }
		public IList<SelectListItem> ActivityTypes { get; set; }
		public IList<SelectListItem> WareHouses { get; set; }
		public IList<ProductActivityForJson> ProductActivities { get; set; } = new List<ProductActivityForJson>();
		public ProductJson ProductJson { get; set; } = new ProductJson();
	}

	public class ProductActivityForJson : BaseModel
	{
		[Required]
		[Display(Name = "Activity Type")]
		public int? ActivityTypeId { get; set; }

		public string ActivityType { get; set; }

		[Required]
		[Display(Name = "WareHouse")]
		public int? WareHouseId { get; set; }

		public string WareHouse { get; set; }

		[Required]
		[Display(Name = "Invoice No.")]
		public string InvoiceNumber { get; set; }

		[Required]
		[Display(Name = "Activity Date")]
		public DateTime ActivityDate { get; set; }
	}

	public class ProductJson : BaseModel
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
	}
}
