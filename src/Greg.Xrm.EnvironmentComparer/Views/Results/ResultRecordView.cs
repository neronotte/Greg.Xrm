using Greg.Xrm.EnvironmentComparer.Engine;
using Greg.Xrm.EnvironmentComparer.Help;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.Theming;
using ScintillaNET;
using System;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public partial class ResultRecordView : DockContent
	{
		private readonly IThemeProvider themeProvider;
		private readonly IMessenger messenger;

		public ResultRecordView(IThemeProvider themeProvider, IMessenger messenger)
		{
			InitializeComponent();

			this.RegisterHelp(messenger, Topics.ResultRecord);

			this.txtValue1.Lexer = Lexer.Json;
			this.txtValue1.Margins[0].Width = 16;
			this.txtValue1.Margins[0].Type = MarginType.Number;
			this.txtValue1.TextChanged += ResizePanel;
			this.txtValue1.ReadOnly = true;

			this.txtValue2.Lexer = Lexer.Json;
			this.txtValue2.Margins[0].Width = 16;
			this.txtValue2.Margins[0].Type = MarginType.Number;
			this.txtValue2.TextChanged += ResizePanel;
			this.txtValue2.ReadOnly = true;

			this.cmbAttributes.DisplayMember = "FieldName";
			this.themeProvider = themeProvider ?? throw new ArgumentNullException(nameof(themeProvider));
			this.messenger = messenger;

			this.messenger.Register<CompareResultRecordSelected>(OnResultRecordSelected);

			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.ConnectionName1)
				.Execute(e =>
				{
					var env1name = e.GetNewValue<string>();
					this.lblEnv1.Text = (string.IsNullOrWhiteSpace(env1name) ? "ENV1" : env1name);
					this.OnResultRecordSelected(null);
				}); 
			this.messenger.WhenObject<EnvironmentComparerViewModel>()
				 .ChangesProperty(_ => _.ConnectionName2)
				 .Execute(e =>
				 {
					 var env2name = e.GetNewValue<string>();
					 this.lblEnv2.Text = (string.IsNullOrWhiteSpace(env2name) ? "ENV2" : env2name);
					 this.OnResultRecordSelected(null);
				 });


			this.lblEnv1.Text = "ENV1";
			this.lblEnv2.Text = "ENV2";

			this.ApplyTheme();
		}

		private void ApplyTheme()
		{
			var theme = this.themeProvider.GetCurrentTheme();

			this.txtValue1.Styles[Style.Default].Font = theme.PanelFont.Name;
			this.txtValue1.Styles[Style.Default].Size = (int)theme.PanelFont.Size;

			this.txtValue2.Styles[Style.Default].Font = theme.PanelFont.Name;
			this.txtValue2.Styles[Style.Default].Size = (int)theme.PanelFont.Size;
		}


		private void ResizePanel(object sender, EventArgs e)
		{
			var scintilla = (Scintilla)sender;

			var maxLineNumberCharLengthOld = scintilla.Tag == null ? 0 : (int)scintilla.Tag;
			// Did the number of characters in the line number display change?
			// i.e. nnn VS nn, or nnnn VS nn, etc...
			var maxLineNumberCharLength = scintilla.Lines.Count.ToString().Length;
			if (maxLineNumberCharLength == maxLineNumberCharLengthOld)
				return;

			// Calculate the width required to display the last line number
			// and include some padding for good measure.
			const int padding = 2;
			scintilla.Margins[0].Width = scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
			scintilla.Tag = maxLineNumberCharLength;
		}

		private void OnResultRecordSelected(CompareResultRecordSelected obj)
		{
			var record = obj?.Result;

			if (record == null)
			{
				this.pnlMessage.BackColor = this.BackColor;
				this.lblMessage.Text = "Please select a result...";

				this.label1.Visible = false;
				this.cmbAttributes.Visible = false;
				this.lblEnv1.Visible = false;
				this.lblEnv2.Visible = false;
				this.txtValue1.Visible = false;
				this.txtValue2.Visible = false;

				return;
			}


			this.pnlMessage.BackColor = record.Result.GetColor();
			this.lblMessage.Text = string.Format(record.Result.GetMessage(), this.lblEnv1.Text, this.lblEnv2.Text);

			this.label1.Visible = record.Result != ObjectComparisonResult.Equals;
			this.cmbAttributes.Visible = record.Result != ObjectComparisonResult.Equals;
			this.lblEnv1.Visible = record.Result != ObjectComparisonResult.Equals;
			this.lblEnv2.Visible = record.Result != ObjectComparisonResult.Equals;
			this.txtValue1.Visible = record.Result != ObjectComparisonResult.Equals;
			this.txtValue2.Visible = record.Result != ObjectComparisonResult.Equals;

			this.cmbAttributes.Items.Clear();
			foreach(var property in record.DifferentProperties)
			{
				this.cmbAttributes.Items.Add(property);
			}

			if (this.cmbAttributes.Items.Count > 0)
			{
				this.cmbAttributes.SelectedIndex = 0;

				var difference = (Difference)this.cmbAttributes.SelectedItem;

				this.txtValue1.ReadOnly = false;
				this.txtValue1.Text = difference.FormattedValue1;
				this.txtValue1.ReadOnly = true;

				this.txtValue2.ReadOnly = false;
				this.txtValue2.Text = difference.FormattedValue2;
				this.txtValue2.ReadOnly = true;
			}
			else
			{
				this.txtValue1.ReadOnly = false;
				this.txtValue1.Text = string.Empty;
				this.txtValue1.ReadOnly = true;

				this.txtValue2.ReadOnly = false;
				this.txtValue2.Text = string.Empty;
				this.txtValue2.ReadOnly = true;
			}
		}

		private void OnSelectionChanged(object sender, EventArgs e)
		{
			var difference = (Difference)this.cmbAttributes.SelectedItem;

			this.txtValue1.ReadOnly = false;
			this.txtValue1.Text = difference.FormattedValue1;
			this.txtValue1.ReadOnly = true;

			this.txtValue2.ReadOnly = false;
			this.txtValue2.Text = difference.FormattedValue2;
			this.txtValue2.ReadOnly = true;
		}
	}
}
