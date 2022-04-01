using System.Collections.Generic;

namespace Greg.Xrm.ConstantsExtractor.Core
{
	public class AttributeMetadataManager
	{
		public AttributeMetadataManager(string entityLogicalName, string displayName, string logicalname, string type, string description)
		{
			this.EntityLogicalName = entityLogicalName;
			this.DisplayNameConstant = displayName;
			this.LogicalNameConstant = logicalname;
			this.LogicalNameConstantLabel = entityLogicalName == null || !entityLogicalName.Equals(logicalname) ? logicalname : logicalname + "Field";
			this.Type = type;
			this.Description = description;
		}



		public string EntityLogicalName { get; }

		public string DisplayNameConstant { get;  }

		public string LogicalNameConstant { get; }

		public string LogicalNameConstantLabel { get; }

		public string Type { get; }

		public string Description { get; }


		public virtual void WriteFieldInfo(List<string> fileRows)
		{
		}
	}
}
