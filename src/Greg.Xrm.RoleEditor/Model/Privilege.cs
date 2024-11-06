using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Model
{
	public class Privilege : EntityWrapper
	{
		private readonly Lazy<IReadOnlyList<Level>> allowedValues;
		
		protected Privilege(Entity entity) : base(entity)
		{
			this.allowedValues = new Lazy<IReadOnlyList<Level>>(CalculateAllowedValues);
		}


		public string name => Get<string>();
		public bool canbebasic => Get<bool>();
		public bool canbelocal => Get<bool>();
		public bool canbedeep => Get<bool>();
		public bool canbeglobal => Get<bool>();


		public IReadOnlyList<Level> AllowedValues => allowedValues.Value;


		private IReadOnlyList<Level> CalculateAllowedValues()
		{
			var temp = new List<Level> { Level.None };
			if (canbebasic) temp.Add(Level.User);
			if (canbelocal) temp.Add(Level.BusinessUnit);
			if (canbedeep) temp.Add(Level.ParentChild);
			if (canbeglobal) temp.Add(Level.Organization);
			return temp;
		}


		public class Repository : IPrivilegeRepository
		{
			public IReadOnlyList<Privilege> GetAll(IOrganizationService crm)
			{
				var query = new QueryExpression("privilege");
				query.ColumnSet.AddColumns("name", "canbebasic", "canbelocal", "canbedeep", "canbeglobal");
				query.Criteria.AddCondition("name", ConditionOperator.NotLike, "%new_reserveentity%");

				var privilegeList = crm.RetrieveAll(query, x => new Privilege(x));
				return privilegeList;
			}
		}
	}

	public interface IPrivilegeRepository
	{
		IReadOnlyList<Privilege> GetAll(IOrganizationService crm);
	}
}
