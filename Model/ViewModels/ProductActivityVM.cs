using Model.Models;
using System.Collections.Generic;

namespace Model.ViewModels
{
	public class ProductActivityVM
	{
		public Product Product { get; set; }
		public ProductActivity ProductActivity { get; set; }
		public IList<Product> Products { get; set; }
		public IList<ActivityType> ActivityTypes { get; set; }
		public IList<WareHouse> WareHouses { get; set; }
	}
}
