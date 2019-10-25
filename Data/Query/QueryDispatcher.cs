namespace Data.Query
{
	/*
	public class QueryDispatcher : IQueryDispatcher
	{
		private readonly IContainer _container;

		public QueryDispatcher(IContainer container)
		{
			_container = container;
		}

		public TResult Query<TQuery, TResult>(TQuery query) where TQuery : IQuery
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}

			var handler = _container.Resolve<IQueryHandler<TQuery, TResult>>();

			if (handler == null)
			{
				throw new QueryHandlerNotFoundException(typeof(TQuery));
			}

			return handler.Query(query);
		}
	}
	*/
}
