using RazorTable.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorTable.Generator
{
	public class SearchHelper
	{
		public static List<string> BooleanTypes = new List<string> { "active", "yes", "success", "open", "aktif", "evet", "başarılı", "açık" };
		public static List<string> OppositeBooleanTypes = new List<string> { "passive", "no", "failed", "closed", "pasif", "hayır", "başarısız", "kapalı" };

		/// <summary>
		/// <para></para>
		/// <para></para>
		/// </summary>
		/// <param name="searchParameters"></param>
		/// <param name="isView"></param>
		/// <returns></returns>
		public static string GenerateFromSearchObj(List<SearchObj> searchParameters, bool isView = false)
		{
			var query = new StringBuilder();

			if (!isView)
			{
				query.Append("IsDeleted==false");
			}
			else
			{
				query.Append("1==1");
			}

			if (searchParameters != null && searchParameters.Any())
			{
				foreach (var parameter in searchParameters)
				{
					if (!parameter.Skip)
					{
						query.Append(AppendToQuery(parameter));
					}
				}
			}

			return query.ToString();
		}

		public static string AppendToQuery(SearchObj parameter)
		{
			var query = new StringBuilder();
			var columnParams = parameter.Column.Split(".");

			switch (parameter.DataType.ToLower())
			{
				case "bool":
					{
						if (!parameter.Column.Contains(".") || !parameter.IsChild)
						{
							query.Append(BooleanTypes.Contains(parameter.Value) ? " && " + parameter.Column + "==true"
								: OppositeBooleanTypes.Contains(parameter.Value) ? " && " + parameter.Column + "==false" : "");
						}
						else if (parameter.IsChild && columnParams.Count() <= 2)
						{
							query.Append(BooleanTypes.Contains(parameter.Value) ? " && " + columnParams[0] + ".ANY(" + columnParams[1] + "==true)"
								: OppositeBooleanTypes.Contains(parameter.Value) ? " && " + columnParams[0] + ".ANY(" + columnParams[1] + "==false)" : "");
						}
						else if (parameter.IsChild && columnParams.Count() == 3)
						{
							query.Append(BooleanTypes.Contains(parameter.Value) ? " && " + columnParams[0] + ".ANY(" + columnParams[1] + "." + columnParams[2] + "==true)"
								: OppositeBooleanTypes.Contains(parameter.Value) ? " && " + columnParams[0] + ".ANY(" + columnParams[1] + "." + columnParams[2] + "==false)" : "");
						}

						//GenerateQueryFromColumn(parameter, ref query);

						break;
					}
				case "int":
				case "float":
				case "decimal":
				case "double":
					{
						query.Append(" && " + parameter.Column + " in (" + parameter.Value + ")");

						break;
					}
				case "date":
					{
						var startDate = new DateTime();

						DateTime.TryParse(parameter.Value, out startDate);

						if (startDate != new DateTime())
						{
							var endDate = startDate.AddDays(1).AddSeconds(-1);

							query.Append(" && " + parameter.Column + ">=DateTime(" + startDate.Year + "," + startDate.Month + "," + startDate.Day + "," + startDate.Hour + "," + startDate.Minute + "," + startDate.Second + ")");
							query.Append(" && " + parameter.Column + "<=DateTime(" + endDate.Year + "," + endDate.Month + "," + endDate.Day + "," + endDate.Hour + "," + endDate.Minute + "," + endDate.Second + ")");
						}

						break;
					}
				case "time":
					{
						var date = new DateTime();

						DateTime.TryParse(parameter.Value, out date);

						var timeSpan = new TimeSpan(date.Hour, date.Minute, date.Second);

						if (date != new DateTime())
						{
							query.Append(" && " + parameter.Column + "==TimeSpan(" + date.Hour + "," + date.Minute + "," + date.Second + ")");
						}

						break;
					}
				case "enum":
					{
						// TODO
						// Create nuget package for enum utilities and get it from there, we need to decouple project dependencies
						//EnumHelper.GetValueOf(parameter.ColumnType, parameter.Value);
						EnumAgent.Utilities.ConvertFromString(parameter.ColumnType,parameter.Value);
						break;
					}
				default:
					{
						break;
					}
			}

			return query.ToString();
		}

		public static string GenerateQueryFromColumn(SearchObj parameter, ref StringBuilder query)
		{
			var columnParams = parameter.Column.Split(".");

			if (parameter.Column.Contains("."))
			{
			}
			else if (parameter.IsChild)
			{
			}
			else if (!parameter.IsChild)
			{
			}
			else if (columnParams.Count() <= 2)
			{
			}
			else if (columnParams.Count() == 3)
			{
			}

			return query.ToString();
		}
	}
}
