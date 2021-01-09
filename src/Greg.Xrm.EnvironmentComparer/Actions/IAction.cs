using Microsoft.Xrm.Sdk;
using System;

namespace Greg.Xrm.EnvironmentComparer.Actions
{
	public interface IAction : IEquatable<IAction>
	{
		void ApplyTo(IOrganizationService crm1, IOrganizationService crm2);
	}
}
