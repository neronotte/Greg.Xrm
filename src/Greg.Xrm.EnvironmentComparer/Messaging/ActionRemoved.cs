using Greg.Xrm.EnvironmentComparer.Actions;
using System;

namespace Greg.Xrm.EnvironmentComparer.Messaging
{

	/// <summary>
	/// Occours when an action is manually removed from the list of the actions to perform
	/// </summary>
	public class ActionRemoved
	{
		public ActionRemoved(IAction action)
		{
			this.Action = action ?? throw new ArgumentNullException(nameof(action));
		}

		public IAction Action { get; }
	}
}
