using Greg.Xrm.Logging;
using Greg.Xrm.ModernThemeBuilder.Model;
using Greg.Xrm.Views;
using Microsoft.Crm.Sdk.Messages;
using System.ComponentModel;
using System.Windows;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.ModernThemeBuilder.Views.Commands
{
	public class SaveThemeCommand : CommandBase
	{
		private readonly MainViewModel viewModel;
		private readonly ILog log;

		public SaveThemeCommand(MainViewModel viewModel, ILog log)
        {
			this.viewModel = viewModel ?? throw new System.ArgumentNullException(nameof(viewModel));
			this.log = log ?? throw new System.ArgumentNullException(nameof(log));
			this.viewModel.PropertyChanged += OnViewModelPropertyChanged;
			this.viewModel.Messenger.Register<SolutionComponentChanged>(OnSolutionComponentChanged);
		}

		private void OnSolutionComponentChanged(SolutionComponentChanged msg)
		{
			this.CanExecute = this.viewModel.Crm != null
				&& this.viewModel.CurrentSolution != null
				&& this.viewModel.CurrentSolutionComponent != null
				&& this.viewModel.CurrentSolutionComponent.IsDirty;
		}

		private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != nameof(MainViewModel.Crm)
				&& e.PropertyName != nameof(MainViewModel.CurrentSolution)
				&& e.PropertyName != nameof(MainViewModel.CurrentSolutionComponent))
				return;


			this.CanExecute = this.viewModel.Crm != null
				&& this.viewModel.CurrentSolution != null 
				&& this.viewModel.CurrentSolutionComponent != null
				&& this.viewModel.CurrentSolutionComponent.IsDirty;
		}

		protected override void ExecuteInternal(object arg)
		{
			this.viewModel.Scheduler.Enqueue(new WorkAsyncInfo
			{
				AsyncArgument = this.viewModel.CurrentSolutionComponent,
				Work = SaveWorkAsync,
				PostWorkCallBack = SaveWorkCompleted,
				Message = "Saving theme, please wait...",
			});
		}



		private void SaveWorkAsync(BackgroundWorker worker, DoWorkEventArgs args)
		{
			var solutionComponent = (SolutionComponent)args.Argument;

			this.log.Debug($"Saving webresource {solutionComponent.WebResource.name}");

			solutionComponent.WebResource.SaveOrUpdate(this.viewModel.Crm);

			if (solutionComponent.IsNew)
			{
				this.log.Debug($"Adding web resource to solution <{this.viewModel.CurrentSolution}>");
				var solutionComponentRepository = SolutionComponent.GetRepository(this.viewModel.Crm);
				solutionComponentRepository.Create(solutionComponent, this.viewModel.CurrentSolution);
			}

			if (this.viewModel.CurrentTheme == solutionComponent.WebResource.name)
			{
				this.log.Debug("The web resource is the current theme, we need to publish all to apply the changes");

				var request2 = new PublishAllXmlRequest();
				this.viewModel.Crm.Execute(request2);
			}
			

			args.Result = solutionComponent;
		}



		private void SaveWorkCompleted(RunWorkerCompletedEventArgs args)
		{
			if (args.Error != null)
			{
				this.log.Error(args.Error.Message, args.Error);
				MessageBox.Show(args.Error.Message, "Error saving theme", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			var solutionComponent = args.Result as SolutionComponent;

			this.log.Info($"Webresource {solutionComponent.WebResource.name} saved successfully");
			solutionComponent.IsDirty = false;
			this.viewModel.Messenger.Send(new SolutionComponentChanged(solutionComponent));
		}
	}
}
