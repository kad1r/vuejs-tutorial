using RazorTable.Dto;
using System.Collections.Generic;

namespace RazorTable.ViewModels
{
	public class TableVM<T> where T : class
	{
		public IList<SearchObj> SearchObjects { get; set; }
		public IList<SortObj> SortObjects { get; set; }
		public IList<T> List { get; set; }
	}
}
