using Greg.Xrm.Logging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution2
{
	public class ResolverForGlobalOptionSets : IComponentResolver
	{
		private readonly ILog log;

		public ResolverForGlobalOptionSets(ILog log)
		{
			this.log = log ?? throw new ArgumentNullException(nameof(log));
		}

		public void Resolve(IReadOnlyCollection<SolutionComponent> componentList, ConnectionModel env)
		{
			var globalOptionSetList = componentList
				.Where(c => c.componenttype.Value == (int)SolutionComponentType.OptionSet)
				.ToList();

			if (globalOptionSetList.Count == 0) return;


			foreach (var item in globalOptionSetList)
			{
				try
				{
					var request = new RetrieveOptionSetRequest();
					request.MetadataId = item.objectid;

					var response = (RetrieveOptionSetResponse)env.Crm.Execute(request);

					item.Label = GetLabel(response.OptionSetMetadata);
				}
				catch(Exception ex)
				{
					log.Error("Error retrieving option set metadata: " + ex.Message, ex);
				}
			}
		}

		private static string GetLabel(OptionSetMetadataBase entity)
		{
			return $"{entity.DisplayName?.UserLocalizedLabel?.Label} ({entity.Name})"
				.Replace("()", string.Empty)
				.Trim();
		}
	}
}
