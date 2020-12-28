using Microsoft.Xrm.Sdk;

namespace Greg.Xrm.SolutionManager.Model
{
	public interface IImportJobRepository
	{
		ImportJob GetLatest(IOrganizationService service);
	}
}
