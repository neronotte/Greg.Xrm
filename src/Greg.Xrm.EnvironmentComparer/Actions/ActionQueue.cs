using System.Collections;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Actions
{
	public class ActionQueue : IEnumerable<IAction>
	{
		private readonly Queue<IAction> actionQueue = new Queue<IAction>();

		public bool TryEnqueue(IAction action, out string errorMessage)
		{
			errorMessage = null;
			if (this.actionQueue.Contains(action))
			{
				errorMessage = "Action already present in the queue!";
				return false;
			}
			this.actionQueue.Enqueue(action);
			return true;
		}

		public IAction Dequeue()
		{
			return this.actionQueue.Dequeue();
		}

		public IAction Peek()
		{
			return this.actionQueue.Peek();
		}

		public void Clear()
		{
			this.actionQueue.Clear();
		}

		public IEnumerator<IAction> GetEnumerator()
		{
			return this.actionQueue.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int Count
		{
			get => this.actionQueue.Count;
		}
	}
}
