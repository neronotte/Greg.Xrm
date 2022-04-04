using Greg.Xrm.Logging;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.ConstantsExtractor.Core
{
	public class WriteConstantsToFileCs : WriteConstantsToFileBase, IWriteConstantsToFile
	{
		private readonly ILog log;

		public override string FilePath { get; set; }

		public override List<string> FileRows { get; set; }

		public override List<EntityMetadataManager> EntitiesData { get; set; }

		private bool ExtractTypes { get; set; }

		private bool ExtractDescriptions { get; set; }

		private EntityMetadataManager ActivityPointerMetadata { get; set; }

		private string CurrentNamespace { get; set; }

		public List<GlobalOptionSetsMetadataManager> GlobalOptionSetsMetadata { get; set; }

		public List<GlobalOptionSetsMetadataManager> GlobalBooleanOptionSetsMetadata { get; set; }

		public WriteConstantsToFileCs(ILog log, ConstantExtractorManager manager)
		{
			this.FileRows = new List<string>();
			this.FilePath = manager.FilePathCS;
			this.EntitiesData = manager.EntityData;
			this.ExtractTypes = manager.ExtractTypes;
			this.ExtractDescriptions = manager.ExtractTypes;
			this.GlobalOptionSetsMetadata = manager.GlobalOptionSetsMetadata;
			this.GlobalBooleanOptionSetsMetadata = manager.GlobalBooleanOptionSetsMetadata;
			this.ActivityPointerMetadata = manager.ActivityPointerMetadata;
			this.CurrentNamespace = manager.NameSpaceCs;
			this.FileRows = new List<string>();
			this.log = log;
		}

		public void WriteConstantsToFile()
		{
			using (log.Track("Writing C# files"))
			{
				this.WriteGlobalOptionSetConstants();
				this.WriteEntityConstantsClass();
			}
		}

		public void WriteGlobalOptionSetConstants()
		{
			using (log.Track("Writing C# global option set constants"))
			{
				this.FileRows.Add($"namespace {this.CurrentNamespace}{Environment.NewLine}{{");
				this.FileRows.Add($"\tpublic static class GlobalOptionSetConstants{Environment.NewLine}\t{{");
				this.WriteGlobalElements(this.GlobalOptionSetsMetadata, "\t\t");

				this.FileRows.Add($"\t}}{Environment.NewLine}}}");
				this.WriteFile(this.FilePath + "/GlobalOptionSetConstants.cs");
				this.FileRows = new List<string>
				{
					$"namespace {this.CurrentNamespace}{Environment.NewLine}{{",
					$"\tpublic static class GlobalBooleanConstants{Environment.NewLine}\t{{"
				};
				this.WriteGlobalElements(this.GlobalBooleanOptionSetsMetadata, "\t\t");
				this.FileRows.Add("\t}" + Environment.NewLine + "}");
				this.WriteFile(this.FilePath + "/GlobalBooleanConstants.cs");
			}
		}

		public void WriteEntityConstantsClass()
		{
			this.FilterAttributes(this.ActivityPointerMetadata);
			this.WriteEntityConstants(log, "cs", "\t\t");
		}

		public override void WriteFileNameSpace()
		{
			this.FileRows.Add("namespace " + this.CurrentNamespace + Environment.NewLine + "{");
		}

		public void FilterAttributes(EntityMetadataManager activityPointerMetadata)
		{
			this.EntitiesData.ForEach(entityData =>
			{
				if (!entityData.IsActivity)
					return;
				List<string> activityPointerAttributesLogicalNames = new List<string>();
				activityPointerMetadata.Attributes.ToList().ForEach(attr => activityPointerAttributesLogicalNames.Add(attr.LogicalNameConstant));
				activityPointerAttributesLogicalNames.Sort();
				entityData.Attributes = entityData.Attributes.Where(attr => !activityPointerAttributesLogicalNames.Contains(attr.LogicalNameConstant)).ToList();
			});
		}

		public override void WriteGlobalOptionSetConstantClassHeader(GlobalOptionSetsMetadataManager optionSetMetadata, string tabulation)
		{
			this.FileRows.Add(Environment.NewLine + tabulation + "/// <summary>");
			this.FileRows.Add(tabulation + "/// " + optionSetMetadata.DisplayName + " constants.");
			this.FileRows.Add(tabulation + "/// </summary>");
			this.FileRows.Add(tabulation + "public enum " + optionSetMetadata.LogicalName + "Values" + Environment.NewLine + tabulation + "{");
		}

		public override void WriteEntityConstantClassHeader(EntityMetadataManager entityConstants)
		{
			this.FileRows.Add(Environment.NewLine + "\t/// <summary>");
			this.FileRows.Add("\t/// " + entityConstants.EntityDisplayName + " constants.");
			this.FileRows.Add("\t/// </summary>");
			if (entityConstants.IsActivity)
				this.FileRows.Add($"\tpublic sealed class {entityConstants.EntityLogicalName} : activitypointer{Environment.NewLine }\t{{");
			else if (entityConstants.EntityLogicalName == "EntityGenericConstants")
				this.FileRows.Add("\tpublic class " + entityConstants.EntityLogicalName + Environment.NewLine + "\t{");
			else if (entityConstants.EntityLogicalName == "activitypointer")
				this.FileRows.Add("\tpublic class " + entityConstants.EntityLogicalName + " : EntityGenericConstants" + Environment.NewLine + "\t{");
			else
				this.FileRows.Add("\tpublic sealed class " + entityConstants.EntityLogicalName + " : EntityGenericConstants" + Environment.NewLine + "\t{");
			if (!(entityConstants.EntityLogicalName != "EntityGenericConstants"))
				return;

			this.FileRows.Add("\t\t/// <summary>");
			this.FileRows.Add("\t\t/// " + entityConstants.EntityLogicalName);
			this.FileRows.Add("\t\t/// </summary>");
			this.FileRows.Add("\t\tpublic static string logicalName => \"" + entityConstants.EntityLogicalName + "\";" + Environment.NewLine);
			this.FileRows.Add("\t\t/// <summary>");
			this.FileRows.Add("\t\t/// " + entityConstants.EntityDisplayName);
			this.FileRows.Add("\t\t/// </summary>");
			this.FileRows.Add("\t\tpublic static string displayName => \"" + entityConstants.EntityDisplayName + "\";" + Environment.NewLine);
		}

		public override void WriteAttributes(EntityMetadataManager manager, string lastAttribute)
		{
			foreach (AttributeMetadataManager attribute in manager.Attributes)
			{
				this.FileRows.Add("\t\t/// <summary>");
				this.FileRows.Add("\t\t/// Display Name: " + attribute.DisplayNameConstant + ",");
				if (this.ExtractTypes)
				{
					this.FileRows.Add("\t\t/// Type: " + attribute.Type + ",");
					attribute.WriteFieldInfo(this.FileRows);
				}
				if (this.ExtractDescriptions)
					this.FileRows.Add("\t\t/// Description: " + attribute.Description);
				this.FileRows.Add("\t\t/// </summary>");
				this.FileRows.Add("\t\tpublic static string " + attribute.LogicalNameConstantLabel + " => \"" + attribute.LogicalNameConstant + "\";" + Environment.NewLine);
			}
		}

		public override void WriteAttributeHeader(AttributeMetadataManager attribute)
		{
			this.FileRows.Add(Environment.NewLine + "\t\t/// <summary>");
			this.FileRows.Add("\t\t/// Values for field " + attribute.DisplayNameConstant);
			this.FileRows.Add("\t\t/// <summary>");
			if (attribute.EntityLogicalName != "EntityGenericConstants")
			{
				switch (attribute)
				{
					case AttributeMetadataManagerForStatusReason _:
					case AttributeMetadataManagerForStatus _:
						this.FileRows.Add("\t\tpublic new enum " + attribute.LogicalNameConstant + "Values" + Environment.NewLine + "\t\t{");
						return;
				}
			}
			this.FileRows.Add("\t\tpublic enum " + attribute.LogicalNameConstant + "Values" + Environment.NewLine + "\t\t{");
		}

		public override void WriteRows(
		  List<KeyValuePair<int, string>> elements,
		  string tabulation = "\t\t\t",
		  bool? isLastAttribute = null)
		{
			List<string> rows = new List<string>();
			this.FormatRowValues(elements).ForEach(couple => rows.Add($"\t{couple.Value} = {couple.Key},"));
			rows[rows.Count - 1] = rows.Last<string>().Remove(rows.Last<string>().Length - 1);
			rows.Add("}");
			rows.ForEach(pickListRow => this.FileRows.Add(tabulation + pickListRow));
		}

		public override string FormatValueForKeywords(string value)
		{
			if (!CodeDomProvider.CreateProvider("C#").IsValidIdentifier(value))
				value = "@" + value;
			return value;
		}

		public override void WriteEndCode()
		{
			this.FileRows.Add("\t};" + Environment.NewLine + "}");
		}
	}
}
