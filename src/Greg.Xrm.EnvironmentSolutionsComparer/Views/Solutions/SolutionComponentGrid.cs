using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class SolutionComponentGrid : List<SolutionComponentComposite>
	{
		public void AddComponents(ConnectionModel env, List<SolutionComponent> resultList)
		{
			foreach (var solutionComponent in resultList)
			{
				var componentType = solutionComponent.TypeLabel;

				var componentTypeNode = this.FirstOrDefault(_ => _.Label == componentType);
				if (componentTypeNode == null)
				{
					componentTypeNode = new SolutionComponentComposite(componentType, solutionComponent.ismetadata);
					this.Add(componentTypeNode);
				}

				var model = componentTypeNode
					.OfType<SolutionComponentLeaf>()
					.FirstOrDefault(_ => _.Label == solutionComponent.Label);
				if (model == null)
				{
					model = componentTypeNode.AddModel(solutionComponent);
				}

				model.Environments[env] = solutionComponent;
			}
		}
	}
}
