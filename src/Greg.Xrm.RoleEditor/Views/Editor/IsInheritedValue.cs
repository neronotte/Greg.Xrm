namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class IsInheritedValue
	{
        public IsInheritedValue(bool isInherited, string label)
        {
			IsInherited = isInherited;
			Label = label;
		}

		public bool IsInherited { get; }
		public string Label { get; }

		public override string ToString()
		{
			return Label;
		}
	}
}
