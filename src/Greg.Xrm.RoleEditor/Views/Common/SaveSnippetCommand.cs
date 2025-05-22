using Greg.Xrm.Core.Views;
using Greg.Xrm.RoleEditor.Services.Snippets;
using Greg.Xrm.Views;
using System.Windows.Forms;

namespace Greg.Xrm.RoleEditor.Views.Common
{
	public class SaveSnippetCommand : CommandBase<SaveSnippetArgs>
	{
		private readonly IPrivilegeSnippetRepository snippetRepository;
		private readonly INotificationProvider notificationProvider;

		public SaveSnippetCommand(
			IPrivilegeSnippetRepository snippetRepository,
			INotificationProvider notificationProvider)
		{
			this.snippetRepository = snippetRepository;
			this.notificationProvider = notificationProvider;
		}


		protected override void ExecuteInternal(SaveSnippetArgs arg)
		{
			var index = arg.Index;
			var table = arg.Holder;

			if (index < 0 || index >= 10) return;
			if (table == null) return;

			var snippet = PrivilegeSnippet.CreateFromTable(table);
			if (this.snippetRepository[index] != null)
			{
				var result = MessageBox.Show($"Position {index} already contains a privilege snippet. Do you want to replace it?", "Save snippet", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (result != DialogResult.Yes) return;
			}

			this.snippetRepository[index] = snippet;


			this.notificationProvider.SendNotification(NotificationType.Success, $"Snippet saved at position {index}!", 5);
		}
	}



	public class SaveSnippetArgs
	{
		public SaveSnippetArgs(int index, IPrivilegeHolder holder)
		{
			Index = index;
			Holder = holder;
		}

		public int Index { get; }
		public IPrivilegeHolder Holder { get; }
	}
}
