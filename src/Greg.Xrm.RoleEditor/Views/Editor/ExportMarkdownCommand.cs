using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Services;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class ExportMarkdownCommand : CommandBase
	{
		private readonly RoleEditorViewModel viewModel;

		public ExportMarkdownCommand(RoleEditorViewModel viewModel)
		{
			this.viewModel = viewModel;
		}

		protected override void ExecuteInternal(object arg)
		{
			var m = viewModel.Model;
			var context = viewModel.Model.GetContext();
			var log = context.Log;


			var dialog = new SaveFileDialog
			{
				Title = "Export Role to Markdown",
				Filter = "Markdown files (*.md)|*.md",
				FileName = $"{m.Name}.md"
			};

			string fileName = null;
			using (dialog)
			{
				if (dialog.ShowDialog() != DialogResult.OK)
					return;

				fileName = dialog.FileName;
			}


			using (log.Track($"Generating excel report for <{m.Name}>"))
			{
				try
				{
					using (var writer = new MarkdownWriter(fileName))
					{
						writer.WriteHeader1("Role: " + m.Name);

						writer.Write("- *Business Unit*: **").Write(m.BusinessUnitName).WriteLine("**");

						if (!string.IsNullOrWhiteSpace(m.Description))
						{
							writer.Write("- *Description*: **").Write(m.Description).WriteLine("**");
						}
						writer.Write("- *Inheritance*: **").Write(m.IsInherited?.ToString()).WriteLine("**");
						writer.WriteLine();

						writer.WriteHeader2("Legend");

						writer.WriteList(
							Level.None.ToEmoji() + ": " + Level.None.Description(),
							Level.User.ToEmoji() + ": " + Level.User.Description(),
							Level.BusinessUnit.ToEmoji() + ": " + Level.BusinessUnit.Description(),
							Level.ParentChild.ToEmoji() + ": " + Level.ParentChild.Description(),
							Level.Organization.ToEmoji() + ": " + Level.Organization.Description()
						);

						writer.WriteHeader2("Table-related privileges");


						foreach (var tableGroup in m.TableGroups.Where(x => x.Exists(t => t.HasAssignedPrivileges)))
						{
							writer.WriteHeader3(tableGroup.Name);

							writer.WriteTable(tableGroup.Where(x => x.HasAssignedPrivileges).ToArray(),
								() => new string[] {
									"Table name",
									"Create",
									"Read",
									"Write",
									"Delete",
									"Append",
									"Append To",
									"Assign",
									"Share"
								},
								table => new string[] {
									"**" + table.Name + "**",
									table[PrivilegeType.Create].ToEmoji(),
									table[PrivilegeType.Read].ToEmoji(),
									table[PrivilegeType.Write].ToEmoji(),
									table[PrivilegeType.Delete].ToEmoji(),
									table[PrivilegeType.Append].ToEmoji(),
									table[PrivilegeType.AppendTo].ToEmoji(),
									table[PrivilegeType.Assign].ToEmoji(),
									table[PrivilegeType.Share].ToEmoji()
								});
						}


						writer.WriteHeader2("Miscellaneous privileges");

						foreach (var g in m.MiscGroups.Where(x=> x.Exists(p => p.IsAssigned)))
						{
							writer.WriteHeader3(g.Name);

							writer.WriteTable(g.Where(x=> x.IsAssigned).ToArray(),
								() => new string[] {
									"Operation",
									"Do action?",
									"Privilege name"
								},
								misc => new string[] {
									"**" + misc.Name + "**",
									misc.Value.ToEmoji(),
									misc.Tooltip
								});
						}
					}
				}
				catch (Exception ex)
				{
					this.viewModel.SendNotification(Core.Views.NotificationType.Error, "Error exporting role to markdown file. See output window for details.");
					log.Error("Error exporting role to markdown file: " + ex.Message, ex);
				}
			}


			try
			{
				var processStartInfo = new ProcessStartInfo(fileName)
				{
					UseShellExecute = true
				};
				Process.Start(processStartInfo);
			}
			catch (Exception ex)
			{
				this.viewModel.SendNotification(Core.Views.NotificationType.Error, "Error opening the exported markdown file. See output window for details.");
				log.Error("Error opening the exported markdown file: " + ex.Message, ex);
			}
		}
	}
}
