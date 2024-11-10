using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Services
{
	public interface IPrivilegeClassificationProvider
	{
		Dictionary<string, string[]> GetForMiscPrivileges();
		Dictionary<string, string[]> GetForTablePrivileges();
	}
}
