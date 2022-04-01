using Microsoft.Xrm.Sdk.Metadata;
using System.Globalization;
using System.Text;

namespace Greg.Xrm.ConstantsExtractor.Core
{
	public static class Extensions
	{
		public static string RemoveSpecialCharacters(this string text)
		{
			if (string.IsNullOrWhiteSpace(text)) return text;

			var stringBuilder = new StringBuilder();
			foreach (char ch in text)
			{
				if (ch >= 'A' && ch <= 'Z' || ch >= 'a' && ch <= 'z' || (ch >= '0' && ch <= '9' || ch == '_'))
					stringBuilder.Append(ch);
			}
			return stringBuilder.ToString();
		}

		public static string RemoveDiacritics(this string text)
		{
			if (string.IsNullOrWhiteSpace(text)) return text;

			var str = text.Normalize(NormalizationForm.FormD);
			var stringBuilder = new StringBuilder(str.Length);
			for (int index = 0; index < str.Length; ++index)
			{
				char ch = str[index];
				if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
					stringBuilder.Append(ch);
			}
			return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
		}

		public static string GetGlobalOptionSetName(this OptionSetMetadataBase optionSetMetadata)
		{
			if (optionSetMetadata == null) return null;

			return optionSetMetadata.IsGlobal.HasValue && optionSetMetadata.IsGlobal.Value ? optionSetMetadata.Name : null;
		}
	}
}
