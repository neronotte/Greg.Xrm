using Greg.Xrm.Async;
using Greg.Xrm.EnvironmentComparer.Actions;
using Greg.Xrm.EnvironmentComparer.Logging;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.EnvironmentComparer.Views.Output;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;

namespace Greg.Xrm.EnvironmentComparer.Views.Actions
{
	public class ActionsViewModel : ViewModel
	{
		private readonly ILog log;
		private readonly IMessenger messenger;
		private readonly IAsyncJobScheduler scheduler;


		public ActionsViewModel(ILog log, IMessenger messenger, IAsyncJobScheduler scheduler)
		{
			this.log = log ?? throw new System.ArgumentNullException(nameof(log));
			this.messenger = messenger ?? throw new System.ArgumentNullException(nameof(messenger));
			this.scheduler = scheduler ?? throw new System.ArgumentNullException(nameof(scheduler));


			this.Actions = new ActionQueue();
			this.messenger.Register<SubmitActionMessage>(OnActionReceived);

			this.WhenChanges(() => this.Crm1)
				.ChangesAlso(() => CanApplyActions);
			this.WhenChanges(() => this.Crm2)
				.ChangesAlso(() => CanApplyActions);

			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.Crm1)
				.Execute(e =>
				{
					this.Crm1 = e.GetNewValue<IOrganizationService>();
				});

			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.Crm2)
				.Execute(e =>
				{
					this.Crm2 = e.GetNewValue<IOrganizationService>();
				});
		}


		public ActionQueue Actions { get; }

		public IOrganizationService Crm1
		{
			get => this.Get<IOrganizationService>();
			private set => this.Set(value);
		}

		public IOrganizationService Crm2
		{
			get => this.Get<IOrganizationService>();
			private set => this.Set(value);
		}

		public bool CanApplyActions
		{
			get => this.Crm1 != null && this.Crm2 != null;
		}



		private void OnActionReceived(SubmitActionMessage obj)
		{
			var errorActions = new List<IAction>();
			string errorMessage = null;
			foreach (var action in obj.Actions)
			{
				if (this.Actions.TryEnqueue(action, out errorMessage))
				{
					//this.EnqueueInView(action);
					continue;
				}

				errorActions.Add(action);
			}

			if (errorActions.Count > 0)
			{
				var message = new StringBuilder();
				message.Append("Error enqueuing action");
				if (errorActions.Count > 1)
				{
					message.Append("s");
				}
				message.AppendLine(":");
				foreach (var action in errorActions)
				{
					message.Append("- ").AppendLine(action.ToString());
				}
				message.Append(errorMessage);

				this.log.Error(message.ToString());
				MessageBox.Show(message.ToString(), "Enqueue action", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		public void ExecuteActions(IReadOnlyCollection<IAction> actionList = null)
		{
			if (this.Crm1 == null || this.Crm2 == null) return;

			if (actionList == null)
			{
				actionList = this.Actions.ToArray();
			}

			if (actionList.Count == 0) return;


			this.scheduler.Enqueue(new WorkAsyncInfo
			{
				Message = "Executing actions, please wait...",
				Work = (s, e) => {
					var result = new ApplyActionsResult();

					var index = 0;
					foreach (var action in actionList)
					{
						index++;
						var percent = (index * 100) / actionList.Count;

						this.messenger.Send(new StatusBarMessageEventArgs(percent, $"Processing action {index}/{actionList.Count}"));


						using (log.Track($"Executing action {index}/{actionList.Count}: {action}"))
						{
							try
							{
								action.ApplyTo(this.Crm1, this.Crm2);
								result.Add(action);
							}
#pragma warning disable CA1031 // Do not catch general exception types
							catch (Exception ex)
							{
								log.Error(ex.Message, ex);
								result.Add(action, ex);
							}
#pragma warning restore CA1031 // Do not catch general exception types
						}

						e.Result = result;
					}
				},
				PostWorkCallBack = e => {
					var result = (ApplyActionsResult)e.Result;

					var succeededCount = result.SucceededCount;

					var sb = new StringBuilder();

					if (succeededCount > 0)
					{
						this.log.Debug("Clearing completed action list");
						foreach (var item in result.Where(x => x.Succeeded))
						{
							this.Actions.Remove(item.Action);
						}
						this.log.Debug("Action list cleared");

						var plural = succeededCount > 1 ? "s" : string.Empty;
						sb.AppendLine($"{succeededCount} action{plural} applied successfully.");
					}

					var errorsCount = result.ErrorsCount;
					if (errorsCount > 0)
					{
						this.messenger.Send<ShowOutputView>();

						var plural = errorsCount > 1 ? "s" : string.Empty;
						sb.AppendLine($"Found {errorsCount} error{plural} while applying actions.");
						sb.AppendLine("See the output window for more details.");
					}

					var message = sb.ToString().TrimEnd(Environment.NewLine.ToCharArray());

					MessageBoxIcon icon;
					if (succeededCount > 0 && errorsCount == 0)
					{
						icon = MessageBoxIcon.Information;
					}
					else if (succeededCount > 0 && errorsCount > 0)
					{
						icon = MessageBoxIcon.Warning;
					}
					else //if (succeededCount == 0 && errorsCount == 0)
					{
						icon = MessageBoxIcon.Error;
					}


					MessageBox.Show(message, "Apply actions", MessageBoxButtons.OK, icon);
					this.messenger.Send(result);
					this.messenger.Send(new StatusBarMessageEventArgs(string.Empty));
				}
			});
		}
	}
}
