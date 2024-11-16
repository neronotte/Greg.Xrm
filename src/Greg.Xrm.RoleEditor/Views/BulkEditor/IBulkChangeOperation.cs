using Greg.Xrm.RoleEditor.Model;

namespace Greg.Xrm.RoleEditor.Views.BulkEditor
{
	public interface IBulkChangeOperation : IChangeOperation
	{
		Role Role { get; }
		string OperationType { get; }
	}
}
