using RazorTable.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorTable.Generator
{
	public class SortHelper
	{
		/// <summary>
		/// <para>Generate order by clause for linq dynamic to sort items</para>
		/// <para>Ex: sortParameters={ column:year, orderby:asc }, skipIdColumn=true; -> generates order by id desc, year asc</para>
		/// <para>Ex: sortParameters={ column:year, orderby:asc }, skipIdColumn=false; -> generates order by year asc</para>
		/// </summary>
		/// <param name="sortParameters">Contains sorting parameters</param>
		/// <param name="skipIdColumn">Remains id column of the table on the query if true.</param>
		/// <returns></returns>
		public static string GenerateFromSortObj(List<SortObj> sortParameters, bool skipIdColumn = true)
		{
			var query = new StringBuilder();

			if (sortParameters != null && sortParameters.Any())
			{
				foreach (var parameter in sortParameters)
				{
					if (skipIdColumn)
					{
						skipIdColumn = false;
						continue;
					}

					var splitColumn = parameter.Column.Split(".");

					if (splitColumn.Count() == 2)
					{
						query.Append(parameter.Column + " " + parameter.OrderBy);
					}
					else if (splitColumn.Count() == 3)
					{
						query.Append(splitColumn[0] + ".ANY(" + splitColumn[1] + "." + splitColumn[2] + ") " + parameter.OrderBy);
					}

					if (parameter != sortParameters.Last())
					{
						query.Append(",");
					}
				}
			}
			else
			{
				query.Append("Id desc");
			}

			return query.ToString();
		}
	}
}
