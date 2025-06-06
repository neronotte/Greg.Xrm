﻿using Greg.Xrm.Async;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.ModernThemeBuilder.Views.Config;
using Greg.Xrm.ModernThemeBuilder.Views.Messages;
using Greg.Xrm.Theming;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using System.Configuration;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility.Interfaces;

namespace Greg.Xrm.ModernThemeBuilder.Views
{
	public partial class MainView : GregPluginControlBase<ModernThemeBuilderPlugin>, ISettingsPlugin
	{
		private readonly MainViewModel viewModel;
		private readonly IMessenger messenger;
		private readonly PaletteEditorView paletteEditor;
		private readonly SolutionView solutionView;
		private readonly OutputView outputView;
		private readonly ISettingsProvider<Settings> settingsProvider;

		public MainView(ISettingsProvider<Settings> settingsProvider, IThemeProvider themeProvider)
		{
			InitializeComponent();

			this.settingsProvider = settingsProvider;
			this.messenger = new Messenger(this);
			this.outputView = new OutputView(themeProvider, messenger);

			this.viewModel = new MainViewModel(messenger, this.outputView);
			var scheduler = new AsyncJobScheduler(this, this.viewModel);
			this.viewModel.Scheduler = scheduler;

			this.paletteEditor = new PaletteEditorView(
				this.outputView, 
				this.messenger, 
				this.settingsProvider, 
				scheduler);
			this.solutionView = new SolutionView(messenger, scheduler);

			this.paletteEditor.Show(this.dockPanel1, DockState.Document);
			this.outputView.Show(this.dockPanel1, DockState.DockBottomAutoHide);
			this.solutionView.Show(this.dockPanel1, DockState.DockLeft);


			this.toolStripMenu.Bind(x => x.Enabled, viewModel, x => x.AllowRequests);
			this.dockPanel1.Bind(x => x.Enabled, viewModel, x => x.AllowRequests);

			this.tLoadSolutions.BindCommand(() => this.viewModel.LoadSolutionsCommand);

			this.tCreateNewSolution.Visible = false;
			this.tCreateNewTheme.BindCommand(() => this.viewModel.CreateNewThemeCommand);
			this.tSaveTheme.BindCommand(() => this.viewModel.SaveThemeCommand);
			this.tSetAsCurrentTheme.BindCommand(() => this.viewModel.SetAsCurrentThemeCommand);
			this.tResetDefaultTheme.BindCommand(() => this.viewModel.ResetDefaultThemeCommand);
			this.tCurrentTheme.Bind(x => x.Text, viewModel, x => x.CurrentThemeDisplayName);

			this.tSettings.Click += (s, e) => ShowSettings();
		}

		private async void AfterLoad(object sender, EventArgs e)
		{
			this.viewModel.Env = this.ConnectionDetail;
			this.tLoadSolutions.Enabled = this.ConnectionDetail != null;

			await this.paletteEditor.RefreshViewAsync();
		}


		/// <summary>
		/// This event occurs when the connection has been updated in XrmToolBox
		/// </summary>
		public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
		{
			base.UpdateConnection(newService, detail, actionName, parameter);
			this.viewModel.Env = this.ConnectionDetail;
			this.tLoadSolutions.Enabled = this.ConnectionDetail != null;

			this.messenger.Send(new ConnectionUpdated(detail, newService));
		}

		private void OnCloseClick(object sender, EventArgs e)
		{
			CloseTool();
		}


		private void OnResetDefaultsClick(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Do you really want to restore the default colors?", "Restore defaults", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result != DialogResult.Yes)
				return;

			this.paletteEditor.ResetDefaults();
		}




		public void ShowSettings()
		{
			using (var dialog = new ConfigDialog(this.settingsProvider))
			{
				dialog.StartPosition = FormStartPosition.CenterParent;
				var result = dialog.ShowDialog(this);

				if (result != DialogResult.OK) return;

				this.messenger.Send<ConfigUpdated>();
			}
		}
	}
}