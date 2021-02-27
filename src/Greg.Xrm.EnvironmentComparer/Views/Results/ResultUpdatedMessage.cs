using Greg.Xrm.EnvironmentComparer.Engine;
using Microsoft.Xrm.Sdk;
using System;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public class ResultUpdatedMessage
	{
		public ResultUpdatedMessage(ObjectComparison<Entity> result)
		{
			this.Result = result ?? throw new ArgumentNullException(nameof(result));
		}

		public ObjectComparison<Entity> Result { get; }
	}
}
