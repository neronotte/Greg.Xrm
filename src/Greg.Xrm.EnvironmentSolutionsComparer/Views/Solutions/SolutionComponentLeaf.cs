using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class SolutionComponentLeaf : SolutionComponentNode
	{
		private readonly SolutionComponent component;


		public SolutionComponentLeaf(SolutionComponentNode parent, SolutionComponent component)
			: base(component.objectid.ToString(), parent)
		{
			this.Environments = new List<ConnectionModel>();
			this.component = component;
		}

		public Guid ObjectId => component.objectid;

		public string ComponentTypeName => this.component.ComponentTypeName;
		
		public int ComponentType => this.component.componenttype?.Value ?? 0;


		public List<ConnectionModel> Environments { get; }

		public override string ToString()
		{
			return Label;
		}


		internal void SetLabelFromEntityMetadata(EntityMetadata entityMetadata)
		{
			this.Label = $"{entityMetadata.DisplayName?.UserLocalizedLabel?.Label} ({entityMetadata.LogicalName})"
				.Replace("()", string.Empty)
				.Trim();
		}
		internal void SetLabelFromWorkflow(Entity entity)
		{
			this.Label = $"{entity.GetAttributeValue<string>("name")} ({entity.GetAttributeValue<string>("uniquename")})"
				.Replace("()", string.Empty)
				.Trim();
		}

		internal void SetLabelFromWebResource(Entity entity)
		{
			this.Label = entity.GetAttributeValue<string>("name");
		}

		internal void SetLabelFromSdkMessageProcessingStep(Entity entity)
		{
			this.Label = entity.GetAttributeValue<string>("name");
		}

		internal void SetLabelFromSystemForm(Entity entity)
		{
			//"objecttypecode", "type", "name", "description"
			var objectTypeCode = entity.GetFormattedValue("objecttypecode");
			var type = entity.GetFormattedValue("type");
			var name = entity.GetAttributeValue<string>("name");
			//var description = entity.GetAttributeValue<string>("description");

			this.Label = $"{objectTypeCode}, {type} form: {name}";
		}

		internal void SetLabel(string v)
		{
			this.Label = v;
		}

		internal void SetLabelFromEmailTemplate(Entity entity)
		{
			this.Label = entity.GetAttributeValue<string>("title");
		}

		internal void SetLabelFromCustomControl(Entity entity)
		{
			this.Label = entity.GetAttributeValue<string>("name");
		}

		internal void SetLabelFromFieldSecurityProfile(Entity entity)
		{
			this.Label = entity.GetAttributeValue<string>("name");
		}

		internal void SetLabelFromPluginAssembly(Entity entity)
		{
			this.Label = entity.GetAttributeValue<string>("name");
		}

		internal void SetLabelFromRole(Entity entity)
		{
			this.Label = entity.GetAttributeValue<string>("name");
		}

		internal void SetLabelFromDisplayString(Entity entity)
		{
			this.Label = entity.GetAttributeValue<string>("displaystringkey");
		}

		internal void SetLabelFromAppModule(Entity entity)
		{
			this.Label = entity.GetAttributeValue<string>("name");
		}

		internal void SetLabelFromSavedQuery(Entity entity)
		{
			var name = entity.GetAttributeValue<string>("name");
			var typecode = entity.GetAttributeValue<string>("returnedtypecode");
			this.Label = $"{typecode}: {name}";
		}

		internal void SetLabelFromRibbonCustomization(Entity entity)
		{
			this.Label = entity.GetAttributeValue<string>("entity");
			if (string.IsNullOrWhiteSpace(Label))
			{
				this.Label = "Application ribbon";
			}
		}

		internal void SetLabelFromAttributeMetadata(EntityMetadata entityMetadata, AttributeMetadata attributeMetadata)
		{
			this.Label = $"{entityMetadata.LogicalName}.{attributeMetadata.LogicalName}: {attributeMetadata.AttributeTypeName?.Value} ({attributeMetadata.DisplayName?.UserLocalizedLabel?.Label})";
		}

		internal void SetLabelFromRelationship(RelationshipMetadataBase relationshipMetadata)
		{
			this.Label = relationshipMetadata.SchemaName;
		}
	}
}
