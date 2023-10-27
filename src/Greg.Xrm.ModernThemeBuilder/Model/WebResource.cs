using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Greg.Xrm.ModernThemeBuilder.Model
{
	public class WebResource : EntityWrapper
	{
        private WebResource(Entity entity) : base(entity)
        {
        }

		public WebResource() : base("webresource")
		{
		}

        public string name
		{
			get => Get<string>();
			set => SetValue(value);
		}
		public string displayname
		{
			get => Get<string>();
			set => SetValue(value);
		}
		public string description
		{
			get => Get<string>();
			set => SetValue(value);
		}
		public string content 
		{
			get => Get<string>();
			set => SetValue(value);
		}

		public OptionSetValue webresourcetype
		{
			get => Get<OptionSetValue>();
			set => SetValue(value);
		}



		public void SetContentFromString(string contentText)
		{
			var bytes = Encoding.UTF8.GetBytes(contentText);
			var base64 = Convert.ToBase64String(bytes);
			this.content = base64;
		}



		public IReadOnlyList<string> SplitNameInParts()
		{
			var publisherPrefix = this.name.Substring(0, this.name.IndexOf('_') + 1);
			var nameWithoutPrefix = this.name.Substring(publisherPrefix.Length).TrimStart('/');
			var parts = nameWithoutPrefix.Split('/');

			var partList = new List<string>();
			partList.Add(publisherPrefix);
			partList.AddRange(parts);
			return partList;
		}



		public static IWebResourceRepository GetRepository(IOrganizationService crm)
		{
			return new WebResourceRepository(crm);
		}


		class WebResourceRepository : IWebResourceRepository
		{
			private readonly IOrganizationService crm;

			public WebResourceRepository(IOrganizationService crm)
			{
				this.crm = crm;
			}

			public bool Exists(string name)
			{
				var query = new QueryExpression("webresource");
				query.Criteria.AddCondition("name", ConditionOperator.Equal, name);
				query.TopCount = 1;
				query.NoLock = true;

				var result = this.crm.RetrieveMultiple(query);
				return result.Entities.Count > 0;
			}

			public IReadOnlyList<WebResource> GetOfTypeXmlById(IEnumerable<Guid> ids)
			{
				var query2 = new QueryExpression("webresource");
				query2.ColumnSet.AddColumns("name", "displayname", "description", "content", "webresourcetype");
				query2.Criteria.AddCondition("webresourceid", ConditionOperator.In, ids.Cast<object>().ToArray());
				query2.Criteria.AddCondition("webresourcetype", ConditionOperator.Equal, (int)WebResourceType.XML);
				query2.NoLock = true;

				var webResources = this.crm
					.RetrieveMultiple(query2)
					.Entities
					.Select(x => new WebResource(x))
					.OrderBy(x => x.name)
					.ToList();

				return webResources;
			}
		}
	}

	public interface IWebResourceRepository
	{
		IReadOnlyList<WebResource> GetOfTypeXmlById(IEnumerable<Guid> ids);

		bool Exists(string name);
	}
}
