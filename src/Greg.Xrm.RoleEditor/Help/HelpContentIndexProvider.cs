using Greg.Xrm.Core.Help;
using System;

namespace Greg.Xrm.RoleEditor.Help
{
	public class HelpContentIndexProvider
	{
		private readonly Lazy<IHelpContentIndex> instance = new Lazy<IHelpContentIndex>(CreateInstance);

		private static IHelpContentIndex CreateInstance()
		{
			var resourcePath = typeof(HelpContentIndexProvider).Namespace + ".Content.";


			return new HelpContentIndex(resourcePath)
			{
				{ Topics.Home, "home.html" },
				{ Topics.Browser, "browser.html" },
				{ Topics.Editor, "editor.html" },
				{ "editor_layout", "editor_layout.html" },
				{ "editor_docking", "editor_docking.html" },
				{ "editor_create", "editor_create.html" },
				{ "editor_update", "editor_update.html" },
				{ "editor_edit", "editor_edit.html" },
				{ "editor_save", "editor_save.html" },
				{ "editor_export", "editor_export.html" },
				{ "editor_import", "editor_import.html" },
				{ "editor_addtosolution", "editor_addtosolution.html" },
				{ "editor_copypasteall", "editor_copypasteall.html" },
				{ Topics.BulkEditor, "editor_bulk.html" },
				{ Topics.Inspector, "inspector.html" },
				{ Topics.Comparer, "comparer.html" },
				{ Topics.UserBrowser, "user_browser.html" },
				{ Topics.AddUserRoles, "add_roles_to_users.html" }
			};
		}



		public IHelpContentIndex GetIndex()
		{
			return instance.Value;
		}
	}
}
