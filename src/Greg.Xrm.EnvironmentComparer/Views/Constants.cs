using Greg.Xrm.EnvironmentComparer.Engine;
using System.Drawing;

namespace Greg.Xrm.EnvironmentComparer.Views
{
	public static class Constants
	{

		public static Color BackColorForEquals { get; } = Color.FromArgb(198, 239, 206);
		public static Color BackColorForLeftMissing { get; } = Color.FromArgb(248, 203, 173);
		public static Color BackColorForRightMissing { get; } = Color.FromArgb(189, 215, 238);
		public static Color BackColorForMatchingButDifferent { get; } = Color.FromArgb(255, 199, 206);
		public static Color BackColorDefault { get; } = Color.FromArgb(255, 235, 156);
		public static Color BackColorForActioned { get; } = Color.FromArgb(219, 219, 219);

		public static Color GetColor(this ObjectComparisonResult result)
		{
			switch (result)
			{
				case ObjectComparisonResult.Equals:
					return BackColorForEquals;

				case ObjectComparisonResult.MatchingButDifferent:
					return BackColorForMatchingButDifferent;

				case ObjectComparisonResult.LeftMissing:
					return BackColorForLeftMissing;

				case ObjectComparisonResult.RightMissing:
					return BackColorForRightMissing;

				default:
					return BackColorDefault;

			}
		}
		public static string GetMessage(this ObjectComparisonResult result)
		{
			switch (result)
			{
				case ObjectComparisonResult.Equals:
					return "The record is present in both environments, and the values of the attributes match";

				case ObjectComparisonResult.MatchingButDifferent:
					return "The record is present in both environments, but the values of some attribute doesn't matches. Review unmatching attributes below.";

				case ObjectComparisonResult.LeftMissing:
					return "The record is present only on {1} (missing on {0})";

				case ObjectComparisonResult.RightMissing:
					return "The record is present only on {0} (missing on {1})";

				default:
					return string.Empty;

			}
		}
	}
}
