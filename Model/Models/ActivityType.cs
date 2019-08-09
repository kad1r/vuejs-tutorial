using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
	public class ActivityType
	{
		[Key]
		public int Id { get; set; }

		public string Heading { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
	}
}
