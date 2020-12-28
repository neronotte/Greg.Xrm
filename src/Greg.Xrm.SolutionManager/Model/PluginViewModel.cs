using Greg.Xrm.Model;
using System.Xml.Linq;

namespace Greg.Xrm.SolutionManager.Model
{
	public class PluginViewModel : PluginViewModelBase
	{
		private bool canStartMonitoring, canStopMonitoring, stopMonitoringRequested;
		private XDocument currentImportJobData;

		public PluginViewModel()
		{
			this.canStartMonitoring = true;
			this.canStopMonitoring = false;
		}


		public bool CanStartMonitoring
		{
			get => this.canStartMonitoring;
			set
			{
				if (value == this.canStartMonitoring) return;

				this.canStartMonitoring = value;
				this.OnPropertyChanged();

				this.CanStopMonitoring = !value;
			}
		}


		public bool CanStopMonitoring
		{
			get => this.canStopMonitoring;
			set
			{
				if (value == this.canStopMonitoring) return;

				this.canStopMonitoring = value;
				this.OnPropertyChanged();
				this.CanStartMonitoring = !value;
			}
		}

		public bool StopMonitoringRequested
		{
			get => this.stopMonitoringRequested;
			set
			{
				if (value == this.stopMonitoringRequested) return;

				this.stopMonitoringRequested = value;
				this.OnPropertyChanged();

				if (value == true)
				{
					this.canStopMonitoring = false;
					this.OnPropertyChanged(nameof(CanStopMonitoring));
				}
			}
		}

		public XDocument CurrentImportJobData
		{
			get => this.currentImportJobData;
			set
			{
				if (value == this.currentImportJobData) return;

				this.currentImportJobData = value;
				this.OnPropertyChanged();
			}
		}
	}
}
