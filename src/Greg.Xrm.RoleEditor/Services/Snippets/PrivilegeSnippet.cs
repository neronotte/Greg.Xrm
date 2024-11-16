using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.BulkEditor.Model;
using Greg.Xrm.RoleEditor.Views.Common;
using Greg.Xrm.RoleEditor.Views.Editor;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Services.Snippets
{
	/// <summary>
	/// This class represents a set of privilege levels associated to table-related privileges
	/// that can be applied to a role.
	/// </summary>
	public class PrivilegeSnippet : Dictionary<PrivilegeType, Level>
	{
		public PrivilegeSnippet(string description)
		{
			this.Description = description;
		}

		public PrivilegeSnippet()
		{
		}


		/// <summary>
		/// A meaningful description of the privilege snippet.
		/// </summary>
		public string Description { get; set; }

		public static PrivilegeSnippet CreateFromTable(IPrivilegeHolder holder)
		{
			var snippet = new PrivilegeSnippet();

			var privilegeList = Enum.GetValues(typeof(PrivilegeType));
			foreach (PrivilegeType privilegeType in privilegeList)
			{
				var level = holder[privilegeType];
				if (level != null)
				{
					snippet[privilegeType] = level.Value;
				}
			}

			return snippet;
		}


		public void ApplyTo(IPrivilegeHolder holder)
		{
			foreach (var kvp in this)
			{
				holder[kvp.Key] = kvp.Value;
			}
		}



		#region Flyweight implementation

		public static PrivilegeSnippet[] DefaultSnippets => defaultSnippets.Value;

		private static readonly Lazy<PrivilegeSnippet[]> defaultSnippets = new Lazy<PrivilegeSnippet[]>(CreateDefaultSnippets);

		private static PrivilegeSnippet[] CreateDefaultSnippets()
		{
			var privilegeSnippets = new PrivilegeSnippet[10];
			privilegeSnippets[0] = new PrivilegeSnippet("No access")
			{
				{ PrivilegeType.Create, Level.None },
				{ PrivilegeType.Read, Level.None },
				{ PrivilegeType.Write, Level.None},
				{ PrivilegeType.Delete, Level.None },
				{ PrivilegeType.Append, Level.None },
				{ PrivilegeType.AppendTo, Level.None},
				{ PrivilegeType.Assign, Level.None },
				{ PrivilegeType.Share, Level.None}
			};
			privilegeSnippets[1] = new PrivilegeSnippet("Read only access (e.g. for configurgation records)")
			{
				{ PrivilegeType.Create, Level.None },
				{ PrivilegeType.Read, Level.Organization },
				{ PrivilegeType.Write, Level.None},
				{ PrivilegeType.Delete, Level.None },
				{ PrivilegeType.Append, Level.None },
				{ PrivilegeType.AppendTo, Level.Organization},
				{ PrivilegeType.Assign, Level.None },
				{ PrivilegeType.Share, Level.None}
			};
			privilegeSnippets[2] = new PrivilegeSnippet("Basic write my own records")
			{
				{ PrivilegeType.Create, Level.User },
				{ PrivilegeType.Read, Level.Organization },
				{ PrivilegeType.Write, Level.User },
				{ PrivilegeType.Delete, Level.None },
				{ PrivilegeType.Append, Level.User },
				{ PrivilegeType.AppendTo, Level.Organization},
				{ PrivilegeType.Assign, Level.None },
				{ PrivilegeType.Share, Level.None}
			};
			privilegeSnippets[3] = new PrivilegeSnippet("Basic write organization records")
			{
				{ PrivilegeType.Create, Level.Organization },
				{ PrivilegeType.Read, Level.Organization },
				{ PrivilegeType.Write, Level.Organization },
				{ PrivilegeType.Delete, Level.None },
				{ PrivilegeType.Append, Level.Organization },
				{ PrivilegeType.AppendTo, Level.Organization},
				{ PrivilegeType.Assign, Level.None },
				{ PrivilegeType.Share, Level.None}
			};
			privilegeSnippets[4] = new PrivilegeSnippet("All")
			{
				{ PrivilegeType.Create, Level.Organization },
				{ PrivilegeType.Read, Level.Organization },
				{ PrivilegeType.Write, Level.Organization },
				{ PrivilegeType.Delete, Level.Organization },
				{ PrivilegeType.Append, Level.Organization },
				{ PrivilegeType.AppendTo, Level.Organization},
				{ PrivilegeType.Assign, Level.Organization},
				{ PrivilegeType.Share, Level.Organization}
			};


			return privilegeSnippets;
		}

		#endregion
	}
}
