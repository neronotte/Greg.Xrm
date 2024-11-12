using Greg.Xrm.RoleEditor.Model;
using Microsoft.Xrm.Sdk.Metadata;
using OfficeOpenXml;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public static class ExcelExtensions
	{
		public static ExcelRange SetValidRange(this ExcelRange range, MiscModel model)
		{
			if (range == null) return range;

			var validityMatrix = model.GetPrivilegeLevelValidityMatrix();
			var validation = range.Worksheet.DataValidations.AddListValidation(range.Address);
			validation.AllowBlank = true;
			validation.Error = "Invalid value";
			validation.ErrorStyle = OfficeOpenXml.DataValidation.ExcelDataValidationWarningStyle.stop;
			validation.ShowErrorMessage = true;

			for (int i = 0; i < validityMatrix.Length; i++)
			{
				if (!validityMatrix[i]) continue;
				validation.Formula.Values.Add(i.ToString());
			}
			return range;
		}

		public static ExcelRange SetValidRange(this ExcelRange range, TableModel model, PrivilegeType privilegeType)
		{
			if (range == null) return range;

			var validityMatrix = model.GetPrivilegeLevelValidityMatrix(privilegeType);
			if (validityMatrix.Length == 0)
			{
				// no valie is allowed on this cell
				range.Style.Locked = true;
				return range;
			}


			var validation = range.Worksheet.DataValidations.AddListValidation(range.Address);
			validation.AllowBlank = true;
			validation.Error = "Invalid value";
			validation.ErrorStyle = OfficeOpenXml.DataValidation.ExcelDataValidationWarningStyle.stop;
			validation.ShowErrorMessage = true;

			for (int i = 0; i < validityMatrix.Length; i++)
			{
				if (!validityMatrix[i]) continue;
				validation.Formula.Values.Add(i.ToString());
			}
			range.Style.Locked = false;
			return range;
		}


		public static string GetTableNameForExcelFile(this TableModel model)
		{
			if (model == null) return null;
			if (string.Equals(model.Name, "Activity")) return "activity";
			return model.Tooltip;
		}

		public static string GenerateSignature(this string roleName)
		{
			using (var sha = SHA256.Create())
			{
				return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes($"Greg|{roleName}|v1")));
			}
		}

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

		public static ExcelRange Hidden(this ExcelRange range)
		{
			range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
			range.Style.Fill.BackgroundColor.SetColor(Color.White);
			range.Style.Font.Color.SetColor(Color.White);
			return range;
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

		public static ExcelRange SetProtection(this ExcelRange x, bool locked)
		{
			if (x == null) return x;
			x.Style.Locked = locked;
			return x;
		}
	}
}
