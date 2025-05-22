using Greg.Xrm.RoleEditor.Model;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Views.AddUserRoles
{
	public class TreeNodeRole : TreeNode
	{
		private readonly Role role;
		private readonly bool isRecordOwnershipAcrossBusinessUnitEnabled;

		public TreeNodeRole(DataverseEnvironment environment, Role role, bool isRecordOwnershipAcrossBusinessUnitEnabled)
		{
			this.role = role;
			this.isRecordOwnershipAcrossBusinessUnitEnabled = isRecordOwnershipAcrossBusinessUnitEnabled;
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
				if (this.isRecordOwnershipAcrossBusinessUnitEnabled)
					return base.GetHashCode();

				return this.Name.GetHashCode() ^ this.CurrentBusinessUnit.Id.GetHashCode();
			}
		}

		public override bool Equals(object obj)
		{
			// when matrix visibility is enabled, i want to be able to add the same role to multiple business units
			// to do that without side effects, i need to ensure that instances of this class are considered different
			if (this.isRecordOwnershipAcrossBusinessUnitEnabled)
				return object.Equals(this, obj);


			if (obj == null) return false;
			if (ReferenceEquals(obj, this)) return true;
			if (!(obj is TreeNodeRole other)) return false;



			return
				this.Name == other.Name &&
				this.CurrentBusinessUnit.Id == other.CurrentBusinessUnit.Id;
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
