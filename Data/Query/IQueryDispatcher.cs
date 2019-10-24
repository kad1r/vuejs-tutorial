namespace Data.Query
{
	public interface IQueryDispatcher<TResult> where TResult : IQuery
	{
		TResult Execute<TQuery>(TQuery query) where TQuery : IQuery;
	}
}
