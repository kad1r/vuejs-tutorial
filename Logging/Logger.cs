using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace Logging
{
	public class Logger
	{
		//public Logger()
		//{

		//}

		public static void Success(string message)
		{

		}

		public void Warning(string message)
		{
			Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().CreateLogger();
		}

		public static void Error()
		{
			Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().CreateLogger();

		}

		public static void Fatal(string message)
		{

		}
	}
}
