using Cyotek.Windows.Forms;
using Greg.Xrm.Async;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.ModernThemeBuilder.AIEngine;
using Greg.Xrm.ModernThemeBuilder.Model;
using Greg.Xrm.ModernThemeBuilder.Views.Config;
using Greg.Xrm.ModernThemeBuilder.Views.Messages;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Label = System.Windows.Forms.Label;

namespace Greg.Xrm.ModernThemeBuilder.Views
{
	public partial class PaletteEditorView : DockContent
	{
		private readonly WebView2 browser = new WebView2();
		private readonly List<Label> nameLabels = new List<Label>();
		private readonly List<Label> colorLabels = new List<Label>();
		private readonly List<Label> hexLabels = new List<Label>();
		private readonly List<Control> labels = new List<Control>();
		private readonly string template;
		private readonly ILog log;
		private readonly IMessenger messenger;
		private readonly ISettingsProvider<Settings> settingsProvider;
		private readonly IAsyncJobScheduler scheduler;
		private AppHeaderColors palette = AppHeaderColors.Default;

		private SolutionComponent currentComponent;
		private string selectedImageUrl;

		public PaletteEditorView(
			ILog log,
			IMessenger messenger, 
			ISettingsProvider<Settings> settingsProvider,
			IAsyncJobScheduler scheduler)
		{
			this.log = log;
			this.messenger = messenger;
			this.settingsProvider = settingsProvider;
			this.scheduler = scheduler;

			browser.Dock = System.Windows.Forms.DockStyle.Top;
			this.browser.Height = 80;

			InitializeComponent();

			this.Icon = Core.Properties.Resources.Icon;

			this.Controls.Add(this.browser);

			this.TabText = this.Text = "Theme Editor";

			this.nameLabels.Add(this.label01);
			this.nameLabels.Add(this.label02);
			this.nameLabels.Add(this.label03);
			this.nameLabels.Add(this.label04);
			this.nameLabels.Add(this.label05);
			this.nameLabels.Add(this.label06);
			this.nameLabels.Add(this.label07);
			this.nameLabels.Add(this.label08);

			this.colorLabels.Add(this.color01);
			this.colorLabels.Add(this.color02);
			this.colorLabels.Add(this.color03);
			this.colorLabels.Add(this.color04);
			this.colorLabels.Add(this.color05);
			this.colorLabels.Add(this.color06);
			this.colorLabels.Add(this.color07);
			this.colorLabels.Add(this.color08);

			this.hexLabels.Add(this.hex01);
			this.hexLabels.Add(this.hex02);
			this.hexLabels.Add(this.hex03);
			this.hexLabels.Add(this.hex04);
			this.hexLabels.Add(this.hex05);
			this.hexLabels.Add(this.hex06);
			this.hexLabels.Add(this.hex07);
			this.hexLabels.Add(this.hex08);

			this.labels.AddRange(this.nameLabels);
			this.labels.AddRange(this.colorLabels);
			this.labels.AddRange(this.hexLabels);

			foreach (var label in nameLabels)
			{
				label.Click += OnSelectColorClick;
			}
			foreach (var label in colorLabels)
			{
				label.Click += OnSelectColorClick;
			}
			foreach (var label in hexLabels)
			{
				label.Click += OnSelectColorClick;
			}
			foreach (var label in labels)
			{
				label.Enabled = false;
			}

			var resourceName = "Greg.Xrm.ModernThemeBuilder.Resources.template.html";
			this.template = Resources.GetContent(resourceName);

			messenger.Register<SolutionComponentSelected>(OnSolutionComponentSelected);
			this.messenger.Register<ConfigUpdated>(m => LoadSettings());



			// AI stuff

			this.btnSelectImage.Click += OnSelectImageClick;
			this.btnRunAI.Click += OnRunAiClick;
			this.btnRunAI.Enabled = false;
			this.btnSelectImage.Enabled = false;
			this.cmbAiEngine.Enabled = false;
		}

		private void OnSolutionComponentSelected(SolutionComponentSelected msg)
		{
			this.currentComponent = msg.SolutionComponent;

			this.TabText = this.Text = msg.SolutionComponent?.WebResource.name ?? "Theme Editor";
			this.Palette = msg.SolutionComponent?.Palette ?? AppHeaderColors.Default;
			foreach (var label in labels)
			{
				label.Enabled = msg.SolutionComponent != null;
			}
			LoadSettings();
		}



		public async Task RefreshViewAsync()
		{
			await this.browser.EnsureCoreWebView2Async();
			RefreshView();
		}

		private void RefreshView()
		{
			for (int i = 0; i < 8; i++)
			{
				this.colorLabels[i].BackColor = this.Palette[i];
				this.hexLabels[i].Text = this.Palette[i].ToHtml();
			}

			var resourceContent = this.Palette.Update(this.template);
			this.browser.NavigateToString(resourceContent);
		}


		public AppHeaderColors Palette
		{
			get => this.palette;
			set
			{
				this.palette = value;
				RefreshView();
			}
		}


		private void OnSelectColorClick(object sender, EventArgs e)
		{
			var tag = ((Label)sender).Tag;
			if (tag is null) return;
			if (!int.TryParse(tag.ToString(), out var index)) return;

			PeekColor(index - 1);
		}

		public void PeekColor(int colorIndex)
		{
			using (var dialog = new ColorPickerDialog())
			{
				dialog.Text = $"Select color for {this.nameLabels[colorIndex].Text}";
				dialog.Color = this.Palette[colorIndex];
				dialog.StartPosition = FormStartPosition.CenterParent;
				dialog.ShowAlphaChannel = false;

				if (dialog.ShowDialog(this) != DialogResult.OK)
					return;

				this.Palette = this.Palette.SetColor(colorIndex, dialog.Color);

				colorLabels[colorIndex].BackColor = dialog.Color;
				hexLabels[colorIndex].Text = dialog.Color.ToHtml();


				var sample = this.Palette.Update(this.template);
				this.browser.NavigateToString(sample);


				this.currentComponent?.UpdatePalette(this.Palette);
				this.messenger.Send(new SolutionComponentChanged(this.currentComponent));
			}
		}

		public void ResetDefaults()
		{
			this.Palette = AppHeaderColors.Default;
		}



		/// AI stuff


		private void LoadSettings()
		{
			if (this.currentComponent == null)
			{
				this.btnSelectImage.Enabled = false;
				this.cmbAiEngine.Enabled = false;
				this.btnRunAI.Enabled = false;
				return;
			}

			try
			{
				var settings = this.settingsProvider.GetSettings();
				this.btnRunAI.Enabled = this.btnSelectImage.Enabled = settings.IsValid();

				this.cmbAiEngine.Items.Clear();
				if (settings.IsAzureOpenAiValid())
				{
					this.cmbAiEngine.Items.Add("Azure OpenAI");
				}
				if (settings.IsChatGptValid())
				{
					this.cmbAiEngine.Items.Add("ChatGPT");
				}
				if (settings.IsClaudeValid())
				{
					this.cmbAiEngine.Items.Add("Claude");
				}
				if (this.cmbAiEngine.Items.Count > 0)
				{
					this.cmbAiEngine.SelectedIndex = 0;
					this.cmbAiEngine.Enabled = true;
				}
				else
				{
					this.cmbAiEngine.Enabled = false;
				}
			}
			catch
			{
				this.btnSelectImage.Enabled = false;
				this.cmbAiEngine.Enabled = false;
				this.btnRunAI.Enabled = false;
			}
		}



		private void OnSelectImageClick(object sender, EventArgs e)
		{
			using (var dialog = new OpenFileDialog())
			{
				dialog.Title = "Select an Image";
				dialog.Filter = "Image Files (*.png; *.jpg; *.jpeg; *.gif)|*.png;*.jpg;*.jpeg;*.gif";
				dialog.FilterIndex = 1;
				dialog.RestoreDirectory = true;
				
				if (dialog.ShowDialog(this) == DialogResult.OK)
				{
					selectedImageUrl = dialog.FileName;

					if (selectedImageUrl.Length > 70)
					{
						// get the last 97 chars and prepend it with "..."
						this.btnSelectImage.Text = "..." + selectedImageUrl.Substring(selectedImageUrl.Length - 67);
					}
					else
					{
						this.btnSelectImage.Text = selectedImageUrl;
					}
				}
			}
		}

		private void OnRunAiClick(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(selectedImageUrl))
			{
				MessageBox.Show("Please select an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			var engine = this.cmbAiEngine.SelectedItem?.ToString();
			if (string.IsNullOrEmpty(engine))
			{
				MessageBox.Show("Please select an AI engine.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}



			scheduler.Enqueue(new XrmToolBox.Extensibility.WorkAsyncInfo
			{
				Message = "Running AI, please wait...",

				Work = (w, e1) =>
				{
					var analyzer = ImageAnalyzerFactory.CreateAnalyzer(Convert(engine), this.settingsProvider.GetSettings());

					var task = analyzer.AnalyzeImageAsync(this.log, selectedImageUrl, Prompt.ImageAnalyzer);
					task.ConfigureAwait(false);
					task.Wait();

					var result = task.Result;

					if (result.Contains("<") && !result.StartsWith("<"))
					{
						result = result.Substring(result.IndexOf("<"));
					}
					if (result.Contains(">") && !result.EndsWith(">"))
					{
						result = result.Substring(0, result.IndexOf(">") + 1);
					}

					e1.Result = result;
				},

				PostWorkCallBack = (e1) =>
				{
					if (e1.Error != null)
					{
						string errorMessage = e1.Error.Message;
						if (e1.Error is AggregateException ex)
						{
							var sb = new StringBuilder();
							foreach (var innerException in ex.InnerExceptions)
							{
								sb.AppendLine(innerException.Message);
								sb.AppendLine();
							}
							errorMessage = sb.ToString().TrimEnd(Environment.NewLine.ToCharArray());
						}

						MessageBox.Show($"Error running AI: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

						log.Error($"Error running AI: {errorMessage}", e1.Error);
						return;
					}



					if (e1.Result is string xmlResult)
					{
						try
						{
							var colors = AppHeaderColors.FromXmlString(xmlResult);
							if (colors == null)
							{
								MessageBox.Show("AI returned an invalid result." + Environment.NewLine + xmlResult, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
								log.Error($"AI returned an invalid result: " + xmlResult, e1.Error);
								return;
							}

							this.Palette = colors;
							this.currentComponent?.UpdatePalette(colors);
							this.messenger.Send(new SolutionComponentChanged(this.currentComponent));
						}
						catch (Exception ex)
						{
							MessageBox.Show($"Error parsing AI result: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				},
			});
		}

		private static AIProvider Convert(string engine)
		{
			if ("Azure OpenAI".Equals(engine)) return AIProvider.AzureOpenAI;
			if ("ChatGPT".Equals(engine)) return AIProvider.OpenAI;
			if ("Claude".Equals(engine)) return AIProvider.Claude;
			throw new NotSupportedException($"AI engine '{engine}' is not supported.");
		}
	}
}
