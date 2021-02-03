using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Greg.Xrm.DataModelWikiEditor.Generator
{
	public class PageGenerator
	{
		public string Generate(string folderName, string entityName, EntityMetadata[] entityList, int locale, string[] fieldExclusions)
		{
			if (fieldExclusions == null) fieldExclusions = Array.Empty<string>();

			var sb = new StringBuilder();

			var entity = entityList.FirstOrDefault(x => string.Equals(x.SchemaName, entityName, StringComparison.OrdinalIgnoreCase));
			if (entity == null)
			{
				GenerateEmpty(sb, entityName);
			}
			else
			{
				GeneratePage(sb, entity, locale, fieldExclusions);
			}


			var fullPath = Path.Combine(folderName, entityName + ".md");

			using (var writer = new StreamWriter(new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write)))
			{
				writer.Write(sb.ToString());
				writer.Flush();
			}

			return entityName;
		}



		private void GenerateEmpty(StringBuilder sb, string entityName)
		{
			sb.Append("# ").Append(entityName).Append(" (NOT FOUND)").AppendLine();
			sb.AppendLine();
			sb.Append("The specified entity has not been found in the environment");
		}




		private void GeneratePage(StringBuilder sb, EntityMetadata entity, int locale, string[] defaultExclusions)
		{
			var entityName = entity.DisplayName.GetLocalized(locale);
			var entityDescription = entity.Description.GetLocalized(locale);

			sb.Append("# ").Append(entityName).Append(" (").Append(entity.SchemaName).Append(")").AppendLine();
			sb.AppendLine();
			sb.AppendLine("## Fields").AppendLine();

			sb.Append("| Data                 | Info |").AppendLine();
			sb.Append("|----------------------|------|").AppendLine();
			sb.Append("| **Display name**     | ").Append(entityName).Append(" |").AppendLine();
			sb.Append("| **Schema name**      | ").Append(entity.SchemaName).Append(" |").AppendLine();
			sb.Append("| **Object type code** | ").Append(entity.ObjectTypeCode).Append(" |").AppendLine();
			sb.Append("| **Description**      | ").Append(entityDescription).Append(" |").AppendLine();
			sb.AppendLine();

			sb.AppendLine("## Fields").AppendLine();

			sb.AppendLine(FieldRow.Header);

			var rows = new List<FieldRow>();
			foreach (var attribute in entity.Attributes.OrderBy(_ => _.LogicalName))
			{
				if (defaultExclusions.Any(_ => string.Equals(_, attribute.LogicalName, StringComparison.OrdinalIgnoreCase)))
					continue; // skipping field
				if (attribute.IsLogical ?? false)
					continue;

				var row = new FieldRow
				{
					LogicalName = attribute.LogicalName,
					DisplayName = attribute.DisplayName.LocalizedLabels.Select(x => $"{x.LanguageCode}) \"{x.Label}\"").Join("<br>"),
					AttributeType = attribute.AttributeType?.ToString(),
					Description = attribute.Description.GetLocalized(locale),
					RequirementMode = GetRequiredLevel(attribute.RequiredLevel.Value),
					Format = GetFormat(attribute, locale)
				};

				sb.Append(row).AppendLine();
			}
		}






		private string GetFormat(AttributeMetadata attribute, int locale)
		{
			var sb = new StringBuilder();
			if (attribute is BigIntAttributeMetadata a2)
			{
				sb.Append("Min: ").Append(a2.MinValue).Append("<br/>");
				sb.Append("Max: ").Append(a2.MaxValue);
			}
			else if (attribute is BooleanAttributeMetadata a3)
			{
				sb.Append("True: ").Append(a3.OptionSet.TrueOption.Label.GetLocalized(locale)).Append(" (").Append(a3.OptionSet.TrueOption.Value).Append(")<br/>");
				sb.Append("False: ").Append(a3.OptionSet.FalseOption.Label.GetLocalized(locale)).Append(" (").Append(a3.OptionSet.FalseOption.Value).Append(")");
			}
			else if (attribute is DateTimeAttributeMetadata a4)
			{
				a4.ToString();
			}
			else if (attribute is DecimalAttributeMetadata a5)
			{
				sb.Append("Min: ").Append(a5.MinValue).Append("<br/>");
				sb.Append("Max: ").Append(a5.MaxValue);
			}
			else if (attribute is DoubleAttributeMetadata a6)
			{
				sb.Append("Min: ").Append(a6.MinValue).Append("<br/>");
				sb.Append("Max: ").Append(a6.MaxValue);
			}
			else if (attribute is FileAttributeMetadata a7)
			{
				a7.ToString();
			}
			else if (attribute is ImageAttributeMetadata a8)
			{
				a8.ToString();
			}
			else if (attribute is IntegerAttributeMetadata a9)
			{
				sb.Append("Min: ").Append(a9.MinValue).Append("<br/>");
				sb.Append("Max: ").Append(a9.MaxValue);
			}
			else if (attribute is LookupAttributeMetadata a10)
			{
				a10.ToString();
			}
			else if (attribute is MemoAttributeMetadata a11)
			{
				sb.Append("Max len: ").Append(a11.MaxLength).Append("<br/>");
				sb.Append("Format: ").Append(a11.Format);
			}
			else if (attribute is MoneyAttributeMetadata a12)
			{
				a12.ToString();
			}
			else if (attribute is MultiSelectPicklistAttributeMetadata a13)
			{
				sb.Append("Multiselect<br/>");
				if (!string.IsNullOrWhiteSpace(a13.OptionSet.Name))
				{
					sb.Append(a13.OptionSet.Name).Append("<br/>");
				}
				foreach (var o in a13.OptionSet.Options)
				{
					sb.Append(" - ").Append(o.Label.GetLocalized(locale)).Append(": ").Append(o.Value).Append("<br/>");
				}
			}
			else if (attribute is PicklistAttributeMetadata a14)
			{
				if (!string.IsNullOrWhiteSpace(a14.OptionSet.Name))
				{
					sb.Append(a14.OptionSet.Name).Append("<br/>");
				}
				foreach (var o in a14.OptionSet.Options)
				{
					sb.Append(" - ").Append(o.Label.GetLocalized(locale)).Append(": ").Append(o.Value).Append("<br/>");
				}
			}
			else if (attribute is StateAttributeMetadata a15)
			{
				if (!string.IsNullOrWhiteSpace(a15.OptionSet.Name))
				{
					sb.Append(a15.OptionSet.Name).Append("<br/>");
				}
				foreach (var o in a15.OptionSet.Options)
				{
					sb.Append(" - ").Append(o.Label.GetLocalized(locale)).Append(": ").Append(o.Value).Append("<br/>");
				}
			}
			else if (attribute is StatusAttributeMetadata a16)
			{
				if (!string.IsNullOrWhiteSpace(a16.OptionSet.Name))
				{
					sb.Append(a16.OptionSet.Name).Append("<br/>");
				}
				foreach (var o in a16.OptionSet.Options)
				{
					sb.Append(" - ").Append(o.Label.GetLocalized(locale)).Append(": ").Append(o.Value).Append("<br/>");
				}
			}
			else if (attribute is StringAttributeMetadata a17)
			{
				sb.Append("Max len: ").Append(a17.MaxLength).Append("<br/>");
				sb.Append("Format: ").Append(a17.Format);
			}
			return sb.ToString();
		}




		private string GetRequiredLevel(AttributeRequiredLevel value)
		{
			switch (value)
			{
				case AttributeRequiredLevel.ApplicationRequired: return "Required";
				case AttributeRequiredLevel.SystemRequired: return "Required";
				case AttributeRequiredLevel.Recommended: return "Recommended";
				case AttributeRequiredLevel.None: return "Optional";
			}
			return null;
		}
	}
}
