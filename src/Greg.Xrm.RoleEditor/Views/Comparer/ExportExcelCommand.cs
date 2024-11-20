using Greg.Xrm.Logging;
using Greg.Xrm.RoleEditor.Views.Editor;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using OfficeOpenXml;
using OfficeOpenXml.ConditionalFormatting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.Comparer
{
	public class ExportExcelCommand : CommandBase<RoleComparisonResult>
	{
		protected override void ExecuteInternal(RoleComparisonResult arg)
		{
			var messenger = arg.Role1.ExecutionContext.Messenger;
			var log = arg.Role1.ExecutionContext.Log;


			var dialog = new SaveFileDialog
			{
				Title = "Export Role to Excel",
				Filter = "Excel files (*.xlsx)|*.xlsx"
			};




			string fileName = null;
			using (dialog)
			{
				if (dialog.ShowDialog() != DialogResult.OK)
					return;

				fileName = dialog.FileName;
			}

			messenger.Send(new WorkAsyncInfo
			{
				Message = "Exporting data to excel file...",
				Work = (worker, e) =>
				{
					messenger.Send<Freeze>();

					ExportToExcel(arg, fileName, log);
				},
				PostWorkCallBack = e => {
					messenger.Send<Unfreeze>();
				}
			});
		}

		private void ExportToExcel(RoleComparisonResult diff, string fileName, ILog log)
		{
			try
			{
				using (var package = new ExcelPackage())
				{
					var ws = package.Workbook.Worksheets.Add("Diff");

					var row = 1;
					var col = 0;


					ws.Cells[row, ++col].SetValue("Group");
					ws.Cells[row, ++col].SetValue("Table Name");
					ws.Cells[row, ++col].SetValue("Privilege");
					ws.Cells[row, ++col].SetValue(diff.Role1).Center();
					ws.Cells[row, ++col].SetValue(diff.Role2).Center();
					ws.Cells[row, ++col].SetValue( "Privilege name");

					foreach (var item in diff.SelectMany(x => Flatten(x)))
					{
						row++;
						col = 0;
						foreach (var cell in item)
						{
							ws.Cells[row, ++col].SetValue(cell);
							if (col == 4 || col == 5)
							{
								ws.Cells[row, col].Center();
							}
						}
					}

					var table = ws.Tables.Add(ws.Cells[1, 1, row, col], "Table1");
					table.TableStyle = OfficeOpenXml.Table.TableStyles.Medium3;

					var formatting = ws.ConditionalFormatting.AddFiveIconSet(ws.Cells[2, 3, row, 5], eExcelconditionalFormatting5IconsSetType.Quarters);
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


					for (int i = 1; i <= col; i++)
					{
						ws.Column(i).AutoFit();
					}


					package.SaveAs(fileName);
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show("Error exporting role to excel file. See output window for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				log.Error("Error exporting role to excel file: " + ex.Message, ex);
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
				MessageBox.Show("Error opening the exported excel file. See output window for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				log.Error("Error opening the exported excel file: " + ex.Message, ex);
			}
		}


		private static List<List<object>> Flatten(Model.TreeNode group)
		{
			var result = new List<List<object>>();

			if (group.OfType<PrivilegeDifference>().Any())
			{
				// the group is misc
				foreach (var item in group.OfType<PrivilegeDifference>())
				{
					var itemData = new List<object>();
					itemData.Add(CleanText(group.Text));
					itemData.Add("-");
					itemData.Add(item.Text);
					itemData.Add((int)item.Level1);
					itemData.Add((int)item.Level2);
					itemData.Add(item.Tooltip);
					result.Add(itemData);
				}
			}
			else
			{
				foreach (var table in group)
				{
					foreach (var item in table.OfType<PrivilegeDifference>())
					{
						var itemData = new List<object>();
						itemData.Add(CleanText(group.Text));
						itemData.Add(CleanText(table.Text));
						itemData.Add(item.Text);
						itemData.Add((int)item.Level1);
						itemData.Add((int)item.Level2);
						itemData.Add(item.Tooltip);
						result.Add(itemData);
					}
				}
			}

			return result;
		}

		private static string CleanText(string text)
		{
			var index = text.IndexOf("(");
			if (index < 0)
				return text;

			text = text.Substring(0, index).Trim();
			return text;
		}
	}
}
