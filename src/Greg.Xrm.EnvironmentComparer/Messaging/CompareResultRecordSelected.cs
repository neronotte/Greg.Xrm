using Greg.Xrm.EnvironmentComparer.Engine;
using Microsoft.Xrm.Sdk;
using System;

namespace Greg.Xrm.EnvironmentComparer.Messaging
{
	public class CompareResultRecordSelected
	{
		public CompareResultRecordSelected(ObjectComparison<Entity> result)
		{
			this.Result = result ?? throw new ArgumentNullException(nameof(result));
		}

		public ObjectComparison<Entity> Result { get; }
	}

	public class HighlightResultRecord
	{
	}
}
