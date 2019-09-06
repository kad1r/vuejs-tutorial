using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace Common.Extensions
{
	public class HtmlTagHelpers : TagHelper
	{
		public static string GetDataTypeOfField(string dataType)
		{
			string type;

			switch (dataType)
			{
				case "EmailAddress":
					type = "email";
					break;

				case "Password":
					type = "password";
					break;

				case "Phone":
					type = "tel";
					break;

				case "DateTime":
					type = "datetime";
					break;

				case "Date":
					type = "date";
					break;

				case "Time":
					type = "time";
					break;

				case "String":
					type = "text";
					break;

				case "Byte":
					type = "number";
					break;

				case "int":
					type = "number";
					break;

				case "bool":
					type = "checkbox";
					break;

				default:
					type = "text";
					break;
			}

			return type;
		}

		public static bool IsAttributeAddible(string attribute)
		{
			var isAddible = true;

			if (!string.IsNullOrWhiteSpace(attribute) && (attribute == "asp-for" || attribute == "asp-items" || attribute == "asp-format" || attribute == "name" ||
				attribute == "has-parent" || attribute == "parent-css" ||
				attribute == "has-label" || attribute == "label-css" || attribute == "input-css"))
			{
				isAddible = false;
			}

			return isAddible;
		}
	}

	[HtmlTargetElement("img")]
	public class ImgHelper : TagHelper
	{
		public string Link { get; set; }
		public string Alt { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "img";
			output.TagMode = TagMode.StartTagOnly;
			output.Attributes.SetAttribute("src", Link);
			output.Attributes.SetAttribute("alt", Alt);
		}
	}

	[HtmlTargetElement("otextboxfor")]
	public class TextBoxForHelper : TagHelper
	{
		private readonly IHtmlGenerator _generator;

		[ViewContext]
		public ViewContext ViewContext { get; set; }

		[HtmlAttributeName("asp-for")]
		public ModelExpression Expression { get; set; }

		public string Name { get; set; }
		public string InputCss { get; set; }
		public string PlaceHolder { get; set; }
		public int MinLength { get; set; }
		public int MaxLength { get; set; }
		public bool IsPassword { get; set; }
		public bool IsRequired { get; set; }
		public string RequiredCss { get; set; }
		public bool PasswordStrenght { get; set; }
		public string HasParent { get; set; }
		public string ParentCss { get; set; }
		public bool HasLabel { get; set; }
		public string LabelCss { get; set; }
		public string VModelName { get; set; }

		public TextBoxForHelper(IHtmlGenerator generator)
		{
			_generator = generator;
		}

		public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = string.Empty;
			output.TagMode = TagMode.SelfClosing;

			using (var sw = new StringWriter())
			{
				var wrapper = new StringWriter();
				var labelBuilder = new TagBuilder("label");
				var inputBuilder = new TagBuilder("input");
				var validationBuilder = new TagBuilder("validation");
				var format = context.AllAttributes["asp-format"] != null ? context.AllAttributes["asp-format"].Value.ToString() : "";
				var labelCss = context.AllAttributes["has-label"] != null ? context.AllAttributes["label-css"].Value.ToString() : "";
				var inputCss = context.AllAttributes["input-css"] != null ? context.AllAttributes["input-css"].Value.ToString() : "";
				var vmodel = context.AllAttributes["VModelName"] != null ? context.AllAttributes["VModelName"].Value.ToString() : "";
				var validationCss = Expression.Metadata.IsRequired || (context.AllAttributes["is-required"] != null && bool.Parse(context.AllAttributes["is-required"].Value.ToString())) ? context.AllAttributes["required-css"].Value.ToString() : "";

				if (Expression.Metadata.IsRequired)
				{
					inputCss += " requiredField";
				}

				switch (Expression.Metadata.DataTypeName)
				{
					case "int":
						inputCss += " inputOnly";
						break;

					case "double":
						inputCss += " doubleOnly";
						break;

					case "decimal":
						inputCss += " doubleOnly";
						break;

					default:
						break;
				}

				// generate label if exist
				if (context.AllAttributes["has-label"] != null && bool.Parse(context.AllAttributes["has-label"].Value.ToString()))
				{
					labelBuilder = _generator.GenerateLabel(ViewContext, Expression.ModelExplorer, Expression.Name, Expression.Metadata.GetDisplayName(), new { @class = LabelCss });
					labelBuilder.WriteTo(sw, NullHtmlEncoder.Default);
				}

				// generate actual input
				inputBuilder = _generator.GenerateTextBox(ViewContext, Expression.ModelExplorer, Expression.Name, Expression.Model, Expression.Metadata.DisplayFormatString, new { @class = inputCss, PlaceHolder = Expression.Metadata.GetDisplayName() });

				foreach (var validationAttribute in Expression.Metadata.ValidatorMetadata)
				{
					if (validationAttribute.GetType().Name == "RegularExpressionAttribute")
					{
						var reAttr = (RegularExpressionAttribute)validationAttribute;
						inputBuilder.Attributes.Add("pattern", reAttr.Pattern);
					}
				}

				foreach (var attribute in context.AllAttributes)
				{
					if (HtmlTagHelpers.IsAttributeAddible(attribute.Name))
					{
						if (attribute.Name == "place-holder")
						{
							inputBuilder.Attributes.Remove("placeholder");
							inputBuilder.Attributes.Add("placeholder", attribute.Value.ToString());
						}
						else if (attribute.Name == "v-model-name")
						{
							inputBuilder.Attributes.Remove("v-model");
							inputBuilder.Attributes.Add("v-model", attribute.Value.ToString());
						}
						else
						{
							inputBuilder.Attributes.Add(attribute.Name, attribute.Value.ToString());
						}
					}
				}

				inputBuilder.WriteTo(sw, NullHtmlEncoder.Default);

				// generate validation if exist
				if (Expression.Metadata.IsRequired || (context.AllAttributes["is-required"] != null && bool.Parse(context.AllAttributes["is-required"].Value.ToString())))
				{
					validationBuilder = _generator.GenerateValidationMessage(ViewContext, Expression.ModelExplorer, Expression.Name, null, ViewContext.ValidationMessageElement, new { @class = validationCss });
					validationBuilder.WriteTo(sw, NullHtmlEncoder.Default);
				}

				// generate wrapper parent element if exist
				if (context.AllAttributes["has-parent"] != null && bool.Parse(context.AllAttributes["has-parent"].Value.ToString()))
				{
					wrapper.Write("<div class='" + context.AllAttributes["parent-css"].Value + "'>");
					wrapper.Write(sw.ToString());
					wrapper.Write("</div>");
				}

				output.Content.SetHtmlContent(wrapper.ToString());
				//output.PreContent.SetHtmlContent("<div class=''>");
				//output.PostContent.SetHtmlContent("</div>");

				return base.ProcessAsync(context, output);
			}
		}
	}

	[HtmlTargetElement("odropdownfor")]
	public class DropDownForHelper : TagHelper
	{
		private readonly IHtmlGenerator _generator;

		[ViewContext]
		public ViewContext ViewContext { get; set; }

		[HtmlAttributeName("asp-for")]
		public ModelExpression Expression { get; set; }

		[HtmlAttributeName("asp-items")]
		public IList<SelectListItem> SelectList { get; set; }

		public string Name { get; set; }
		public string InputCss { get; set; }
		public string PlaceHolder { get; set; }
		public int MinLength { get; set; }
		public int MaxLength { get; set; }
		public bool IsPassword { get; set; }
		public bool IsRequired { get; set; }
		public string RequiredCss { get; set; }
		public bool PasswordStrenght { get; set; }
		public string HasParent { get; set; }
		public string ParentCss { get; set; }
		public bool HasLabel { get; set; }
		public string LabelCss { get; set; }
		public string VModelName { get; set; }
		public bool VGetText { get; set; }

		public DropDownForHelper(IHtmlGenerator generator)
		{
			_generator = generator;
		}

		public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = string.Empty;
			output.TagMode = TagMode.SelfClosing;

			using (var sw = new StringWriter())
			{
				var wrapper = new StringWriter();
				var labelBuilder = new TagBuilder("label");
				var selectBuilder = new TagBuilder("select");
				var validationBuilder = new TagBuilder("validation");
				var format = context.AllAttributes["asp-format"] != null ? context.AllAttributes["asp-format"].Value.ToString() : "";
				var labelCss = context.AllAttributes["has-label"] != null ? context.AllAttributes["label-css"].Value.ToString() : "";
				var inputCss = context.AllAttributes["input-css"] != null ? context.AllAttributes["input-css"].Value.ToString() : "";
				var validationCss = Expression.Metadata.IsRequired || (context.AllAttributes["is-required"] != null && bool.Parse(context.AllAttributes["is-required"].Value.ToString())) ? context.AllAttributes["required-css"].Value.ToString() : "";

				if (Expression.Metadata.IsRequired)
				{
					inputCss += " requiredField";
				}

				// generate label if exist
				if (context.AllAttributes["has-label"] != null && bool.Parse(context.AllAttributes["has-label"].Value.ToString()))
				{
					labelBuilder = _generator.GenerateLabel(ViewContext, Expression.ModelExplorer, Expression.Name, Expression.Metadata.GetDisplayName(), new { @class = LabelCss });
					labelBuilder.WriteTo(sw, NullHtmlEncoder.Default);
				}

				// generate actual input
				selectBuilder = _generator.GenerateSelect(ViewContext, Expression.ModelExplorer, "Please select", Expression.Name, SelectList, false, new { @class = inputCss, PlaceHolder = Expression.Metadata.GetDisplayName() });

				foreach (var attribute in context.AllAttributes)
				{
					if (HtmlTagHelpers.IsAttributeAddible(attribute.Name))
					{
						if (attribute.Name == "v-model-name")
						{
							selectBuilder.Attributes.Remove("v-model");
							selectBuilder.Attributes.Add("v-model", attribute.Value.ToString());
						}
						else if (attribute.Name == "v-get-text")
						{
							selectBuilder.Attributes.Remove("data-gettext");
							selectBuilder.Attributes.Add("data-gettext", attribute.Value.ToString());
						}
						else
						{
							selectBuilder.Attributes.Add(attribute.Name, attribute.Value.ToString());
						}
					}
				}

				selectBuilder.WriteTo(sw, NullHtmlEncoder.Default);

				// generate validation if exist
				if (Expression.Metadata.IsRequired || (context.AllAttributes["is-required"] != null && bool.Parse(context.AllAttributes["is-required"].Value.ToString())))
				{
					validationBuilder = _generator.GenerateValidationMessage(ViewContext, Expression.ModelExplorer, Expression.Name, null, ViewContext.ValidationMessageElement, new { @class = validationCss });
					validationBuilder.WriteTo(sw, NullHtmlEncoder.Default);
				}

				// generate wrapper parent element if exist
				if (context.AllAttributes["has-parent"] != null && bool.Parse(context.AllAttributes["has-parent"].Value.ToString()))
				{
					wrapper.Write("<div class='" + context.AllAttributes["parent-css"].Value + "'>");
					wrapper.Write(sw.ToString());
					wrapper.Write("</div>");
				}

				output.Content.SetHtmlContent(wrapper.ToString());
				//output.PreContent.SetHtmlContent("<div class=''>");
				//output.PostContent.SetHtmlContent("</div>");

				return base.ProcessAsync(context, output);
			}
		}
	}
}
