﻿using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.SolutionManager.Views.Timeline
{
	public partial class TimelineView : DockContent
	{
		public TimelineView()
		{
			InitializeComponent();
			Reset();
		}

		public void Reset()
		{
			var chart = this.chart1.ChartAreas[0];
			chart.Name = "Main area";
			chart.BackColor = Color.White;
			chart.AxisX.IntervalType = DateTimeIntervalType.Number;
			chart.AxisX.MajorGrid.Interval = 10;
			chart.AxisX.MinorGrid.Interval = 2;
			chart.AxisX.MajorGrid.LineColor = Color.FromArgb(221, 221, 221);
			chart.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

			chart.AxisY.IntervalType = DateTimeIntervalType.Number;
			chart.AxisY.MajorGrid.Interval = 10;
			chart.AxisY.MinorGrid.Interval = 2;
			chart.AxisY.MajorGrid.LineColor = Color.FromArgb(221, 221, 221);
			chart.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

			chart.AxisY.Minimum = 0;
			chart.AxisY.Minimum = 0;

			this.chart1.Series.Clear();

			var series1 = this.chart1.Series.Add("Elapsed");
			series1.ChartType = SeriesChartType.Spline;
			series1.ChartArea = chart.Name;
			series1.BorderWidth = 2;

			var series2 = this.chart1.Series.Add("Total");
			series2.ChartType = SeriesChartType.Spline;
			series2.ChartArea = chart.Name;
			series2.BorderWidth = 2;
		}


		public void AddTimelineData(TimelineData data)
		{
			this.chart1.Series["Elapsed"].Points.AddXY(data.Iteration, data.Elapsed.TotalMinutes);
			this.chart1.Series["Total"].Points.AddXY(data.Iteration, data.Total.TotalMinutes);
		}
	}
}
