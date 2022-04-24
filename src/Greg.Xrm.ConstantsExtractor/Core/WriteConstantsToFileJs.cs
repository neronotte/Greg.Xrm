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

		private string NameSpaceJsName { get; set; }

		private string[] JsHeaderLines { get; set; }

		public override List<EntityMetadataManager> EntitiesData { get; set; }

		public List<GlobalOptionSetsMetadataManager> GlobalOptionSetsMetadata { get; set; }

		public List<GlobalOptionSetsMetadataManager> GlobalBooleanOptionSetsMetadata { get; set; }

		public WriteConstantsToFileJs(ILog log, ConstantExtractorManager manager)
		{
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
				this.JsHeaderLines.ToList<string>().ForEach(row => this.WriteLine(row));
				this.WriteLine(Environment.NewLine);
				this.WriteLine(this.NameSpaceJsName + ".GlobalPickListConstants = new function () {" + Environment.NewLine + "\tvar self = this;");
				this.WriteLine("\tself.GlobalOptionSets = {");
				this.WriteGlobalElements(this.GlobalOptionSetsMetadata, "\t\t");
				this.WriteLine("\t};");
				this.WriteLine(Environment.NewLine + "\tself.GlobalBooleans = {");
				this.WriteGlobalElements(this.GlobalBooleanOptionSetsMetadata, "\t\t");
				this.WriteLine("\t};" + Environment.NewLine + "};");
				this.CommitToFileAndRestart(this.FilePath + "/GlobalOptionSetsConstants.js");
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
			this.WriteLine(Environment.NewLine + "\t\t/// " + optionSetMetadata.DisplayName + " constants.");
			this.WriteLine("\t\t" + optionSetMetadata.LogicalName + "Values: {");
		}

		public override void WriteEntityConstantClassHeader(EntityMetadataManager entityConstants)
		{
			JsHeaderLines.ToList().ForEach(row => this.WriteLine(row));
			this.WriteLine(Environment.NewLine + this.NameSpaceJsName + "." + entityConstants.EntityLogicalName + " = {");
			this.WriteLine("\t///" + entityConstants.EntityDisplayName + " constants.");
			if (!(entityConstants.EntityLogicalName != "EntityGenericConstants"))
				return;
			this.WriteLine("\tlogicalName: \"" + entityConstants.EntityLogicalName + "\",");
			this.WriteLine("\tdisplayName: \"" + entityConstants.EntityDisplayName + "\",");
		}

		public override void WriteAttributes(
		  EntityMetadataManager entityConstants,
		  string lastAttribute)
		{
			foreach (var attribute in entityConstants.Attributes)
				this.WriteAttribute(attribute, lastAttribute, (uint)entityConstants.OptionSetAttributes.Count > 0U, entityConstants.StatusReasonAttribute);
		}

		private void WriteAttribute(
		  AttributeMetadataManager attr,
		  string lastAttribute,
		  bool optionSetValuesInList,
		  AttributeMetadataManager statusReasonAttribute)
		{
			this.WriteLine("\t///" + attr.DisplayNameConstant);
			string str = "\t" + attr.LogicalNameConstant + ": \"" + attr.LogicalNameConstant + "\"";
			if (attr.LogicalNameConstant != lastAttribute | optionSetValuesInList || statusReasonAttribute != null && attr != statusReasonAttribute)
				str += ",";
			this.WriteLine(str);
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
			rows.ForEach(pickListRow => this.WriteLine(tabulation + pickListRow));
		}

		public override void WriteAttributeHeader(AttributeMetadataManager attribute)
		{
			this.WriteLine(Environment.NewLine + "\t/// Values for field " + attribute.DisplayNameConstant);
			this.WriteLine("\t" + attribute.LogicalNameConstant + "Values: {");
		}

		public override void WriteEndCode()
		{
			this.WriteLine("};");
		}
	}
}
