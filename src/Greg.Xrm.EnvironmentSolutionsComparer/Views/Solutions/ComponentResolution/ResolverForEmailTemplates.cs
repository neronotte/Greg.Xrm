using Greg.Xrm.Logging;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution
{
	public class ResolverForEmailTemplates : ResolverCommon
	{
		public ResolverForEmailTemplates(ILog log)
		: base(log)
		{
		}

		public override SolutionComponentType ComponentType => SolutionComponentType.EmailTemplate;

		public override string ComponentName => "template";

		public override string ComponentLabelField => "title";
	}
}
