using Greg.Xrm.EnvironmentComparer.Engine;
using Microsoft.Xrm.Sdk;
using System;

namespace Greg.Xrm.EnvironmentComparer.Actions
{
	/// <summary>
	/// A generic action that can be applied upon a specific record comparison result
	/// </summary>
	public interface IAction : IEquatable<IAction>
	{
		/// <summary>
		/// Gets the record comparison result this action is build upon
		/// </summary>
		ObjectComparison<Entity> Result { get; }


		/// <summary>
		/// Applies the action
		/// </summary>
		/// <param name="crm1">The first (left) organization</param>
		/// <param name="crm2">The second (right) organization</param>
		void ApplyTo(IOrganizationService crm1, IOrganizationService crm2);
	}
}
