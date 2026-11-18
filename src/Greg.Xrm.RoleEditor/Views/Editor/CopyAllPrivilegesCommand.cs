using Greg.Xrm.Core;
using Greg.Xrm.Views;
using System.Windows;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class CopyAllPrivilegesCommand : CommandBase<RoleEditorView>
	{
		private readonly RoleEditorViewModel viewModel;

		public CopyAllPrivilegesCommand(RoleEditorViewModel viewModel)
		{
			this.viewModel = viewModel;
		}

		protected override void ExecuteInternal(RoleEditorView arg)
		{
			var memento = viewModel.Model.GetMemento();
			MemoryClipboard.Store("role-memento", memento);
			MessageBox.Show("All privileges have been copied to the clipboard.", "Copy All Privileges", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}
}
