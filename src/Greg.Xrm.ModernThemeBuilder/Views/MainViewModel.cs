using Greg.Xrm.Async;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.ModernThemeBuilder.Model;
using Greg.Xrm.ModernThemeBuilder.Views.Commands;
using Greg.Xrm.ModernThemeBuilder.Views.Messages;
using Greg.Xrm.Views;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.ModernThemeBuilder.Views
{
	public class MainViewModel : PluginViewModelBase
	{

		public MainViewModel(IMessenger messenger, ILog log)
		{
			this.Messenger = messenger;
			this.Log = log;

			this.LoadSolutionsCommand = new LoadSolutionsCommand(this, log);
			this.CreateNewSolutionCommand = new CreateNewSolutionCommand(this);
			this.CreateNewThemeCommand = new CreateNewThemeCommand(this, log);
			this.SaveThemeCommand = new SaveThemeCommand(this, log);
			this.SetAsCurrentThemeCommand = new SetAsCurrentThemeCommand(this, log);
			this.ResetDefaultThemeCommand = new ResetDefaultThemeCommand(this, log);

			this.WhenChanges(() => Env)
				.ChangesAlso(() => ConnectionName);

			this.WhenChanges(() => Crm)
				.Execute(OnLoadTheme);

			this.WhenChanges(() => CurrentTheme)
				.Execute(o => this.Messenger.Send(new CurrentThemeSelected(this.CurrentTheme)));

			this.Messenger.Register<SolutionComponentSelected>(msg => this.CurrentSolutionComponent = msg.SolutionComponent);
			this.AllowRequests = true;
		}

		public IMessenger Messenger { get; }
		public ILog Log { get; }

		public IAsyncJobScheduler Scheduler { get; set; }


		public ConnectionDetail Env
		{
			get => Get<ConnectionDetail>();
			set
			{
				Set(value);
				Set(value?.GetCrmServiceClient(), nameof(Crm));
				Set<Solution>(null, nameof(CurrentSolution));
			}
		}
		public string CurrentTheme
		{
			get => Get<string>();
			set
			{
				Set(value);
				Set(value ?? "[Default]", nameof(CurrentThemeDisplayName));
			}
		}

		public string CurrentThemeDisplayName
		{
			get => Get<string>();
		}

		public Solution CurrentSolution
		{
			get => Get<Solution>();
			set => Set(value);
		}


		public List<SolutionComponent> SolutionComponentList { get; } = new List<SolutionComponent>();



		public SolutionComponent CurrentSolutionComponent
		{
			get => Get<SolutionComponent>();
			set => Set(value);
		}


		public ICommand LoadSolutionsCommand { get; }
		public ICommand CreateNewThemeCommand { get; }
		public ICommand SaveThemeCommand { get; }
		public ICommand SetAsCurrentThemeCommand { get; }
		public ICommand ResetDefaultThemeCommand { get; }
		public ICommand CreateNewSolutionCommand { get; }




		public IOrganizationService Crm { get => Get<IOrganizationService>(); }


		public string ConnectionName { get => this.Env?.ConnectionName; }




		private void OnLoadTheme(object obj)
		{
			if (this.Env == null)
			{
				this.CurrentTheme = null;
				return;
			}

			this.Scheduler.Enqueue(new WorkAsyncInfo
			{
				Message = "Getting current theme...",
				Work = ReadCurrentThemeAsync,
				PostWorkCallBack = ReadCurrentThemeCompleted,
			});
		}

		private void ReadCurrentThemeAsync(BackgroundWorker worker, DoWorkEventArgs args)
		{
			var settingName = "OverrideAppHeaderColor";
			this.Log.Debug("Checking if the setting for the theme exists");

			var query = new QueryExpression("settingdefinition");
			query.Criteria.AddCondition("uniquename", ConditionOperator.Equal, settingName);
			query.TopCount = 1;
			query.NoLock = true;

			var setting = this.Crm.RetrieveMultiple(query).Entities.FirstOrDefault();
			if (setting == null)
			{
				throw new InvalidOperationException($"Setting {settingName} not found, please check the environment version.");
			}

			var request = new OrganizationRequest("RetrieveSetting");
			request["SettingName"] = settingName;

			var response = this.Crm.Execute(request);
			var settingDetail = (SettingDetail)response["SettingDetail"];

			args.Result = settingDetail;
		}

		private void ReadCurrentThemeCompleted(RunWorkerCompletedEventArgs args)
		{
			if (args.Error != null)
			{
				this.CurrentTheme = null;
				this.Log.Error(args.Error.Message, args.Error);
				MessageBox.Show(args.Error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			var settingDetail = (SettingDetail)args.Result;

			if (string.IsNullOrWhiteSpace(settingDetail.Value) || "-".Equals(settingDetail.Value))
			{
				this.CurrentTheme = null;
				return;
			}	

			this.CurrentTheme = settingDetail.Value;
		}
	}
}
