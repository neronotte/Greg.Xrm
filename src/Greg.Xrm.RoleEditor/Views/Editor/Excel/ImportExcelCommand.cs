using Greg.Xrm.Core.Views;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk.Metadata;
using OfficeOpenXml;
using System;
using System.IO;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.Editor.Excel
{
	public class ImportExcelCommand : CommandBase
	{
		private readonly RoleEditorViewModel viewModel;

		public ImportExcelCommand(RoleEditorViewModel viewModel)
		{
			this.viewModel = viewModel;

			this.viewModel.PropertyChanged += (s, e) => {
				if (e.PropertyName == nameof(this.viewModel.IsEnabled) 
					|| e.PropertyName == nameof(this.viewModel.IsCustomizable)) {
					SetCanExecute();
				}
			};
		}

		private void SetCanExecute()
		{
			this.CanExecute = this.viewModel.IsEnabled && this.viewModel.IsCustomizable;
		}



		protected override void ExecuteInternal(object arg)
		{
			if (!(viewModel.Model is RoleModel m))
				return;
			var context = m.GetContext();
			var messenger = context.Messenger;
			var log = context.Log;

			var dialog = new OpenFileDialog
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
				Message = "Importing Excel file, please wait...",
				Work = (worker, args) =>
				{
					messenger.Send<Freeze>();
					args.Result = ImportExcelFile(fileName);
				},
				PostWorkCallBack = e =>
				{	
					try
					{
						if (e.Error != null)
						{
							log.Error("Error importing excel file: " + e.Error.Message, e.Error);
							this.viewModel.SendNotification(NotificationType.Error, "Error importing excel file. See output window for details");
							return;
						}

						if (!(e.Result is ExcelMap map) || !map.HasSomethingToDo)
						{
							this.viewModel.SendNotification(NotificationType.Warning, "The import file contains no data.");
							return;
						}

						var result = MessageBox.Show($"Do you really want to import the data?{Environment.NewLine}This will overwrite any unsaved change.", "Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if (result != DialogResult.Yes)
						{
							return;
						}

						map.ApplyTo(m);
						viewModel.ForceViewRefresh();
					}
					finally
					{
						messenger.Send<Unfreeze>();
					}
				}
			});


		}


		private ExcelMap ImportExcelFile(string fileName)
		{
			var context = viewModel.Model.GetContext();
			var log = context.Log;

			using (log.Track("Reading excel file contents..."))
			using (var package = new ExcelPackage())
			{
				using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
				{
					package.Load(stream);
				}

				if (package.Workbook.Worksheets.Count != 3)
				{
					throw new InvalidOperationException("The selected file is not valid for import. Please peek a file that has been exported via _n.RoleManager");
				}

				var ws1 = package.Workbook.Worksheets[0];

				var roleName = ws1.Cells["A1"].Text?.Replace("Role: ", "");
				if (string.IsNullOrWhiteSpace(roleName))
				{
					throw new InvalidOperationException("The selected file is not valid for import. Please peek a file that has been exported via _n.RoleManager");
				}

				var check = ws1.Cells["B10"].Text;
				var signature = roleName.GenerateSignature();
				if (check != signature)
				{
					throw new InvalidOperationException("The selected file is not valid for import. Please peek a file that has been exported via _n.RoleManager");
				}


				var ws2 = package.Workbook.Worksheets[1];

				var map = new ExcelMap();

				var privilegeTypes = new[] {
					PrivilegeType.Create,
					PrivilegeType.Read,
					PrivilegeType.Write,
					PrivilegeType.Delete,
					PrivilegeType.Append,
					PrivilegeType.AppendTo,
					PrivilegeType.Assign,
					PrivilegeType.Share,
					};

				var row = 1;
				var col = 3;
				while (!string.IsNullOrWhiteSpace(ws2.Cells[row, 1].Text))
				{
					row++;

					var tableLogicalName = ws2.Cells[row, Constants.ColumnContainingTableNames].Text;

					for (var i = 0; i < privilegeTypes.Length; i++)
					{
						var privilegeType = privilegeTypes[i];
						var value = ws2.Cells[row, col + i].GetValue<int?>();
						map.Add(tableLogicalName, privilegeType, value);
					}
				}

				var ws3 = package.Workbook.Worksheets[2];

				row = 1;
				while (!string.IsNullOrWhiteSpace(ws3.Cells[row, 1].Text))
				{
					row++;

					var privilegeName = ws3.Cells[row, Constants.ColumnContainingMiscNames].Text;
					var privilegeValue = ws3.Cells[row, 3].GetValue<int?>();

					map.Add(privilegeName, privilegeValue);
				}


				return map;
			}
		}
	}
}
