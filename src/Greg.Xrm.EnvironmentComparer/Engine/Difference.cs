using Microsoft.Xrm.Sdk;

namespace Greg.Xrm.EnvironmentComparer.Engine
{
	public class Difference
	{
		public string FieldName { get; set; }
		public object Value1 { get; set; }
		public object Value2 { get; set; }
		public string FormattedValue1 { get; set; }
		public string FormattedValue2 { get; set; }

		internal static Difference Create(string attributeName, Entity x, Entity y)
		{
			return new Difference
			{
				FieldName = attributeName,
				Value1 = x?[attributeName],
				FormattedValue1 = x?.GetFormattedValue(attributeName) ?? x?[attributeName]?.ToString() ?? "NULL",
				Value2 = y?[attributeName],
				FormattedValue2 = y?.GetFormattedValue(attributeName) ?? y?[attributeName]?.ToString() ?? "NULL",
			};
		}
	}
}
