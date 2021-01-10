using Greg.Xrm.EnvironmentComparer.Engine;
using Microsoft.Xrm.Sdk;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public class ResultUpdatedEventArgs
	{
		public ResultUpdatedEventArgs(ObjectComparison<Entity> result)
		{
			this.Result = result ?? throw new System.ArgumentNullException(nameof(result));
		}

		public ObjectComparison<Entity> Result { get; }
	}
}