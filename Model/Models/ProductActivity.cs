using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
	public class ProductActivity
	{
		[Key]
		public int Id { get; set; }

		public int ProductId { get; set; }
		public int ActivityTypeId { get; set; }
		public int WareHouseId { get; set; }
		public string InvoiceNumber { get; set; }
		public DateTime ActivityDate { get; set; }
		public virtual Product Product { get; set; }
		public virtual ActivityType ActivityType { get; set; }
		public virtual WareHouse WareHouse { get; set; }
	}
}
