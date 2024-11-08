using Greg.Xrm.RoleEditor.Model;
using OfficeOpenXml;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public static class ExcelExtensions
	{

		public static ExcelRange SetValue(this ExcelRange x, object value)
		{
			if (x == null) return x;
			x.Value = value;
			return x;
		}

		public static ExcelRange SetTooltip(this ExcelRange x, string tooltip)
		{
			if (x == null) return x;
			if (string.IsNullOrWhiteSpace(tooltip)) return x;
			x.AddComment(tooltip);
			return x;
		}

		public static ExcelRange SetTitle(this ExcelRange x)
		{
			if (x == null) return x;

			x.Style.Font.Size = 16;
			x.Style.Font.Bold = true;
			x.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#0e2841"));
			return x;
		}

		public static ExcelRange SetExplanatory(this ExcelRange x)
		{
			if (x == null) return x;

			x.Style.Font.Italic = true;
			x.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#7f7f7f"));
			return x;
		}

		public static ExcelRange Right(this ExcelRange x)
		{
			if (x == null) return x;
			x.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
			return x;
		}

		public static ExcelRange Center(this ExcelRange x)
		{
			if (x == null) return x;
			x.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
			return x;
		}

		public static string Description(this Level? level)
		{
			if (level == null) return null;

			FieldInfo fi = level.GetType().GetField(level.ToString());

			var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if (attributes != null && attributes.Length > 0) return attributes[0].Description;
			else return level.ToString();
		}

		public static string Description(this Level level)
		{
			FieldInfo fi = level.GetType().GetField(level.ToString());

			var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if (attributes != null && attributes.Length > 0) return attributes[0].Description;
			else return level.ToString();
		}

		public static int? ToInt(this Level? level)
		{
			if (level == null) return null;
			return (int)level.Value;
		}
		public static int? ToInt(this Level level)
		{
			return (int)level;
		}

		public static string ToEmoji(this Level? level)
		{
			if (level == null) return null;
			return ToEmoji(level.Value);
		}
		public static string ToEmoji(this Level level)
		{
			switch (level)
			{
				case Level.None: return "⚪";
				case Level.User: return "🔵";
				case Level.BusinessUnit: return "\U0001f7e1";
				case Level.ParentChild: return "\U0001f7e0";
				case Level.Organization: return "\U0001f7e2";
				default: return level.ToString();
			}
		}
	}
}
