using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		public string Heading { get; set; }
		public string Description { get; set; }
		public string Barcode { get; set; }
		public int ProductionYear { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
	}
}
