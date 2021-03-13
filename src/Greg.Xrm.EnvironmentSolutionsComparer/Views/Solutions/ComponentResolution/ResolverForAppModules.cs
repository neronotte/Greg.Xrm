using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution
{


	public class ResolverForAppModules : ResolverCommon
	{
		public ResolverForAppModules(ILog log)
		: base(log)
		{
		}

		public override SolutionComponentType ComponentType => SolutionComponentType.AppModule;

		public override string ComponentName => "appmodule";
	}
}
