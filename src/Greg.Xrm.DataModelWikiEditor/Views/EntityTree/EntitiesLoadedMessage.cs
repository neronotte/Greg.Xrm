using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.DataModelWikiEditor.Views.EntityTree
{
	public class EntitiesLoadedMessage
	{
		public EntitiesLoadedMessage(IReadOnlyCollection<EntityMetadata> entityMetadataList)
		{
			this.EntityMetadataList = entityMetadataList ?? throw new ArgumentNullException(nameof(entityMetadataList));
		}

		public IReadOnlyCollection<EntityMetadata> EntityMetadataList { get; }
	}
}
