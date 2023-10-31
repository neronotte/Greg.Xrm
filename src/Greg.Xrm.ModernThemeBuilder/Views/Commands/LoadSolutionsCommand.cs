using Greg.Xrm.Logging;
using Greg.Xrm.ModernThemeBuilder.Model;
using Greg.Xrm.ModernThemeBuilder.Views.Messages;
using Greg.Xrm.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Greg.Xrm.ModernThemeBuilder.Views.Commands
{
	public class LoadSolutionsCommand : CommandBase
	{
		private readonly MainViewModel viewModel;
		private readonly ILog log;

		public LoadSolutionsCommand(MainViewModel viewModel, ILog log)
		{
			viewModel.PropertyChanged += OnViewModelPropertyChanged;
			this.viewModel = viewModel;
			this.log = log;
		}

		private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != nameof(MainViewModel.Crm))
				return;

			this.CanExecute = this.viewModel.Crm != null && this.viewModel.CurrentTheme != null;
		}


		protected override void ExecuteInternal(object arg)
		{
			Solution solution;
			using (var dialog = new SolutionDialog(this.viewModel.Crm, this.viewModel.Scheduler, this.log))
			{
				dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
				if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
					return;

				solution = dialog.SelectedSolution;
			}

			this.viewModel.CurrentSolution = solution;

			this.viewModel.Scheduler.Enqueue(new XrmToolBox.Extensibility.WorkAsyncInfo
			{
				Work = LoadSolutionComponents,
				Message = "Loading solution components...",
				PostWorkCallBack = OnSolutionComponentsLoaded,
				AsyncArgument = solution
			});
		}
		private void LoadSolutionComponents(BackgroundWorker worker, DoWorkEventArgs args)
		{
			var solution = args.Argument as Solution;
			if (solution == null)
			{
				args.Result = new List<SolutionComponent>();
				return;
			}

			var solutionComponentRepository = SolutionComponent.GetRepository(this.viewModel.Crm);
			var solutionComponents = solutionComponentRepository.GetSolutionComponentBySolutionId(solution.Id);
			args.Result = solutionComponents;
		}



		private void OnSolutionComponentsLoaded(RunWorkerCompletedEventArgs args)
		{
			if (args.Error != null)
			{
				MessageBox.Show("An error occurred while loading solution components: " + args.Error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				this.viewModel.Messenger.Send(new SolutionComponentLoaded(this.viewModel.CurrentSolution, new List<SolutionComponent>()));
				return;
			}

			var solutionComponents = args.Result as List<SolutionComponent>;

			this.viewModel.SolutionComponentList.Clear();
			this.viewModel.SolutionComponentList.AddRange(solutionComponents);

			this.viewModel.Messenger.Send(new SolutionComponentLoaded(this.viewModel.CurrentSolution, solutionComponents));
		}
	}
}
