using Greg.Xrm.Core;
using Greg.Xrm.Core.Views;
using Greg.Xrm.Views;
using System.Windows;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class PasteAllPrivilegesCommand : CommandBase<RoleEditorView>
	{
		private readonly RoleEditorViewModel viewModel;

		public PasteAllPrivilegesCommand(RoleEditorViewModel viewModel)
		{
			this.viewModel = viewModel;

			this.viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(this.viewModel.IsEnabled)
					|| e.PropertyName == nameof(this.viewModel.IsCustomizable))
				{
					CalculateCanExecute();
				}
			};
			CalculateCanExecute();
		}

		private void CalculateCanExecute()
		{
			this.CanExecute = this.viewModel.IsEnabled && this.viewModel.IsCustomizable;
		}

		protected override void ExecuteInternal(RoleEditorView arg)
		{
			var memento = MemoryClipboard.Retrieve<string>("role-memento");
			
			if (string.IsNullOrWhiteSpace(memento))
			{
				viewModel.SendNotification(NotificationType.Warning, "Clipboard is empty or contains no text.");
				return;
			}

			var result = viewModel.Model.MergeMemento(memento);
			viewModel.EvaluateDirty();
			viewModel.ForceViewRefresh();

			if (result.IsSuccess)
			{
				if (result.TablePrivilegesApplied > 0 || result.MiscPrivilegesApplied > 0)
				{
					viewModel.SendNotification(NotificationType.Success, result.GetSummaryMessage());
				}
				else
				{
					viewModel.SendNotification(NotificationType.Info, "Paste completed successfully, but no privileges were applied (all values were lower than or equal to existing values).");
				}
			}
			else
			{
				viewModel.SendNotification(NotificationType.Error, result.GetSummaryMessage());
			}
		}
	}
}
