namespace RazorTable.Dto
{
	public class SearchObj
	{
		public string Title { get; set; }
		public string Url { get; set; }
		public string Column { get; set; }
		public string DataType { get; set; }
		public string Value { get; set; }
		public string ColumnType { get; set; }
		public string EnumType { get; set; }
		public string OrderBy { get; set; }
		public bool IsChild { get; set; } = true;
		public bool IsEnable { get; set; } = true;
		public bool IsSearchingEnable { get; set; } = true;
		public bool IsSortingEnable { get; set; } = true;
	}
}
