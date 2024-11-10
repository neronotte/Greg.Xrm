using Greg.Xrm.Core;

namespace Greg.Xrm.RoleEditor.Model
{
	public interface IBusinessUnitRepository
	{
		BusinessUnit GetTree(IXrmToolboxPluginContext context);
	}
}
