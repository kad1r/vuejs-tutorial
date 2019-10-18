using RazorTable.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorTable.Generator
{
	public class TableGenerator
	{
		public static string THeadGenerator(IList<SearchObj> headers, bool addCheckbox = true)
		{
			var result = new StringBuilder();

			result.Append("<tr class='hide search-box'>");


			#region Adding searching feature to the table

			foreach (var header in headers)
			{
				if (header != null && header == headers.First())
				{
					result.Append("<td class='" + (!header.IsEnable ? "input-disabled" : "") + "'></td>");
					continue;
				}
				else if (header != null)
				{
					var inputClass = GenerateInputClass(header);

					result.Append("<td>");
					result.Append("<input type='" + header.DataType + "' name='column-search' class='" + inputClass + "'" + 
						"data-isChild=''" + 
						">");
					result.Append("<td>");
					result.Append("<td>");
					result.Append("</td>");
				}
			}

			#endregion

			return string.Empty;
		}





		public static string GenerateInputClass(SearchObj header)
		{
			var className = "form-control search-frm " + (!header.IsEnable ? "input-disabled" : "");

			switch (header.ColumnType)
			{
				case "int":
					{
						className += " intOnly";
						break;
					}
				case "decimal":
				case "double":
				case "float":
					{
						className += " doubleOnly";
						break;
					}
				case "date":
					{
						className += " dateOnly";
						break;
					}
				case "datetime":
					{
						className += " datetimeOnly";
						break;
					}
				case "time":
					{
						className += " timeOnly";
						break;
					}
				default:
					{
						break;
					}
			}

			return className;
		}
	}
}
