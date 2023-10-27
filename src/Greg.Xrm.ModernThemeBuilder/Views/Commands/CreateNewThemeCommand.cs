using Greg.Xrm.ModernThemeBuilder.Model;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using System.ComponentModel;

namespace Greg.Xrm.ModernThemeBuilder.Views.Commands
{
	public class CreateNewThemeCommand : CommandBase
	{
		private MainViewModel viewModel;

		public CreateNewThemeCommand(MainViewModel viewModel)
		{
			viewModel.PropertyChanged += OnViewModelPropertyChanged;
			this.viewModel = viewModel;
		}

		private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != nameof(MainViewModel.Crm)
				&& e.PropertyName != nameof(MainViewModel.CurrentSolution))
				return;

			this.CanExecute = viewModel.Crm != null && viewModel.CurrentSolution != null;
		}

		protected override void ExecuteInternal(object arg)
		{
			if (this.viewModel.CurrentSolution == null) return;

			using (var dialog = new CreateTheme.LightDialog(this.viewModel.CurrentSolution, this.viewModel.Crm, this.viewModel.Scheduler))
			{
				dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
				if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
					return;

				var webResource = new WebResource();
				webResource.name = dialog.ThemeName;
				webResource.displayname = dialog.ThemeName;
				webResource.description = "Modern theme for Power Apps";
				webResource.webresourcetype = new OptionSetValue((int)WebResourceType.XML);

				var palette = AppHeaderColors.Default;
				webResource.SetContentFromString(palette.ToXmlString());

				var solutionComponent = new SolutionComponent(webResource);
				solutionComponent.UpdatePalette(palette);

				this.viewModel.Messenger.Send(new SolutionComponentAdded(solutionComponent));
			}
		}
	}
}
