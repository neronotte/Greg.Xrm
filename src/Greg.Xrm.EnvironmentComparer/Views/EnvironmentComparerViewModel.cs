using Greg.Xrm.Model;

namespace Greg.Xrm.EnvironmentComparer.Views
{
	public class EnvironmentComparerViewModel : PluginViewModelBase
	{
		private bool canLoadEntities;
		public bool CanLoadEntities
		{
			get => this.canLoadEntities;
			set 
			{
				if (value == this.canLoadEntities) return;
				this.canLoadEntities = value;
				this.OnPropertyChanged();
			}
		}
	}
}
