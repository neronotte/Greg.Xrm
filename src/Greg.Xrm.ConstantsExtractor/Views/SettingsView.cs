using Greg.Xrm.ConstantsExtractor.Messaging;
using Greg.Xrm.ConstantsExtractor.Model;
using Greg.Xrm.Messaging;
using Greg.Xrm.Theming;
using System;
using System.IO;
using System.Linq;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.ConstantsExtractor.Views
{
	public class SettingsView : DockContent
	{
		private System.Windows.Forms.CheckBox chkCs;
		private System.Windows.Forms.CheckBox chkJs;
		private System.Windows.Forms.CheckBox chkExtractTypes;
		private System.Windows.Forms.CheckBox chkExtractDescriptions;
		private System.Windows.Forms.TextBox txtCsNamespace;
		private System.Windows.Forms.TextBox txtJsNamespace;
		private System.Windows.Forms.Label lblJsNamespace;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtJsHeaders;
		private System.Windows.Forms.ComboBox cmbSolutionList;
		private System.Windows.Forms.Label lblSolutions;
		private System.Windows.Forms.Label lblCsNamespace;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtCsFolder;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtJsFolder;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.ComponentModel.IContainer components;
		private readonly IMessenger messenger;

		public SettingsView( IThemeProvider themeProvider, IMessenger messenger)
		{
			InitializeComponent();

			themeProvider.GetCurrentTheme().ApplyTo(this.txtCsFolder);
			themeProvider.GetCurrentTheme().ApplyTo(this.txtJsFolder);
			themeProvider.GetCurrentTheme().ApplyTo(this.txtCsNamespace);
			themeProvider.GetCurrentTheme().ApplyTo(this.txtJsNamespace);
			themeProvider.GetCurrentTheme().ApplyTo(this.txtJsHeaders);
			themeProvider.GetCurrentTheme().ApplyTo(this.cmbSolutionList);


			messenger.Register<LoadSolutionsCompleted>(OnLoadSolutionCompleted);
			messenger.Register<ExportCompleted>(m => this.Enabled = true);

			this.RefreshView();
			this.messenger = messenger;

			this.Icon = PluginExtensions.GetIcon();
		}


		public Settings Settings
		{
			get; private set;
		}

		private void RefreshView()
		{
			if (this.Settings == null)
			{
				this.chkCs.Enabled = false;
				this.chkCs.Checked = false;
				this.chkJs.Enabled = false;
				this.chkJs.Checked = false;
				this.chkExtractTypes.Enabled = false;
				this.chkExtractTypes.Checked = false;
				this.chkExtractDescriptions.Enabled = false;
				this.chkExtractDescriptions.Checked = false;
				this.txtCsNamespace.Enabled = false;
				this.txtCsNamespace.Text = String.Empty;
				this.txtJsNamespace.Enabled = false;
				this.txtJsNamespace.Text = String.Empty;
				this.txtJsHeaders.Enabled = false;
				this.txtJsHeaders.Text = String.Empty;
				this.cmbSolutionList.Enabled = false;
				this.txtCsFolder.Enabled = true;
				this.txtCsFolder.Text = String.Empty;
				this.txtJsFolder.Enabled = true;
				this.txtJsFolder.Text = String.Empty;
			}
			else
			{
				this.chkCs.Enabled = true;
				this.chkCs.Checked = this.Settings.GetCsConstants;
				this.txtCsNamespace.Enabled = this.chkCs.Checked;
				this.txtCsNamespace.Text = this.Settings.NamespaceCs;
				this.txtCsFolder.Enabled = this.chkCs.Checked;
				this.txtCsFolder.Text = this.Settings.CsFolder;

				this.chkJs.Enabled = true;
				this.chkJs.Checked = this.Settings.GetJsConstants;
				this.txtJsNamespace.Enabled = this.chkJs.Checked;
				this.txtJsNamespace.Text = this.Settings.NamespaceJs;
				this.txtJsHeaders.Enabled = this.chkJs.Checked;
				this.txtJsHeaders.Text = this.Settings.JsHeaderLines;
				this.txtJsFolder.Enabled = this.chkJs.Checked;
				this.txtJsFolder.Text = this.Settings.JsFolder;


				this.chkExtractTypes.Enabled = true;
				this.chkExtractTypes.Checked = this.Settings.ExtractTypes;
				this.chkExtractDescriptions.Enabled = true;
				this.chkExtractDescriptions.Checked = this.Settings.ExtractDescriptions;

				this.cmbSolutionList.Enabled = true;
				this.cmbSolutionList.SelectedItem = this.cmbSolutionList.Items?.OfType<Solution>()
					.FirstOrDefault(x => string.Equals(x.uniquename, this.Settings.SolutionName, StringComparison.OrdinalIgnoreCase));
			}
		}

		private void OnLoadSolutionCompleted(LoadSolutionsCompleted msg)
		{
			this.cmbSolutionList.Items.Clear();
			this.cmbSolutionList.Items.AddRange(msg.SolutionList.ToArray());


			// Loads or creates the settings for the plugin
			if (!SettingsManager.Instance.TryLoad(typeof(MainView), out Settings settings))
			{
				this.Settings = new Settings();
			}
			else
			{
				this.Settings = settings;
			}

			RefreshView();
		}



		private void OnCsCheckChanged(object sender, EventArgs e)
		{
			this.txtCsNamespace.Enabled = this.chkCs.Checked;
			this.txtCsFolder.Enabled = this.chkCs.Checked;
		}

		private void OnJsCheckChanged(object sender, EventArgs e)
		{
			this.txtJsNamespace.Enabled = this.chkJs.Checked;
			this.txtJsHeaders.Enabled = this.chkJs.Checked;
			this.txtJsFolder.Enabled = this.chkJs.Checked;
		}

		private void OnExportClick(object sender, EventArgs e)
		{
			var hasError = false;

			if (!this.chkCs.Checked && !this.chkJs.Enabled)
			{
				errorProvider.SetError(this.chkCs, "At least one should be checked!");
				errorProvider.SetError(this.chkJs, "At least one should be checked!");
				hasError = true;
			}
			else
			{
				if (this.chkCs.Checked)
				{
					if (string.IsNullOrWhiteSpace(this.txtCsNamespace.Text))
					{
						errorProvider.SetError(this.txtCsNamespace, "Please insert the namespace");
						hasError = true;
					}
					if (string.IsNullOrWhiteSpace(this.txtCsFolder.Text))
					{
						errorProvider.SetError(this.txtCsFolder, "Please select the folder that will contain C# files");
						hasError = true;
					}
					else if (!Directory.Exists(this.txtCsFolder.Text))
					{
						errorProvider.SetError(this.txtCsFolder, "Please select a folder that exists!");
						hasError = true;
					}
				}
				if (this.chkJs.Checked)
				{
					if (string.IsNullOrWhiteSpace(this.txtJsNamespace.Text))
					{
						errorProvider.SetError(this.txtJsNamespace, "Please insert the namespace");
						hasError = true;
					}
					if (string.IsNullOrWhiteSpace(this.txtJsFolder.Text))
					{
						errorProvider.SetError(this.txtJsFolder, "Please select the folder that will contain JS files");
						hasError = true;
					}
					else if (!Directory.Exists(this.txtJsFolder.Text))
					{
						errorProvider.SetError(this.txtJsFolder, "Please select a folder that exists!");
						hasError = true;
					}
				}
			}


			if (this.cmbSolutionList.SelectedItem == null)
			{

				errorProvider.SetError(this.cmbSolutionList, "Please select a solution");
				hasError = true;
			}

			if (hasError) return;

			this.Settings.GetCsConstants = this.chkCs.Checked;
			this.Settings.GetJsConstants = this.chkJs.Checked;
			this.Settings.CsFolder = this.txtCsFolder.Text;
			this.Settings.JsFolder = this.txtJsFolder.Text;
			this.Settings.NamespaceCs = this.txtCsNamespace.Text;
			this.Settings.NamespaceJs = this.txtJsNamespace.Text;
			this.Settings.JsHeaderLines = this.txtJsHeaders.Text;
			this.Settings.ExtractTypes = this.chkExtractTypes.Checked;
			this.Settings.ExtractDescriptions = this.chkExtractDescriptions.Checked;
			this.Settings.SolutionName = ((Solution)this.cmbSolutionList.SelectedItem).uniquename;

			SettingsManager.Instance.Save(typeof(MainView), this.Settings);

			this.messenger.Send(new Export(this.Settings));
			this.Enabled = false;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.chkCs = new System.Windows.Forms.CheckBox();
			this.chkJs = new System.Windows.Forms.CheckBox();
			this.chkExtractTypes = new System.Windows.Forms.CheckBox();
			this.chkExtractDescriptions = new System.Windows.Forms.CheckBox();
			this.txtCsNamespace = new System.Windows.Forms.TextBox();
			this.txtJsNamespace = new System.Windows.Forms.TextBox();
			this.lblJsNamespace = new System.Windows.Forms.Label();
			this.lblCsNamespace = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtJsHeaders = new System.Windows.Forms.TextBox();
			this.cmbSolutionList = new System.Windows.Forms.ComboBox();
			this.lblSolutions = new System.Windows.Forms.Label();
			this.btnExport = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtCsFolder = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtJsFolder = new System.Windows.Forms.TextBox();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// chkCs
			// 
			this.chkCs.AutoSize = true;
			this.chkCs.Location = new System.Drawing.Point(13, 13);
			this.chkCs.Name = "chkCs";
			this.chkCs.Size = new System.Drawing.Size(193, 17);
			this.chkCs.TabIndex = 0;
			this.chkCs.Text = "Generate constants for plugins (C#)";
			this.chkCs.UseVisualStyleBackColor = true;
			this.chkCs.CheckedChanged += new System.EventHandler(this.OnCsCheckChanged);
			// 
			// chkJs
			// 
			this.chkJs.AutoSize = true;
			this.chkJs.Location = new System.Drawing.Point(12, 161);
			this.chkJs.Name = "chkJs";
			this.chkJs.Size = new System.Drawing.Size(224, 17);
			this.chkJs.TabIndex = 1;
			this.chkJs.Text = "Generate constants for webresources (JS)";
			this.chkJs.UseVisualStyleBackColor = true;
			this.chkJs.CheckedChanged += new System.EventHandler(this.OnJsCheckChanged);
			// 
			// chkExtractTypes
			// 
			this.chkExtractTypes.AutoSize = true;
			this.chkExtractTypes.Location = new System.Drawing.Point(12, 395);
			this.chkExtractTypes.Name = "chkExtractTypes";
			this.chkExtractTypes.Size = new System.Drawing.Size(157, 17);
			this.chkExtractTypes.TabIndex = 2;
			this.chkExtractTypes.Text = "Write type into C# comment";
			this.chkExtractTypes.UseVisualStyleBackColor = true;
			// 
			// chkExtractDescriptions
			// 
			this.chkExtractDescriptions.AutoSize = true;
			this.chkExtractDescriptions.Location = new System.Drawing.Point(12, 418);
			this.chkExtractDescriptions.Name = "chkExtractDescriptions";
			this.chkExtractDescriptions.Size = new System.Drawing.Size(210, 17);
			this.chkExtractDescriptions.TabIndex = 3;
			this.chkExtractDescriptions.Text = "Write field description into C# comment";
			this.chkExtractDescriptions.UseVisualStyleBackColor = true;
			// 
			// txtCsNamespace
			// 
			this.txtCsNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCsNamespace.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCsNamespace.Location = new System.Drawing.Point(30, 58);
			this.txtCsNamespace.Name = "txtCsNamespace";
			this.txtCsNamespace.Size = new System.Drawing.Size(322, 22);
			this.txtCsNamespace.TabIndex = 4;
			// 
			// txtJsNamespace
			// 
			this.txtJsNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtJsNamespace.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtJsNamespace.Location = new System.Drawing.Point(30, 206);
			this.txtJsNamespace.Name = "txtJsNamespace";
			this.txtJsNamespace.Size = new System.Drawing.Size(322, 22);
			this.txtJsNamespace.TabIndex = 5;
			// 
			// lblJsNamespace
			// 
			this.lblJsNamespace.AutoSize = true;
			this.lblJsNamespace.Location = new System.Drawing.Point(31, 189);
			this.lblJsNamespace.Name = "lblJsNamespace";
			this.lblJsNamespace.Size = new System.Drawing.Size(79, 13);
			this.lblJsNamespace.TabIndex = 6;
			this.lblJsNamespace.Text = "JS Namespace";
			// 
			// lblCsNamespace
			// 
			this.lblCsNamespace.AutoSize = true;
			this.lblCsNamespace.Location = new System.Drawing.Point(31, 41);
			this.lblCsNamespace.Name = "lblCsNamespace";
			this.lblCsNamespace.Size = new System.Drawing.Size(81, 13);
			this.lblCsNamespace.TabIndex = 7;
			this.lblCsNamespace.Text = "C# Namespace";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(30, 237);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "JS Header lines";
			// 
			// txtJsHeaders
			// 
			this.txtJsHeaders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtJsHeaders.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtJsHeaders.Location = new System.Drawing.Point(29, 254);
			this.txtJsHeaders.Multiline = true;
			this.txtJsHeaders.Name = "txtJsHeaders";
			this.txtJsHeaders.Size = new System.Drawing.Size(323, 57);
			this.txtJsHeaders.TabIndex = 8;
			// 
			// cmbSolutionList
			// 
			this.cmbSolutionList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbSolutionList.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbSolutionList.FormattingEnabled = true;
			this.cmbSolutionList.Location = new System.Drawing.Point(12, 478);
			this.cmbSolutionList.Name = "cmbSolutionList";
			this.cmbSolutionList.Size = new System.Drawing.Size(340, 22);
			this.cmbSolutionList.TabIndex = 10;
			// 
			// lblSolutions
			// 
			this.lblSolutions.AutoSize = true;
			this.lblSolutions.Location = new System.Drawing.Point(13, 462);
			this.lblSolutions.Name = "lblSolutions";
			this.lblSolutions.Size = new System.Drawing.Size(272, 13);
			this.lblSolutions.TabIndex = 11;
			this.lblSolutions.Text = "Select the solution that contains the entities to generate:";
			// 
			// btnExport
			// 
			this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExport.Location = new System.Drawing.Point(297, 526);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(75, 23);
			this.btnExport.TabIndex = 12;
			this.btnExport.Text = "Export";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.OnExportClick);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(30, 89);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(168, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "Folder that will contain the C# files";
			// 
			// txtCsFolder
			// 
			this.txtCsFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCsFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.txtCsFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this.txtCsFolder.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCsFolder.Location = new System.Drawing.Point(29, 106);
			this.txtCsFolder.Name = "txtCsFolder";
			this.txtCsFolder.Size = new System.Drawing.Size(323, 22);
			this.txtCsFolder.TabIndex = 13;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(30, 320);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(168, 13);
			this.label3.TabIndex = 16;
			this.label3.Text = "Folder that will contain the C# files";
			// 
			// txtJsFolder
			// 
			this.txtJsFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtJsFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.txtJsFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this.txtJsFolder.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtJsFolder.Location = new System.Drawing.Point(29, 337);
			this.txtJsFolder.Name = "txtJsFolder";
			this.txtJsFolder.Size = new System.Drawing.Size(323, 22);
			this.txtJsFolder.TabIndex = 15;
			// 
			// errorProvider
			// 
			this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.errorProvider.ContainerControl = this;
			// 
			// SettingsView
			// 
			this.ClientSize = new System.Drawing.Size(384, 561);
			this.CloseButton = false;
			this.CloseButtonVisible = false;
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtJsFolder);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtCsFolder);
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this.lblSolutions);
			this.Controls.Add(this.cmbSolutionList);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtJsHeaders);
			this.Controls.Add(this.lblCsNamespace);
			this.Controls.Add(this.lblJsNamespace);
			this.Controls.Add(this.txtJsNamespace);
			this.Controls.Add(this.txtCsNamespace);
			this.Controls.Add(this.chkExtractDescriptions);
			this.Controls.Add(this.chkExtractTypes);
			this.Controls.Add(this.chkJs);
			this.Controls.Add(this.chkCs);
			this.MinimumSize = new System.Drawing.Size(400, 600);
			this.Name = "SettingsView";
			this.TabText = "Settings";
			this.Text = "Settings";
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
