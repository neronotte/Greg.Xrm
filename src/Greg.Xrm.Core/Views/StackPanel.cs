using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Greg.Xrm.Core.Views
{
	/// <summary>
	/// A stackpanel similar to the Wpf stackpanel.
	/// </summary>
	public class StackPanel : FlowLayoutPanel
	{
		public StackPanel() : base()
		{
			InitializeComponent();
			this.ForceAutoresizeOfControls = true;
			this.FlowDirection = FlowDirection.TopDown;
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			//
			// StackPanel
			//
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.WrapContents = false;
			this.ResumeLayout(false);
		}

		/// <summary>
		/// Override it just in order to hide it in design mode.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool WrapContents
		{
			get { return base.WrapContents; }
			set { base.WrapContents = value; }
		}

		/// <summary>
		/// Override it just in order to set its default value.
		/// </summary>
		[DefaultValue(typeof(AutoSizeMode), "GrowAndShrink")]
		public override AutoSizeMode AutoSizeMode
		{
			get { return base.AutoSizeMode; }
			set { base.AutoSizeMode = value; }
		}

		/// <summary>
		/// Get or set a value that when is true forces the resizing of each control.
		/// If this value is false then only control that have AutoSize == true will be resized to
		/// fit the client size of this container.
		/// </summary>
		[DefaultValue(true)]
		public bool ForceAutoresizeOfControls { get; set; }

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.SuspendLayout();
			switch (FlowDirection)
			{
				case FlowDirection.BottomUp:
				case FlowDirection.TopDown:
					foreach (Control control in this.Controls)
						if (ForceAutoresizeOfControls || control.AutoSize)
							control.Width = this.ClientSize.Width - control.Margin.Left - control.Margin.Right;
					break;
				case FlowDirection.LeftToRight:
				case FlowDirection.RightToLeft:
					foreach (Control control in this.Controls)
						if (ForceAutoresizeOfControls || control.AutoSize)
							control.Height = this.ClientSize.Height - control.Margin.Top - control.Margin.Bottom;
					break;
				default:
					break;
			}
			this.ResumeLayout();
		}

		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout(levent);

			if (levent != null && levent.AffectedControl != null)
			{
				Control control = levent.AffectedControl;
				if (ForceAutoresizeOfControls || control.AutoSize)
				{
					switch (FlowDirection)
					{
						case FlowDirection.BottomUp:
						case FlowDirection.TopDown:
							control.Width = this.ClientSize.Width - control.Margin.Left - control.Margin.Right;
							break;
						case FlowDirection.LeftToRight:
						case FlowDirection.RightToLeft:
							control.Height = this.ClientSize.Height - control.Margin.Top - control.Margin.Bottom;
							break;
						default:
							break;
					}
				}
			}
		}
	}
}
