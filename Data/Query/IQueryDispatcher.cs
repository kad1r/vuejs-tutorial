namespace Data.Query
{
	public interface IQueryDispatcher
	{
		TResult Query<TQuery, TResult>(TQuery query) where TQuery : IQuery;
	}
}
