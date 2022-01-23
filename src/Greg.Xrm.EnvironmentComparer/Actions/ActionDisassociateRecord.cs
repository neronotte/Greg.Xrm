using Greg.Xrm.EnvironmentComparer.Engine;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;

namespace Greg.Xrm.EnvironmentComparer.Actions
{
	public class ActionDisassociateRecord : IAction
	{
		public ActionDisassociateRecord(ObjectComparison<Entity> result, Entity entity, int sourceEnvironmentIndex, string sourceEnvironmentName)
		{
			this.Result = result ?? throw new ArgumentNullException(nameof(result));
			this.Entity = entity ?? throw new ArgumentNullException(nameof(entity));
			this.SourceEnvironmentIndex = sourceEnvironmentIndex;
			this.SourceEnvironmentName = sourceEnvironmentName;
		}

		/// <summary>
		/// Gets the logical name of the entity
		/// </summary>
		public string EntityName => this.Entity.LogicalName;


		public ObjectComparison<Entity> Result { get; }

		/// <summary>
		/// The entity to delete
		/// </summary>
		public Entity Entity { get; }

		/// <summary>
		/// Gets the name of the environment where the entity is present
		/// </summary>
		public string SourceEnvironmentName { get; }

		/// <summary>
		/// Gets the index of the environment where the entity is present (1 or 2)
		/// </summary>
		public int SourceEnvironmentIndex { get; }



		public void ApplyTo(IOrganizationService crm1, IOrganizationService crm2)
		{
			var crm = this.SourceEnvironmentIndex == 1 ? crm1 : crm2;


			var request = new DisassociateRequest
			{
				Relationship = new Relationship(Entity.LogicalName)
			};

			// TODO: capire come fare

		}

		public bool Equals(IAction other)
		{
			throw new NotImplementedException();
		}
	}
}
