using Greg.Xrm.Logging;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution
{
	public class ResolverForDisplayStrings : ResolverCommon
	{
		public ResolverForDisplayStrings(ILog log)
		: base(log)
		{
		}

		public override SolutionComponentType ComponentType => SolutionComponentType.DisplayString;

		public override string ComponentName => "displaystring";

		public override string ComponentLabelField => "displaystringkey";
	}
}
