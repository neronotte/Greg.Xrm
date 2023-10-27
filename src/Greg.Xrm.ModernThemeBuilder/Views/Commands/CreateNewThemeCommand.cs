using Greg.Xrm.Logging;
using Greg.Xrm.ModernThemeBuilder.Model;
using Greg.Xrm.ModernThemeBuilder.Views.Messages;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using System.ComponentModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Greg.Xrm.ModernThemeBuilder.Views.Commands
{
	public class CreateNewThemeCommand : CommandBase
	{
		private readonly MainViewModel viewModel;
		private readonly ILog log;

		public CreateNewThemeCommand(MainViewModel viewModel, ILog log)
		{
			viewModel.PropertyChanged += OnViewModelPropertyChanged;
			this.viewModel = viewModel;
			this.log = log;
		}

		private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != nameof(MainViewModel.Crm) 
			&& e.PropertyName != nameof(MainViewModel.CurrentSolution))
				return;

			this.CanExecute = viewModel.Crm != null && this.viewModel.CurrentSolution != null;
		}

		protected override void ExecuteInternal(object arg)
		{
			if (this.viewModel.CurrentSolution == null)
			{
				using (var dialog = new CreateTheme.FullDialog(this.viewModel.Crm, this.viewModel.Scheduler, log))
				{
					dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
					if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
						return;


					// TODO: here we have a problem of parallel concurrency, to fix
					this.viewModel.CurrentSolution = dialog.SelectedSolution;
					this.viewModel.Messenger.Send(new SolutionSelected(dialog.SelectedSolution));

					CreateTheme(dialog.ThemeName);
				}
			}
			else
			{
				using (var dialog = new CreateTheme.LightDialog(this.viewModel.CurrentSolution, this.viewModel.Crm, this.viewModel.Scheduler))
				{
					dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
					if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
						return;

					CreateTheme(dialog.ThemeName);
				}
			}
		}


		private void CreateTheme(string themeName)
		{
			var webResource = new WebResource();
			webResource.name = themeName;
			webResource.displayname = themeName;
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
