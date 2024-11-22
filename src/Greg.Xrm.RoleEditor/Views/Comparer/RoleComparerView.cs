using Greg.Xrm.Core.Views.Help;
using Greg.Xrm.Messaging;
using Greg.Xrm.RoleEditor.Help;
using Greg.Xrm.RoleEditor.Views.Messages;
using WeifenLuo.WinFormsUI.Docking;
using TreeNode = Greg.Xrm.RoleEditor.Model.TreeNode;

namespace Greg.Xrm.RoleEditor.Views.Comparer
{
	public partial class RoleComparerView : DockContent
	{
		private readonly IScopedMessenger messenger;

		public RoleComparerView(
			ISettingsProvider<Settings> settingsProvider,
			RoleComparisonResult result)
		{

			InitializeComponent();
			this.Text = this.TabText = "Role Comparer";


			this.messenger = result.Role1.ExecutionContext.Messenger.CreateScope();

			this.RegisterHelp(this.messenger, Topics.Comparer);


			var settings = settingsProvider.GetSettings();
			this.SetIcons(settings.UseLegacyPrivilegeIcons);
			messenger.Register<ChangePrivilegeIcons>(m => SetIcons(m.UseLegacyIcons));

			TreeNode.Bind(tree);

			this.cRole1.Text = result.Role1.ToString();
			this.cRole1.ImageGetter = x =>
			{
				if (!(x is PrivilegeDifference d)) return null;
				return (int)d.Level1;
			};

			this.cRole2.Text = result.Role2.ToString();
			this.cRole2.ImageGetter = x =>
			{
				if (!(x is PrivilegeDifference d)) return null;
				return (int)d.Level2;
			};

			this.tree.SetObjects(result);
			this.tree.ExpandAll();

			this.FormClosed += (s, e) =>
			{
				this.messenger.Dispose();
			};

			this.ExportExcelCommand = new ExportExcelCommand();

			this.tExportExcel.BindCommand(() => this.ExportExcelCommand, () => result);
		}

		private ExportExcelCommand ExportExcelCommand { get; }



		private void SetIcons(bool useLegacyIcons)
		{
			if (useLegacyIcons)
			{
				this.tree.SmallImageList = this.privilegeImagesOld;
			}
			else
			{
				this.tree.SmallImageList = this.privilegeImagesNew;
			}
		}
	}
}
