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
				var componentType = solutionComponent.componenttype.Value;

				var componentTypeModel = this.FirstOrDefault(_ => _.ComponentTypeCode == componentType);
				if (componentTypeModel == null)
				{
					componentTypeModel = new SolutionComponentComposite(solutionComponent.ComponentTypeName, componentType);
					this.Add(componentTypeModel);
				}

				var model = componentTypeModel
					.OfType<SolutionComponentLeaf>()
					.FirstOrDefault(_ => _.ObjectId == solutionComponent.objectid);
				if (model == null)
				{
					model = componentTypeModel.AddModel(solutionComponent);
				}

				model.Environments.Add(env);
			}
		}
	}
}
