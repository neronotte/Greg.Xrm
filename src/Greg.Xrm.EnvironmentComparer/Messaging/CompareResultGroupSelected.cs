using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Messaging
{
	public class CompareResultGroupSelected
	{
		public CompareResultGroupSelected(IReadOnlyCollection<Model.Comparison<Entity>> results)
		{
			this.Results = results ?? throw new ArgumentNullException(nameof(results));
		}


		public IReadOnlyCollection<Model.Comparison<Entity>> Results { get; }
	}
}
