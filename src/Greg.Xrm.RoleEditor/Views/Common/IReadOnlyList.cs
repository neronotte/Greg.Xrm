using System.Collections;

namespace Greg.Xrm.RoleEditor.Views.Common
{
	public interface IReadOnlyList : IEnumerable
	{
		int Count { get; }
	}
}
