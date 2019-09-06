using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
	public class ProductActivityVM
	{
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
		[Display(Name = "Activity Type")]
		public int? ActivityTypeId { get; set; }

		public string ActivityType { get; set; }

		[Display(Name = "WareHouse")]
		public int? WareHouseId { get; set; }

		public string WareHouse { get; set; }

		[Display(Name = "Invoice No.")]
		public string InvoiceNumber { get; set; }
	}

	public class ProductJson : BaseModel
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
	}
}
