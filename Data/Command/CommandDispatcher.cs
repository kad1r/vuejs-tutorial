namespace Data.Command
{
	/*
    public class CommandDispatcher : ICommandDispatcher
    {
		private readonly IContainer container;

		public void Execute<TCommand>(TCommand command) where TCommand : ICommand
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}

			var handler = container.Resolve<ICommandHandler<TCommand>>();

			if (handler == null)
			{
				throw new CommandHandlerNotFoundException(typeof(TCommand));
			}

			handler.Execute(command);
		}
	}
	*/
}
