using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
	public partial class Role
	{
		[Key]
		public int Id { get; set; }

		public string Heading { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime CreatedDate { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? UpdatedDate { get; set; }

		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }

		public virtual ICollection<MenuAuthorization> MenuAuthorizations { get; set; }
	}
}
