namespace Greg.Xrm.RoleEditor.Model
{
	/// <summary>
	/// Represents a change on a given role configuration
	/// </summary>
	public interface IChangeOperation
	{
		string Text { get; }
	}
}
