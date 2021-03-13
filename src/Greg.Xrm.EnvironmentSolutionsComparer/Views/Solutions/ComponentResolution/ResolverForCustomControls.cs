using Greg.Xrm.Logging;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution
{
	public class ResolverForCustomControls : ResolverCommon
	{
		public ResolverForCustomControls(ILog log)
		: base(log)
		{
		}

		public override SolutionComponentType ComponentType => SolutionComponentType.CustomControl;

		public override string ComponentName => "customcontrol";
	}
}
