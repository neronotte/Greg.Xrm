using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Greg.Xrm.Core.Views
{
	public class NotificationPanel : StackPanel
	{
		private readonly List<System.Timers.Timer> timers = new List<System.Timers.Timer>();
		private readonly ToolTip tooltip = new ToolTip();

		public NotificationPanel()
		{
			this.Padding = new Padding(0);
		}

		private void AddPanel(string message, Color backColor, Bitmap icon, int? timerInSeconds)
		{
			var timer = new System.Timers.Timer(timerInSeconds.GetValueOrDefault(10) * 1000);
			timer.Start();
			this.timers.Add(timer);


			var panel = new Panel();
			panel.BackColor = backColor;
			panel.Width = 600;
			panel.Height = 26;
			panel.Padding = new Padding(5);
			panel.Margin = new Padding(0);

			Action closeAll = () =>
			{
#pragma warning disable S2486 // Generic exceptions should not be ignored
				try
				{
					this.Controls.Remove(panel);
					panel.Dispose();
					timer.Dispose();
					this.timers.Remove(timer);
				}
				catch { }
#pragma warning restore S2486 // Generic exceptions should not be ignored
			};
			timer.Elapsed += (s, e) =>
			{
				try
				{
					BeginInvoke(closeAll);
				}
				catch { }
			};

			if (icon != null)
			{
				var picture = new PictureBox();
				picture.Image = icon;
				picture.Width = 16;
				picture.Height = 16;
				picture.Left = 5;
				picture.Top = 5;
				picture.Anchor = AnchorStyles.Left | AnchorStyles.Top;
				panel.Controls.Add(picture);
			}


			var label = new Label();
			label.Text = message;
			label.AutoSize = false;
			label.AutoEllipsis = true;
			label.ForeColor = Color.FromArgb(50, 49, 48);
			label.Top = 0;
			label.Left = icon == null ? 5 : 30;
			label.Width = panel.Width - label.Left - 30;
			label.Height = panel.Height;
			label.TextAlign = ContentAlignment.MiddleLeft;
			label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			panel.Controls.Add(label);


			var closeButton = new PictureBox();
			closeButton.Image = Properties.Resources.cross;
			closeButton.Width = 16;
			closeButton.Height = 16;
			closeButton.Left = panel.Width - closeButton.Width - 5;
			closeButton.Top = 5;
			closeButton.Anchor = AnchorStyles.Right | AnchorStyles.Top;
			closeButton.Cursor = Cursors.Hand;
			closeButton.BorderStyle = BorderStyle.None;
			closeButton.Click += (s, e) => closeAll();
			panel.Controls.Add(closeButton);

			tooltip.SetToolTip(label, message);

			this.Controls.Add(panel);


		}


		public void AddSuccess(string message, int? timerInSeconds = null) => AddPanel(message, Color.FromArgb(223, 246, 221), Properties.Resources.success, timerInSeconds);

		public void AddWarning(string message, int? timerInSeconds = null) => AddPanel(message, Color.FromArgb(255, 244, 206), Properties.Resources.error, timerInSeconds);

		public void AddError(string message, int? timerInSeconds = null) => AddPanel(message, Color.FromArgb(253, 231, 233), Properties.Resources.exclamation, timerInSeconds);

		public void AddInfo(string message, int? timerInSeconds = null) => AddPanel(message, Color.FromArgb(204, 229, 255), Properties.Resources.information, timerInSeconds);




		public void Bind(INotificationProvider provider)
		{
			provider.Notify += (s, e) =>
			{
				switch (e.Type)
				{
					case NotificationType.Success:
						AddSuccess(e.Message, e.TimerInSeconds);
						break;
					case NotificationType.Warning:
						AddWarning(e.Message, e.TimerInSeconds);
						break;
					case NotificationType.Error:
						AddError(e.Message, e.TimerInSeconds);
						break;
					case NotificationType.Info:
						AddInfo(e.Message, e.TimerInSeconds);
						break;
				}
			};
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this.timers.ForEach(t => t.Dispose());
				this.timers.Clear();
			}
		}
	}
}
