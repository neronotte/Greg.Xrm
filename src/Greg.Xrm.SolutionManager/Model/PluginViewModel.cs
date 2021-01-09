using Greg.Xrm.Model;
using System.Xml.Linq;

namespace Greg.Xrm.SolutionManager.Model
{
	public class PluginViewModel : PluginViewModelBase
	{
		public PluginViewModel()
		{
			this.CanStartMonitoring = true;
			this.WhenChanges(() => CanStartMonitoring).ChangesAlso(() => CanStopMonitoring);
		}


		public bool CanStartMonitoring
		{
			get => Get<bool>();
			set => Set(value);
		}


		public bool CanStopMonitoring
		{
			get => !CanStartMonitoring;
		}

		public bool StopMonitoringRequested
		{
			get => Get<bool>();
			set
			{
				Set(value);

				if (value == true)
				{
					this.CanStartMonitoring = true;
				}
			}
		}

		public XDocument CurrentImportJobData
		{
			get => Get<XDocument>();
			set => Set(value);
		}
	}
}
