﻿using Greg.Xrm.Async;
using Greg.Xrm.Core.Views.Help;
using Greg.Xrm.EnvironmentComparer.Actions;
using Greg.Xrm.EnvironmentComparer.Help;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Theming;
using System;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.EnvironmentComparer.Views.Actions
{
	public partial class ActionsView : DockContent
	{
		private readonly IThemeProvider themeProvider;
		private readonly ActionsViewModel viewModel;

		public ActionsView(IThemeProvider themeProvider, IAsyncJobScheduler scheduler, IMessenger messenger, ILog log)
		{
			InitializeComponent();

			this.RegisterHelp(messenger, Topics.Actions);

			this.themeProvider = themeProvider ?? throw new ArgumentNullException(nameof(themeProvider));
			this.viewModel = new ActionsViewModel(log, messenger, scheduler);

			this.Bind(_ => _.Text, this.viewModel, _ => _.Title);
			this.Bind(_ => _.TabText, this.viewModel, _ => _.Title);

			this.ApplyTheme();

			this.chlActionList.DataSource = viewModel.Actions;
		}


		private void ApplyTheme()
		{
			var theme = this.themeProvider.GetCurrentTheme();
			theme.ApplyTo(this.chlActionList);
		}


		private void OnClearClick(object sender, EventArgs e)
		{
			if (this.viewModel.Actions.Count == 0) return;

			var result = MessageBox.Show("Do you really want to clear all the listed actions?", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result != DialogResult.Yes) return;

			this.viewModel.ClearActions();
		}

		private void OnApplyAllClick(object sender, EventArgs e)
		{
			if (this.viewModel.Actions.Count == 0)
			{
				MessageBox.Show("Please insert at least one action in the action queue.", "Apply all", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var result = MessageBox.Show("Do you really want to apply all the actions?", this.tApplyAll.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result != DialogResult.Yes) return;


			this.viewModel.ExecuteActions();
		}

		private void OnApplyCheckedClick(object sender, EventArgs e)
		{
			if (this.chlActionList.CheckedItems.Count == 0)
			{
				var actionTerm = this.chlActionList.Items.Count == 0 ? "insert" : "check";
				MessageBox.Show($"Please {actionTerm} at least one action in the action queue.", "Apply checked", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var result = MessageBox.Show("Do you really want to apply the selected actions?", this.tApplyChecked.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result != DialogResult.Yes) return;


			var actionList = this.chlActionList.CheckedItems.Cast<IAction>().ToArray();
			this.viewModel.ExecuteActions(actionList);
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
			foreach (var item in this.viewModel.Actions)
			{
				var index = this.chlActionList.Items.IndexOf(item);
				this.chlActionList.SetItemChecked(index, selected);
			}
		}

		private void OnRemoveClick(object sender, EventArgs e)
		{
			var action = (IAction)this.chlActionList.SelectedItem;
			if (action == null) return;

			this.viewModel.RemoveAction(action);

		}
	}
}
