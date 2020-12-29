using Greg.Xrm.EnvironmentComparer.Logging;
using Greg.Xrm.EnvironmentComparer.Model;
using Greg.Xrm.EnvironmentComparer.Model.Memento;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.IO;
using System.ServiceModel;
using System.Text;

namespace Greg.Xrm.EnvironmentComparer.Views
{
	public class EnvironmentComparerPresenter
	{
		private readonly ILog log;
		private readonly IEnvironmentComparerView view;
		private readonly EnvironmentComparerViewModel viewModel;
		private ConnectionDetail env1;
		private ConnectionDetail env2;
		private IOrganizationService crm1;
		private IOrganizationService crm2;
		private ICompareEngine engine;
		private EngineMemento memento;
		private EntityMetadata[] entityMetadataList;

		public EnvironmentComparerPresenter(ILog log, IEnvironmentComparerView view, EnvironmentComparerViewModel viewModel)
		{
			this.log = log ?? throw new ArgumentNullException(nameof(log));
			this.view = view ?? throw new ArgumentNullException(nameof(view));
			this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
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
				this.viewModel.CanLoadEntities = true;
				this.view.CanOpenConfig((this.env1 != null && this.env2 != null));
				this.view.CanExecuteComparison(false);
				this.view.ShowMemento(null);
				this.view.ShowComparisonResult(null);
			}
			catch (Exception ex)
			{
				this.view.SetConnectionNames(env1?.ConnectionName, env2?.ConnectionName);
				this.viewModel.CanLoadEntities = true;
				this.view.CanOpenConfig(false);
				this.view.CanExecuteComparison(false);
				this.view.ShowMemento(null);
				this.view.ShowComparisonResult(null);

				this.log.Error("Unable to load environments: " + ex.Message, ex);
			}
		}


		public void OpenMemento(string mementoFileName)
		{
			if (this.env1 == null)
				throw new InvalidOperationException("Environment 1 must be initialized!");
			if (this.env2 == null)
				throw new InvalidOperationException("Environment 2 must be initialized!");

			try
			{
				this.memento = null;
				this.engine = null;


				this.engine = Compare
					.FromMemento(mementoFileName, out EngineMemento engineMemento)
					.GetEngine(this.crm1, this.crm2, this.log);

				this.memento = engineMemento;
				this.log.Debug("Engine created successfully from file " + mementoFileName);

				this.view.ShowComparisonResult(null);
				this.view.ShowMemento(this.memento);
				this.view.CanExecuteComparison(true);
			}
			catch (ArgumentException ex)
			{
				this.log.Error(ex.Message);
			}
			catch (ExtendedValidationException ex)
			{
				var sb = new StringBuilder();
				sb.AppendLine(ex.Message);

				foreach (var item in ex.Errors)
				{
					sb.Append(" - ").AppendLine(item.ErrorMessage);
				}

				this.log.Error(sb.ToString());
			}
			catch (Exception ex)
			{
				this.log.Error("Error creating engine from JSON config: " + ex.Message, ex);
			}
		}

		public void LoadEntities()
		{
			throw new NotImplementedException();
		}





		internal void ExecuteComparison()
		{
			if (this.memento == null)
				throw new InvalidOperationException("Comparison cannot be performed without memento!");
			if (this.engine == null)
				throw new InvalidOperationException("Comparison cannot be performed without engine!");

			using (log.Track("Executing comparison"))
			{
				try
				{
					var result = this.engine.CompareAll();

					log.Info("Compare completed");

					this.view.ShowComparisonResult(result);
				}
				catch (FaultException<OrganizationServiceFault> ex)
				{
					log.Error("Error while comparing environments: " + ex.Message, ex);
				}
			}
		}

		internal void SetEntities(EntityMetadata[] newEntityMetadataList)
		{
			this.entityMetadataList = newEntityMetadataList;
		}

		internal void DownloadComparisonResultAsExcelFile(string outputPath, CompareResult compareResult)
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
