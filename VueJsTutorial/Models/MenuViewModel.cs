using Model.Models;
using System.Collections.Generic;

namespace VueJsTutorial.Models
{
	public class MenuViewModel
	{
		public Menu Menu { get; set; }
		public IEnumerable<Menu> List { get; set; }
	}
}
