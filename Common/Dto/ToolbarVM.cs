namespace Common.Dto
{
	public class ToolbarVM
	{
		public int MenuId { get; set; } = 0;
		public string HelpUrl { get; set; } = string.Empty;
		public bool CanCreate { get; set; } = false;
		public bool CanDelete { get; set; } = false;
		public bool CanEdit { get; set; } = false;
		public bool CanExport { get; set; } = false;
		public bool CanRead { get; set; } = false;
		public bool CanSave { get; set; } = false;
	}
}
