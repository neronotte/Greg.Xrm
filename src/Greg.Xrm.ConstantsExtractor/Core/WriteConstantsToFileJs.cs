using Greg.Xrm.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.ConstantsExtractor.Core
{
	internal class WriteConstantsToFileJs : WriteConstantsToFileBase, IWriteConstantsToFile
	{
		private readonly ILog log;

		public override string FilePath { get; set; }

		public override List<string> FileRows { get; set; }

		private string NameSpaceJsName { get; set; }

		private string[] JsHeaderLines { get; set; }

		public override List<EntityMetadataManager> EntitiesData { get; set; }

		public List<GlobalOptionSetsMetadataManager> GlobalOptionSetsMetadata { get; set; }

		public List<GlobalOptionSetsMetadataManager> GlobalBooleanOptionSetsMetadata { get; set; }

		public WriteConstantsToFileJs(ILog log, ConstantExtractorManager manager)
		{
			this.FileRows = new List<string>();
			this.FilePath = manager.FilePathJS;
			this.NameSpaceJsName = manager.NameSpaceJs;
			this.JsHeaderLines = manager.JsHeaderLines.Replace("\\n", "\n").Split('\n');
			this.EntitiesData = manager.EntityData;
			this.GlobalOptionSetsMetadata = manager.GlobalOptionSetsMetadata;
			this.GlobalBooleanOptionSetsMetadata = manager.GlobalBooleanOptionSetsMetadata;
			this.log = log;
		}

		public void WriteConstantsToFile()
		{
			using (log.Track("Writing JS files"))
			{
				this.WriteGlobalOptionSetConstants();
				this.WriteEntityConstantsClass();
			}
		}

		public void WriteGlobalOptionSetConstants()
		{
			using (log.Track("Writing JS global option set constants"))
			{
				this.JsHeaderLines.ToList<string>().ForEach(row => this.FileRows.Add(row));
				this.FileRows.Add(Environment.NewLine);
				this.FileRows.Add(this.NameSpaceJsName + ".GlobalPickListConstants = new function () {" + Environment.NewLine + "\tvar self = this;");
				this.FileRows.Add("\tself.GlobalOptionSets = {");
				this.WriteGlobalElements(this.GlobalOptionSetsMetadata, "\t\t");
				this.FileRows.Add("\t};");
				this.FileRows.Add(Environment.NewLine + "\tself.GlobalBooleans = {");
				this.WriteGlobalElements(this.GlobalBooleanOptionSetsMetadata, "\t\t");
				this.FileRows.Add("\t};" + Environment.NewLine + "};");
				this.WriteFile(this.FilePath + "/GlobalOptionSetsConstants.js");
			}
		}

		public void WriteEntityConstantsClass()
		{
			this.WriteEntityConstants(log, "js", "\t");
		}

		public override void WriteGlobalOptionSetConstantClassHeader(
		  GlobalOptionSetsMetadataManager optionSetMetadata,
		  string tabulation)
		{
			this.FileRows.Add(Environment.NewLine + "\t\t/// " + optionSetMetadata.DisplayName + " constants.");
			this.FileRows.Add("\t\t" + optionSetMetadata.LogicalName + "Values: {");
		}

		public override void WriteEntityConstantClassHeader(EntityMetadataManager entityConstants)
		{
			((IEnumerable<string>)this.JsHeaderLines).ToList<string>().ForEach((Action<string>)(row => this.FileRows.Add(row)));
			this.FileRows.Add(Environment.NewLine + this.NameSpaceJsName + "." + entityConstants.EntityLogicalName + " = {");
			this.FileRows.Add("\t///" + entityConstants.EntityDisplayName + " constants.");
			if (!(entityConstants.EntityLogicalName != "EntityGenericConstants"))
				return;
			this.FileRows.Add("\tlogicalName: \"" + entityConstants.EntityLogicalName + "\",");
			this.FileRows.Add("\tdisplayName: \"" + entityConstants.EntityDisplayName + "\",");
		}

		public override void WriteAttributes(
		  EntityMetadataManager entityConstants,
		  string lastAttribute)
		{
			foreach (AttributeMetadataManager attribute in entityConstants.Attributes)
				this.WriteAttribute(attribute, lastAttribute, (uint)entityConstants.OptionSetAttributes.Count > 0U, (AttributeMetadataManager)entityConstants.StatusReasonAttribute);
		}

		private void WriteAttribute(
		  AttributeMetadataManager attr,
		  string lastAttribute,
		  bool optionSetValuesInList,
		  AttributeMetadataManager statusReasonAttribute)
		{
			this.FileRows.Add("\t///" + attr.DisplayNameConstant);
			string str = "\t" + attr.LogicalNameConstant + ": \"" + attr.LogicalNameConstant + "\"";
			if (attr.LogicalNameConstant != lastAttribute | optionSetValuesInList || statusReasonAttribute != null && attr != statusReasonAttribute)
				str += ",";
			this.FileRows.Add(str);
		}

		public override void WriteRows(
		  List<KeyValuePair<int, string>> elements,
		  string tabulation = "\t\t",
		  bool? isLastAttribute = null)
		{
			List<string> rows = new List<string>();
			List<KeyValuePair<int, string>> keyValuePairList = this.FormatRowValues(elements);
			string str = "},";
			if (isLastAttribute.HasValue && isLastAttribute.Value)
				str = "}";
			void action(KeyValuePair<int, string> couple) => rows.Add($"\t{couple.Value}: {couple.Key},");
			keyValuePairList.ForEach(action);
			rows[rows.Count - 1] = rows.Last().Remove(rows.Last().Length - 1);
			rows.Add(str ?? String.Empty);
			rows.ForEach(pickListRow => this.FileRows.Add(tabulation + pickListRow));
		}

		public override void WriteAttributeHeader(AttributeMetadataManager attribute)
		{
			this.FileRows.Add(Environment.NewLine + "\t/// Values for field " + attribute.DisplayNameConstant);
			this.FileRows.Add("\t" + attribute.LogicalNameConstant + "Values: {");
		}

		public override void WriteEndCode()
		{
			this.FileRows.Add("};");
		}
	}
}
