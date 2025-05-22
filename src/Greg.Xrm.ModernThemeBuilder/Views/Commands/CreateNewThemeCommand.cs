using Greg.Xrm.Logging;
using Greg.Xrm.ModernThemeBuilder.Model;
using Greg.Xrm.ModernThemeBuilder.Views.Messages;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

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
			if (e.PropertyName != nameof(MainViewModel.Crm))
				return;

			this.CanExecute = viewModel.Crm != null;
		}

		protected override void ExecuteInternal(object arg)
		{
			if (this.viewModel.CurrentSolution == null)
			{
				FullExperience();
			}
			else
			{
				LightExperience();
			}
		}

		private void LightExperience()
		{
			using (var dialog = new CreateTheme.LightDialog(this.viewModel.CurrentSolution, this.viewModel.Crm, this.viewModel.Scheduler))
			{
				dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
				if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
					return;

				CreateTheme(dialog.ThemeName);
			}
		}


		private void FullExperience()
		{
			Solution solution;
			string themeName;
			using (var dialog = new CreateTheme.FullDialog(this.viewModel.Crm, this.viewModel.Scheduler, log))
			{
				dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
				if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
					return;


				solution = dialog.SelectedSolution;
				themeName = dialog.ThemeName;
			}

			this.viewModel.CurrentSolution = solution;

			this.viewModel.Scheduler.Enqueue(new XrmToolBox.Extensibility.WorkAsyncInfo
			{
				Work = LoadSolutionComponents,
				Message = "Loading solution components...",
				PostWorkCallBack = OnSolutionComponentsLoaded,
				AsyncArgument = new RunArgs { Solution = solution, ThemeName = themeName }
			});
		}

		private void LoadSolutionComponents(BackgroundWorker worker, DoWorkEventArgs args)
		{
			var d = args.Argument as RunArgs;
			if (d.Solution == null)
			{
				args.Result = new List<SolutionComponent>();
				return;
			}



			var solutionComponentRepository = SolutionComponent.GetRepository(this.viewModel.Crm);
			var solutionComponents = solutionComponentRepository.GetSolutionComponentBySolutionId(d.Solution.Id);
			args.Result = new RunResult { SolutionComponents = solutionComponents, ThemeName = d.ThemeName };
		}



		private void OnSolutionComponentsLoaded(RunWorkerCompletedEventArgs args)
		{
			if (args.Error != null)
			{
				MessageBox.Show("An error occurred while loading solution components: " + args.Error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				this.viewModel.Messenger.Send(new SolutionComponentLoaded(this.viewModel.CurrentSolution, new List<SolutionComponent>()));
				return;
			}

			var result = args.Result as RunResult;
			var solutionComponents = result.SolutionComponents;
			var themeName = result.ThemeName;

			this.viewModel.SolutionComponentList.Clear();
			this.viewModel.SolutionComponentList.AddRange(solutionComponents);

			this.viewModel.Messenger.Send(new SolutionComponentLoaded(this.viewModel.CurrentSolution, solutionComponents));
			CreateTheme(themeName);
		}








		private void CreateTheme(string themeName)
		{
			var webResource = new WebResource
			{
				name = themeName,
				displayname = themeName,
				description = "Modern theme for Power Apps",
				webresourcetype = new OptionSetValue((int)WebResourceType.XML)
			};

			var palette = AppHeaderColors.Default;
			webResource.SetContentFromString(palette.ToXmlString());

			var solutionComponent = new SolutionComponent(webResource);
			solutionComponent.UpdatePalette(palette);

			this.viewModel.Messenger.Send(new SolutionComponentAdded(solutionComponent));
		}


		class RunArgs
		{
			public Solution Solution { get; set; }
			public string ThemeName { get; set; }
		}



		class RunResult
		{
			public List<SolutionComponent> SolutionComponents { get; set; }
			public string ThemeName { get; set; }
		}
	}
}
