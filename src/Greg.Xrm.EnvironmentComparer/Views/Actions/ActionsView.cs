using Greg.Xrm.EnvironmentComparer.Actions;
using Greg.Xrm.EnvironmentComparer.Logging;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Theming;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.EnvironmentComparer.Views.Actions
{
	public partial class ActionsView : DockContent
	{
		private readonly ActionQueue queue = new ActionQueue();
		private readonly IThemeProvider themeProvider;
		private readonly IMessenger messenger;
		private readonly ILog log;

		public ActionsView(IThemeProvider themeProvider, IMessenger messenger, ILog log)
		{
			InitializeComponent();
			this.themeProvider = themeProvider ?? throw new ArgumentNullException(nameof(themeProvider));
			this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
			this.log = log;
			this.messenger.Register<SubmitActionMessage>(OnActionReceived);
			this.ApplyTheme();
		}


		private void ApplyTheme()
		{
			var theme = this.themeProvider.GetCurrentTheme();
			theme.ApplyTo(this.chlActionList);
		}

		private void OnActionReceived(SubmitActionMessage obj)
		{
			var errorActions = new List<IAction>();
			string errorMessage = null;
			foreach (var action in obj.Actions)
			{
				if (this.queue.TryEnqueue(action, out errorMessage))
				{
					this.EnqueueInView(action);
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

		private void EnqueueInView(IAction action)
		{
			this.chlActionList.Items.Add(action);
		}

		private void OnClearClick(object sender, EventArgs e)
		{
			if (this.queue.Count == 0) return;

			var result = MessageBox.Show("Do you really want to clear all the listed actions?", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result != DialogResult.Yes) return;

			this.log.Debug("Clearing action list");
			this.queue.Clear();
			this.chlActionList.Items.Clear();
			this.log.Debug("Action list cleared");
		}

		private void OnApplyAllClick(object sender, EventArgs e)
		{
			if (this.queue.Count == 0)
			{
				MessageBox.Show("Please insert at least one action in the action queue.", "Apply all", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// TODO: apply actions


			this.log.Debug("Clearing action list");
			this.queue.Clear();
			this.chlActionList.Items.Clear();
			this.log.Debug("Action list cleared");
		}

		private void OnApplyCheckedClick(object sender, EventArgs e)
		{
			if (this.chlActionList.CheckedItems.Count == 0)
			{
				var actionTerm = this.chlActionList.Items.Count == 0 ? "insert" : "check";
				MessageBox.Show($"Please {actionTerm} at least one action in the action queue.", "Apply checked", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// TODO: apply actions


			this.log.Debug("Clearing action list");
			this.queue.Clear();
			this.chlActionList.Items.Clear();
			this.log.Debug("Action list cleared");
		}

		private void OnSelectAllClick(object sender, EventArgs e)
		{
			SelectUnselectAll(true);
		}

		private void OnUnselectAllClick(object sender, EventArgs e)
		{
			SelectUnselectAll(false);
		}

		private void SelectUnselectAll(bool selected)
		{
			foreach (var item in this.queue)
			{
				var index = this.chlActionList.Items.IndexOf(item);
				this.chlActionList.SetItemChecked(index, selected);
			}
		}
	}
}
