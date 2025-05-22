namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class IsInheritedValue
	{
		public IsInheritedValue(int isInherited, string label)
		{
			Value = isInherited;
			Label = label;
		}

		public int Value { get; }
		public string Label { get; }

		public override string ToString()
		{
			return Label;
		}
	}
}
