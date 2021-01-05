using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Messaging
{
	public class LoadEntitiesResponse
	{
		public LoadEntitiesResponse(IReadOnlyCollection<EntityMetadata> entities)
		{
			this.Entities = entities ?? Array.Empty<EntityMetadata>();
		}

		public IReadOnlyCollection<EntityMetadata> Entities { get; private set; }
	}
}
