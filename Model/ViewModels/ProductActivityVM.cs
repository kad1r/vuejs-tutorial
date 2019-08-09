using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels
{
	public class ProductActivityVM
	{
		public IList<Product> Products { get; set; }
		public IList<ActivityType> ActivityTypes { get; set; }
		public IList<WareHouse> WareHouses { get; set; }
	}
}
