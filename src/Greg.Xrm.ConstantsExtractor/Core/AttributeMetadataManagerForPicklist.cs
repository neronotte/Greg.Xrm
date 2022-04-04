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


		public override IEnumerable<string> WriteFieldInfo()
		{
			yield return ("\t\t/// Values:");
			foreach (var picklistValue in this.PicklistValues)
				yield return "\t\t" + string.Format("/// {0}: {1},", picklistValue.Value, picklistValue.Key);
		}
	}
}
