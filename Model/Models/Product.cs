using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
	public partial class Product
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Title")]
		public string Heading { get; set; }

		public string Description { get; set; }
		public string Barcode { get; set; }

		[Display(Name = "Email")]
		[DataType(DataType.EmailAddress)]
		[Required(ErrorMessage = "Email is required.")]
		[StringLength(20)]
		[RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}", ErrorMessage = "Please enter a valid email address")]
		public string Email { get; set; }

		[Display(Name = "Password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public int ProductionYear { get; set; }

		[Display(Name = "Price")]
		[Required(ErrorMessage = "Price is required.")]
		[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
		[RegularExpression(@"^\d*([.,]*\d{1,4})$")]
		[Column(TypeName = "decimal(18,4)")]
		public decimal Price { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime CreatedDate { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? UpdatedDate { get; set; }

		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
	}
}
