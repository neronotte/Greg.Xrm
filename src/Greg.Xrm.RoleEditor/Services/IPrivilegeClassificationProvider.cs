using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Services
{
	public interface IPrivilegeClassificationProvider
	{
		Dictionary<string, string[]> GetForMiscPrivileges();
		void SaveForMiscPrivileges(Dictionary<string, string[]> classification);
		void ResetMiscPrivileges();
	}
}
