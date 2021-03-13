using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.DataModelWikiEditor.Views.EntityTree
{
	public class NodeForEntity
	{
		private readonly EntityMetadata entityMetadata;

		public NodeForEntity(EntityMetadata entityMetadata)
		{
			this.entityMetadata = entityMetadata ?? throw new ArgumentNullException(nameof(entityMetadata));
			this.AttributeList = new List<NodeForAttribute>();
		}


		public string LogicalName => this.entityMetadata.LogicalName;


		public List<NodeForAttribute> AttributeList { get; private set; }




		public override string ToString()
		{
			return this.LogicalName;
		}
	}
}
