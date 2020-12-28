using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Greg.Xrm.Model
{
	public abstract class PluginViewModelBase : INotifyPropertyChanged
	{
		private bool allowRequests;
		public bool AllowRequests
		{
			get => this.allowRequests;
			set 
			{
				this.allowRequests = value;
				this.OnPropertyChanged();
			}
		}


		protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


		public event PropertyChangedEventHandler PropertyChanged;
	}
}
