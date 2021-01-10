using Greg.Xrm.EnvironmentComparer.Engine;
using Greg.Xrm.EnvironmentComparer.Engine.Config;
using Greg.Xrm.EnvironmentComparer.Logging;
using Greg.Xrm.EnvironmentComparer.Messaging;
using Greg.Xrm.EnvironmentComparer.Engine.Memento;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;

namespace Greg.Xrm.EnvironmentComparer.Views.Configurator
{
	public class ConfiguratorViewModel : ViewModel
	{
		private readonly ILog log;
		private readonly IMessenger messenger;

		public ConfiguratorViewModel(ILog log, IMessenger messenger)
		{
			this.log = log;
			this.messenger = messenger;
			this.OverrideSetDefaultValue(() => EntityMetadataList, () => Array.Empty<EntityMetadata>());

			this.WhenChanges(() => EntityMetadataList)
				.ChangesAlso(() => CanAdd)
				.ChangesAlso(() => CanEdit);

			this.WhenChanges(() => SelectedNode)
				.ChangesAlso(() => CanEdit)
				.ChangesAlso(() => EditLabel)
				.ChangesAlso(() => CanRemove)
				.ChangesAlso(() => RemoveLabel);

			this.WhenChanges(() => Memento)
				.ChangesAlso(() => CanSaveMemento)
				.ChangesAlso(() => CanSaveAsMemento)
				.ChangesAlso(() => CanExecuteComparison);

			this.WhenChanges(() => Engine)
				.ChangesAlso(() => CanExecuteComparison);

			this.WhenChanges(() => Crm1)
				.ChangesAlso(() => CanLoadEntities)
				.ChangesAlso(() => CanNewMemento)
				.ChangesAlso(() => CanOpenMemento);

			this.WhenChanges(() => Crm2)
				.ChangesAlso(() => CanNewMemento)
				.ChangesAlso(() => CanOpenMemento);

			messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.Crm1)
				.Execute(m =>
				{
					this.Crm1 = m.GetNewValue<IOrganizationService>();
					this.Memento = null;
				});

			messenger.WhenObject<EnvironmentComparerViewModel>()
				.ChangesProperty(_ => _.Crm2)
				.Execute(m =>
				{
					this.Crm2 = m.GetNewValue<IOrganizationService>();
					this.Memento = null;
				});
		}


		public IOrganizationService Crm1
		{
			get => Get<IOrganizationService>();
			private set => this.Set(value);
		}

		public IOrganizationService Crm2
		{
			get => Get<IOrganizationService>();
			private set => this.Set(value);
		}

		public bool CanLoadEntities
		{
			get => this.Crm1 != null;
		}


		public IReadOnlyCollection<EntityMetadata> EntityMetadataList
		{
			get => Get<IReadOnlyCollection<EntityMetadata>>();
			set => this.Set(value);
		}

		public bool CanAdd
		{
			get => this.EntityMetadataList.Count > 0;
		}


		public TreeNode SelectedNode
		{
			get => Get<TreeNode>();
			set => this.Set(value);
		}

		public bool CanEdit
		{
			get => this.SelectedNode != null && this.EntityMetadataList != null && this.EntityMetadataList.Count > 0;
		}

		public string EditLabel
		{
			get => $"Edit {SelectedNode?.Text}".Trim();
		}

		public bool CanRemove
		{
			get => this.SelectedNode != null;
		}

		public string RemoveLabel
		{
			get => $"Remove {SelectedNode?.Text}".Trim();
		}


		public bool CanNewMemento
		{
			get => this.Crm1 != null && this.Crm2 != null;
		}

		public bool CanOpenMemento
		{
			get => this.Crm1 != null && this.Crm2 != null;
		}

		public bool CanSaveMemento
		{
			get => !string.IsNullOrWhiteSpace(this.Memento?.FileName);
		}

		public bool CanSaveAsMemento
		{
			get => this.Memento != null && this.Memento.Entities.Count > 0;
		}

		public EngineMemento Memento
		{
			get => Get<EngineMemento>();
			set => this.Set(value);
		}


		public ICompareEngine Engine
		{
			get => this.Get<ICompareEngine>();
			set => this.Set(value);
		}


		public CompareResultSet CompareResultSet
		{
			get => Get<CompareResultSet>();
			private set => Set(value);
		}



		public void OpenMemento()
		{
			if (!this.CanOpenMemento) return;

			string mementoFileName;
			using (var dialog = new OpenFileDialog())
			{
				dialog.Filter = "JSON (*.json)|*.json";
				dialog.Title = "Open JSON configuration";

				if (dialog.ShowDialog() != DialogResult.OK) return;
				mementoFileName = dialog.FileName;
			}

			try
			{
				this.Engine = Compare
							.FromMemento(mementoFileName, out EngineMemento engineMemento)
							.GetEngine(this.Crm1, this.Crm2, this.log);

				this.Memento = engineMemento;
				this.CompareResultSet = null;

				this.log.Debug("Engine created successfully from file " + mementoFileName);
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
#pragma warning disable CA1031 // Do not catch general exception types
			catch (Exception ex)
			{
				this.log.Error("Error creating engine from JSON config: " + ex.Message, ex);
			}
#pragma warning restore CA1031 // Do not catch general exception types
		}




		public void NewMemento()
		{
			this.Memento = new EngineMemento();
		}


		public void SaveAsMemento()
		{
			var memento = this.Memento;
			if (memento == null) return;


			string mementoFileName;
			using (var dialog = new SaveFileDialog())
			{
				dialog.Title = "Save as...";
				dialog.Filter = "JSON (*.json)|*.json";
				dialog.FileName = memento.FileName;

				if (dialog.ShowDialog() != DialogResult.OK) return;
				mementoFileName = dialog.FileName;
			}

			Save(memento, mementoFileName);
		}

		public void SaveMemento()
		{
			var memento = this.Memento;
			if (memento == null) return;

			if (string.IsNullOrWhiteSpace(memento.FileName))
			{
				SaveAsMemento();
				return;
			}

			Save(memento, memento.FileName);
		}




		private void Save(EngineMemento memento, string fileName)
		{
			if (memento == null) return;
			if (string.IsNullOrWhiteSpace(fileName)) return;
			


			try
			{
				var mementoString = JsonConvert.SerializeObject(memento, Formatting.Indented);
				this.log.Info("Serialized memento: " + mementoString);

				File.WriteAllText(fileName, mementoString);

				memento.FileName = fileName;
			}
#pragma warning disable CA1031 // Do not catch general exception types
			catch (Exception ex)
			{
				log.Error($"{ex.GetType().Name} saving memento: {ex.Message}" + ex.Message, ex);
			}
#pragma warning restore CA1031 // Do not catch general exception types
		}





		public bool CanExecuteComparison
		{
			get => this.Engine != null && this.Memento != null && Memento.Entities.Count > 0;
		}


		public void ExecuteComparison()
		{
			if (this.Memento == null)
				throw new InvalidOperationException("Comparison cannot be performed without memento!");
			if (this.Memento.Entities.Count == 0)
				throw new InvalidOperationException("Comparison cannot be performed without memento!");
			if (this.Engine == null)
				throw new InvalidOperationException("Comparison cannot be performed without engine!");

			using (log.Track("Executing comparison"))
			{
				try
				{
					var result = this.Engine.CompareAll();

					log.Info("Compare completed");

					this.CompareResultSet = result;
					this.messenger.Send(new CompareResultSetAvailable(result));
				}
				catch (FaultException<OrganizationServiceFault> ex)
				{
					log.Error("Error while comparing environments: " + ex.Message, ex);
				}
			}
		}
	}
}
