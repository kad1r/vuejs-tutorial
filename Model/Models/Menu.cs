using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
	public partial class Menu
	{
		[Key]
		public int Id { get; set; }

		public int? RootId { get; set; }

		[Required]
		[StringLength(50)]
		public string Heading { get; set; }

		[StringLength(150)]
		public string Url { get; set; }

		public bool IsRoot { get; set; }
		public int Sequence { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime CreatedDate { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? UpdatedDate { get; set; }

		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }

		public virtual Menu Root { get; set; }
		public virtual ICollection<MenuAuthorization> MenuAuthorizations { get; set; }
	}
}
