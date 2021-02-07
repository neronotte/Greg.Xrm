using Greg.Xrm.EnvironmentSolutionsComparer.Messaging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Theming;
using System;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Environments
{
	public partial class EnvironmentListView : DockContent
	{
		private readonly IThemeProvider themeProvider;
		private readonly IMessenger messenger;
		private readonly EnvironmentListViewModel viewModel;

		public EnvironmentListView(IThemeProvider themeProvider, IMessenger messenger)
		{
			InitializeComponent();

			this.themeProvider = themeProvider;
			this.ApplyTheme();
			this.messenger = messenger;

			this.viewModel = new EnvironmentListViewModel(messenger);

			this.tAddEnvironment.Bind(_ => _.Enabled, this.viewModel, _ => _.CanAddEnvironment);
			this.tRemoveEnvironment.Bind(_ => _.Enabled, this.viewModel, _ => _.CanRemoveEnvironment);
			this.mRemoveEnvironment.Bind(_ => _.Enabled, this.viewModel, _ => _.CanRemoveEnvironment);


			this.environmentList.BindCollection(this.viewModel.EnvironmentList, i =>
			{
				return new ListViewItem(i.ToString())
				{
					ImageKey = "environment",
					Tag = i
				};
			});

			this.tAddEnvironment.Click += (s, e) => this.messenger.Send<AddNewConnectionMessage>();
			this.tRemoveEnvironment.Click += (s, e) => RemoveEnvironment();
			this.mRemoveEnvironment.Click += (s, e) => RemoveEnvironment();

			this.environmentList.SelectedIndexChanged += (s, e) =>
			{
				var selectedModel = this.environmentList.SelectedItems
					.OfType<ListViewItem>()
					.Select(_ => _.Tag)
					.OfType<ConnectionModel>()
					.FirstOrDefault();

				this.viewModel.SelectedModel = selectedModel;
			};
		}

		private void RemoveEnvironment()
		{
			if (this.environmentList.SelectedItems.Count == 0) return;

			var model = (ConnectionModel)this.environmentList.SelectedItems[0].Tag;

			var result = MessageBox.Show($"Do you really want to remove environment <{model.Detail.ConnectionName}>", "Remove environment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result != DialogResult.Yes) return;

			this.messenger.Send(new RemoveConnectionMessage(model.Detail));
		}




		private void ApplyTheme()
		{
			var theme = this.themeProvider.GetCurrentTheme();
			theme.ApplyTo(this.environmentList);
		}
	}
}
