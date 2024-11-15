using System.ComponentModel;

namespace Greg.Xrm.RoleEditor.Views.BulkEditor.Model
{
	public interface IEditorChild : INotifyPropertyChanged
	{
		string Name { get; }
		object Parent { get; set; }

		bool IsDirty { get; }
	}
}
