using System.Collections.Generic;

namespace Greg.Xrm.ConstantsExtractor.Core
{
	public class EntityMetadataManager
	{
		internal string EntityDisplayName { get; }

		internal string EntityDisplayNameWithoutSpecialChar { get; }

		internal string EntityLogicalName { get; }

		internal bool IsActivity { get; }

		internal List<AttributeMetadataManagerForPicklist> OptionSetAttributes { get; set; }

		internal AttributeMetadataManagerForStatus StatusAttribute { get; set; }

		internal AttributeMetadataManagerForStatusReason StatusReasonAttribute { get; set; }

		internal List<AttributeMetadataManager> Attributes { get; set; }

		internal List<string> CommonFields { get; }

		public EntityMetadataManager(
		  string _entityDisplayName,
		  string _entityLogicalName,
		  bool _isActivity,
		  List<string> _commonFields)
		{
			this.EntityDisplayName = _entityDisplayName;
			this.EntityDisplayNameWithoutSpecialChar = this.EntityDisplayName.Replace(" ", string.Empty).RemoveSpecialCharacters();
			this.EntityLogicalName = _entityLogicalName;
			this.IsActivity = _isActivity;
			this.Attributes = new List<AttributeMetadataManager>();
			this.OptionSetAttributes = new List<AttributeMetadataManagerForPicklist>();
			this.CommonFields = _commonFields;
		}

		internal void AddAttribute(AttributeMetadataManager attributeElem)
		{
			if (!this.CommonFields.Contains(attributeElem.LogicalNameConstant))
				this.Attributes.Add(attributeElem);

			if (attributeElem is AttributeMetadataManagerForPicklist manager)
				this.OptionSetAttributes.Add(manager);

			if (attributeElem is AttributeMetadataManagerForStatus manager1)
				this.StatusAttribute = manager1;

			if (attributeElem is AttributeMetadataManagerForStatusReason manager2)
				this.StatusReasonAttribute = manager2;
		}

		public string GetLastAttribute()
		{
			return this.Attributes.Count == 0 ? this.EntityLogicalName : this.Attributes[this.Attributes.Count - 1].LogicalNameConstant;
		}
	}
}
