using Microsoft.Xrm.Sdk.Metadata;

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

		public override string ToString()
		{
			return Name;
		}
	}
}
