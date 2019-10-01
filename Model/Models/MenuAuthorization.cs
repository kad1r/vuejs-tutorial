using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
	public partial class MenuAuthorization
	{
		[Key]
		public int Id { get; set; }

		public int RoleId { get; set; }
		public int MenuId { get; set; }
		public bool CanRead { get; set; }
		public bool CanCreate { get; set; }
		public bool CanEdit { get; set; }
		public bool CanDelete { get; set; }

		public virtual Role Role { get; set; }
		public virtual Menu Menu { get; set; }
	}
}
