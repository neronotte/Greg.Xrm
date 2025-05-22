using Greg.Xrm.EnvironmentComparer.Engine;
using Microsoft.Xrm.Sdk;
using System;

namespace Greg.Xrm.EnvironmentComparer.Actions
{
	public class ActionDeleteRecord : IAction
	{
		public ActionDeleteRecord(ObjectComparison<Entity> result, Entity entity, int sourceEnvironmentIndex, string sourceEnvironmentName)
		{
			if (sourceEnvironmentIndex != 1 && sourceEnvironmentIndex != 2)
			{
				throw new ArgumentOutOfRangeException(nameof(sourceEnvironmentIndex), "Target environment index must be 1 or 2");
			}

			if (string.IsNullOrWhiteSpace(sourceEnvironmentName))
			{
				throw new ArgumentNullException(nameof(sourceEnvironmentName), $"'{nameof(sourceEnvironmentName)}' cannot be null or whitespace");
			}

			this.Result = result;
			this.Entity = entity ?? throw new ArgumentNullException(nameof(entity));
			this.EntityKey = result.Key;
			this.SourceEnvironmentIndex = sourceEnvironmentIndex;
			this.SourceEnvironmentName = sourceEnvironmentName;
		}

		/// <summary>
		/// Gets the logical name of the entity
		/// </summary>
		public string EntityName => this.Entity.LogicalName;

		/// <summary>
		/// Gets the logical unique identifier for this record
		/// </summary>
		public string EntityKey { get; }

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


		public ObjectComparison<Entity> Result { get; }



		/// <summary>
		/// Checks whether the specified <paramref name="other"/> action is equal to the current action.
		/// </summary>
		/// <param name="obj">The object to compare with the current one.</param>
		/// <returns>True if the specified <paramref name="other"/> action is equal to the current action, false otherwise.</returns>
		public bool Equals(IAction other)
		{
			if (other == null) return false;
			if (ReferenceEquals(this, other)) return true;
			if (!(other is ActionDeleteRecord o1)) return false;

			return this.EntityName.Equals(o1.EntityName) &&
				this.EntityKey.Equals(o1.EntityKey) &&
				this.SourceEnvironmentIndex.Equals(o1.SourceEnvironmentIndex);
		}



		/// <summary>
		/// Checks whether the specified <paramref name="obj"/> is equal to the current action.
		/// </summary>
		/// <param name="obj">The object to compare with the current one.</param>
		/// <returns>True if the specified <paramref name="obj"/> is equal to the current action, false otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (!(obj is IAction other)) return false;
			return Equals(other);
		}



		/// <summary>
		/// Gets an hash code for the current action
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.CalculateHashCode(
				() => EntityName,
				() => EntityKey,
				() => SourceEnvironmentIndex
			);
		}


		/// <summary>
		/// Returns a string representation of the current action.
		/// </summary>
		/// <returns>A string representation of the current action.</returns>
		public override string ToString()
		{
			return $"Delete the <{EntityName}> record with key <{EntityKey}> from <{SourceEnvironmentName}>";
		}


		/// <summary>
		/// Applies the current action
		/// </summary>
		/// <param name="crm1">The first (left) crm instance</param>
		/// <param name="crm2">The second (right) crm instance</param>
		public void ApplyTo(IOrganizationService crm1, IOrganizationService crm2)
		{
			var crm = this.SourceEnvironmentIndex == 1 ? crm1 : crm2;

			var entityRef = this.Entity.ToEntityReference();
			crm.Delete(entityRef);
		}
	}
}
