using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Model
{
	/// <summary>
	/// Represents the set of privileges that are valid for a specific table.
	/// </summary>
	public interface ITemplateForTable : IEnumerable<KeyValuePair<PrivilegeType, SecurityPrivilegeMetadata>>
	{
		/// <summary>
		/// Takes as input a <see cref="PrivilegeType"/> and, if the privilege is valid for the table, returns the metadata for that privilege.
		/// If the privilege is not valid for the table, returns null.
		/// </summary>
		/// <param name="privilegeType">The type of privilege we want to retrieve</param>
		/// <returns>
		/// The security privilege metadata, or null if the privilege is not valid for the table.
		/// </returns>
		SecurityPrivilegeMetadata this[PrivilegeType privilegeType] { get; }

		/// <summary>
		/// The display name of the table.
		/// </summary>
		string Name { get;  }

		/// <summary>
		/// The logical name of the table.
		/// </summary>
		string LogicalName { get; }

		/// <summary>
		/// Returns the list of all the privileges that are valid for the table.
		/// </summary>
		/// <returns></returns>
		IReadOnlyList<SecurityPrivilegeMetadata> GetAllPrivileges();
	}
}
