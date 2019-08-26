using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace Common.Extensions
{
	public static class HtmlHelpers
	{
		public static IHtmlContent oTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> html,
			Expression<Func<TModel, TProperty>> expression,
			IDictionary<string, object> htmlAttributes = null,
			bool readOnly = false,
			bool sublist = false,
			bool inputGroup = false,
			bool isGuide = false,
			string guideUrl = "")
		{
			var outerDiv = new TagBuilder("div");
			var label = new TagBuilder("label");
			var labelAttributes = new Dictionary<string, object>();

			outerDiv.AddCssClass("form-group");
			labelAttributes.Add("class", "control-label col-xs-12");

			if (!sublist)
			{
				labelAttributes.Add("class", "control-label col-xs-12 col-sm-4 col-md-4 col-lg-3");
			}

			var labelText = html.LabelFor(expression, labelAttributes).ToString();
			label.InnerHtml.AppendHtml(labelText);

			var innerDiv = new TagBuilder("div");
			innerDiv.AddCssClass("col-xs-12");

			if (!sublist)
			{
				innerDiv.AddCssClass("col-xs-12 col-sm-8 col-md-8 col-lg-9");
			}

			var summaryDiv = new TagBuilder("div");
			var small = new TagBuilder("small");
			var span = new TagBuilder("span");

			summaryDiv.AddCssClass("error-summary");
			summaryDiv.InnerHtml.AppendHtml(small.InnerHtml);
			summaryDiv.InnerHtml.AppendHtml(summaryDiv.ToString());
			span.AddCssClass("field-validation-valid");
			span.InnerHtml.AppendHtml(html.ValidationMessageFor(expression).ToString());
			small.InnerHtml.AppendHtml(span.InnerHtml);
			small.InnerHtml.AppendHtml(small.ToString());

			if (htmlAttributes == null)
			{
				htmlAttributes = new Dictionary<string, object>();
			}

			var oMetaData = ExpressionMetadataProvider.FromLambdaExpression(expression, html.ViewData, html.MetadataProvider);

			if (oMetaData == null)
			{
				if (readOnly)
				{
					if (!htmlAttributes.ContainsKey("readonly"))
					{
						htmlAttributes.Add("readonly", "read-only");
					}
				}
			}
			else
			{
				if (!htmlAttributes.ContainsKey("placeholder"))
				{
					var fieldName = ExpressionHelper.GetExpressionText(expression);
					var placeholder = oMetaData.Metadata.DisplayName ?? oMetaData.Metadata.PropertyName ?? fieldName.Split('.').Last();

					if (!string.IsNullOrWhiteSpace(placeholder))
					{
						htmlAttributes.Add("placeholder", placeholder);
					}

					if (readOnly || oMetaData.Metadata.IsReadOnly)
					{
						if (!htmlAttributes.ContainsKey("readonly"))
						{
							htmlAttributes.Add("readonly", "read-only");
						}
					}
				}
			}

			var oExpression = expression.Body as MemberExpression;

			if (oExpression != null)
			{
				var strLengthAttribute = oExpression.Member
					.GetCustomAttributes(typeof(StringLengthAttribute), false)
					.FirstOrDefault() as StringLengthAttribute;

				if (strLengthAttribute != null)
				{
					if (!htmlAttributes.ContainsKey("maxlength"))
					{
						htmlAttributes.Add("maxlength", strLengthAttribute.MaximumLength);
					}
				}
			}

			if (htmlAttributes.ContainsKey("date"))
			{
				var inputGroupDiv = new TagBuilder("div");
				inputGroupDiv.AddCssClass("input-group date");

				var spanDate = new TagBuilder("span");
				spanDate.AddCssClass("input-group-addon input-pe-disabled");

				var spanIcon = new TagBuilder("span");
				spanIcon.AddCssClass("fa fa-calendar input-pe-disabled");

				spanIcon.InnerHtml.AppendHtml(spanIcon.ToString());
				spanDate.InnerHtml.AppendHtml(spanIcon.InnerHtml);
				spanDate.InnerHtml.AppendHtml(spanDate.ToString());

				inputGroupDiv.InnerHtml.AppendHtml(spanDate.InnerHtml + (html.TextBoxFor(expression, htmlAttributes)).ToString());
				innerDiv.InnerHtml.AppendHtml(inputGroupDiv.ToString() + summaryDiv.InnerHtml);
				outerDiv.InnerHtml.AppendHtml(labelText + innerDiv.ToString());
			}
			else
			{
				var sp = expression.Body.ToString().Split('.');
				var id = string.Empty;

				for (int i = 0; i < sp.Length; i++)
				{
					if (i > 0)
					{
						id += sp[i];
						if (i < sp.Length - 1)
							id += "_";
					}
				}

				innerDiv.InnerHtml.AppendHtml((inputGroup ? "<div class='input-group'>" : "") +
					html.TextBoxFor(expression, htmlAttributes) +
					(inputGroup ? "<span class='input-group-btn'><button class='btn btn-primary" + (isGuide ? " openGuide" : "") + "' type='button' " + (isGuide ? " data-href='" + guideUrl + "'" : "") + " id='groupBtn_" + id + "'><i class='fa fa-search'></i></button></span></div>" : "") +
					summaryDiv.InnerHtml);
				outerDiv.InnerHtml.AppendHtml(labelText + innerDiv.ToString());
			}

			return outerDiv;
		}

		public static IHtmlContent oDropDownFor<TModel, TProperty>(this HtmlHelper<TModel> html,
			Expression<Func<TModel, TProperty>> expression,
			IEnumerable<SelectListItem> list,
			string first,
			IDictionary<string, object> htmlAttributes = null,
			bool readOnly = false,
			bool sublist = false,
			bool isGuide = false,
			string guideUrl = "")
		{
			var outerDiv = new TagBuilder("div");
			var label = new TagBuilder("label");
			var labelAttributes = new Dictionary<string, object>();

			outerDiv.AddCssClass("form-group");

			if (!sublist)
			{
				labelAttributes.Add("class", "control-label col-xs-12 col-sm-4 col-md-4 col-lg-3");
			}
			else
			{
				labelAttributes.Add("class", "control-label col-xs-12");
			}

			var labelText = html.LabelFor(expression, labelAttributes).ToString();

			var innerDiv = new TagBuilder("div");
			var summaryDiv = new TagBuilder("div");
			var small = new TagBuilder("small");
			var span = new TagBuilder("span");

			if (!sublist)
			{
				innerDiv.AddCssClass("col-xs-12 col-sm-8 col-md-8 col-lg-9");
			}
			else
			{
				innerDiv.AddCssClass("col-xs-12");
			}

			label.InnerHtml.AppendHtml(labelText);
			summaryDiv.AddCssClass("error-summary");
			span.AddCssClass("field-validation-valid");
			span.InnerHtml.AppendHtml(html.ValidationMessageFor(expression).ToString());
			small.InnerHtml.AppendHtml(span.InnerHtml);
			small.InnerHtml.AppendHtml(small.ToString());
			summaryDiv.InnerHtml.AppendHtml(small.InnerHtml);
			summaryDiv.InnerHtml.AppendHtml(summaryDiv.ToString());

			var dict = new RouteValueDictionary(htmlAttributes);

			if (isGuide)
			{
				var inputGroupDiv = new TagBuilder("div");
				var inputGroupSpan = new TagBuilder("span");
				var inputGroupBtn = new TagBuilder("button");
				var inputGroupBtnI = new TagBuilder("i");

				inputGroupDiv.AddCssClass("input-group");
				inputGroupSpan.AddCssClass("input-group-btn");
				inputGroupBtn.AddCssClass("btn btn-primary openGuide");
				inputGroupBtn.MergeAttribute("type", "button");
				inputGroupBtn.MergeAttribute("data-href", guideUrl);
				inputGroupBtnI.AddCssClass("fa fa-search");

				inputGroupBtn.InnerHtml.AppendHtml(inputGroupBtnI.ToString());
				inputGroupSpan.InnerHtml.AppendHtml(inputGroupBtn.ToString());
				inputGroupDiv.InnerHtml.AppendHtml(html.DropDownListFor(expression, list, first, dict) + inputGroupSpan.ToString());
				innerDiv.InnerHtml.AppendHtml(inputGroupDiv.ToString() + summaryDiv.InnerHtml);
				outerDiv.InnerHtml.AppendHtml(labelText + innerDiv);
			}
			else
			{
				innerDiv.InnerHtml.AppendHtml(html.DropDownListFor(expression, list, first, dict).ToString() + summaryDiv.InnerHtml);
				outerDiv.InnerHtml.AppendHtml(labelText + innerDiv);
			}

			return outerDiv;
		}

		public static IHtmlContent oEnumDropDownFor<TModel, TProperty, TEnum>(this HtmlHelper<TModel> html,
			Expression<Func<TModel, TProperty>> expression,
			TEnum selectedValue,
			IEnumerable<SelectListItem> list = null,
			string first = "",
			IDictionary<string, object> htmlAttributes = null,
			bool readOnly = false,
			bool sublist = false,
			bool isGuide = false,
			string guideUrl = "")
		{
			var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

			if (list == null)
			{
				list = from value in values
					   select new SelectListItem
					   {
						   Text = value.ToString(),
						   Value = Convert.ToInt32(value).ToString(),
						   Selected = (value.Equals(selectedValue))
					   };
			}

			var outerDiv = new TagBuilder("div");
			outerDiv.AddCssClass("form-group");

			var label = new TagBuilder("label");
			var labelAttributes = new Dictionary<string, object>();

			if (!sublist)
			{
				labelAttributes.Add("class", "control-label col-xs-12 col-sm-4 col-md-4 col-lg-3");
			}
			else
			{
				labelAttributes.Add("class", "control-label col-xs-12");
			}

			var labelText = html.LabelFor(expression, labelAttributes).ToString();
			label.InnerHtml.AppendHtml(labelText);

			var innerDiv = new TagBuilder("div");

			if (!sublist)
			{
				innerDiv.AddCssClass("col-xs-12 col-sm-8 col-md-8 col-lg-9");
			}
			else
			{
				innerDiv.AddCssClass("col-xs-12");
			}

			var summaryDiv = new TagBuilder("div");
			var small = new TagBuilder("small");
			var span = new TagBuilder("span");

			summaryDiv.AddCssClass("error-summary");
			span.AddCssClass("field-validation-valid");
			span.InnerHtml.AppendHtml(html.ValidationMessageFor(expression).ToString());
			small.InnerHtml.AppendHtml(span.InnerHtml);
			small.InnerHtml.AppendHtml(small);
			summaryDiv.InnerHtml.AppendHtml(small.InnerHtml);
			summaryDiv.InnerHtml.AppendHtml(summaryDiv);

			var dict = new RouteValueDictionary(htmlAttributes);

			if (isGuide)
			{
				var inputGroupDiv = new TagBuilder("div");
				var inputGroupSpan = new TagBuilder("span");
				var inputGroupBtn = new TagBuilder("button");
				var inputGroupBtnI = new TagBuilder("i");

				inputGroupSpan.AddCssClass("input-group-btn");
				inputGroupDiv.AddCssClass("input-group");
				inputGroupBtn.AddCssClass("btn btn-primary openGuide");
				inputGroupBtn.MergeAttribute("type", "button");
				inputGroupBtn.MergeAttribute("data-href", guideUrl);
				inputGroupBtnI.AddCssClass("fa fa-search");

				inputGroupBtn.InnerHtml.AppendHtml(inputGroupBtnI);
				inputGroupSpan.InnerHtml.AppendHtml(inputGroupBtn);
				inputGroupDiv.InnerHtml.AppendHtml(html.DropDownListFor(expression, list, first, dict).ToString() + inputGroupSpan);
				innerDiv.InnerHtml.AppendHtml(inputGroupDiv.ToString() + summaryDiv.InnerHtml);
				outerDiv.InnerHtml.AppendHtml(labelText + innerDiv);
			}
			else
			{
				innerDiv.InnerHtml.AppendHtml(html.DropDownListFor(expression, list, first, dict).ToString() + summaryDiv.InnerHtml);
				outerDiv.InnerHtml.AppendHtml(labelText + innerDiv);
			}

			return outerDiv;
		}

		public static IHtmlContent oEnumDropDownList<TEnum>(this HtmlHelper html,
			string name,
			TEnum selectedValue,
			IDictionary<string, object> htmlAttributes = null)
		{
			var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
			var items = from value in values
						select new SelectListItem
						{
							Text = value.ToString(),
							Value = Convert.ToInt32(value).ToString(),
							Selected = (value.Equals(selectedValue))
						};

			return html.DropDownList(name, items, htmlAttributes);
		}
	}
}
