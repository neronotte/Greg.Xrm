using System.Collections.Generic;

namespace Greg.Xrm.ConstantsExtractor.Core
{
	public class GlobalOptionSetsMetadataManager
	{
		public GlobalOptionSetsMetadataManager(
			string displayName,
			string logicalname,
			Dictionary<int, string> optionsetvalues)
		{
			this.DisplayName = displayName;
			this.LogicalName = logicalname;
			this.PickListValues = optionsetvalues ?? new Dictionary<int, string>();
		}


		public string DisplayName { get; }

		public string LogicalName { get; }

		public Dictionary<int, string> PickListValues { get; }
	}
}
