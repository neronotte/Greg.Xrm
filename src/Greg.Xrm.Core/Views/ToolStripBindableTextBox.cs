using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Greg.Xrm.Views
{
	public class ToolStripBindableTextBox : ToolStripTextBox, IBindableComponent
	{
		private ControlBindingsCollection dataBindings;
		private BindingContext bindingContext;
		private string watermark;



		private const int EM_SETCUEBANNER = 0x1501;
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);


		public ToolStripBindableTextBox()
		{
			this.Control.HandleCreated += Control_HandleCreated;
		}


		private void Control_HandleCreated(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(watermark))
				UpdateCueBanner();
		}


		public string Watermark
		{
			get { return this.watermark; }
			set
			{
				this.watermark = value;
				UpdateCueBanner();
			}
		}
		private void UpdateCueBanner()
		{
			SendMessage(this.Control.Handle, EM_SETCUEBANNER, 0, watermark);
		}




		public ControlBindingsCollection DataBindings
		{
			get
			{
				if (dataBindings == null) dataBindings = new ControlBindingsCollection(this);
				return dataBindings;
			}
		}

		public BindingContext BindingContext
		{
			get
			{
				if (bindingContext == null) bindingContext = new BindingContext();
				return bindingContext;
			}
			set { bindingContext = value; }
		}
	}
}
