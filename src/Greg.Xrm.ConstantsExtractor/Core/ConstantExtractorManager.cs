using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greg.Xrm.ConstantsExtractor.Core
{
	public class ConstantExtractorManager
	{
		private static readonly string[] EntityCommonFields = new string[17]
		{
			"createdby",
			"createdon",
			"createdonbehalfby",
			"importsequencenumber",
			"modifiedby",
			"modifiedon",
			"modifiedonbehalfby",
			"overriddencreatedon",
			"ownerid",
			"owningbusinessunit",
			"owningteam",
			"owninguser",
			"statecode",
			"statuscode",
			"timezoneruleversionnumber",
			"utcconversiontimezonecode",
			"versionnumber"
		};

		public string SolutionName { get; set; }

		public List<Guid> MetadataEntityGuidsFromSolution { get; set; }

		public EntityMetadata[] EntitiesMetadata { get; set; }

		public EntityMetadataManager ActivityPointerMetadata { get; set; }

		public EntityMetadata AccountMetadata { get; set; }

		public List<GlobalOptionSetsMetadataManager> GlobalOptionSetsMetadata { get; set; }

		public List<GlobalOptionSetsMetadataManager> GlobalBooleanOptionSetsMetadata { get; set; }

		public List<EntityMetadataManager> EntityData { get; set; }

		public List<string> EntityNamesFromFile { get; set; }
		public ILog Log { get; }
		public IOrganizationService Service { get; set; }

		public string FilePathCS { get; set; }

		public string FilePathJS { get; set; }

		public bool ExtractTypes { get; set; }

		public bool ExtractDescriptions { get; set; }

		public string NameSpaceCs { get; set; }

		public string NameSpaceJs { get; set; }

		public string JsHeaderLines { get; set; }

		public EntityMetadataManager EntityCommonAttributes { get; set; }

		public ConstantExtractorManager(
			ILog log,
			IOrganizationService service,
			string solutionName,
			bool extractTypes,
			bool extractDescriptions,
			string filePathCs = null,
			string filePathJs = null,
			string nameSpaceCs = null,
			string nameSpaceJs = null,
			string jsHeaderLines = null)
		{
			Log = log;
			this.Service = service;
			this.SolutionName = solutionName;
			this.EntityData = new List<EntityMetadataManager>();
			this.GlobalOptionSetsMetadata = new List<GlobalOptionSetsMetadataManager>();
			this.GlobalBooleanOptionSetsMetadata = new List<GlobalOptionSetsMetadataManager>();
			this.EntityNamesFromFile = null;
			this.FilePathCS = filePathCs;
			this.FilePathJS = filePathJs;
			this.ExtractTypes = extractTypes;
			this.ExtractDescriptions = extractDescriptions;
			this.MetadataEntityGuidsFromSolution = new List<Guid>();
			this.NameSpaceJs = !string.IsNullOrEmpty(nameSpaceJs) ? nameSpaceJs : string.Empty;
			this.NameSpaceCs = !string.IsNullOrEmpty(nameSpaceCs) ? nameSpaceCs : string.Empty;
			this.JsHeaderLines = !string.IsNullOrEmpty(jsHeaderLines) ? jsHeaderLines : string.Empty;
		}

		public void ExtractConstants()
		{
			using (Log.Track("Reading info from Dataverse"))
			{
				this.GetEntities();
				this.GetGlobalOptionSets();
				this.GetEntityMetadataFromSolution();
				this.CheckActivityEntities();
				this.ParallelExtractData();
				this.ExtractEntityCommonFieldMetadata();
			}
		}

		private void GetEntities()
		{
			using (Log.Track("Reading entities"))
			{
				this.EntitiesMetadata = ((RetrieveAllEntitiesResponse)this.Service.Execute(new RetrieveAllEntitiesRequest
				{
					EntityFilters = (EntityFilters.Entity | EntityFilters.Attributes | EntityFilters.Relationships)
				})).EntityMetadata;

				this.AccountMetadata = this.EntitiesMetadata.FirstOrDefault(ent => ent.LogicalName == "account");
			}
		}

		private void GetGlobalOptionSets()
		{
			using (Log.Track("Reading global option sets"))
			{
				var optionSetsResponse = (RetrieveAllOptionSetsResponse)this.Service.Execute(new RetrieveAllOptionSetsRequest());

				this.GlobalOptionSetsMetadata = optionSetsResponse.OptionSetMetadata
					.Where(optMetadata => optMetadata is OptionSetMetadata metadata && metadata.Options.Count > 0)
					.Select(opt => GetGlobalOptionSetsMetadataManager(opt))
					.OrderBy(opt => opt.LogicalName)
					.ToList();

				this.GlobalBooleanOptionSetsMetadata = optionSetsResponse.OptionSetMetadata
					.Where(optMetadata => optMetadata is BooleanOptionSetMetadata)
					.Select(opt => GetGlobalOptionSetsMetadataManager(opt))
					.OrderBy(opt => opt.LogicalName)
					.ToList();
			}
		}

		private void GetEntityMetadataFromSolution()
		{
			using (Log.Track("Getting entity metadata from solution"))
			{
				var query = new QueryExpression("solution");
				query.Criteria.AddCondition(new ConditionExpression("uniquename", ConditionOperator.Equal, SolutionName));
				var solution = this.Service.RetrieveMultiple(query).Entities.FirstOrDefault()
					?? throw new InvalidOperationException("Invalid solution name: " + SolutionName);

				query = new QueryExpression("solutioncomponent");
				query.ColumnSet.AllColumns = true;
				query.Criteria.AddCondition(new ConditionExpression("solutionid", ConditionOperator.Equal, solution.Id));
				query.Criteria.AddCondition(new ConditionExpression("componenttype", ConditionOperator.Equal, 1));

				this.MetadataEntityGuidsFromSolution = this.Service.RetrieveMultiple(query).Entities
					.Where(x => x.Contains("objectid"))
					.Select(x => x.GetAttributeValue<Guid>("objectid"))
					.ToList();
			}

		}

		private void CheckActivityEntities()
		{
			using (Log.Track("Checking for activity entities"))
			{
				var activityPointerMetadata = this.EntitiesMetadata.FirstOrDefault(ent => ent.LogicalName == "activitypointer");

				var list = this.EntitiesMetadata.Where(ent => this.MetadataEntityGuidsFromSolution.Contains(ent.MetadataId.Value)).ToList();
				if (list.Where(ent => ent.IsActivity.Value).ToList().Count > 0 && list.FirstOrDefault(ent => ent.LogicalName == "activitypointer") == null)
					list.Add(activityPointerMetadata);

				this.EntitiesMetadata = list.ToArray();
			}
		}

		private void ParallelExtractData()
		{
			using (Log.Track("Transforming metadata"))
			{
				var entitiesMetadata = this.EntitiesMetadata;
				var parallelOptions = new ParallelOptions
				{
					MaxDegreeOfParallelism = 1
				};

				var entityConcurrentData = new ConcurrentBag<EntityMetadataManager>();

				void body(EntityMetadata entMetadata)
				{
					if (entMetadata.DisplayName.LocalizedLabels.Count <= 0)
						return;

					string label = entMetadata.DisplayName.LocalizedLabels.FirstOrDefault()?.Label;
					var entityMetadataManager = new EntityMetadataManager(label, entMetadata.LogicalName, entMetadata.IsActivity.Value, EntityCommonFields.ToList());

					Console.WriteLine(label);
					foreach (AttributeMetadata attribute in entMetadata.Attributes)
					{
						if (attribute.DisplayName.LocalizedLabels.Count > 0)
						{
							AttributeMetadataManager elementFromMetadata = GetAttributeElementFromMetadata(attribute, entMetadata.LogicalName);
							entityMetadataManager.AddAttribute(elementFromMetadata);
						}
					}
					entityMetadataManager.OptionSetAttributes = entityMetadataManager.OptionSetAttributes.OrderBy(opt => opt.LogicalNameConstant).ToList();
					entityConcurrentData.Add(entityMetadataManager);
				}
				Parallel.ForEach(entitiesMetadata, parallelOptions, body);
				this.EntityData = entityConcurrentData.OrderBy(data => data.EntityLogicalName).ToList();
			}
		}

		private void ExtractEntityCommonFieldMetadata()
		{
			this.ActivityPointerMetadata = this.EntityData.FirstOrDefault(ent => ent.EntityLogicalName == "activitypointer");
			var list = this.AccountMetadata.Attributes.Where(attr => EntityCommonFields.Contains(attr.LogicalName)).ToList();
			this.EntityCommonAttributes = new EntityMetadataManager("Entity Generic", "EntityGenericConstants", false, new List<string>());
			void action(AttributeMetadata attr)
			{
				if (attr.DisplayName.LocalizedLabels.Count <= 0)
					return;
				this.EntityCommonAttributes.AddAttribute(GetAttributeElementFromMetadata(attr, null));
			}
			list.ForEach(action);
			this.EntityData.Insert(0, this.EntityCommonAttributes);
		}

		private static GlobalOptionSetsMetadataManager GetGlobalOptionSetsMetadataManager(OptionSetMetadataBase opt)
		{
			string displayName = string.Empty;
			var label = opt.DisplayName.LocalizedLabels.FirstOrDefault();
			if (label != null)
				displayName = label.Label;

			return new GlobalOptionSetsMetadataManager(displayName, opt.Name, GetOptionsetValuesFromMetadata(opt));
		}

		private static AttributeMetadataManager GetAttributeElementFromMetadata(
		  AttributeMetadata attribute,
		  string entityLogicalName = null)
		{
			var label = attribute.DisplayName.LocalizedLabels.FirstOrDefault()?.Label;

			var description = string.Empty;
			if (attribute.Description.LocalizedLabels.Count != 0)
				description = attribute.Description.LocalizedLabels[0].Label.Replace("\n", string.Empty).Replace("\t", string.Empty);



			if (attribute.AttributeType == AttributeTypeCode.Lookup)
			{
				var list = ((LookupAttributeMetadata)attribute).Targets.ToList();
				return new AttributeMetadataManagerForLookup(entityLogicalName, label, attribute.LogicalName, attribute.AttributeType.ToString(), description, list);
			}

			if (attribute.AttributeType == AttributeTypeCode.Picklist)
			{
				var optionSet = ((EnumAttributeMetadata)attribute).OptionSet;
				var globalOptionSetName = optionSet.GetGlobalOptionSetName();
				var valuesFromMetadata = GetOptionsetValuesFromMetadata(optionSet);
				return new AttributeMetadataManagerForPicklist(entityLogicalName, label, attribute.LogicalName, attribute.AttributeType.ToString(), description, valuesFromMetadata, globalOptionSetName);
			}

			if (attribute.AttributeType == AttributeTypeCode.Virtual)
			{
				if (attribute is MultiSelectPicklistAttributeMetadata)
				{
					var optionSet = ((EnumAttributeMetadata)attribute).OptionSet;
					var globalOptionSetName = optionSet.GetGlobalOptionSetName();
					var valuesFromMetadata = GetOptionsetValuesFromMetadata(optionSet);
					return new AttributeMetadataManagerForPicklist(entityLogicalName, label, attribute.LogicalName, attribute.AttributeType.ToString(), description, valuesFromMetadata, globalOptionSetName);
				}

				return new AttributeMetadataManager(entityLogicalName, label, attribute.LogicalName, attribute.AttributeType.ToString(), description);
			}


			if (attribute.AttributeType == AttributeTypeCode.Boolean)
			{
				var optionSet = ((BooleanAttributeMetadata)attribute).OptionSet;
				var valuesFromMetadata = GetOptionsetValuesFromMetadata(optionSet);
				var globalOptionSetName = optionSet.GetGlobalOptionSetName();
				return new AttributeMetadataManagerForPicklist(entityLogicalName, label, attribute.LogicalName, attribute.AttributeType.ToString(), description, valuesFromMetadata, globalOptionSetName);
			}


			if (attribute.AttributeType == AttributeTypeCode.State)
			{
				var valuesFromMetadata = GetOptionsetValuesFromMetadata(((EnumAttributeMetadata)attribute).OptionSet);
				return new AttributeMetadataManagerForStatus(entityLogicalName, label, attribute.LogicalName, attribute.AttributeType.ToString(), description, valuesFromMetadata);
			}

			if (attribute.AttributeType == AttributeTypeCode.Status)
			{
				var valuesFromMetadata = GetStatusReasonValuesFromMetadata(((EnumAttributeMetadata)attribute).OptionSet);
				return new AttributeMetadataManagerForStatusReason(entityLogicalName, label, attribute.LogicalName, attribute.AttributeType.ToString(), description, valuesFromMetadata);
			}


			return new AttributeMetadataManager(entityLogicalName, label, attribute.LogicalName, attribute.AttributeType.ToString(), description);
		}

		private static Dictionary<int, string> GetOptionsetValuesFromMetadata(OptionSetMetadataBase optionSets)
		{
			var optionsetValues = new Dictionary<int, string>();
			if (optionSets is OptionSetMetadata options)
			{
				foreach (OptionMetadata option in options.Options)
				{
					var value = option.Value;
					var label = option.Label.LocalizedLabels[0].Label;
					optionsetValues[value.Value] = label;
				}
			}

			if (optionSets is BooleanOptionSetMetadata boolOptions)
			{
				var trueOption = boolOptions.TrueOption;
				var falseOption = boolOptions.FalseOption;
				var dictionary1 = optionsetValues;
				int? nullable = trueOption.Value;
				int key1 = nullable.Value;
				string label1 = trueOption.Label.LocalizedLabels[0].Label;
				dictionary1.Add(key1, label1);
				Dictionary<int, string> dictionary2 = optionsetValues;
				nullable = falseOption.Value;
				int key2 = nullable.Value;
				string label2 = falseOption.Label.LocalizedLabels[0].Label;
				dictionary2.Add(key2, label2);
			}
			return optionsetValues;
		}

		private static List<Tuple<int, int, string>> GetStatusReasonValuesFromMetadata(OptionSetMetadata optionSets)
		{
			var statusReasontValues = new List<Tuple<int, int, string>>();
			foreach (var option in optionSets.Options.OfType<StatusOptionMetadata>())
			{
				var tuple = new Tuple<int, int, string>(option.Value.Value, option.State.Value, option.Label.LocalizedLabels[0].Label);
				statusReasontValues.Add(tuple);
			}
			return statusReasontValues;
		}
	}
}
