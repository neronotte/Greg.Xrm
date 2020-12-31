using Greg.Xrm.EnvironmentComparer.Actions;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Messaging
{
	public class SubmitActionMessage
	{
		public SubmitActionMessage(IReadOnlyCollection<IAction> actions)
		{
			this.Actions = actions ?? throw new System.ArgumentNullException(nameof(actions));
			if (actions.Count == 0)
				throw new System.ArgumentNullException(nameof(actions));
		}

		public IReadOnlyCollection<IAction> Actions { get; }
	}
}
