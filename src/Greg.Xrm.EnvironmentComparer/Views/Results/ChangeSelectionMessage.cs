namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public class ChangeSelectionMessage
	{
		public ChangeSelectionMessage(int indexIncrement)
		{
			this.IndexIncrement = indexIncrement;
		}

		public int IndexIncrement { get; private set; }
	}
}
