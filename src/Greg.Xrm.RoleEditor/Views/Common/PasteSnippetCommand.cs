using Greg.Xrm.Core.Views;
using Greg.Xrm.RoleEditor.Services.Snippets;
using Greg.Xrm.Views;
using System.Windows.Forms;

namespace Greg.Xrm.RoleEditor.Views.Common
{
	public class PasteSnippetCommand : CommandBase<PasteSnippetArgs>
	{
		private readonly IPrivilegeSnippetRepository snippetRepository;
		private readonly INotificationProvider notificationProvider;

		public PasteSnippetCommand(
			IPrivilegeSnippetRepository snippetRepository,
			INotificationProvider notificationProvider)
		{
			this.snippetRepository = snippetRepository;
			this.notificationProvider = notificationProvider;
		}


		protected override void ExecuteInternal(PasteSnippetArgs arg)
		{
			var index = arg.Index;
			var tableList = arg.HolderList;

			if (index < 0 || index >= 10) return;
			if (tableList.Length == 0) return;

			var snippet = snippetRepository[index];
			if (snippet == null)
			{
				MessageBox.Show($"No snippet found at position {index}.", "Paste snippet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			foreach (var table in tableList)
			{
				snippet.ApplyTo(table);
			}
			var plural = tableList.Length > 1 ? "s" : "";
			this.notificationProvider.SendNotification(NotificationType.Success, $"Snippet applied to {tableList.Length} table{plural}.", 5);
		}
	}

	public class PasteSnippetArgs
	{
		public PasteSnippetArgs(int index, IPrivilegeHolder[] holderList)
		{
			Index = index;
			HolderList = holderList;
		}

		public int Index { get; }
		public IPrivilegeHolder[] HolderList { get; }
	}
}
