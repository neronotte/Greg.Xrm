using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Services.Snippets;
using Microsoft.Xrm.Sdk.Metadata;

namespace Greg.Xrm.RoleEditor.Views.Config
{
	public class PrivilegeSnippetViewModel : ViewModel
	{
		private readonly PrivilegeSnippet snippet;

		public PrivilegeSnippetViewModel(int index, PrivilegeSnippet snippet)
        {
			this.Index = index;
			this.snippet = snippet ?? new PrivilegeSnippet();
		}

		public bool IsEditable => this.Index > 4;

		internal void SetFrom(PrivilegeSnippet privilegeSnippet)
		{
			if (privilegeSnippet == null)
			{
				this.snippet.Clear();
				return;
			}

			foreach (var kvp in privilegeSnippet)
			{
				this.snippet[kvp.Key] = kvp.Value;
			}
		}

		public int Index
		{
			get => Get<int>();
			set => Set(value);
		}


		public Level? Get(PrivilegeType privilege)
		{
			if (!this.snippet.ContainsKey(privilege)) return null;
			return this.snippet[privilege];
		}


		public void Set(PrivilegeType privilege, Level? level)
		{
			if (level.HasValue)
			{
				this.snippet[privilege] = level.Value;
				return;
			}

			this.snippet.Remove(privilege);
		}

		public void Increase(PrivilegeType privilegeType)
		{
			var current = this.Get(privilegeType);
			if (!current.HasValue)
			{
				this.Set(privilegeType, Level.None);
				return;
			}

			var next = (int)current.Value + 1;
			if (next == 5)
			{
				this.Set(privilegeType, null);
				return;	
			}

			this.Set(privilegeType, (Level)next);
		}

		public PrivilegeSnippet ToPrivilegeSnippet()
		{
			if (this.snippet.Count == 0) return null;
			return this.snippet;
		}
	}
}
