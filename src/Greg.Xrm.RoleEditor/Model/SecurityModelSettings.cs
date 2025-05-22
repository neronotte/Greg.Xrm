namespace Greg.Xrm.RoleEditor.Model
{
	/// <summary>
	/// Check: https://dev.to/_neronotte/record-ownership-across-business-units-under-the-hood-2l15
	/// </summary>
	public class SecurityModelSettings
	{
		/// <summary>
		/// Indicates if the matrix visibility is enabled or not
		/// If enabled, the logic to assign roles to users is different
		/// 
		/// Default value is <c>false</c>
		/// </summary>
		public bool IsRecordOwnershipAcrossBusinessUnitsEnabled { get; set; } = false;


		/// <summary>
		/// Indicates if, when a record is reassigned, the record owningbusinessunit id field 
		/// is moved to the business unit of the new owner
		/// 
		/// Default value is <c>true</c>
		/// </summary>
		public bool AlwaysMoveRecordToOwnerBusinessUnit { get; set; } = true;


		/// <summary>
		/// Indicates if, when an user changes business units, all roles should be removed or not from the user.
		/// 
		/// Default value is <c>false</c>.
		/// </summary>
		public bool DoNotRemoveRolesOnChangeBusinessUnit { get; set; } = false;
	}
}
