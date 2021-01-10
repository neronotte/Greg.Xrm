using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentComparer.Engine.Config
{
	public class SkipAttributes : ISkipAttributeCriteria
	{
		public SkipAttributes(string[] attributesToSkip)
		{
			this.AttributesToSkip = attributesToSkip;
		}

		public IReadOnlyCollection<string> AttributesToSkip { get; }

		public bool ShouldSkip(string attributeName)
		{
			return this.AttributesToSkip.Any(_ => string.Equals(_, attributeName, System.StringComparison.OrdinalIgnoreCase));
		}
	}
}
