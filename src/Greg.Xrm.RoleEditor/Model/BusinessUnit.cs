using Greg.Xrm.Core;
using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Model
{
	public class BusinessUnit : EntityWrapper
	{
		private BusinessUnit(Entity entity) : base(entity)
		{
		}

		public string name => Get<string>();

		public EntityReference parentbusinessunitid => Get<EntityReference>();


		private readonly List<BusinessUnit> children = new List<BusinessUnit>();
		public IReadOnlyList<BusinessUnit> Children => this.children;

		private void AddChild(BusinessUnit child)
		{
			this.children.Add(child);
			this.children.Sort((a, b) => string.Compare(a.name, b.name));
		}



		private readonly List<Role> roles = new List<Role>();
		public IReadOnlyList<Role> Roles => this.roles;

		public bool HasAnyRole()
		{
			if (this.roles.Count > 0) return true;
			return this.children.Exists(x => x.HasAnyRole());
		}

		public bool Contains(Role role, bool recursive = false)
		{
			if (role == null) return false;

			if (this.roles.Contains(role)) return true;

			if (recursive)
			{
				return this.children.Exists(x => x.Contains(role));
			}
			return false;
		}

		public void AddRole(Role role)
		{
			if (role.businessunitid?.Id == this.Id)
			{
				this.roles.Add(role);
				this.roles.Sort((a, b) => string.Compare(a.name, b.name));
			}
			else
			{
				foreach (var child in this.Children)
				{
					child.AddRole(role);
				}
			}
		}




		public class Repository : IBusinessUnitRepository
		{
			public BusinessUnit GetTree(IXrmToolboxPluginContext context)
			{
				using (context.Log.Track($"Reading list of business units from <{context.Details.ConnectionName}>"))
				{
					var query = new QueryExpression("businessunit");
					query.ColumnSet = new ColumnSet("name", "parentbusinessunitid");

					var buList = context.RetrieveAll(query, x => new BusinessUnit(x));
					var buDict = buList.ToDictionary(x => x.Id);


					BusinessUnit root = null;
					foreach (var bu in buList)
					{
						if (bu.parentbusinessunitid == null)
						{
							root = bu;
							continue;
						}

						if (buDict.TryGetValue(bu.parentbusinessunitid.Id, out var parent))
						{
							parent.AddChild(bu);
						}
					}
					return root;
				}

			}
		}
	}
}
