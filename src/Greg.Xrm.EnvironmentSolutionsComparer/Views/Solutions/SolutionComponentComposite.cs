using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class SolutionComponentComposite : SolutionComponentNode, IReadOnlyCollection<SolutionComponentNode>
	{
		protected readonly List<SolutionComponentNode> children = new List<SolutionComponentNode>();

		public SolutionComponentComposite(string label, int componentTypeCode)
			: base(label)
		{
			this.ComponentTypeCode = componentTypeCode;
		}

		public int Count => this.children.Count;

		public int ComponentTypeCode { get; }


		public IEnumerator<SolutionComponentNode> GetEnumerator()
		{
			return children.OrderBy(_ => _.Label).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}


		public SolutionComponentLeaf AddModel(SolutionComponent component)
		{
			var child = new SolutionComponentLeaf(this, component);
			this.children.Add(child);
			return child;
		}

		internal void SetAnalyzed()
		{
			this.Label = "*" + this.Label;
		}
	}
}
