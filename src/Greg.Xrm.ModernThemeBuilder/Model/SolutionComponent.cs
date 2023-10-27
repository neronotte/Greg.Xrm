using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using XrmToolBox.Extensibility;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Greg.Xrm.ModernThemeBuilder.Model
{
	public class SolutionComponent
	{
        public SolutionComponent(WebResource webResource, Guid? id = null)
        {
			this.WebResource = webResource;
			this.Id = id ?? Guid.Empty;
		}


        public Guid Id { get; private set; }
		public WebResource WebResource { get; }

		public bool IsDirty { get; set; }

		public bool IsNew => this.Id == Guid.Empty;


		public AppHeaderColors Palette { get; private set; }


		public void UpdatePalette(AppHeaderColors palette)
		{
			this.Palette = palette;
			this.IsDirty = true;
			this.WebResource.SetContentFromString(palette.ToXmlString());
		}



		public SolutionComponent TryDeserializeContent()
		{
			var log = new LogManager(typeof(ModernThemeBuilderPlugin));
			try
			{
				var bytes = Convert.FromBase64String(this.WebResource.content);
				var xml = System.Text.Encoding.UTF8.GetString(bytes);

				var serializer = new System.Xml.Serialization.XmlSerializer(typeof(AppHeaderColors));
				this.Palette = (AppHeaderColors)serializer.Deserialize(new StringReader(xml));
				
				if (Palette != null)
				{
					var context = new ValidationContext(Palette);

					var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
					var isValid = Validator.TryValidateObject(Palette, context, validationResults, true);
					
					if (!isValid)
					{
						foreach(var error in validationResults)
							log.LogWarning("Failed to deserialize web resource {0}: {1}", this.WebResource.name, error.ErrorMessage);
						this.Palette = null;
					}
				}
			}
			catch(Exception ex)
			{
				log.LogError("Failed to deserialize web resource {0}: {1}{2}{3}", this.WebResource.name, ex.Message,Environment.NewLine, ex.ToString());
			}
			return this;
		}


        public override string ToString()
		{
			return this.WebResource.name;
		}



		public static ISolutionComponentRepository GetRepository(IOrganizationService crm)
		{
			return new SolutionComponentRepository(crm);
		}


		class SolutionComponentRepository : ISolutionComponentRepository
		{
			private readonly IOrganizationService crm;
			private readonly IWebResourceRepository webResourceRepository;

			public SolutionComponentRepository(IOrganizationService crm)
			{
				this.crm = crm ?? throw new ArgumentNullException(nameof(crm));
				this.webResourceRepository = WebResource.GetRepository(crm);
			}

			public void Create(SolutionComponent solutionComponent, Solution solution)
			{
				var request = new AddSolutionComponentRequest
				{
					AddRequiredComponents = false,
					SolutionUniqueName = solution.uniquename,
					ComponentType = 61,
					ComponentId = solutionComponent.WebResource.Id
				};

				var response = (AddSolutionComponentResponse)this.crm.Execute(request);
				solutionComponent.Id = response.id;
			}

			public List<SolutionComponent> GetSolutionComponentBySolutionId(Guid solutionId)
			{
				var query = new QueryExpression("solutioncomponent");
				query.ColumnSet.AddColumns("objectid", "componenttype");
				query.Criteria.AddCondition("solutionid", ConditionOperator.Equal, solutionId);
				query.Criteria.AddCondition("componenttype", ConditionOperator.Equal, 61);

				var entities = this.crm.RetrieveMultiple(query);
				if (entities.Entities.Count == 0) 
					return new List<SolutionComponent>();


				var objectIdList = entities.Entities.Select(x => x.GetAttributeValue<Guid>("objectid")).ToList();


				var webResources = this.webResourceRepository.GetOfTypeXmlById(objectIdList);

				var solutionComponentList = (from sc in entities.Entities
											 let objectId = sc.GetAttributeValue<Guid>("objectid")
											 let webResource = webResources.FirstOrDefault(x => x.Id == objectId)
											 where webResource != null
											 select new SolutionComponent(webResource, sc.Id)).ToList();


				solutionComponentList = solutionComponentList
					.OrderBy(x => x.WebResource.name)
					.Select(x => x.TryDeserializeContent())
					.Where(x => x.Palette != null)
					.ToList();

				return solutionComponentList;
			}
		}
	}
}
