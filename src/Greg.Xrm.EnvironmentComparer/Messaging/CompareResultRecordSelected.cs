using Greg.Xrm.EnvironmentComparer.Model;
using Microsoft.Xrm.Sdk;
using System;

namespace Greg.Xrm.EnvironmentComparer.Messaging
{
	public class CompareResultRecordSelected
	{
		public CompareResultRecordSelected(Model.Comparison<Entity> result)
		{
			this.Result = result ?? throw new ArgumentNullException(nameof(result));
		}

		public Model.Comparison<Entity> Result { get; }
	}

	public class HighlightResultRecord
	{
	}
}
