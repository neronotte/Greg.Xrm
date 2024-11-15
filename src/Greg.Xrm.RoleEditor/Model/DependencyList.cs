using Greg.Xrm.Core;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Model
{
	public class DependencyList : List<Dependency>
	{
		public DependencyList()
		{
		}

		public DependencyList(IEnumerable<Dependency> collection) : base(collection)
		{
		}

		public DependencyList(int capacity) : base(capacity)
		{
		}


		public IReadOnlyList<Dependency> OfType(ComponentType componentType)
		{
			return this.Where(x => x.dependentcomponenttype.Value == (int)componentType).ToArray();
		}
	}
}
