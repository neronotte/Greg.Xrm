using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution
{
	public class ResolverForOptionSets : IResolver
	{
		private readonly ILog log;

		public ResolverForOptionSets(ILog log)
		{
			this.log = log ?? throw new System.ArgumentNullException(nameof(log));
		}

		public void Resolve(SolutionComponentGrid grid, IReadOnlyCollection<ConnectionModel> connections)
		{
			using (this.log.Track("Fetching OptionSet definitions"))
			{
				var group = grid.FirstOrDefault(_ => _.ComponentTypeCode == (int)SolutionComponentType.OptionSet);
				if (group == null) return;

				group.SetAnalyzed();

				var leafList = group
					.OfType<SolutionComponentLeaf>()
					.ToList();

				foreach (var solutionComponent in leafList)
				{
					var env = solutionComponent.Environments.First();

					try
					{
						var request = new RetrieveOptionSetRequest
						{
							MetadataId = solutionComponent.ObjectId
						};

						var response = (RetrieveOptionSetResponse)env.Crm.Execute(request);

						solutionComponent.SetLabel(response.OptionSetMetadata.Name);
					}
					catch (FaultException<OrganizationServiceFault> ex)
					{
						log.Error($"Error fetching option set {solutionComponent.ObjectId} : {ex.Message}", ex);
					}
				}
			}
		}
	}
}
