﻿using Greg.Xrm.Messaging;
using Greg.Xrm.Theming;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.Logging
{
	public partial class OutputView : DockContent, ILog
	{
		private readonly IThemeProvider themeProvider;

		public OutputView(IThemeProvider themeProvider, IMessenger messenger)
		{
			this.themeProvider = themeProvider;
			InitializeComponent();


			ApplyTheme();

			messenger.Register<ShowOutputView>(m =>
			{
				this.Show();
			});
		}

		private void ApplyTheme()
		{
			var theme = this.themeProvider.GetCurrentTheme();
			theme.ApplyTo(this.listView1);
		}

		public void Debug(string message)
		{
			this.Log("DEBUG", message, null, Color.LightGray);
		}

		public void Error(string message)
		{
			this.Log("ERROR", message, null, Color.Red);
		}

		public void Error(string message, Exception ex)
		{
			this.Log("ERROR", message, ex, Color.Red);
		}

		public void Fatal(string message, Exception ex)
		{
			this.Log("FATAL", message, ex, Color.DarkRed);
		}

		public void Info(string message)
		{
			this.Log("INFO", message, null, Color.RoyalBlue);
		}

		public IDisposable Track(string message)
		{
			return new Tracker(this, message);
		}


		class Tracker : IDisposable
		{
			private readonly ILog log;
			private readonly string message;
			private readonly Stopwatch sw;

			public Tracker(ILog log, string message)
			{
				this.log = log;
				this.message = message;
				this.sw = Stopwatch.StartNew();
				this.log.Debug(message + "...");
			}

			public void Dispose()
			{
				this.sw.Stop();
				this.log.Debug(message + "...COMPLETED in " + sw.Elapsed);
			}
		}

		public void Warn(string message)
		{
			this.Log("WARN", message, null, Color.Orange);
		}


		private void Log(string level, string message, Exception ex, Color color)
		{
			if (this.InvokeRequired)
			{
				Action d = () => Log(level, message, ex, color);
				this.Invoke(d);

				return;
			}


			var item = this.listView1.Items.Add(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
			item.ForeColor = color;
			item.SubItems.Add(level).ForeColor = color;
			item.SubItems.Add(message).ForeColor = color;
			if (ex != null)
			{
				var subItem = item.SubItems.Add(ex.ToString()) ;
				subItem.ForeColor = color;
				subItem.Tag = ex;
			}
			item.EnsureVisible();
		}

		private void OnSelectedIndexChanged(object sender, EventArgs e)
		{
			this.cmiCopyMessage.Enabled = this.listView1.SelectedItems.Count > 0;
			this.cmiCopyException.Enabled = this.listView1.SelectedItems.Count == 1 && this.listView1.SelectedItems[0].SubItems.Count >= 4;
		}

		private void OnCopyMessageClick(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count == 0) return;

			var textToCopy = this.listView1.SelectedItems
				.OfType<ListViewItem>()
				.Select(_ => _.SubItems[2].Text)
				.Join(Environment.NewLine);

			Clipboard.SetText(textToCopy);
		}

		private void OnCopyException(object sender, EventArgs e)
		{
			if (this.listView1.SelectedItems.Count == 0) return;

			var selectedItem = this.listView1.SelectedItems[0];
			if (selectedItem.SubItems.Count < 4) return;

			var subItem = selectedItem.SubItems[3];
			var ex = subItem.Tag;

			Clipboard.SetText(ex.ToJsonString());
		}
	}
}
