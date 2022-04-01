using System.Collections.Generic;

namespace Greg.Xrm.ConstantsExtractor.Core
{
	public class AttributeMetadataManagerForLookup : AttributeMetadataManager
	{
		public AttributeMetadataManagerForLookup(
			string entityLogicalName,
			string displayName,
			string logicalname,
			string type,
			string description,
			List<string> targetEntities)
		  : base(entityLogicalName, displayName, logicalname, type, description)
		{

			this.TargetEntities = targetEntities ?? new List<string>();
		}
		public List<string> TargetEntities { get; }


		public override void WriteFieldInfo(List<string> fileRows)
		{
			string relatedEntityNames = string.Empty;
			this.TargetEntities.ForEach(ent => relatedEntityNames = relatedEntityNames + ent + ",");
			fileRows.Add("\t\t/// Related entities: " + relatedEntityNames);
		}
	}
}
