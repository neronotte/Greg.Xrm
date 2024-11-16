using Greg.Xrm.Core;

namespace Greg.Xrm.RoleEditor.Services.ComponentResolvers
{
	public interface IComponentResolverFactory
	{
		IComponentResolver GetComponentResolverFor(ComponentType componentType);
	}
}