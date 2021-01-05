using Greg.Xrm.EnvironmentComparer.Logging;
using Greg.Xrm.EnvironmentComparer.Model;
using Greg.Xrm.EnvironmentComparer.Model.Memento;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using System.IO;

namespace Greg.Xrm.EnvironmentComparer.Views
{
	public class EnvironmentComparerPresenter
	{
		private readonly ILog log;
		private readonly IEnvironmentComparerView view;
		private ConnectionDetail env1;
		private ConnectionDetail env2;
		private IOrganizationService crm1;
		private IOrganizationService crm2;
		private ICompareEngine engine;
		private EngineMemento memento;

		public EnvironmentComparerPresenter(ILog log, IEnvironmentComparerView view)
		{
			this.log = log ?? throw new ArgumentNullException(nameof(log));
			this.view = view ?? throw new ArgumentNullException(nameof(view));
		}


		public void SetEnvironments(ConnectionDetail env1, ConnectionDetail env2)
		{
			try
			{
				this.env1 = env1;
				this.env2 = env2;
				this.crm1 = env1?.GetCrmServiceClient();
				this.crm2 = env2?.GetCrmServiceClient();

				this.view.SetConnectionNames(env1?.ConnectionName, env2?.ConnectionName);
			}
			catch (Exception ex)
			{
				this.view.SetConnectionNames(env1?.ConnectionName, env2?.ConnectionName);

				this.log.Error("Unable to load environments: " + ex.Message, ex);
			}
		}






		


		internal void DownloadComparisonResultAsExcelFile(string outputPath, CompareResultSet compareResult)
		{
			using (log.Track("Exporting comparison result on excel file " + outputPath))
			{
				try
				{
					var reportBuilder = new ReportBuilder(new DirectoryInfo(outputPath));
					reportBuilder.GenerateReport(compareResult, this.env1.ConnectionName, this.env2.ConnectionName);


				}
				catch (Exception ex)
				{
					log.Error("Error during export: " + ex.Message, ex);
				}
			}		
		}
	}
}
