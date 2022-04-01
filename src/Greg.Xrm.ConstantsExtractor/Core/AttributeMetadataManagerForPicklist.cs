using System.Collections.Generic;

namespace Greg.Xrm.ConstantsExtractor.Core
{
	public class AttributeMetadataManagerForPicklist : AttributeMetadataManager
	{

		public AttributeMetadataManagerForPicklist(
			string entityLogicalName,
			string displayName,
			string logicalname,
			string type,
			string description,
			Dictionary<int, string> optionsetvalues,
			string globalOptionSetLogicalName)
		  : base(entityLogicalName, displayName, logicalname, type, description)
		{
			this.PicklistValues = optionsetvalues ?? new Dictionary<int, string>();
			this.GlobalOptionSetLogicalName = globalOptionSetLogicalName;
		}


		public Dictionary<int, string> PicklistValues { get; }

		public string GlobalOptionSetLogicalName { get; }


		public override void WriteFieldInfo(List<string> fileRows)
		{
			fileRows.Add("\t\t/// Values:");
			foreach (KeyValuePair<int, string> picklistValue in this.PicklistValues)
				fileRows.Add("\t\t" + string.Format("/// {0}: {1},", (object)picklistValue.Value, (object)picklistValue.Key));
		}
	}
}
