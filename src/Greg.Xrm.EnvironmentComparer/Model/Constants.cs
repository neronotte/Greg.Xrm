using System.Collections.Generic;
using System.Drawing;

namespace Greg.Xrm.EnvironmentComparer.Model
{
	public static class Constants
	{
		public static IReadOnlyCollection<string> AttributesToIgnore { get; } = new[] {
			"ownerid",
			"createdby",
			"createdonbehalfby",
			"createdon",
			"modifiedby",
			"modifiedon",
			"owninguser",
			"owningbusinessunit",
			"importsequencenumber",
			"modifiedonbehalfby",
			"overriddencreatedon",
			"timezoneruleversionnumber",
			"utcconversiontimezonecode",
			"versionnumber"
		};


		public static Color BackColorForEquals { get; } = Color.FromArgb(198, 239, 206);
		public static Color BackColorForLeftMissing { get; } = Color.FromArgb(248, 203, 173);
		public static Color BackColorForRightMissing { get; } = Color.FromArgb(189, 215, 238);
		public static Color BackColorForMatchingButDifferent { get; } = Color.FromArgb(255, 199, 206);
		public static Color BackColorDefault { get; } = Color.FromArgb(255, 235, 156);

		public static Color GetColor(this RecordComparisonResult result)
		{
			switch (result)
			{
				case RecordComparisonResult.Equals:
					return BackColorForEquals;

				case RecordComparisonResult.MatchingButDifferent:
					return BackColorForMatchingButDifferent;

				case RecordComparisonResult.LeftMissing:
					return BackColorForLeftMissing;

				case RecordComparisonResult.RightMissing:
					return BackColorForRightMissing;

				default:
					return BackColorDefault;

			}
		}
		public static string GetMessage(this RecordComparisonResult result)
		{
			switch (result)
			{
				case RecordComparisonResult.Equals:
					return "The record is present in both environments, and the values of the attributes match";

				case RecordComparisonResult.MatchingButDifferent:
					return "The record is present in both environments, but the values of some attribute doesn't matches. Review unmatching attributes below.";

				case RecordComparisonResult.LeftMissing:
					return "The record is present only on {0}";

				case RecordComparisonResult.RightMissing:
					return "The record is present only on {1}";

				default:
					return string.Empty;

			}
		}
	}
}
