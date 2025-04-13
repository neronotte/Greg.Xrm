using Greg.Xrm.RoleEditor.Model;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Views.AddUserRoles
{
	public class TreeNodeRole : TreeNode
	{
		private readonly Role role;

		public TreeNodeRole(DataverseEnvironment environment, Role role)
		{
			this.role = role;
			this.Name = role.name;
			this.BusinessUnit = role.businessunitidFormatted;


			var roleBu = environment.FindBusinessUnit(role.businessunitid?.Id);
			if (roleBu != null)
			{
				this.ValidBusinessUnits.Add(AsEntityReference(roleBu));
				this.ValidBusinessUnits.AddRange(roleBu.GetAllChildren().Select(AsEntityReference));
			}

			this.CurrentBusinessUnit = this.ValidBusinessUnits.FirstOrDefault(x => x.Id == this.role.businessunitid.Id);
		}


		private EntityReference currentBusinessUnit;

		public EntityReference CurrentBusinessUnit 
		{
			get => this.currentBusinessUnit; 
			set
			{
				this.currentBusinessUnit = value;
				this.BusinessUnit = value.Name;
			}
		}

		public List<EntityReference> ValidBusinessUnits { get; } = new List<EntityReference>();



		public bool RequiresFetch
		{
			get
			{
				return this.role.businessunitid?.Id != this.CurrentBusinessUnit?.Id;
			}
		}





		public override int GetHashCode()
		{
			unchecked
			{
				return this.role.name.GetHashCode() ^ this.role.businessunitid.Id.GetHashCode();
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (ReferenceEquals(obj, this)) return true;
			if (!(obj is TreeNodeRole other)) return false;

			return 
				this.role.name == other.role.name && 
				this.role.businessunitid.Id == other.role.businessunitid.Id;
		}


		public Role GetRole() => this.role;




		private static EntityReference AsEntityReference(BusinessUnit source)
		{
			var sourceRef = source.ToEntityReference();
			if (string.IsNullOrWhiteSpace(sourceRef.Name))
			{
				sourceRef.Name = source.name;
			}
			return sourceRef;
		}
	}
}
