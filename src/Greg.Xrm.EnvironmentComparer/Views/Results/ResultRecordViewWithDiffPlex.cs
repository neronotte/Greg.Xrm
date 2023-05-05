using DiffPlex.WindowsForms.Controls;
using Greg.Xrm.Core.Views.Help;
using Greg.Xrm.EnvironmentComparer.Engine;
using Greg.Xrm.EnvironmentComparer.Help;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.Theming;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public partial class ResultRecordViewWithDiffPlex : DockContent
	{
		private readonly IThemeProvider themeProvider;
		private readonly IMessenger messenger;
		private readonly DiffViewer diffView;

		public ResultRecordViewWithDiffPlex(IThemeProvider themeProvider, IMessenger messenger)
		{
			InitializeComponent();
			this.themeProvider = themeProvider;
			this.messenger = messenger;

			this.diffView = new DiffViewer
			{
				Margin = new Padding(0),
				Dock = DockStyle.Fill,
				OldText = "",
				NewText = "",
				AutoScroll = true,
				IgnoreWhiteSpace = true,
				HeaderForeColor = Color.Black,
			};
			pnlContainer.Controls.Add(diffView);

			this.RegisterHelp(messenger, Topics.ResultRecord);


			this.cmbAttributes.DisplayMember = "FieldName";

			this.messenger.Register<CompareResultRecordSelected>(OnResultRecordSelected);

			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.ConnectionName1)
				.Execute(e =>
				{
					var env1name = e.GetNewValue<string>();
					this.diffView.OldTextHeader = (string.IsNullOrWhiteSpace(env1name) ? "ENV1" : env1name);
					this.OnResultRecordSelected(null);
				});
			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				 .ChangesProperty(_ => _.ConnectionName2)
				 .Execute(e =>
				 {
					 var env2name = e.GetNewValue<string>();
					 this.diffView.NewTextHeader = (string.IsNullOrWhiteSpace(env2name) ? "ENV2" : env2name);
					 this.OnResultRecordSelected(null);
				 });


			this.diffView.OldTextHeader = "ENV1";
			this.diffView.NewTextHeader = "ENV2";

			this.ApplyTheme();
		}

		private void ApplyTheme()
		{
			var theme = this.themeProvider.GetCurrentTheme();
			this.diffView.Font = theme.PanelFont;
		}


		private void OnResultRecordSelected(CompareResultRecordSelected obj)
		{
			var record = obj?.Result;

			this.diffView.OldText = string.Empty;
			this.diffView.NewText = string.Empty;

			if (record == null)
			{
				this.pnlMessage.BackColor = this.BackColor;
				this.lblMessage.Text = "Please select a result...";

				this.label1.Visible = false;
				this.cmbAttributes.Visible = false;
				return;
			}


			this.pnlMessage.BackColor = record.Result.GetColor();
			this.lblMessage.Text = string.Format(record.Result.GetMessage(), this.diffView.OldTextHeader, this.diffView.NewTextHeader);

			this.label1.Visible = record.Result != ObjectComparisonResult.Equals;
			this.cmbAttributes.Visible = record.Result != ObjectComparisonResult.Equals;

			this.cmbAttributes.Items.Clear();
			foreach (var property in record.DifferentProperties)
			{
				this.cmbAttributes.Items.Add(property);
			}

			if (this.cmbAttributes.Items.Count > 0)
			{
				this.cmbAttributes.SelectedIndex = 0;

				var difference = (Difference)this.cmbAttributes.SelectedItem;

				this.diffView.OldText = difference?.FormattedValue1;
				this.diffView.NewText = difference?.FormattedValue2;
			}
		}

		private void OnSelectionChanged(object sender, EventArgs e)
		{
			var difference = (Difference)this.cmbAttributes.SelectedItem;

			this.diffView.OldText = difference?.FormattedValue1;
			this.diffView.NewText = difference?.FormattedValue2;
		}

		private void OnSelectPrevious(object sender, EventArgs e)
		{
			this.messenger.Send(new ChangeSelectionMessage(-1));
		}

		private void OnSelectNext(object sender, EventArgs e)
		{
			this.messenger.Send(new ChangeSelectionMessage(1));
		}
	}
}
