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
					var name = solutionComponent.ComponentTypeName;
					if (string.IsNullOrWhiteSpace(name))
					{
						try
						{
							var componentTypeEnum = (SolutionComponentType)componentType;
							name = componentTypeEnum.ToString();
						}
#pragma warning disable CA1031 // Do not catch general exception types
						catch // casting exception may be caused by a missing component type
						{
							name = "Component type: " + componentType;
						}
#pragma warning restore CA1031 // Do not catch general exception types
					}

					componentTypeModel = new SolutionComponentComposite(name, componentType);
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
