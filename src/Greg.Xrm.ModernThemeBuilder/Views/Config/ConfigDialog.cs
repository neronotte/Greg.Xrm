using System;
using System.Windows.Forms;

namespace Greg.Xrm.ModernThemeBuilder.Views.Config
{
	public partial class ConfigDialog : Form
	{
		private readonly ISettingsProvider<Settings> settingsProvider;
		private Settings model;
		
		public ConfigDialog(ISettingsProvider<Settings> settingsProvider)
		{
			InitializeComponent();
			this.settingsProvider = settingsProvider;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.model = this.settingsProvider.GetSettings();
			this.txtAzureApiKey.Text = this.model.AzureOpenAiApiKey;
			this.txtAzureEndpoint.Text = this.model.AzureOpenAiEndpoint;
			this.txtAzureDeployment.Text = this.model.AzureOpenAiDeploymentName;
			this.txtChatGptApiKey.Text = this.model.ChatGptApiKey;
			this.txtClaudeApiKey.Text = this.model.ClaudeApiKey;
		}

		private void OnOkClick(object sender, EventArgs e)
		{
			try
			{
				this.model.AzureOpenAiApiKey = this.txtAzureApiKey.Text;
				this.model.AzureOpenAiEndpoint = this.txtAzureEndpoint.Text;
				this.model.AzureOpenAiDeploymentName = this.txtAzureDeployment.Text;
				this.model.ChatGptApiKey = this.txtChatGptApiKey.Text;
				this.model.ClaudeApiKey = this.txtClaudeApiKey.Text;
				this.model.Save();

				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show($"An error occurred while saving settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
