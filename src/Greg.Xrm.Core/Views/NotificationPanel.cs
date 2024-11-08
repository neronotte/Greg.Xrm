using System;
using System.Drawing;
using System.Windows.Forms;

namespace Greg.Xrm.Core.Views
{
	public class NotificationPanel : StackPanel
	{
		private readonly ToolTip tooltip = new ToolTip();

		public NotificationPanel()
		{
			this.Padding = new Padding(0);
		}

		private void AddPanel(string message, Color backColor, Bitmap icon = null)
		{
			var timer = new System.Timers.Timer(15 * 1000);
			timer.Start();


			var panel = new Panel();
			panel.BackColor = backColor;
			panel.Width = 600;
			panel.Height = 26;
			panel.Padding = new Padding(5);
			panel.Margin = new Padding(0);

			Action closeAll = () =>
			{
				this.Controls.Remove(panel);
				panel.Dispose();
				timer.Dispose();
			};
			timer.Elapsed += (s, e) => BeginInvoke(closeAll);


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


		public void AddSuccess(string message) => AddPanel(message, Color.FromArgb(223, 246, 221), Properties.Resources.success);

		public void AddWarning(string message) => AddPanel(message, Color.FromArgb(255, 244, 206), Properties.Resources.error);

		public void AddError(string message) => AddPanel(message, Color.FromArgb(253, 231, 233), Properties.Resources.exclamation);

		public void AddInfo(string message) => AddPanel(message, Color.FromArgb(204, 229, 255), Properties.Resources.information);




		public void Bind(INotificationProvider provider)
		{
			provider.Notify += (s, e) =>
			{
				switch (e.Type)
				{
					case NotificationType.Success:
						AddSuccess(e.Message);
						break;
					case NotificationType.Warning:
						AddWarning(e.Message);
						break;
					case NotificationType.Error:
						AddError(e.Message);
						break;
					case NotificationType.Info:
						AddInfo(e.Message);
						break;
				}
			};
		}
	}
}
