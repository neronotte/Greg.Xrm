namespace Greg.Xrm.RoleEditor.Services.Snippets
{
	public interface IPrivilegeSnippetRepository
	{
		PrivilegeSnippet this[int index] { get; set; }

		PrivilegeSnippet Get(int index);

		void Set(int index, PrivilegeSnippet snippet);
	}
}
