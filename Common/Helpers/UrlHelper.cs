using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Common.Helpers
{
	public class UrlHelper
	{
		public static int GetMenuFromUrl(HttpRequest request)
		{
			var menuId = 0;

			if (request.Query.Count > 0)
			{
				var queryString = request.Query;
				StringValues menuParameters;

				queryString.TryGetValue("menu", out menuParameters);
				menuId = int.Parse(menuParameters);
			}

			return menuId;
		}
	}
}
