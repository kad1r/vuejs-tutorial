namespace Model.ViewModels
{
	public abstract class IEntity
	{
		public int RowId { get; set; }
		public int Status { get; set; }
		public bool Show { get; set; }
	}
}
