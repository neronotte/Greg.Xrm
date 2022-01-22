using Greg.Xrm.Messaging;
using Greg.Xrm.Theming;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.Logging
{
	public partial class OutputView : DockContent, ILog
	{
		private readonly IThemeProvider themeProvider;

		private readonly List<LogEntry> logList = new List<LogEntry>();

		private string currentLevelFilter = null;

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

		public void Info(string message)
		{
			this.Log("INFO", message, null, Color.RoyalBlue);
		}

		public void Warn(string message)
		{
			this.Log("WARN", message, null, Color.Orange);
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


		private void Log(string level, string message, Exception ex, Color color)
		{
			if (this.InvokeRequired)
			{
				Action d = () => Log(level, message, ex, color);
				this.Invoke(d);

				return;
			}


			var logEntry = new LogEntry
			{
				Timestamp = DateTime.Now,
				ForeColor =color,
				Level = level,
				Message = message,
				Exception = ex
			};
			this.logList.Add(logEntry);

			if (!string.IsNullOrWhiteSpace(this.currentLevelFilter) && !this.currentLevelFilter.Contains(logEntry.Level))
			{
				return;
			}

			var item = logEntry.ToListViewItem();
			this.listView1.Items.Add(item);
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

		private void OnFilterByLevel(object sender, EventArgs e)
		{
			var sourceButton = (ToolStripMenuItem)sender;

			var levelToFilter = sourceButton.Tag?.ToString();

			if (string.Equals(currentLevelFilter, levelToFilter, StringComparison.OrdinalIgnoreCase)) return;

			if (string.IsNullOrWhiteSpace(levelToFilter))
			{
				this.tFilterByLevel.Text = "Filter by level";
			}
			else
			{
				var levelLabel = sourceButton.Text;
				this.tFilterByLevel.Text = "Filter by level: " + levelLabel;
			}

			this.currentLevelFilter = levelToFilter;

			this.listView1.BeginUpdate();

			this.listView1.Items.Clear();
			ListViewItem item = null;
			foreach (var logEntry in logList)
			{
				if (!string.IsNullOrWhiteSpace(this.currentLevelFilter) && !this.currentLevelFilter.Contains(logEntry.Level))
				{
					continue;
				}

				item = logEntry.ToListViewItem();
				this.listView1.Items.Add(item);
			}

			this.listView1.EndUpdate();
			if (item != null)
				item.EnsureVisible();
		}

		private void OnExportLogs(object sender, EventArgs e)
		{
			var logEntryToExportList= this.listView1.Items.Cast<ListViewItem>().Select(x => x.Tag).OfType<LogEntry>().ToList();
			if (logEntryToExportList.Count == 0)
			{
				MessageBox.Show("Nothing to export!", "Export logs", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}


			using (var dialog = new SaveFileDialog())
			{
				dialog.Title = "Export logs";
				dialog.Filter = "Text file (*.txt)|*.txt";

				if (dialog.ShowDialog() != DialogResult.OK) return;


				try
				{
					using (var writer = new StreamWriter(dialog.OpenFile()))
					{
						foreach (var logEntry in this.listView1.Items.Cast<ListViewItem>().Select(x => x.Tag).OfType<LogEntry>())
						{
							writer.WriteLine(logEntry.ToString());
						}
						writer.Flush();
					}

					Process.Start(dialog.FileName);
				}
				catch(Exception ex)
				{
					this.Error("Error exporting logs: " + ex.Message, ex);
				}
			}
		}
	}
}
