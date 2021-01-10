using Greg.Xrm.EnvironmentComparer.Engine;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Messaging
{
	public class CompareResultGroupSelected
	{
		public CompareResultGroupSelected(IReadOnlyCollection<ObjectComparison<Entity>> results)
		{
			this.Results = results ?? throw new ArgumentNullException(nameof(results));
		}


		public IReadOnlyCollection<ObjectComparison<Entity>> Results { get; }
	}
}
