using BrightIdeasSoftware;
using Greg.Xrm.Core.Views.Help;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.RoleEditor.Help;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Services;
using Greg.Xrm.RoleEditor.Views.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections;
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

		private readonly ToolStripMenuItem[] contextMenuItems = new ToolStripMenuItem[5];

		public RoleEditorView(
			ISettingsProvider<Settings> settingsProvider,
			IPrivilegeClassificationProvider privilegeClassificationProvider,
			Role role)
		{
			this.role = role;
			this.log = role.ExecutionContext.Log;
			this.messenger = role.ExecutionContext.Messenger;

			this.RegisterHelp(messenger, Topics.Editor);

			InitializeComponent();

			this.contextMenuItems[0] = this.mSet0;
			this.contextMenuItems[1] = this.mSet1;
			this.contextMenuItems[2] = this.mSet2;
			this.contextMenuItems[3] = this.mSet3;
			this.contextMenuItems[4] = this.mSet4;

			for (int i = 0; i < this.contextMenuItems.Length; i++)
			{
				this.contextMenuItems[i].Tag = i;
				this.contextMenuItems[i].Click += OnContextMenuClick;
			}



			// here we will check which icons to use
			var settings = settingsProvider.GetSettings();
			this.SetIcons(settings.UseLegacyPrivilegeIcons);
			this.messenger.Register<ChangePrivilegeIcons>(m => SetIcons(m.UseLegacyIcons));


			this.viewModel = new RoleEditorViewModel(privilegeClassificationProvider, role);
			this.viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(RoleEditorViewModel.ShouldShowOnlyAssignedPrivileges))
				{
					RefreshTableFilters();
					RefreshMiscFilters();
				}

				if (e.PropertyName == nameof(Model))
				{
					this.RefreshDataBindings();
				}
			};

			this.Bind(x => x.Text, viewModel, vm => vm.ViewTitle);
			this.Bind(x => x.TabText, viewModel, vm => vm.ViewTitle);

			this.tShowAllPrivileges.BindCommand(() => this.viewModel.ShowAllPrivilegesCommand, behavior: CommandExecuteBehavior.Visible);
			this.tShowOnlyAssignedPrivileges.BindCommand(() => this.viewModel.ShowOnlyAssignedPrivilegesCommand, behavior: CommandExecuteBehavior.Visible);
			this.tSave.BindCommand(() => this.viewModel.SaveCommand);
			this.tAddToSolution.BindCommand(() => this.viewModel.AddRoleToSolutionCommand, () => this);
			this.tExportExcel.BindCommand(() => this.viewModel.ExportExcelCommand);
			this.tExportMarkdown.BindCommand(() => this.viewModel.ExportMarkdownCommand);

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
					if (e.Column.Tag is PrivilegeColumnTag tag
						&& table.TryGetPrivilegeName(tag.PrivilegeType, out var tooltip))
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
			this.treeTables.UseCustomSelectionColors = true;
			this.treeTables.HighlightBackgroundColor = Color.FromArgb(240, 240, 240);
			this.treeTables.HighlightForegroundColor = Color.Black;
			this.treeTables.CellRightClick += OnCellRightClick;
			this.treeTables.ExpandAll();


			this.treeMisc.UseFiltering = true;
			this.treeMisc.UseCellFormatEvents = true;
			this.treeMisc.CellClick += OnCellClick;
			this.treeMisc.FormatCell += OnFormatCell;
			this.treeMisc.ColumnClick += OnTreeColumnClick;
			this.treeMisc.KeyDown += OnTreeMiscKeyDown;
			this.treeMisc.Roots = this.viewModel.Model?.MiscGroups;
			this.treeMisc.UseCustomSelectionColors = true;
			this.treeMisc.HighlightBackgroundColor = Color.FromArgb(240, 240, 240);
			this.treeMisc.HighlightForegroundColor = Color.Black;
			this.treeMisc.CellRightClick += OnCellRightClick;
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
			this.txtSearchMisc.KeyUp += OnSearchMiscKeyUp;



			// setup of the General Info tab
			this.cmbRoleInheritance.DropDownStyle = ComboBoxStyle.DropDownList;
			RefreshDataBindings();


			RefreshTableFilters();
			RefreshMiscFilters();


			this.txtRoleName.Enabled = this.viewModel.IsCustomizable;
			this.txtRoleDescription.Enabled = this.viewModel.IsCustomizable;
			this.cmbRoleInheritance.Enabled = this.viewModel.IsCustomizable;
			this.tools.Bind(x => x.Enabled, viewModel, vm => vm.IsEnabled);
			this.tabs.Bind(x => x.Enabled, viewModel, vm => vm.IsEnabled);
			if (this.role.IsNew)
			{
				this.tabs.SelectedTab = this.tabGeneral;
			}
			this.tabs.SelectedIndexChanged += OnTabChanged;


			this.notificationPanel.Bind(this.viewModel);
		}

		private void RefreshDataBindings()
		{
			this.tabs.SelectedTab = this.tabTables;
			this.treeTables.Roots = this.viewModel.Model?.TableGroups;
			this.treeMisc.Roots = this.viewModel.Model?.MiscGroups;
			this.treeTables.ExpandAll();
			this.treeMisc.ExpandAll();

			this.txtRoleName.Bind(x => x.Text, viewModel.Model, vm => vm.Name);
			this.txtRoleDescription.Bind(x => x.Text, viewModel.Model, vm => vm.Description);

			this.txtRoleBusinessUnit.Bind(x => x.Text, viewModel.Model, vm => vm.BusinessUnitName);
			this.btnRoleBusinessUnitLookup.BindCommand(() => viewModel.Model.ChangeBusinessUnit, () => this);

			this.cmbRoleInheritance.Items.Clear();
			this.cmbRoleInheritance.Items.AddRange(this.viewModel.Model.InheritedValues);
			this.cmbRoleInheritance.Bind(x => x.SelectedItem, viewModel.Model, vm => vm.IsInherited);

			if (this.viewModel.Model.Id == Guid.Empty)
			{
				this.toolTip1.SetToolTip(this.txtRoleName, "When creating a new role, consider adding a well defined prefix to the name to make it easily identifiable among all system roles.");
			}
			else
			{
				this.toolTip1.SetToolTip(this.txtRoleName, null);
			}
		}


		private void SetIcons(bool useLegacyIcons)
		{
			if (useLegacyIcons)
			{
				this.treeTables.SmallImageList = this.privilegeImagesOld;
				this.treeMisc.SmallImageList = this.privilegeImagesOld;
				for (int i = 0; i < this.contextMenuItems.Length; i++)
				{
					this.contextMenuItems[i].Image = this.privilegeImagesOld.Images[i];
				}
			}
			else
			{
				this.treeTables.SmallImageList = this.privilegeImagesNew;
				this.treeMisc.SmallImageList = this.privilegeImagesNew;
				for (int i = 0; i < this.contextMenuItems.Length; i++)
				{
					this.contextMenuItems[i].Image = this.privilegeImagesNew.Images[i];
				}
			}
		}



		private void OnSearchTableKeyUp(object sender, KeyEventArgs e)
		{
			this.viewModel.SearchTableText = this.tSearchTableText.Text;
			RefreshTableFilters();
			e.Handled = true;
		}

		private void OnSearchMiscKeyUp(object sender, KeyEventArgs e)
		{
			this.viewModel.SearchMiscText = this.txtSearchMisc.Text;
			RefreshMiscFilters();
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
		}


		private void RefreshMiscFilters()
		{
			this.treeMisc.ModelFilter = new ModelFilter(model =>
			{
				if (!(model is MiscModel misc)) return true;

				if (!string.IsNullOrWhiteSpace(this.viewModel.SearchMiscText)
					&& !misc.Name.ToLowerInvariant().Contains(this.viewModel.SearchMiscText?.ToLowerInvariant())
					&& !misc.Tooltip.ToLowerInvariant().Contains(this.viewModel.SearchMiscText?.ToLowerInvariant()))
					return false;

				if (this.viewModel.ShouldShowOnlyAssignedPrivileges)
				{
					return misc.IsAssigned;
				}

				return true;
			});
		}



		private void OnCellClick(object sender, CellClickEventArgs e)
		{
			if (!this.viewModel.IsCustomizable) return;
			if (e.ColumnIndex == 0) return;

			if (e.Model is TableModel table)
			{
				var column = e.Column;
				if (column == null) return;

				if (!(column.Tag is PrivilegeColumnTag tag)) return;

				table.Increase(tag.PrivilegeType);
				this.treeTables.RefreshObject(table);
				this.viewModel.EvaluateDirty();
				e.Handled = true;
			}

			if (e.Model is MiscModel misc)
			{
				var column = e.Column;
				if (column != this.cMiscValue) return;


				misc.Increase();
				this.treeMisc.RefreshObject(misc);
				this.viewModel.EvaluateDirty();
				e.Handled = true;
			}
		}




		private void OnCellRightClick(object sender, CellRightClickEventArgs e)
		{
			if (!this.viewModel.IsCustomizable) return;
			if (e.ColumnIndex == 0) return;
			if (e.Model is TableModel table)
			{
				var column = e.Column;
				if (column == null) return;

				if (!(column.Tag is PrivilegeColumnTag tag)) return;

				var validityMatrix = table.GetPrivilegeLevelValidityMatrix(tag.PrivilegeType);
				if (validityMatrix.Length == 0) return;

				for (int i = 0; i < contextMenuItems.Length; i++)
				{
					contextMenuItems[i].Visible = validityMatrix[i];
				}
				this.contextMenu.Tag = e;
				this.contextMenu.Show(this.treeTables, e.Location);
				e.Handled = true;
			}

			if (e.Model is MiscModel misc)
			{
				var column = e.Column;
				if (column == null) return;
				if (column != this.cMiscValue) return;

				var validityMatrix = misc.GetPrivilegeLevelValidityMatrix();
				if (validityMatrix.Length == 0) return;

				for (int i = 0; i < contextMenuItems.Length; i++)
				{
					contextMenuItems[i].Visible = validityMatrix[i];
				}
				this.contextMenu.Tag = e;
				this.contextMenu.Show(this.treeMisc, e.Location);
				e.Handled = true;
			}
		}

		private void OnContextMenuClick(object sender, EventArgs e1)
		{
			if (!this.viewModel.IsCustomizable) return;

			var e = this.contextMenu.Tag as CellRightClickEventArgs;
			if (e == null) return;

			var menu = (ToolStripMenuItem)sender;
			var selectedLevel = (Level)menu.Tag;

			if (e.Model is TableModel table)
			{
				var column = e.Column;
				if (column == null) return;

				if (!(column.Tag is PrivilegeColumnTag tag)) return;
				table.Set(tag.PrivilegeType, selectedLevel);
				this.treeTables.RefreshObject(table);
				this.viewModel.EvaluateDirty();
				e.Handled = true;
			}


			if (e.Model is MiscModel misc)
			{
				var column = e.Column;
				if (column == null) return;

				misc.Value = selectedLevel;
				this.treeMisc.RefreshObject(misc);
				this.viewModel.EvaluateDirty();

				e.Handled = true;
			}
		}


		private void OnFormatCell(object sender, FormatCellEventArgs e)
		{
			if (e.Model is TableModel table)
			{
				var column = e.Column;
				if (column == null) return;

				if (!(column.Tag is PrivilegeColumnTag tag)) return;

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
			if (!this.viewModel.IsCustomizable) return;
			var tree = (TreeListView)sender;
			var column = (OLVColumn)tree.Columns[e.Column];



			foreach (var table in tree.FilteredObjects.OfType<TableModel>())
			{
				if (column.Tag is PrivilegeColumnTag tag)
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
			this.viewModel.EvaluateDirty();
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
					this.notificationPanel.AddWarning($"You selected {tableList.Length} tables, only the first table ({table.Name}) configuration will be copied.");
				}

				var text = table.GenerateConfigurationCommand();
				Clipboard.SetText(text);

				this.log.Debug("Table configuration copied to clipboard.");

				e.Handled = true;
				e.SuppressKeyPress = true;
				return;
			}


			// we enable copying the role configuration, but we cannot allow paste or other write operations
			if (!this.viewModel.IsCustomizable) return;

			if (e.Control && e.KeyCode == Keys.V)
			{
				var text = Clipboard.GetText();
				if (string.IsNullOrWhiteSpace(text)) return;

				foreach (var table in tableList)
				{
					table.ApplyConfigurationCommand(this.log, text);
				}
				this.treeTables.RefreshObjects(tableList);
				this.viewModel.EvaluateDirty();


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
				this.viewModel.EvaluateDirty();


				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}

		private void OnTreeMiscKeyDown(object sender, KeyEventArgs e)
		{
			var miscList = this.treeMisc.SelectedObjects.OfType<MiscModel>().ToArray();
			if (miscList.Length == 0) return;

			if (e.Control && e.KeyCode == Keys.C)
			{
				var misc = miscList[0];
				if (miscList.Length > 1)
				{
					this.notificationPanel.AddWarning($"You selected {miscList.Length} privileges, only the first ({misc.Name}) configuration will be copied.");
				}

				var text = misc.GenerateConfigurationCommand();
				Clipboard.SetText(text);

				this.log.Debug("Misc configuration copied to clipboard.");

				e.Handled = true;
				e.SuppressKeyPress = true;
				return;
			}




			// we enable copying the role configuration, but we cannot allow paste or other write operations
			if (!this.viewModel.IsCustomizable) return;

			if (e.Control && e.KeyCode == Keys.V)
			{
				var text = Clipboard.GetText();
				if (string.IsNullOrWhiteSpace(text)) return;

				foreach (var table in miscList)
				{
					table.ApplyConfigurationCommand(this.log, text);
				}
				this.treeTables.RefreshObjects(miscList);
				this.viewModel.EvaluateDirty();

				this.log.Debug($"Misc configuration pasted on {miscList.Length} tables.");

				e.Handled = true;
				e.SuppressKeyPress = true;
				return;
			}


			if (e.KeyCode == Keys.Space)
			{
				foreach (var misc in miscList)
				{
					misc.Increase();
				}
				this.treeMisc.RefreshObjects(miscList);
				this.viewModel.EvaluateDirty();

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
