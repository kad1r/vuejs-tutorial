﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Title")]
		public string Heading { get; set; }

		public string Description { get; set; }
		public string Barcode { get; set; }

		[Display(Name = "Eposta")]
		[DataType(DataType.EmailAddress)]
		[Required(ErrorMessage = "Email is required.")]
		[StringLength(20)]
		public string Email { get; set; }

		[Display(Name = "Password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public int ProductionYear { get; set; }

		[Display(Name = "Price")]
		[Required(ErrorMessage = "Price is required.")]
		[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
		[RegularExpression("[0,9]")]
		public decimal Price { get; set; }

		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
	}
}
