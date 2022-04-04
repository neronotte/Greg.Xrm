using Greg.Xrm.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Greg.Xrm.ConstantsExtractor.Core
{
	public abstract class WriteConstantsToFileBase
	{
		public abstract string FilePath { get; set; }

		public abstract List<string> FileRows { get; set; }

		public abstract List<EntityMetadataManager> EntitiesData { get; set; }

		public void WriteGlobalElements(List<GlobalOptionSetsMetadataManager> globalMetadata, string tabulation)
		{
			foreach (GlobalOptionSetsMetadataManager optionSetMetadata in globalMetadata)
			{
				this.WriteGlobalOptionSetConstantClassHeader(optionSetMetadata, tabulation);
				this.WriteRows(optionSetMetadata.PickListValues.ToList(), tabulation, new bool?(optionSetMetadata == globalMetadata.Last()));
			}
		}

		public void WriteEntityConstants(ILog log, string fileType, string tabulation)
		{
			foreach (EntityMetadataManager entityConstants in this.EntitiesData)
			{
				log.Debug($"Writing file {entityConstants.EntityLogicalName}.{fileType}");


				this.FileRows = new List<string>();
				this.WriteFileNameSpace();
				this.WriteEntityConstantClassHeader(entityConstants);
				if (entityConstants.Attributes.Count > 0)
				{
					entityConstants.Attributes = entityConstants.Attributes.OrderBy(attr => attr.LogicalNameConstant).ToList();
					string lastAttribute = entityConstants.GetLastAttribute();
					this.WriteCurrentEntityConstants(entityConstants, lastAttribute, tabulation);
				}
				this.WriteEndCode();
				this.WriteFile(this.FilePath + "/" + entityConstants.EntityLogicalName + "." + fileType);
			}
		}

		public static Dictionary<int, string> StatusReasonElementsWithStateValues(AttributeMetadataManager statusReasonAttribute, AttributeMetadataManager stateAttribute)
		{
			var result = new Dictionary<int, string>();
			((AttributeMetadataManagerForStatusReason)statusReasonAttribute).StatusReasonValues.ToList().ForEach(value =>
			{
				string str = ((AttributeMetadataManagerForStatus)stateAttribute).StatusValues.Where(stat => stat.Key == value.Item2).FirstOrDefault().Value.RemoveDiacritics();
				result.Add(value.Item1, $"{value.Item3}_State{str}");
			});
			return result;
		}

		public List<KeyValuePair<int, string>> FormatRowValues(List<KeyValuePair<int, string>> elements)
		{
			var result = new List<KeyValuePair<int, string>>();
			elements.ToList().ForEach(elem =>
			{
				string str1 = elem.Value.RemoveDiacritics().Replace(" ", "").RemoveSpecialCharacters();
				if (Regex.IsMatch(str1, "^\\d"))
					str1 = str1.Insert(0, "_");
				if (elements.Where(ele => ele.Value == elem.Value).ToList().Count > 1)
					str1 = this.RecursiveGetValueFormatted(result, str1, 1);
				string str2 = this.FormatValueForKeywords(str1);
				result.Add(new KeyValuePair<int, string>(elem.Key, str2));
			});
			return result.OrderBy(coup => coup.Value).ToList();
		}

		private string RecursiveGetValueFormatted(List<KeyValuePair<int, string>> result, string valueFormatted, int index)
		{
			var key = $"{valueFormatted}{index}";
			return result.Where(couple => couple.Value == key).ToList().Count > 0 ? this.RecursiveGetValueFormatted(result, valueFormatted, index + 1) : key;
		}

		internal void WriteCurrentEntityConstants(EntityMetadataManager entityConstants, string lastAttribute, string tabulation)
		{
			if (entityConstants.Attributes.Count == 0)
				return;
			this.WriteAttributes(entityConstants, lastAttribute);
			if (entityConstants.StatusAttribute != null)
			{
				this.WriteStateValues(entityConstants.StatusAttribute, tabulation);
				var source = StatusReasonElementsWithStateValues(entityConstants.StatusReasonAttribute, entityConstants.StatusAttribute);
				this.WriteAttributeHeader(entityConstants.StatusReasonAttribute);
				this.WriteRows(source.ToList(), tabulation, new bool?(entityConstants.OptionSetAttributes.Count == 0));
			}
			entityConstants.OptionSetAttributes.ForEach(opt =>
			{
				if (opt.PicklistValues.Count <= 0)
					return;
				this.WriteAttributeHeader(opt);
				this.WriteRows(opt.PicklistValues.ToList(), tabulation, new bool?(opt == entityConstants.OptionSetAttributes.Last()));
			});
		}

		public void WriteStateValues(AttributeMetadataManager stateAttribute, string tabulation)
		{
			this.WriteAttributeHeader(stateAttribute);
			this.WriteRows(((AttributeMetadataManagerForStatus)stateAttribute).StatusValues.ToList(), tabulation, new bool?());
		}

		public void WriteFile(string fileName)
		{
			using (var w = new StreamWriter(File.Create(fileName)))
				this.FileRows.ForEach(row => w.WriteLine(row));
		}

		public virtual void WriteEntityConstantClassHeader(EntityMetadataManager entityConstants)
		{
		}

		public virtual void WriteGlobalOptionSetConstantClassHeader(GlobalOptionSetsMetadataManager optionSetMetadata, string tabulation)
		{
		}

		public virtual void WriteAttributes(EntityMetadataManager entityConstants, string lastAttribute)
		{
		}

		public virtual void WriteAttributeHeader(AttributeMetadataManager attr)
		{
		}

		public virtual void WriteRows(List<KeyValuePair<int, string>> elements, string tabulation = "\t\t", bool? isLastAttribute = null)
		{
		}

		public virtual void WriteFileNameSpace()
		{
		}

		public virtual void WriteEndCode()
		{
		}

		public virtual string FormatValueForKeywords(string value)
		{
			return value;
		}
	}
}
