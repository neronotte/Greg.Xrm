using Greg.Xrm.RoleEditor.Model;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Views.AddUserRoles
{
	public class RoleDto
	{
		private readonly DataverseEnvironment environment;
		private readonly Role role;

		public RoleDto(DataverseEnvironment environment, Role role)
		{
			this.environment = environment;
			this.role = role;

			var roleBu = environment.FindBusinessUnit(role.businessunitid?.Id);
			if (roleBu != null)
			{
				this.ValidBusinessUnits.Add(AsEntityReference(roleBu));
				this.ValidBusinessUnits.AddRange(roleBu.GetAllChildren().Select(AsEntityReference));
			}
		}

		private static EntityReference AsEntityReference(BusinessUnit source)
		{
			var sourceRef = source.ToEntityReference();
			if (string.IsNullOrWhiteSpace(sourceRef.Name))
			{
				sourceRef.Name = source.name;
			}
			return sourceRef;
		}

		public string RoleName => this.role.name;

		public string BusinessUnitName => this.role.businessunitid?.Name ?? string.Empty;

		public List<EntityReference> ValidBusinessUnits { get; } = new List<EntityReference>();
	}
}
