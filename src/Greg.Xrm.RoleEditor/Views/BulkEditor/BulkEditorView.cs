using BrightIdeasSoftware;
using Greg.Xrm.Core.Views.Help;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.RoleEditor.Help;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Services;
using Greg.Xrm.RoleEditor.Services.Snippets;
using Greg.Xrm.RoleEditor.Views.BulkEditor.Model;
using Greg.Xrm.RoleEditor.Views.Common;
using Greg.Xrm.RoleEditor.Views.Editor;
using Greg.Xrm.RoleEditor.Views.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Greg.Xrm.RoleEditor.Views.BulkEditor
{
	public partial class BulkEditorView : DockContent
	{
		private readonly IScopedMessenger messenger;
		private readonly ILog log;
		private readonly BulkEditorViewModel viewModel;

		private readonly ToolStripMenuItem[] contextMenuItems = new ToolStripMenuItem[5];
		private readonly Role[] roleList;

		public BulkEditorView(
			ISettingsProvider<Settings> settingsProvider,
			IPrivilegeSnippetRepository privilegeSnippetRepository,
			IPrivilegeClassificationProvider privilegeClassificationProvider,
			Role[] roleList)
		{
			if (roleList == null || roleList.Length == 0)
				throw new ArgumentNullException(nameof(roleList));


			this.messenger = roleList[0].ExecutionContext.Messenger.CreateScope();
			this.log = roleList[0].ExecutionContext.Log;

			InitializeComponent();

			this.RegisterHelp(messenger, Topics.BulkEditor);


			this.viewModel = new BulkEditorViewModel(
				privilegeClassificationProvider,
				privilegeSnippetRepository,
				roleList);
			this.viewModel.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(RoleEditorViewModel.ShouldShowOnlyAssignedPrivileges))
				{
					RefreshTableFilters();
					RefreshMiscFilters();
				}
			};

			this.notificationPanel.Bind(this.viewModel);


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
			this.mSetNull.Click += OnContextMenuClick;


			var settings = settingsProvider.GetSettings();
			this.SetIcons(settings.UseLegacyPrivilegeIcons);
			this.messenger.Register<ChangePrivilegeIcons>(m => SetIcons(m.UseLegacyIcons));


			this.Bind(v => v.Text, viewModel, vm => vm.Title);
			this.Bind(v => v.TabText, viewModel, vm => vm.Title);


			this.treeTables.Roots = this.viewModel.TableGroups;
			this.treeTables.CanExpandGetter = CanExpandTree;
			this.treeTables.ChildrenGetter = GetChildren;
			this.treeTables.UseCellFormatEvents = true;
			this.treeTables.UseFiltering = true;
			this.treeTables.FormatCell += OnFormatCell;
			this.treeTables.CellClick += OnCellClick;
			this.treeTables.CellRightClick += OnCellRightClick;
			this.treeTables.KeyDown += OnTreeTablesKeyDown;
			this.treeTables.ColumnClick += OnTreeColumnClick;
			this.treeTables.UseLightSelection();
			this.treeTables.ExpandAll();
			this.treeTables.CellToolTipShowing += OnCellToolTipShowing;
			this.RefreshTableFilters();


			this.cCreate.SetPrivilegeColumn(PrivilegeType.Create).SetImageGetter(x => GetImage(x, PrivilegeType.Create));
			this.cRead.SetPrivilegeColumn(PrivilegeType.Read).SetImageGetter(x => GetImage(x, PrivilegeType.Read));
			this.cWrite.SetPrivilegeColumn(PrivilegeType.Write).SetImageGetter(x => GetImage(x, PrivilegeType.Write));
			this.cDelete.SetPrivilegeColumn(PrivilegeType.Delete).SetImageGetter(x => GetImage(x, PrivilegeType.Delete));
			this.cAppend.SetPrivilegeColumn(PrivilegeType.Append).SetImageGetter(x => GetImage(x, PrivilegeType.Append));
			this.cAppendTo.SetPrivilegeColumn(PrivilegeType.AppendTo).SetImageGetter(x => GetImage(x, PrivilegeType.AppendTo));
			this.cAssign.SetPrivilegeColumn(PrivilegeType.Assign).SetImageGetter(x => GetImage(x, PrivilegeType.Assign));
			this.cShare.SetPrivilegeColumn(PrivilegeType.Share).SetImageGetter(x => GetImage(x, PrivilegeType.Share));

			this.cMiscValue.SetPrivilegeColumn(PrivilegeType.None).SetImageGetter(x => GetImage(x, PrivilegeType.None));

			this.treeMisc.Roots = this.viewModel.MiscGroups;
			this.treeMisc.CanExpandGetter = CanExpandTree;
			this.treeMisc.ChildrenGetter = GetChildren;
			this.treeMisc.UseCellFormatEvents = true;
			this.treeMisc.UseFiltering = true;
			this.treeMisc.FormatCell += OnFormatCell;
			this.treeMisc.CellClick += OnCellClick;
			this.treeMisc.CellRightClick += OnCellRightClick;
			this.treeMisc.KeyDown += OnTreeMiscKeyDown;
			this.treeMisc.ColumnClick += OnTreeColumnClick;
			this.treeMisc.UseLightSelection();
			this.treeMisc.ExpandAll();
			this.treeMisc.CellToolTipShowing += OnCellToolTipShowing;


			this.RefreshMiscFilters();


			this.tShowAll.BindCommand(() => this.viewModel.ShowAllPrivilegesCommand, behavior: CommandExecuteBehavior.Visible);
			this.tShowOnlyAssigned.BindCommand(() => this.viewModel.ShowOnlyAssignedPrivilegesCommand, behavior: CommandExecuteBehavior.Visible);
			this.tSave.BindCommand(() => this.viewModel.SaveCommand);
			this.viewModel.SaveCommand.SaveCompleted += (s, e) =>
			{
				this.tabs.SelectedTab = this.tabTables;
			};


			RoleEditor.Model.TreeNode.Bind(this.treeChangeSummary);
			this.tabs.SelectedIndexChanged += (s, e) =>
			{

				if (this.tabs.SelectedTab == this.tabChangeSummary)
				{
					var changes = this.viewModel.GetChangeSummary().ToTree();
					this.treeChangeSummary.Roots = changes;
					this.treeChangeSummary.ExpandAll();
				}
			};

			this.tSearchTableText.KeyUp += OnSearchTableKeyUp;
			this.txtSearchMisc.KeyUp += OnSearchMiscKeyUp;

			this.tools.Bind(x => x.Enabled, viewModel, vm => vm.IsEnabled);
			this.tabs.Bind(x => x.Enabled, viewModel, vm => vm.IsEnabled);

			this.treeMisc.UseLightSelection();
			this.roleList = roleList;

			this.FormClosed += (s, e) => this.messenger.Dispose();
		}

		private static object GetImage(object model, PrivilegeType privilegeType)
		{
			if (model is BulkTableModel table)
				return (int?)table.Get(privilegeType);
			if (model is BulkMiscModel misc)
				return (int?)misc.Value;
			return null;
		}



		private void OnFormatCell(object sender, FormatCellEventArgs e)
		{
			if (e.Model is IReadOnlyList)
			{
				e.SubItem.Font = new System.Drawing.Font(e.SubItem.Font, System.Drawing.FontStyle.Bold);
				return;
			}


			if (e.Model is BulkTableModel table)
			{
				if (!(e.Column.Tag is PrivilegeColumnTag tag)) return;

				if (!table.IsValid(tag.PrivilegeType))
				{
					e.SubItem.BackColor = System.Drawing.Color.LightGray;
				}
				else if (table.CheckDirty(tag.PrivilegeType))
				{
					e.SubItem.BackColor = System.Drawing.Color.Yellow;
				}
			}


			if (e.Model is BulkMiscModel misc)
			{

				if (e.Column == this.cMiscValue && misc.IsDirty)
				{
					e.SubItem.BackColor = System.Drawing.Color.Yellow;
				}
				if (e.Column == this.cMiscTooltip)
				{
					e.SubItem.ForeColor = System.Drawing.Color.Gray;
				}
			}
		}

		private void OnCellClick(object sender, CellClickEventArgs e)
		{
			if (!(e.Column?.Tag is PrivilegeColumnTag tag)) return;

			if (e.Model is BulkTableModel table)
			{
				table.Increase(tag.PrivilegeType);
				this.treeTables.RefreshObject(table);
				e.Handled = true;
			}
			if (e.Model is BulkMiscModel misc)
			{
				misc.Increase();
				this.treeMisc.RefreshObject(misc);
				e.Handled = true;
			}
		}



		private void OnCellRightClick(object sender, CellRightClickEventArgs e)
		{
			if (e.ColumnIndex == 0) return;
			if (e.Model is BulkTableModel table)
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

			if (e.Model is BulkMiscModel misc)
			{
				var column = e.Column;
				if (column == null) return;

				var validityMatrix = misc.GetPrivilegeLevelValidityMatrix();
				if (validityMatrix.Length == 0) return;

				for (int i = 0; i < contextMenuItems.Length; i++)
				{
					contextMenuItems[i].Visible = validityMatrix[i];
				}
				this.contextMenu.Tag = e;
				this.contextMenu.Show(this.treeTables, e.Location);
				e.Handled = true;
			}
		}




		private void OnContextMenuClick(object sender, EventArgs e1)
		{
			if (!(this.contextMenu.Tag is CellRightClickEventArgs e)) return;

			var menu = (ToolStripMenuItem)sender;

			Level? selectedLevel = null;
			if (menu.Tag != null)
			{
				selectedLevel = (Level)menu.Tag;
			}

			if (e.Model is BulkTableModel table)
			{
				var column = e.Column;
				if (column == null) return;

				if (!(column.Tag is PrivilegeColumnTag tag)) return;
				table.Set(tag.PrivilegeType, selectedLevel);
				this.treeTables.RefreshObject(table);
				e.Handled = true;
			}

			if (e.Model is BulkMiscModel misc)
			{
				var column = e.Column;
				if (column == null) return;

				misc.Set(selectedLevel);
				this.treeMisc.RefreshObject(misc);
				e.Handled = true;
			}
		}


		private void OnTreeColumnClick(object sender, ColumnClickEventArgs e)
		{
			var tree = (TreeListView)sender;
			var column = (OLVColumn)tree.Columns[e.Column];

			foreach (var table in tree.FilteredObjects.OfType<BulkTableModel>())
			{
				if (column.Tag is PrivilegeColumnTag tag)
				{
					if (!tree.IsExpanded(table.Parent))
						continue;

					table.Increase(tag.PrivilegeType);
				}
			}

			foreach (var misc in tree.FilteredObjects.OfType<BulkMiscModel>())
			{
				if (!tree.IsExpanded(misc.Parent))
					continue;

				misc.Increase();
			}

			tree.RefreshObject(tree.FilteredObjects);
		}



		private void OnTreeTablesKeyDown(object sender, KeyEventArgs e)
		{
			if (!this.viewModel.IsEnabled) return;
			if (e.KeyCode == Keys.ShiftKey) return;
			if (e.KeyCode == Keys.ControlKey) return;

			var tableList = this.treeTables.SelectedObjects.OfType<BulkTableModel>().ToArray();
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


			if (e.Control && e.KeyCode == Keys.V)
			{
				var text = Clipboard.GetText();
				if (string.IsNullOrWhiteSpace(text)) return;

				foreach (var table in tableList)
				{
					table.ApplyConfigurationCommand(this.log, text);
				}
				this.treeTables.RefreshObjects(tableList);

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


				e.Handled = true;
				e.SuppressKeyPress = true;
				return;
			}


			if (e.Control && e.Shift && e.KeyCode >= Keys.D5 && e.KeyCode <= Keys.D9)
			{
				// User pressed CTRL+SHIFT+ any digit from 4 to 9
				int digit = e.KeyCode - Keys.D0;
				this.viewModel.SaveSnippetCommand.Execute(new SaveSnippetArgs(digit, tableList[0]));
				e.Handled = true;
				e.SuppressKeyPress = true;
				return;
			}

			if (e.Control && e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
			{
				// User pressed CTRL+SHIFT+ any digit from 0 to 9
				int digit = e.KeyCode - Keys.D0;
				this.viewModel.PasteSnippetCommand.Execute(new PasteSnippetArgs(digit, tableList));
				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}



		private void OnTreeMiscKeyDown(object sender, KeyEventArgs e)
		{
			if (!this.viewModel.IsEnabled) return;
			if (e.KeyCode == Keys.ShiftKey) return;
			if (e.KeyCode == Keys.ControlKey) return;

			var miscList = this.treeTables.SelectedObjects.OfType<BulkMiscModel>().ToArray();
			if (miscList.Length == 0) return;

			if (e.Control && e.KeyCode == Keys.C)
			{
				var table = miscList[0];
				if (miscList.Length > 1)
				{
					this.notificationPanel.AddWarning($"You selected {miscList.Length} misc privileges, only the first ({table.Name}) configuration will be copied.");
				}

				var text = table.GenerateConfigurationCommand();
				Clipboard.SetText(text);

				this.log.Debug("Miscellaneous privilege configuration copied to clipboard.");

				e.Handled = true;
				e.SuppressKeyPress = true;
				return;
			}


			if (e.Control && e.KeyCode == Keys.V)
			{
				var text = Clipboard.GetText();
				if (string.IsNullOrWhiteSpace(text)) return;

				foreach (var table in miscList)
				{
					table.ApplyConfigurationCommand(this.log, text);
				}
				this.treeMisc.RefreshObjects(miscList);

				this.log.Debug($"Misc configuration pasted on {miscList.Length} tables.");

				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}

		private void OnCellToolTipShowing(object sender, ToolTipShowingEventArgs e)
		{
			if (e.Model is BulkTableModel table
				&& e.Column.Tag is PrivilegeColumnTag tag
				&& table.TryGetPrivilegeName(tag.PrivilegeType, out var tooltip))
			{
				e.Text = tooltip;
			}
			if (e.Model is BulkMiscModel misc)
			{
				e.Text = misc.Tooltip;
			}

			e.Handled = true;
		}








		private static bool CanExpandTree(object model)
		{
			if (model is IReadOnlyList group)
			{
				return group.Count > 0;
			}
			return false;
		}

		private static IEnumerable GetChildren(object model)
		{
			return model as IEnumerable;
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

		private void RefreshTableFilters()
		{
			this.treeTables.ModelFilter = new ModelFilter(model =>
			{
				if (!(model is BulkTableModel table)) return true;

				if (!string.IsNullOrWhiteSpace(this.viewModel.SearchTableText)
					&& !table.Name.ToLowerInvariant().Contains(this.viewModel.SearchTableText?.ToLowerInvariant())
					&& !table.LogicalName.ToLowerInvariant().Contains(this.viewModel.SearchTableText?.ToLowerInvariant()))
					return false;

				if (this.viewModel.ShouldShowOnlyAssignedPrivileges)
				{
					return table.IsDirty;
				}

				return true;
			});
		}


		private void RefreshMiscFilters()
		{
			this.treeMisc.ModelFilter = new ModelFilter(model =>
			{
				if (!(model is BulkMiscModel misc)) return true;

				if (!string.IsNullOrWhiteSpace(this.viewModel.SearchMiscText)
					&& !misc.Name.ToLowerInvariant().Contains(this.viewModel.SearchMiscText?.ToLowerInvariant())
					&& !misc.Tooltip.ToLowerInvariant().Contains(this.viewModel.SearchMiscText?.ToLowerInvariant()))
					return false;

				if (this.viewModel.ShouldShowOnlyAssignedPrivileges)
				{
					return misc.IsDirty;
				}

				return true;
			});
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
			this.messenger.Send(new CloseRoleView(this.roleList));
		}
	}
}
