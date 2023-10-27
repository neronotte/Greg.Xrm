using Cyotek.Windows.Forms;
using Greg.Xrm.Messaging;
using Greg.Xrm.ModernThemeBuilder.Model;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.ModernThemeBuilder.Views
{
	public partial class PaletteEditorView : DockContent
	{
		private readonly WebView2 browser = new WebView2();
		private readonly List<System.Windows.Forms.Label> nameLabels = new List<System.Windows.Forms.Label>();
		private readonly List<System.Windows.Forms.Label> colorLabels = new List<System.Windows.Forms.Label>();
		private readonly List<System.Windows.Forms.Label> hexLabels = new List<System.Windows.Forms.Label>();
		private readonly List<System.Windows.Forms.Control> labels = new List<System.Windows.Forms.Control>();
		private readonly string template;
		private readonly IMessenger messenger;
		private AppHeaderColors palette = AppHeaderColors.Default;

		private SolutionComponent currentComponent;

		public PaletteEditorView(IMessenger messenger)
		{
			this.browser.Dock = System.Windows.Forms.DockStyle.Top;
			this.browser.Height = 80;

			InitializeComponent();

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
			this.messenger = messenger;
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
	}
}
