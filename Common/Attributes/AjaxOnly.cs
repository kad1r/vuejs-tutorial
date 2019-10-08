using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;

namespace Common.Attributes
{
	public sealed class AjaxOnly : ActionMethodSelectorAttribute
	{
		public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
		{
			if (routeContext.HttpContext.Request.Headers != null &&
				routeContext.HttpContext.Request.Headers.ContainsKey("X-Requested-With") &&
				routeContext.HttpContext.Request.Headers.TryGetValue("X-Requested-With", out StringValues requestedWithHeader))
			{
				if (requestedWithHeader.Contains("XMLHttpRequest"))
				{
					return true;
				}
			}

			//TODO: check how we can redirect or change the status code for the ajax responses
			//These doesn't work!!!
			//routeContext.HttpContext.Response.StatusCode = 401;
			//routeContext.HttpContext.Response.Redirect("/asdsad");

			return false;
		}
	}
}
