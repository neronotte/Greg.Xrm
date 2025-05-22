using BrightIdeasSoftware;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Greg.Xrm.RoleEditor.Views.Lookup
{
	public partial class LookupDialog : Form
	{
		private EntityMetadata entityMetadata = null;
		private readonly IOrganizationService crm;
		private readonly QueryExpression query;

		private int currentPage = 1;
		private int pageToLoad = 1;

		public LookupDialog(IOrganizationService crm, QueryExpression query)
		{
			if (crm == null)
				throw new ArgumentNullException(nameof(crm));
			if (query == null)
				throw new ArgumentNullException(nameof(query));
			if (query.ColumnSet == null || query.ColumnSet.Columns == null)
				throw new ArgumentException("query ColumnSet cannot be null", nameof(query));
			if (query.ColumnSet.AllColumns)
				throw new ArgumentException("You cannot use AllColumns in a lookup dialog", nameof(query));
			if (query.ColumnSet.Columns.Count == 0)
				throw new ArgumentException("You need to specify at least one column", nameof(query));


			InitializeComponent();
			this.crm = crm;
			this.query = query;
			this.SelectedItem = null;

			this.btnFirst.Enabled = false;
			this.btnPrev.Enabled = false;
			this.btnNext.Enabled = false;
			this.lblPage.Text = String.Empty;

			foreach (var columnName in query.ColumnSet.Columns)
			{
				var col = new OLVColumn();
				col.Text = "Loading...";
				col.Tag = columnName;
				col.Width = 150;
				col.AspectGetter = x =>
				{
					var entity = x as Entity;
					if (entity == null) return null;

					if (!entity.Contains(columnName)) return null;

					return entity.GetFormattedValue(columnName);
				};
				this.grid.Columns.Add(col);
			}


			var textOverlay = this.grid.EmptyListMsgOverlay as TextOverlay;
			textOverlay.TextColor = Color.Gray;
			textOverlay.BackColor = Color.White;
			textOverlay.BorderColor = Color.White;
			textOverlay.BorderWidth = 1f;
			textOverlay.Font = new Font(this.Font.FontFamily, 12f);

			this.grid.EmptyListMsg = "Loading, please wait...";
			this.grid.MultiSelect = false;
			this.grid.FullRowSelect = true;
			this.grid.ShowGroups = false;
			this.grid.GridLines = true;
			this.grid.SelectedIndexChanged += (s, e) =>
			{
				var entity = this.grid.SelectedObject as Entity;
				this.SelectedItem = entity;
				this.SelectedItemReference = entity?.ToEntityReference();

				if (this.SelectedItemReference != null && this.SelectedItemReference.Name == null)
				{
					this.SelectedItemReference.Name = entity.GetFormattedValue(entityMetadata.PrimaryNameAttribute);
				}

				this.btnOk.Enabled = this.SelectedItem != null;
			};

			this.grid.MouseDoubleClick += (s, e) =>
			{
				if (this.SelectedItem != null)
				{
					this.DialogResult = DialogResult.OK;
					Close();
				}
			};
		}

		public string Description
		{
			get => this.lblDescription.Text;
			set => this.lblDescription.Text = value;
		}

		public Entity SelectedItem { get; private set; }
		public EntityReference SelectedItemReference { get; private set; }



		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.ExecuteQueryAsync();
		}

		private void ExecuteQueryAsync()
		{
			this.grid.EmptyListMsg = $"Loading page {this.pageToLoad}, please wait...";
			this.grid.ClearObjects();


			var worker = new BackgroundWorker();
			worker.WorkerReportsProgress = true;
			worker.DoWork += RetrieveItems;
			worker.RunWorkerCompleted += RenderItems;
			worker.RunWorkerAsync();
		}

		private void SafeCall(Action action)
		{
			if (this.InvokeRequired)
			{
				this.BeginInvoke(action);
			}
			else
			{
				action();
			}
		}

		private void RetrieveItems(object sender, DoWorkEventArgs e)
		{
			if (entityMetadata == null)
			{
				var request = new RetrieveEntityRequest();
				request.EntityFilters = EntityFilters.Attributes;
				request.LogicalName = this.query.EntityName;

				var response = (RetrieveEntityResponse)crm.Execute(request);
				entityMetadata = response.EntityMetadata;

				SafeCall(() =>
				{
					foreach (OLVColumn col in this.grid.Columns)
					{
						var columnName = col.Tag as string;

						var attribute = Array.Find(entityMetadata.Attributes, (x => string.Equals(x.LogicalName, columnName, StringComparison.OrdinalIgnoreCase)));
						if (attribute == null) continue;

						col.Text = attribute.DisplayName?.UserLocalizedLabel.Label;
					}
				});

				if (!query.ColumnSet.Columns.Contains(entityMetadata.PrimaryNameAttribute))
				{
					query.ColumnSet.AddColumn(entityMetadata.PrimaryNameAttribute);
				}
			}



			this.query.PageInfo = new PagingInfo
			{
				Count = 50,
				PageNumber = this.pageToLoad,
				PagingCookie = null,
				ReturnTotalRecordCount = true
			};

			var result = this.crm.RetrieveMultiple(this.query);

			if (result.MoreRecords)
			{
				this.query.PageInfo.PagingCookie = result.PagingCookie;
			}
			this.currentPage = this.pageToLoad;

			e.Result = result;
		}


		private void RenderItems(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				this.pageToLoad = this.currentPage;
				this.messages.AddError(e.Error.Message);
				this.grid.EmptyListMsg = "An error occurred while loading the data.";
				this.grid.ClearObjects();
				return;
			}

			if (!(e.Result is EntityCollection result))
			{
				this.grid.EmptyListMsg = "An error occurred while loading the data.";
				this.grid.ClearObjects();
				return;
			}

			this.grid.SetObjects(result.Entities);
			this.grid.EmptyListMsg = "No records found.";
			this.btnFirst.Enabled = this.currentPage > 1;
			this.btnPrev.Enabled = this.currentPage > 1;
			this.btnNext.Enabled = result.MoreRecords;
			this.lblPage.Text = $"Page {this.currentPage}";
		}

		private void OnCancelClick(object sender, EventArgs e)
		{
			this.SelectedItem = null;
			this.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void OnOkClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			Close();
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			this.pageToLoad = this.currentPage + 1;
			this.ExecuteQueryAsync();
		}

		private void btnPrev_Click(object sender, EventArgs e)
		{
			this.pageToLoad = this.currentPage - 1;
			this.query.PageInfo.PagingCookie = null;
			this.ExecuteQueryAsync();
		}

		private void btnFirst_Click(object sender, EventArgs e)
		{
			this.pageToLoad = 1;
			this.query.PageInfo.PagingCookie = null;
			this.ExecuteQueryAsync();
		}
	}
}
