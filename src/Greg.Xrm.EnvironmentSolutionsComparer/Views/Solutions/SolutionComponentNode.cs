using System;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class SolutionComponentNode
	{
		public SolutionComponentNode(string label)
		{
			this.Label = label;
		}

		protected SolutionComponentNode(string label, SolutionComponentNode parent)
			:this(label)
		{
			this.Parent = parent ?? throw new ArgumentNullException(nameof(parent));
		}


		public string Label { get; protected set; }


		public SolutionComponentNode Parent { get; }
	}
}
