using Microsoft.Xrm.Sdk;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Model
{
	[System.Serializable]
	public class CompareResult : Dictionary<string, IReadOnlyCollection<Comparison<Entity>>>
	{
	}
}
