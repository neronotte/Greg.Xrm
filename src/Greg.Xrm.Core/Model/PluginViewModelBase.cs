using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Greg.Xrm.Model
{
	public abstract class PluginViewModelBase : ViewModel
	{
		public bool AllowRequests
		{
			get => Get<bool>();
			set => Set(value);
		}
	}
}
