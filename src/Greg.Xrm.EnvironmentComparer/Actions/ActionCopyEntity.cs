using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;

namespace Greg.Xrm.EnvironmentComparer.Actions
{
	public class ActionCopyEntity : IAction
	{
		public ActionCopyEntity(Entity entity, string entityKey, int targetEnvironmentIndex, string targetEnvironmentName)
		{
			if (string.IsNullOrWhiteSpace(entityKey))
			{
				throw new System.ArgumentNullException(nameof(entityKey), $"'{nameof(entityKey)}' cannot be null or whitespace");
			}

			if (targetEnvironmentIndex != 1 && targetEnvironmentIndex != 2)
			{
				throw new ArgumentOutOfRangeException(nameof(targetEnvironmentIndex), "Target environment index must be 1 or 2");
			}

			if (string.IsNullOrWhiteSpace(targetEnvironmentName))
			{
				throw new System.ArgumentNullException(nameof(targetEnvironmentName), $"'{nameof(targetEnvironmentName)}' cannot be null or whitespace");
			}

			this.Entity = entity ?? throw new System.ArgumentNullException(nameof(entity));
			this.EntityKey = entityKey;
			this.TargetEnvironmentIndex = targetEnvironmentIndex;
			this.TargetEnvironmentName = targetEnvironmentName;
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
		/// Gets the name of the environment where the entity will be copied into
		/// </summary>
		public string TargetEnvironmentName { get; }

		/// <summary>
		/// Gets the index of the environment where the entity will be copied into (1 or 2)
		/// </summary>
		public int TargetEnvironmentIndex { get; }

		/// <summary>
		/// The entity to copy
		/// </summary>
		public Entity Entity { get; }




		/// <summary>
		/// Checks whether the specified <paramref name="other"/> action is equal to the current action.
		/// </summary>
		/// <param name="obj">The object to compare with the current one.</param>
		/// <returns>True if the specified <paramref name="other"/> action is equal to the current action, false otherwise.</returns>
		public bool Equals(IAction other)
		{
			if (other == null) return false;
			if (ReferenceEquals(this, other)) return true;
			if (!(other is ActionCopyEntity o1)) return false;

			return this.EntityName.Equals(o1.EntityName) &&
				this.EntityKey.Equals(o1.EntityKey) &&
				this.TargetEnvironmentIndex.Equals(o1.TargetEnvironmentIndex);
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
				() => TargetEnvironmentIndex
			);
		}


		/// <summary>
		/// Returns a string representation of the current action.
		/// </summary>
		/// <returns>A string representation of the current action.</returns>
		public override string ToString()
		{
			return $"Copy the <{EntityName}> record with key <{EntityKey}> on <{TargetEnvironmentName}>";
		}



		/// <summary>
		/// Applies the current action
		/// </summary>
		/// <param name="crm1">The first (left) crm instance</param>
		/// <param name="crm2">The second (right) crm instance</param>
		public void ApplyTo(IOrganizationService crm1, IOrganizationService crm2)
		{
			var crm = this.TargetEnvironmentIndex == 1 ? crm1 : crm2;

			var request = new UpsertRequest
			{
				Target = this.Entity
			};
			crm.Execute(request);
		}
	}
}
