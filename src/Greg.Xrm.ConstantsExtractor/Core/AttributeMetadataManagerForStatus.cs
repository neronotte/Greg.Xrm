using System.Collections.Generic;

namespace Greg.Xrm.ConstantsExtractor.Core
{
	public class AttributeMetadataManagerForStatus : AttributeMetadataManager
    {
        public AttributeMetadataManagerForStatus(
          string entityLogicalName,
          string displayName,
          string logicalname,
          string type,
          string description,
          Dictionary<int, string> optionsetvalues)
          : base(entityLogicalName, displayName, logicalname, type, description)
        {
            this.StatusValues = optionsetvalues ?? new Dictionary<int, string>();
        }
        public Dictionary<int, string> StatusValues { get; }


		public override void WriteFieldInfo(List<string> fileRows)
        {
            fileRows.Add("\t\t/// Values:");
            foreach (KeyValuePair<int, string> statusValue in this.StatusValues)
                fileRows.Add("\t\t" + string.Format("/// {0}: {1},", (object)statusValue.Value, (object)statusValue.Key));
        }
	}
}
