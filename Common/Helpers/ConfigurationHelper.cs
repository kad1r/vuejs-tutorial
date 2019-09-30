using Common.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace Common.Helpers
{
	public class ConfigurationHelper
	{
		public static IConfiguration GetConfig()
		{
			var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			return builder.Build();
		}

		/*
		public static IConfiguration GetConfig(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			return builder.Build();
		}
		*/

		public static AppSettings GetAppSettings()
		{
			var settings = GetConfig();
			var appSettings = new AppSettings
			{
				Environment = GetEnvironment(),
				ApplicationUrl = GetApplicationUrl()
			};

			return appSettings;
		}

		public static string GetApplicationUrl()
		{
			var settings = GetConfig();
			return settings["AppSettings:ApplicationUrl"];
		}

		public static string GetEnvironment()
		{
			var settings = GetConfig();
			return settings != null ? settings["AppSettings:Environment"] : "";
		}

		public static bool IsDevelopment()
		{
			var settings = GetConfig();
			return (settings["AppSettings:Environment"] != null && settings["AppSettings.Environment"].ToLower() == "development") ? true : false;
		}
	}
}
