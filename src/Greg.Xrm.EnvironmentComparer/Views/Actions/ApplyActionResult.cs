using Greg.Xrm.EnvironmentComparer.Actions;
using System;

namespace Greg.Xrm.EnvironmentComparer.Views.Actions
{
	public class ApplyActionResult
	{
		public ApplyActionResult(IAction action, Exception exception = null)
		{
			this.Action = action ?? throw new ArgumentNullException(nameof(action));
			this.Exception = exception;
		}


		public IAction Action { get; }

		public bool Succeeded => this.Exception == null;

		public Exception Exception { get; }
	}
}
