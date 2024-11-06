using BrightIdeasSoftware;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	public partial class RoleEditorView : DockContent
	{
		private readonly ILog log;
		private readonly IMessenger messenger;
		private readonly Role role;
		private readonly RoleEditorViewModel viewModel;

		public RoleEditorView(ILog log, IMessenger messenger, IOrganizationService crm, Role role, TemplateForRole template)
		{
			this.log = log;
			this.messenger = messenger;
			this.role = role;
			InitializeComponent();


			this.viewModel = new RoleEditorViewModel(log, messenger, crm, role, template);
			this.viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(RoleEditorViewModel.ShouldShowOnlyAssignedPrivileges))
				{
					RefreshTableFilters();
				}
			};

			this.SetTitle();

			this.tShowAllPrivileges.BindCommand(() => this.viewModel.ShowAllPrivilegesCommand, behavior: CommandExecuteBehavior.Visible);
			this.tShowOnlyAssignedPrivileges.BindCommand(() => this.viewModel.ShowOnlyAssignedPrivilegesCommand, behavior: CommandExecuteBehavior.Visible);
			this.tSave.BindCommand(() => this.viewModel.SaveCommand);

			SetColumn(this.cCreate, PrivilegeType.Create);
			SetColumn(this.cRead, PrivilegeType.Read);
			SetColumn(this.cWrite, PrivilegeType.Write);
			SetColumn(this.cDelete, PrivilegeType.Delete);
			SetColumn(this.cAppend, PrivilegeType.Append);
			SetColumn(this.cAppendTo, PrivilegeType.AppendTo);
			SetColumn(this.cAssign, PrivilegeType.Assign);
			SetColumn(this.cShare, PrivilegeType.Share);
			SetColumn(this.cMiscValue, PrivilegeType.None);

			this.treeTables.CanExpandGetter =
				this.treeMisc.CanExpandGetter =
					x => (x is ICollection collection) && collection.Count > 0;

			this.treeTables.ChildrenGetter = 
				this.treeMisc.ChildrenGetter = 
					x => (x is ICollection collection) ? collection : Array.Empty<object>();

			this.treeTables.CellToolTipShowing += (object sender, ToolTipShowingEventArgs e) =>
			{
				if (!(e.Model is TableModel table)) return;

				if (e.ColumnIndex == 0)
				{
					e.Text = table.Tooltip;
				}
				else
				{
					var tag = e.Column.Tag as PrivilegeColumnTag;
					if (tag != null && table.TryGetPrivilegeName(tag.PrivilegeType, out var tooltip))
					{
						e.Text = tooltip;
					}
				}
				e.Handled = true;
			};

			this.treeTables.UseFiltering = true;
			this.treeTables.UseCellFormatEvents = true;
			this.treeTables.CellClick += OnCellClick;
			this.treeTables.FormatCell += OnFormatCell;
			this.treeTables.ColumnClick += OnTreeColumnClick;
			this.treeTables.KeyDown += OnTreeTablesKeyDown;
			this.treeTables.Roots = this.viewModel.Model?.TableGroups;
			this.treeTables.ExpandAll();


			this.treeMisc.UseFiltering = true;
			this.treeMisc.UseCellFormatEvents = true;
			this.treeMisc.CellClick += OnCellClick;
			this.treeMisc.FormatCell += OnFormatCell;
			this.treeMisc.ColumnClick += OnTreeColumnClick;
			//this.treeMisc.KeyDown += OnTreeKeyDown;
			this.treeMisc.Roots = this.viewModel.Model?.MiscGroups;
			this.treeMisc.ExpandAll();



			this.cSummaryText.GroupKeyGetter = m =>
			{
				return m.GetType();
			};
			this.cSummaryText.GroupKeyToTitleConverter = m =>
			{
				var operationType = m as Type;
				if (operationType == null) return null;

				if (operationType == typeof(ChangeOperationAdd)) return "Privileges to add";
				if (operationType == typeof(ChangeOperationRemove)) return "Privileges to remove";
				if (operationType == typeof(ChangeOperationReplace)) return "Privileges to replace";

				return "???";
			};
			this.cSummaryText.ImageGetter = m =>
			{
				if (m.GetType() == typeof(ChangeOperationAdd)) return "add";
				if (m.GetType() == typeof(ChangeOperationRemove)) return "remove";
				if (m.GetType() == typeof(ChangeOperationReplace)) return "replace";
				return null;
			};

			this.tSearchTableText.KeyUp += OnSearchTableKeyUp;


			RefreshTableFilters();

			this.tabs.Bind(x => x.Enabled, viewModel, vm => vm.IsEnabled);
			this.tools.Bind(x => x.Enabled, viewModel, vm => vm.IsEnabled);
		}


		private void SetTitle()
		{
			this.Text = this.TabText = this.viewModel.Model.Name + (this.viewModel.Model.IsDirty ? " *" : string.Empty);
		}




		private void OnSearchTableKeyUp(object sender, KeyEventArgs e)
		{
			this.viewModel.SearchTableText = this.tSearchTableText.Text;
			RefreshTableFilters();
			e.Handled = true;
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			this.messenger.Send(new CloseRoleView(this.role));
		}


		private static void SetColumn(OLVColumn column, PrivilegeType privilegeType)
		{
			column.Width = 60;
			column.TextAlign = HorizontalAlignment.Center;
			column.AspectToStringConverter = x => string.Empty;
			column.Sortable = false;
			column.ImageGetter = delegate (object x)
			{
				if (x is TableModel table)
					return (int?)table[privilegeType];
				if (x is MiscModel misc)
					return (int)misc.Value;
				return null;
			};
			column.Tag = new PrivilegeColumnTag(privilegeType);
		}


		private void RefreshTableFilters()
		{
			this.treeTables.ModelFilter = new ModelFilter(model =>
			{
				if (!(model is TableModel table)) return true;

				if (!string.IsNullOrWhiteSpace(this.viewModel.SearchTableText)
					&& !table.Name.ToLowerInvariant().Contains(this.viewModel.SearchTableText?.ToLowerInvariant())
					&& !table.Tooltip.ToLowerInvariant().Contains(this.viewModel.SearchTableText?.ToLowerInvariant()))
					return false;

				if (this.viewModel.ShouldShowOnlyAssignedPrivileges)
				{
					return table.HasAssignedPrivileges;
				}

				return true;
			});

			this.treeMisc.ModelFilter = new ModelFilter(model =>
			{
				if (!(model is MiscModel misc)) return true;

				if (this.viewModel.ShouldShowOnlyAssignedPrivileges)
				{
					return misc.IsAssigned;
				}

				return true;
			});
		}




		private void OnCellClick(object sender, CellClickEventArgs e)
		{
			if (e.ColumnIndex == 0) return;

			if (e.Model is TableModel table)
			{
				var column = e.Column;
				if (column == null) return;

				var tag = column.Tag as PrivilegeColumnTag;
				if (tag == null) return;

				table.Increase(tag.PrivilegeType);
				this.treeTables.RefreshObject(table);
				this.SetTitle();
				e.Handled = true;
			}

			if (e.Model is MiscModel misc)
			{
				var column = e.Column;
				if (column != this.cMiscValue) return;

				
				misc.Increase();
				this.treeTables.RefreshObject(misc);
				this.SetTitle();
				e.Handled = true;
			}
		}

		private void OnFormatCell(object sender, FormatCellEventArgs e)
		{
			if (e.Model is TableModel table)
			{
				var column = e.Column;
				if (column == null) return;

				var tag = column.Tag as PrivilegeColumnTag;
				if (tag == null) return;

				if (table.IsChanged(tag.PrivilegeType))
					e.SubItem.BackColor = Color.Yellow;
			}

			if (e.Model is MiscModel misc)
			{
				var column = e.Column;
				if (column == this.cMiscValue)
				{
					if (misc.IsChanged)
						e.SubItem.BackColor = Color.Yellow;
				}
				if (column == this.cMiscTooltip)
				{
					e.SubItem.ForeColor = Color.Gray;
				}
			}

			if (e.Model is TableGroupModel || e.Model is MiscGroupModel)
			{
				e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Bold);
			}
		}


		private void OnTreeColumnClick(object sender, ColumnClickEventArgs e)
		{
			var tree = (TreeListView)sender;

			var column = (OLVColumn)tree.Columns[e.Column];



			foreach (var table in tree.FilteredObjects.OfType<TableModel>())
			{
				var tag = column.Tag as PrivilegeColumnTag;
				if (tag != null)
				{
					if (!tree.IsExpanded(table.Parent))
						continue;

					table.Increase(tag.PrivilegeType);
				}
			}

			foreach (var misc in tree.FilteredObjects.OfType<MiscModel>())
			{
				if (!tree.IsExpanded(misc.Parent))
					continue;

				misc.Increase();
			}

			tree.RefreshObject(tree.FilteredObjects);
			this.SetTitle();
		}


		private void OnTreeTablesKeyDown(object sender, KeyEventArgs e)
		{
			var tableList = this.treeTables.SelectedObjects.OfType<TableModel>().ToArray();
			if (tableList.Length == 0) return;

			if (e.Control && e.KeyCode == Keys.C)
			{
				var table = tableList[0];
				if (tableList.Length > 1)
				{
					MessageBox.Show($"You selected {tableList.Length} tables, only the first table ({table.Name}) configuration will be copied.", "Copy table configuration", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				var text = table.GenerateConfigurationCommand();
				Clipboard.SetText(text);

				this.log.Debug("Table configuration copied to clipboard.");

				e.Handled = true;
				e.SuppressKeyPress = true;
				return;
			}
			if (e.Control && e.KeyCode == Keys.V)
			{
				var text = Clipboard.GetText();
				if (string.IsNullOrWhiteSpace(text)) return;

				foreach (var table in tableList)
				{
					table.ApplyConfigurationCommand(this.log, text);
				}
				this.treeTables.RefreshObjects(tableList);
				this.SetTitle();


				this.log.Debug($"Table configuration pasted on {tableList.Length} tables.");

				e.Handled = true;
				e.SuppressKeyPress = true;
				return;
			}


			if (e.KeyCode == Keys.Space)
			{
				foreach (var table in tableList)
				{
					table.IncreaseAll();
				}
				this.treeTables.RefreshObjects(tableList);
				this.SetTitle();


				e.Handled = true;
				e.SuppressKeyPress = true;
			}



			if (e.KeyCode == Keys.A)
			{
				foreach (var table in tableList)
				{
					table.Set(Level.User, Level.User, Level.User, Level.None, Level.User, Level.User, Level.User, Level.User);
				}
				this.treeTables.RefreshObjects(tableList);
				this.SetTitle();

				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}



		private void OnTabChanged(object sender, EventArgs e)
		{
			if (this.tabs.SelectedTab == this.tabChangeSummary)
			{
				var changeSummary = this.viewModel.GetChangeSummary();
				this.olvChangeSummary.SetObjects(changeSummary);
			}
		}





		class PrivilegeColumnTag
		{
			public PrivilegeColumnTag(PrivilegeType privilegeType)
			{
				PrivilegeType = privilegeType;
			}

			public PrivilegeType PrivilegeType { get; }
		}
	}
}
