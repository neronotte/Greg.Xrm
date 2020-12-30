using Greg.Xrm.EnvironmentComparer.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.EnvironmentComparer.Views.Results
{
	public partial class ResultDetailsView : DockContent
	{
		private IReadOnlyCollection<Model.Comparison<Entity>> results;

		public ResultDetailsView()
		{
			InitializeComponent();
		}

		public void SetEnvironmentNames(string env1name, string env2name)
		{
			this.cEnv1.Text = (string.IsNullOrWhiteSpace(env1name) ? "ENV1" : env1name) + " values";
			this.cEnv2.Text = (string.IsNullOrWhiteSpace(env2name) ? "ENV2" : env2name) + " values";
		}


		public IReadOnlyCollection<Model.Comparison<Entity>> Results
		{
			get => this.results;
			set
			{
				this.results = value;
				this.OnResultsChanged();
			}
		}

		private void OnResultsChanged()
		{
			if (this.InvokeRequired)
			{
				Action d = OnResultsChanged;
				this.BeginInvoke(d);
				return;
			}

			this.listView1.BeginUpdate();
			this.listView1.Items.Clear();

			if (this.results == null || this.results.Count == 0)
			{
				this.listView1.EndUpdate();
				return;
			}

			foreach (var result in this.results.OrderBy(x => x.Key))
			{
				var color = GetColor(result.Result);

				var item = this.listView1.Items.Add(result.Key);
				item.BackColor = color;

				var subItem = item.SubItems.Add(result.Result.ToString());
				subItem.BackColor = color;

				subItem = item.SubItems.Add(result.DifferentProperties.OrderBy(_ => _.FieldName).Select(_ => _.FieldName).Join(" | "));
				subItem.BackColor = color;

				subItem = item.SubItems.Add(result.DifferentProperties.OrderBy(_ => _.FieldName).Select(_ => _.FormattedValue1).Join(" | "));
				subItem.BackColor = color;

				subItem = item.SubItems.Add(result.DifferentProperties.OrderBy(_ => _.FieldName).Select(_ => _.FormattedValue2).Join(" | "));
				subItem.BackColor = color;
			}



			this.listView1.EndUpdate();
		}


		Point prevMousePos = new Point(-1, -1);

		private void ListView_MouseMove(object sender, MouseEventArgs e)
		{
			var lv = sender as ListView;

			if (lv == null) return;
			if (prevMousePos == MousePosition) return;  // to avoid annoying flickering

			var hitTestInfo = lv.HitTest(lv.PointToClient(MousePosition));


			if (hitTestInfo.SubItem != null && !String.IsNullOrEmpty(hitTestInfo.SubItem.Text))
			{
				//AndAlso .Item.SubItems.IndexOf(.SubItem) = 1
				//...when a specific Column is needed


				var t = toolTip;  //using a form's control
				t.ShowAlways = true;
				t.UseFading = true;
				//To display at exact mouse position:

				t.Show(hitTestInfo.SubItem.Text, hitTestInfo.Item.ListView, hitTestInfo.Item.ListView.PointToClient(MousePosition), 2000);
			}
			else
			{
				toolTip.Hide(this);
			}
			prevMousePos = MousePosition;
		}



		private Color GetColor(RecordComparisonResult result)
		{
			switch (result)
			{
				case RecordComparisonResult.Equals:
					return Color.FromArgb(198, 239, 206);

				case RecordComparisonResult.MatchingButDifferent:
					return Color.FromArgb(255, 199, 206);

				case RecordComparisonResult.LeftMissing:
					return Color.FromArgb(248, 203, 173);

				case RecordComparisonResult.RightMissing:
					return Color.FromArgb(189, 215, 238);

				default:
					return Color.FromArgb(255, 235, 156);

			}
		}

		private void listView1_MouseLeave(object sender, EventArgs e)
		{
		}
	}
}
