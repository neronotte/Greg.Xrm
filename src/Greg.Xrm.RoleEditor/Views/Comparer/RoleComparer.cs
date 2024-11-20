using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Editor;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Views.Comparer
{
	public class RoleComparer
	{
		public RoleComparisonResult CompareRoles(Role role1, Role role2)
		{
			var result = new RoleComparisonResult(role1, role2);



			var tableNodes = new TreeNode("Table-Related Privileges");

			var tableList = role1.Template.Tables.Values.Select(x => new {
				x.Name,
				x.LogicalName
			})
			.Union(role2.Template.Tables.Values.Select(y => new {
				y.Name,
				y.LogicalName
			}))
			.GroupBy(x => x.LogicalName)
			.Select(x => new {
				LogicalName = x.Key,
				DisplayName = string.Join(", ", x.Select(y => y.Name))
			}).ToList();

			foreach (var table in tableList)
			{
				var tableNode = new TreeNode($"{table.DisplayName}", tooltip: table.LogicalName);


				role1.Template.Tables.TryGetValue(table.LogicalName, out var template1);
				role2.Template.Tables.TryGetValue(table.LogicalName, out var template2);

				var privileges = new List<dynamic>();
				if (template1 != null)
				{
					privileges.AddRange(template1.GetAllPrivileges().Select(x => new {x.Name, x.PrivilegeType, Order = GetOrder(x.PrivilegeType) }));
				}
				if (template2 != null)
				{
					privileges.AddRange(template2.GetAllPrivileges().Select(x => new { x.Name, x.PrivilegeType, Order = GetOrder(x.PrivilegeType) }));
				}
				privileges = privileges.Distinct().ToList();


				foreach (var privilege in privileges.OrderBy(x => x.Order))
				{
					var privilege1 = role1.Privileges.FirstOrDefault(x => x.PrivilegeName == privilege.Name);
					var privilege2 = role2.Privileges.FirstOrDefault(x => x.PrivilegeName == privilege.Name);

                    if (privilege1?.Depth != privilege2?.Depth)
					{
						tableNode.Add(new PrivilegeDifference(privilege.PrivilegeType.ToString(), privilege1?.Depth, privilege2?.Depth, privilege.Name));
					}
                }

				if (tableNode.Count > 0)
				{
					tableNodes.Add(tableNode);
				}
			}
			if (tableNodes.Count > 0)
			{
				tableNodes.Sort((a, b) => string.Compare(a.Text, b.Text));
				result.Add(tableNodes);
			}





			var miscNodes = new TreeNode("Miscellaneous privileges");
			var miscList = role1.Template.Misc.Values.Select(x => new {
				x.Name,
				x.PrivilegeName
			})
			.Union(role2.Template.Misc.Values.Select(y => new {
				y.Name,
				y.PrivilegeName
			}))
			.GroupBy(x => x.PrivilegeName)
			.Select(x => new {
				LogicalName = x.Key,
				DisplayName = string.Join(", ", x.Select(y => y.Name))
			}).ToList();


			foreach (var misc in miscList)
			{
				var privilege1 = role1.Privileges.FirstOrDefault(x => x.PrivilegeName == misc.LogicalName);
				var privilege2 = role2.Privileges.FirstOrDefault(x => x.PrivilegeName == misc.LogicalName);

				if (privilege1?.Depth != privilege2?.Depth)
				{
					miscNodes.Add(new PrivilegeDifference(misc.DisplayName, privilege1?.Depth, privilege2?.Depth, misc.LogicalName));
				}
			}
			if (miscNodes.Count > 0)
			{
				miscNodes.Sort((a,b) => string.Compare(a.Text, b.Text));
				result.Add(miscNodes);
			}


			return result;
		}

		private static int GetOrder(PrivilegeType privilegeType)
		{
			switch (privilegeType)
			{
				case PrivilegeType.None:
					return 0;
				case PrivilegeType.Create:
					return 1;
				case PrivilegeType.Read:
					return 2;
				case PrivilegeType.Write:
					return 3;
				case PrivilegeType.Delete:
					return 4;
				case PrivilegeType.Append:
					return 5;
				case PrivilegeType.AppendTo:
					return 6;
				case PrivilegeType.Assign:
					return 7;
				case PrivilegeType.Share:
					return 8;
				default:
					return 99;
			}
		}
	}
}
