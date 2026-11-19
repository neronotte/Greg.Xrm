using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Views.Configurator
{
	public class AttributeMetadataDto
	{
		private readonly AttributeMetadata attribute;

		public AttributeMetadataDto(AttributeMetadata attribute)
		{
			this.attribute = attribute;
		}

		public string Name => this.attribute.LogicalName;

		public IReadOnlyCollection<string> Targets => this.attribute is LookupAttributeMetadata lookup
			? lookup.Targets ?? Array.Empty<string>()
			: Array.Empty<string>();

		public override string ToString()
		{
			return Name;
		}
	}
}
