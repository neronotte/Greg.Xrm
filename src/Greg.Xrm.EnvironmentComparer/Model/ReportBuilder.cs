using Greg.Xrm.EnvironmentComparer.Engine;
using Microsoft.Xrm.Sdk;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Greg.Xrm.EnvironmentComparer.Model
{
	public class ReportBuilder
	{
		private readonly DirectoryInfo localFolder;

		public ReportBuilder(DirectoryInfo localFolder)
		{
			this.localFolder = localFolder ?? throw new ArgumentNullException(nameof(localFolder));
		}

		public void GenerateReport(Dictionary<string, CompareResultForEntity> compareResult, string crm1name, string crm2name)
		{
			var fileName = $"Compare_{DateTime.Now.Ticks}.xlsx";
			var fileFullName = Path.Combine(this.localFolder.FullName, fileName);


			using(var wb = new ExcelPackage())
			{
				GenerateSummary(wb, compareResult, crm1name, crm2name);


				foreach (var kvp in compareResult)
				{
					GenerateSheet(wb, kvp.Key, kvp.Value, crm1name, crm2name);
				}

				wb.SaveAs(new FileInfo(fileFullName));
			}


			Process.Start(fileFullName);
		}

		private void GenerateSummary(ExcelPackage wb, Dictionary<string, CompareResultForEntity> compareResult, string crm1name, string crm2name)
		{
			var ws = wb.Workbook.Worksheets.Add("Summary");



			var row = 1;
			var col = 0;

			ws.Cells[row, ++col].Value = "Entity";
			ws.Cells[row, ++col].Value = "Only on " + crm1name;
			ws.Cells[row, ++col].Value = "Only on " + crm2name;
			ws.Cells[row, ++col].Value = "On both, with differences";
			ws.Cells[row, ++col].Value = "On both, matching";

			foreach (var comparison in compareResult)
			{
				row++;
				col = 0;


				ws.Cells[row, ++col].Value = comparison.Key;
				ws.Cells[row, ++col].Value = comparison.Value.Count(_ => _.Result == ObjectComparisonResult.RightMissing);
				ws.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
				if (ws.Cells[row, col].GetValue<int>() > 0)
				{
					var color = GetColor(ObjectComparisonResult.RightMissing);

					ws.Cells[row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
					ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(color);
				}


				ws.Cells[row, ++col].Value = comparison.Value.Count(_ => _.Result == ObjectComparisonResult.LeftMissing);
				ws.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
				if (ws.Cells[row, col].GetValue<int>() > 0)
				{
					var color = GetColor(ObjectComparisonResult.LeftMissing);

					ws.Cells[row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
					ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(color);
				}


				ws.Cells[row, ++col].Value = comparison.Value.Count(_ => _.Result == ObjectComparisonResult.MatchingButDifferent);
				ws.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
				if (ws.Cells[row, col].GetValue<int>() > 0)
				{
					var color = GetColor(ObjectComparisonResult.MatchingButDifferent);

					ws.Cells[row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
					ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(color);
				}


				ws.Cells[row, ++col].Value = comparison.Value.Count(_ => _.Result == ObjectComparisonResult.Equals);
				ws.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
				if (ws.Cells[row, col].GetValue<int>() > 0)
				{
					var color = GetColor(ObjectComparisonResult.Equals);

					ws.Cells[row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
					ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(color);
				}


				if (comparison.Value.All(x => x.Result == ObjectComparisonResult.Equals))
				{
					var color = GetColor(ObjectComparisonResult.Equals);

					ws.Cells[row, 1, row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
					ws.Cells[row, 1, row, col].Style.Fill.BackgroundColor.SetColor(color);
				}
			}


			var table = ws.Tables.Add(ws.Cells[1, 1, row, col], "Table_Summary");
			table.TableStyle = OfficeOpenXml.Table.TableStyles.Medium6;

			for (int i = 1; i <= col; i++)
			{
				ws.Column(i).AutoFit();
			}

			ws.View.ShowGridLines = false;
		}

		private void GenerateSheet(ExcelPackage wb, string entityName, IReadOnlyCollection<ObjectComparison<Entity>> resultList, string crm1name, string crm2name)
		{
			var ws = wb.Workbook.Worksheets.Add(entityName);

			if (resultList.All(x => x.Result == ObjectComparisonResult.Equals))
			{
				ws.TabColor = Color.Green;
			}

			var row = 1;
			var col = 0;

			ws.Cells[row, ++col].Value = "Key";
			ws.Cells[row, ++col].Value = "Result";
			ws.Cells[row, ++col].Value = "Unmatching properties";
			ws.Cells[row, ++col].Value = crm1name + " values";
			ws.Cells[row, ++col].Value = crm2name + " values";


			foreach (var result in resultList.OrderBy(x => x.Key))
			{
				row++;
				col = 0;

				var color = GetColor(result.Result);

				ws.Cells[row, ++col].Value = result.Key;
				ws.Cells[row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
				ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(color);

				ws.Cells[row, ++col].Value = result.Result;
				ws.Cells[row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
				ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(color);

				ws.Cells[row, ++col].Value = string.Join(", ", result.DifferentProperties.OrderBy(_ => _.FieldName).Select(_ => _.FieldName));
				ws.Cells[row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
				ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(color);

				ws.Cells[row, ++col].Value = string.Join(" | ", result.DifferentProperties.OrderBy(_ => _.FieldName).Select(_ => _.FormattedValue1));
				ws.Cells[row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
				ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(color);

				ws.Cells[row, ++col].Value = string.Join(" | ", result.DifferentProperties.OrderBy(_ => _.FieldName).Select(_ => _.FormattedValue2));
				ws.Cells[row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
				ws.Cells[row, col].Style.Fill.BackgroundColor.SetColor(color);
			}

			var table = ws.Tables.Add(ws.Cells[1, 1, row, col], "Table_" + entityName);
			table.TableStyle = OfficeOpenXml.Table.TableStyles.Medium6;

			for (int i = 1; i <= col; i++)
			{
				ws.Column(i).AutoFit();
			}
			ws.Column(3).Width = 50;
			ws.Column(4).Width = 50;
			ws.Column(5).Width = 50;
		}

		private Color GetColor(ObjectComparisonResult result)
		{
			switch(result)
			{
				case ObjectComparisonResult.Equals:
					return Color.FromArgb(198, 239, 206);

				case ObjectComparisonResult.MatchingButDifferent:
					return Color.FromArgb(255, 199, 206);

				case ObjectComparisonResult.LeftMissing:
					return Color.FromArgb(248, 203, 173);

				case ObjectComparisonResult.RightMissing:
					return Color.FromArgb(189, 215, 238);

				default:
					return Color.FromArgb(255, 235, 156);

			}
		}
	}
}
