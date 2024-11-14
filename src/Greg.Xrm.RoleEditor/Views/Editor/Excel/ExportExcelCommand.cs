using Greg.Xrm.Logging;
using Greg.Xrm.RoleEditor.Views.Editor.Excel;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk.Metadata;
using OfficeOpenXml;
using OfficeOpenXml.ConditionalFormatting;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public class ExportExcelCommand : CommandBase
	{
		private readonly RoleEditorViewModel viewModel;

		public ExportExcelCommand(RoleEditorViewModel viewModel)
        {
			this.viewModel = viewModel;
		}

        protected override void ExecuteInternal(object arg)
		{
			if (!(viewModel.Model is RoleModel m)) return;

			var context = m.GetContext();
			var log = context.Log;


			var dialog = new SaveFileDialog
			{
				Title = "Export Role to Excel",
				Filter = "Excel files (*.xlsx)|*.xlsx",
				FileName = $"{m.Name}.xlsx"
			};

			string fileName = null;
			using(dialog)
			{
				if (dialog.ShowDialog() != DialogResult.OK)
					return;

				fileName = dialog.FileName;
			}

			context.Messenger.Send(new WorkAsyncInfo
			{
				Message = "Exporting data to excel file...",
				Work = (worker, e) =>
				{
					context.Messenger.Send<Freeze>();

					ExportToExcel(m, fileName, log);
				},
				PostWorkCallBack = e => {
					context.Messenger.Send<Unfreeze>();
				}
				
			});
		}

		private void ExportToExcel(RoleModel m, string fileName, ILog log)
		{
			using (log.Track($"Generating excel report for <{m.Name}>"))
			{
				try
				{
					using (var package = new ExcelPackage())
					{
						package.Workbook.Protection.SetPassword("fatti non foste a viver come bruti, ma per seguir virtute et canoscenza");
						package.Workbook.Protection.LockStructure = true;
						package.Workbook.Protection.LockWindows = true;

						var ws1 = package.Workbook.Worksheets.Add("General");
						ws1.View.ShowGridLines = false;

						var ws2 = package.Workbook.Worksheets.Add("Tables");
						ws2.View.ShowGridLines = false;

						var ws3 = package.Workbook.Worksheets.Add("Misc");
						ws3.View.ShowGridLines = false;

						ws1.Column(1).Width = 15;
						ws1.Cells["A1"].SetValue("Role: " + m.Name).SetTitle();
						ws1.Cells["A3"].SetValue("Business Unit:").SetExplanatory().Right();
						ws1.Cells["B3"].SetValue(m.BusinessUnitName);
						ws1.Cells["A4"].SetValue("Description:").SetExplanatory().Right();
						ws1.Cells["B4"].SetValue(m.Description);
						ws1.Cells["A5"].SetValue("Inheritance:").SetExplanatory().Right();
						ws1.Cells["B5"].SetValue(m.IsInherited?.ToString());
						ws1.Cells["B10"].SetValue(m.Name.GenerateSignature()).Hidden();
						ws1.Protection.IsProtected = true;
						ws1.Protection.AllowSelectLockedCells = true;

						var row = 1;
						var col = 0;

						ws2.Cells[row, ++col].SetValue("Group");
						ws2.Cells[row, ++col].SetValue("Table name");
						ws2.Cells[row, ++col].SetValue("Create");
						ws2.Cells[row, ++col].SetValue("Read");
						ws2.Cells[row, ++col].SetValue("Write");
						ws2.Cells[row, ++col].SetValue("Delete");
						ws2.Cells[row, ++col].SetValue("Append");
						ws2.Cells[row, ++col].SetValue("Append To");
						ws2.Cells[row, ++col].SetValue("Assign");
						ws2.Cells[row, ++col].SetValue("Share");
						ws2.Cells[row, ++col].SetValue("Has privileges?");

						foreach (var tableGroup in m.TableGroups)
						{
							foreach (var table in tableGroup)
							{
								row++;
								col = 0;


								ws2.Cells[row, ++col].SetValue(tableGroup.Name);
								ws2.Cells[row, ++col].SetValue(table.Name);
								ws2.Cells[row, ++col].SetValue(table[PrivilegeType.Create].ToInt()).Center().SetValidRange(table, PrivilegeType.Create);
								ws2.Cells[row, ++col].SetValue(table[PrivilegeType.Read].ToInt()).Center().SetValidRange(table, PrivilegeType.Read);
								ws2.Cells[row, ++col].SetValue(table[PrivilegeType.Write].ToInt()).Center().SetValidRange(table, PrivilegeType.Write);
								ws2.Cells[row, ++col].SetValue(table[PrivilegeType.Delete].ToInt()).Center().SetValidRange(table, PrivilegeType.Delete);
								ws2.Cells[row, ++col].SetValue(table[PrivilegeType.Append].ToInt()).Center().SetValidRange(table, PrivilegeType.Append);
								ws2.Cells[row, ++col].SetValue(table[PrivilegeType.AppendTo].ToInt()).Center().SetValidRange(table, PrivilegeType.AppendTo);
								ws2.Cells[row, ++col].SetValue(table[PrivilegeType.Assign].ToInt()).Center().SetValidRange(table, PrivilegeType.Assign);
								ws2.Cells[row, ++col].SetValue(table[PrivilegeType.Share].ToInt()).Center().SetValidRange(table, PrivilegeType.Share);
								ws2.Cells[row, ++col].SetValue(table.HasAssignedPrivileges ? "X" : string.Empty).Center();


								ws2.Cells[row, Constants.ColumnContainingTableNames].SetValue(table.GetTableNameForExcelFile()).Hidden();
							}
						}
						ws2.Protection.IsProtected = true;
						ws2.Protection.AllowSelectLockedCells = true;
						ws2.Protection.AllowSort = true;
						ws2.Protection.AllowAutoFilter = true;
						ws2.Protection.AllowDeleteRows = false;
						ws2.Protection.AllowDeleteColumns = false;
						ws2.Protection.AllowSelectLockedCells = true;
						ws2.Protection.AllowFormatColumns = true;


						var formatting = ws2.ConditionalFormatting.AddFiveIconSet(ws2.Cells[2, 3, row, col - 1], eExcelconditionalFormatting5IconsSetType.Quarters);
						formatting.ShowValue = false;
						formatting.Icon1.Value = 0;
						formatting.Icon1.Type = eExcelConditionalFormattingValueObjectType.Num;
						formatting.Icon2.Value = 1;
						formatting.Icon2.Type = eExcelConditionalFormattingValueObjectType.Num;
						formatting.Icon3.Value = 2;
						formatting.Icon3.Type = eExcelConditionalFormattingValueObjectType.Num;
						formatting.Icon4.Value = 3;
						formatting.Icon4.Type = eExcelConditionalFormattingValueObjectType.Num;
						formatting.Icon5.Value = 4;
						formatting.Icon5.Type = eExcelConditionalFormattingValueObjectType.Num;

						var table1 = ws2.Tables.Add(ws2.Cells[1, 1, row, col], "Tables");
						table1.TableStyle = OfficeOpenXml.Table.TableStyles.Medium3;

						for (int i = 0; i < col; i++)
						{
							ws2.Column(i + 1).AutoFit();
						}



						row = 1;
						col = 0;

						ws3.Cells[row, ++col].SetValue("Group");
						ws3.Cells[row, ++col].SetValue("Privilege");
						ws3.Cells[row, ++col].SetValue("Do action?");
						ws3.Cells[row, Constants.ColumnContainingMiscNames].SetValue("Privilege name");

						foreach (var g in m.MiscGroups)
						{
							foreach (var misc in g)
							{
								row++;
								col = 0;


								ws3.Cells[row, ++col].SetValue(g.Name);
								ws3.Cells[row, ++col].SetValue(misc.Name);
								ws3.Cells[row, ++col].SetValue(misc.Value.ToInt()).Center().SetValidRange(misc);
								ws3.Cells[row, Constants.ColumnContainingMiscNames].SetValue(misc.Tooltip).Center();
								++col;
							}
						}
						ws3.Protection.IsProtected = true;
						ws3.Protection.AllowSelectLockedCells = true;
						ws3.Protection.AllowSort = true;
						ws3.Protection.AllowAutoFilter = true;
						ws3.Protection.AllowDeleteRows = false;
						ws3.Protection.AllowDeleteColumns = false;
						ws3.Protection.AllowSelectLockedCells = true;
						ws2.Protection.AllowFormatColumns = true;

						formatting = ws3.ConditionalFormatting.AddFiveIconSet(ws3.Cells[2, 3, row, 3], eExcelconditionalFormatting5IconsSetType.Quarters);
						formatting.ShowValue = false;
						formatting.Icon1.Value = 0;
						formatting.Icon1.Type = eExcelConditionalFormattingValueObjectType.Num;
						formatting.Icon2.Value = 1;
						formatting.Icon2.Type = eExcelConditionalFormattingValueObjectType.Num;
						formatting.Icon3.Value = 2;
						formatting.Icon3.Type = eExcelConditionalFormattingValueObjectType.Num;
						formatting.Icon4.Value = 3;
						formatting.Icon4.Type = eExcelConditionalFormattingValueObjectType.Num;
						formatting.Icon5.Value = 4;
						formatting.Icon5.Type = eExcelConditionalFormattingValueObjectType.Num;

						var table2 = ws3.Tables.Add(ws3.Cells[1, 1, row, col], "Misc");
						table2.TableStyle = OfficeOpenXml.Table.TableStyles.Medium3;


						for (int i = 0; i < col; i++)
						{
							ws3.Column(i + 1).AutoFit();
						}

						package.SaveAs(fileName);
					}
				}
				catch (Exception ex)
				{
					this.viewModel.SendNotification(Core.Views.NotificationType.Error, "Error exporting role to excel file. See output window for details.");
					log.Error("Error exporting role to excel file: " + ex.Message, ex);
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
				this.viewModel.SendNotification(Core.Views.NotificationType.Error, "Error opening the exported excel file. See output window for details.");
				log.Error("Error opening the exported excel file: " + ex.Message, ex);
			}
		}
	}
}
