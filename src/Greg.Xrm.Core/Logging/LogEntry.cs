using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Greg.Xrm.Logging
{
	class LogEntry
	{
		public DateTime Timestamp { get; set; }
		public Color ForeColor { get; set; }
		public string Level { get; set; }
		public string Message { get; set; }
		public Exception Exception { get; set; }


		public ListViewItem ToListViewItem()
		{
			var item = new ListViewItem(Timestamp.ToString("dd/MM/yyyy HH:mm:ss"))
			{
				Tag = this,
				ForeColor = this.ForeColor
			};
			item.SubItems.Add(Level).ForeColor = this.ForeColor;
			item.SubItems.Add(Message).ForeColor = this.ForeColor;
			if (this.Exception != null)
			{
				var subItem = item.SubItems.Add(this.Exception.ToString());
				subItem.ForeColor = this.ForeColor;
				subItem.Tag = this.Exception;
			}
			return item;
		}


		public override string ToString()
		{
			var sb = new StringBuilder();

			sb.Append(Timestamp.ToString("dd/MM/yyyy HH:mm:ss.fff"));
			sb.Append(" [").Append((Level ?? String.Empty).PadRight(5)).Append("] ");
			sb.Append(this.Message);
			if (this.Exception != null)
			{
				sb.AppendLine();
				sb.Append(sb.ToJsonString());
				sb.AppendLine();
			}

			return sb.ToString();
		}

	}
}
