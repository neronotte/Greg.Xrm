using Microsoft.Xrm.Sdk.Metadata;
using System;

namespace Greg.Xrm.DataModelWikiEditor.Views.EntityTree
{
	public class NodeForAttribute
	{
		private readonly AttributeMetadata attributeMetadata;

		public NodeForAttribute(AttributeMetadata attributeMetadata)
		{
			this.attributeMetadata = attributeMetadata ?? throw new ArgumentNullException(nameof(attributeMetadata));
		}


		public string LogicalName => this.attributeMetadata.LogicalName;

		public AttributeTypeCode? AttributeType => this.attributeMetadata.AttributeType;


		public override string ToString()
		{
			return $"{attributeMetadata.EntityLogicalName}.{attributeMetadata.LogicalName}: {AttributeType}";
		}
	}
}
