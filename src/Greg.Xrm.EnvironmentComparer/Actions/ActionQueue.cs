using System.ComponentModel;

namespace Greg.Xrm.EnvironmentComparer.Actions
{
	public class ActionQueue : BindingList<IAction>
	{

		public ActionQueue()
		{
		}

		public bool TryEnqueue(IAction action, out string errorMessage)
		{
			errorMessage = null;
			if (this.Contains(action))
			{
				errorMessage = "Action already present in the queue!";
				return false;
			}
			this.Add(action);
			return true;
		}

		public IAction Dequeue()
		{
			if (this.Count == 0) return null;


			var action = this[0];
			this.RemoveAt(0);
			return action;
		}

		public IAction Peek()
		{
			if (this.Count == 0) return null;
			return this[0];
		}
	}
}
