using System;
using System.Text;

namespace Greg.Xrm.DataModelWikiEditor.Generator
{
	public class FieldRow
	{
		public string LogicalName { get; set; }
		public string DisplayName { get; set; }
		public string AttributeType { get; set; }
		public string Description { get; set; }
		public string RequirementMode { get; set; }
		public string Format { get; set; }


		public static string Header => $"| Logical name | Display name | Type | Format | Requirement | Description |{Environment.NewLine}|---|---|---|---|---|---|";

		public override string ToString()
		{
			var sb = new StringBuilder();

			sb.Append("| **");
			sb.Append(LogicalName);
			sb.Append("** | ");
			sb.Append(DisplayName);
			sb.Append(" | ");
			sb.Append(AttributeType);
			sb.Append(" | ");
			sb.Append(Format);
			sb.Append(" | ");
			sb.Append(RequirementMode);
			sb.Append(" | ");
			sb.Append(Description);
			sb.Append(" | ");


			return sb.ToString();
		}
	}
}
