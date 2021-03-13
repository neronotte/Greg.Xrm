using Greg.Xrm.Logging;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution
{
	public class ResolverForFieldSecurityProfiles : ResolverCommon
	{
		public ResolverForFieldSecurityProfiles(ILog log) : base(log)
		{
		}

		public override SolutionComponentType ComponentType => SolutionComponentType.FieldSecurityProfile;

		public override string ComponentName => "fieldsecurityprofile";
	}
}
