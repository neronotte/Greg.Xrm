using Greg.Xrm.Logging;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution
{
	public class ResolverForPluginAssemblies : ResolverCommon
	{
		public ResolverForPluginAssemblies(ILog log)
		: base(log)
		{
		}

		public override SolutionComponentType ComponentType => SolutionComponentType.PluginAssembly;

		public override string ComponentName => "pluginassembly";
	}
}
