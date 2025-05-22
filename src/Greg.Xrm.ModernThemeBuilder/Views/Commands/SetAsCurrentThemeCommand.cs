using Greg.Xrm.Logging;
using Greg.Xrm.Views;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.ModernThemeBuilder.Views.Commands
{
	/// <summary>
	/// https://learn.microsoft.com/en-us/power-apps/maker/data-platform/create-edit-configure-settings
	/// 
	/// </summary>
	public class SetAsCurrentThemeCommand : CommandBase
	{
		private readonly MainViewModel viewModel;
		private readonly ILog log;

		public SetAsCurrentThemeCommand(MainViewModel viewModel, ILog log)
		{
			this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
			this.log = log ?? throw new ArgumentNullException(nameof(log));


			this.viewModel.PropertyChanged += OnViewModelPropertyChanged;
			this.viewModel.Messenger.Register<SolutionComponentChanged>(msg => RefreshCanExecute());
		}


		public override void RefreshCanExecute()
		{
			this.CanExecute = this.viewModel.Crm != null
				&& this.viewModel.CurrentSolution != null
				&& this.viewModel.CurrentSolutionComponent != null
				&& this.viewModel.CurrentTheme != this.viewModel.CurrentSolutionComponent.WebResource.name
				&& !this.viewModel.CurrentSolutionComponent.IsDirty;
		}

		private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != nameof(MainViewModel.Crm)
				&& e.PropertyName != nameof(MainViewModel.CurrentSolution)
				&& e.PropertyName != nameof(MainViewModel.CurrentSolutionComponent)
				&& e.PropertyName != nameof(MainViewModel.CurrentTheme))
				return;

			RefreshCanExecute();
		}


		protected override void ExecuteInternal(object arg)
		{
			var webResourceName = this.viewModel.CurrentSolutionComponent.WebResource.name;

			this.viewModel.Messenger.Send<ShowOutputView>();
			this.viewModel.Scheduler.Enqueue(new WorkAsyncInfo
			{
				Work = SetAsCurrentThemeWorkAsync,
				PostWorkCallBack = SetAsCurrentThemeWorkCompleted,
				Message = $"Setting {webResourceName} as current theme, please wait...",
			});
		}

		private void SetAsCurrentThemeWorkAsync(BackgroundWorker worker, DoWorkEventArgs args)
		{
			var solution = this.viewModel.CurrentSolution;
			var solutionComponent = this.viewModel.CurrentSolutionComponent;
			var crm = this.viewModel.Crm;

			var settingName = "OverrideAppHeaderColor";

			var query = new QueryExpression("settingdefinition");
			query.Criteria.AddCondition("uniquename", ConditionOperator.Equal, settingName);
			query.TopCount = 1;
			query.NoLock = true;

			var setting = crm.RetrieveMultiple(query).Entities.FirstOrDefault();
			if (setting == null)
			{
				throw new InvalidOperationException($"Setting {settingName} not found, please check the environment version.");
			}


			// TODO: this will be added later when they will fix the bug that prevents adding via code a setting definition to a given solution
			//this.log.Debug($"Adding setting definition <{settingName}> to the current solution");

			//var request1 = new AddSolutionComponentRequest();
			//request1.ComponentId = setting.Id;
			//request1.ComponentType = 10058; // SettingDefinition
			//request1.SolutionUniqueName = solution.uniquename;

			//crm.Execute(request1);



			this.log.Debug($"Setting {solutionComponent.WebResource.name} as current theme for solution {solution.uniquename}");

			var request = new OrganizationRequest("SaveSettingValue");
			request["SettingName"] = "OverrideAppHeaderColor";
			request["Value"] = solutionComponent.WebResource.name;
			request["SolutionUniqueName"] = solution.uniquename;
			crm.Execute(request);




			this.log.Debug($"Publishing solution {solution.uniquename}");

			var request2 = new PublishAllXmlRequest();
			crm.Execute(request2);
		}

		private void SetAsCurrentThemeWorkCompleted(RunWorkerCompletedEventArgs args)
		{
			if (args.Error != null)
			{
				this.log.Error(args.Error.Message, args.Error);
				MessageBox.Show(args.Error.Message, "Error saving theme", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			this.log.Info($"Theme applied and published. {Environment.NewLine}Open your app to see the changes (you may need to hit F5).");
			MessageBox.Show($"Theme applied and published. {Environment.NewLine}Open your app to see the changes (you may need to hit F5).", "Theme applied", MessageBoxButton.OK, MessageBoxImage.Information);
			this.viewModel.CurrentTheme = this.viewModel.CurrentSolutionComponent.WebResource.name;
		}
	}
}
