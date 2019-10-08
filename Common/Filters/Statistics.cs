using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Common.Filters
{
	public sealed class Statistics : IAsyncActionFilter
	{
		public DateTime begin;
		public DateTime end;
		public double difference;

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			#region Before Executing

			begin = DateTime.Now;

			#endregion Before Executing

			#region Run Normal Logic

			var resultContext = await next();

			#endregion Run Normal Logic

			#region After Executing

			end = DateTime.Now;
			difference = (end - begin).TotalMilliseconds;

			//TODO put a limit here
			//Make smthn like that: if request takes more than 3 seconds then log it and create a ticket to check why it takes too much time
			if (difference > 0)
			{
				// log statistics
			}

			#endregion After Executing
		}
	}
}
