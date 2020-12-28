using Microsoft.Xrm.Sdk;
using System;
using System.Runtime.CompilerServices;
using static Greg.Xrm.Extensions;

namespace Greg.Xrm.Model
{
	/// <summary>
	/// Base class to be used as wrapper for the CRM Entities
	/// </summary>
	public abstract class EntityWrapper : IEntityWrapperInternal
	{
		private readonly Entity preImage;
		private readonly Entity target;

		/// <summary>
		/// Creates a new EntityWrapper instance wrapped around an existing entity.
		/// </summary>
		/// <param name="entity">The entity to wrap</param>
		protected EntityWrapper(Entity entity)
		{
			this.preImage = entity ?? throw new ArgumentNullException(nameof(entity));
			this.target = new Entity(entity.LogicalName) { Id = entity.Id };
			this.IsDeleted = false;
		}

		/// <summary>
		/// Creates a new, empty, entityWrapper for an entity with the given name.
		/// </summary>
		/// <param name="entityName">The name of the entity to wrap</param>
		protected EntityWrapper(string entityName)
		{
			if (string.IsNullOrWhiteSpace(entityName))
				throw new ArgumentNullException(nameof(entityName));

			this.preImage = new Entity(entityName);
			this.target = new Entity(entityName);
			this.IsDeleted = false;
		}

		/// <summary>
		/// The unique identifier of the current entity
		/// </summary>
		public Guid Id => this.target.Id;


		/// <summary>
		/// Indicates whether the current entity is a new entity or is already been created.
		/// </summary>
		public bool IsNew => this.target.Id == Guid.Empty;

		/// <summary>
		/// Gets the logical name of the current entity
		/// </summary>
		public string EntityName => this.target.LogicalName;

		/// <summary>
		/// Indicates whether the current entity has been deleted or not
		/// </summary>
		public bool IsDeleted { get; private set; }

		/// <summary>
		/// Gets a value indicating whether the current entity has pending changes or not.
		/// </summary>
		public bool IsDirty => this.target.Attributes.Count > 0;


		/// <summary>
		/// Gets the value of the property with the given <paramref name="propertyName"/>.
		/// If not specified, takes the name of the Member (Property or Method) invoking the current function.
		/// </summary>
		/// <typeparam name="T">The type of the property value</typeparam>
		/// <param name="propertyName">The name of the property to retrieve</param>
		/// <returns>
		/// The value of the property with the given name
		/// </returns>
		protected T Get<T>([CallerMemberName] string propertyName = null)
		{
			// recupero il campo dal target
			if (this.target.Contains(propertyName))
			{
				return this.target.GetAttributeValue<T>(propertyName);
			}

			// poi dalla pre-image
			return this.preImage.GetAttributeValue<T>(propertyName);
		}

		/// <summary>
		/// Gets the value of the aliased property with the given <paramref name="propertyName"/>.
		/// <paramref name="propertyName"/> must include the entity alias.
		/// </summary>
		/// <typeparam name="T">The type of the property value</typeparam>
		/// <param name="propertyName">The name of the property to retrieve, including the alias</param>
		/// <returns>
		/// The value of the property with the given name
		/// </returns>
		protected T GetAliased<T>(string propertyName)
		{
			var aliasedValue = this.target.GetAttributeValue<AliasedValue>(propertyName);
			if (aliasedValue == null)
			{
				aliasedValue = this.preImage.GetAttributeValue<AliasedValue>(propertyName);
				if (aliasedValue == null)
				{
					return default;
				}
			}
			return (T)aliasedValue.Value;
		}

		/// <summary>
		/// Gets the value of the aliased property with the given <paramref name="propertyName"/>.
		/// <paramref name="propertyName"/> must not include the entity alias.
		/// </summary>
		/// <typeparam name="T">The type of the property value</typeparam>
		/// <param name="alias">The alias of the related entity containing the property value to be retrieved</param>
		/// <param name="propertyName">The name of the property to retrieve, without the alias</param>
		/// <returns>
		/// The value of the property with the given name
		/// </returns>
		protected T GetAliased<T>(string alias, string propertyName)
		{
			return this.GetAliased<T>($"{alias}.{propertyName}");
		}


		/// <summary>
		/// Gets the formatted value of the property with the given <paramref name="propertyName"/>.
		/// <paramref name="propertyName"/> must include the entity alias.
		/// </summary>
		/// <param name="propertyName">The name of the property to retrieve, including the alias</param>
		/// <returns>
		/// The formatted value of the given attribute
		/// </returns>
		protected string GetFormatted([CallerMemberName] string propertyName = null)
		{
			if (this.target.Contains(propertyName))
			{
				if (this.target.FormattedValues.ContainsKey(propertyName))
				{
					return this.target.FormattedValues[propertyName];
				}
			}
			else if (this.preImage.FormattedValues.ContainsKey(propertyName))
			{
				return this.preImage.FormattedValues[propertyName];
			}
			return null;
		}



		/// <summary>
		/// Sets the <paramref name="value"/> of the property with the given <paramref name="propertyName"/>.
		/// If not specified, takes the name of the Member (Property or Method) invoking the current function.
		/// </summary>
		/// <typeparam name="T">The type of the property value</typeparam>
		/// <param name="value">The value of the property</param>
		/// <param name="propertyName">The name of the property to set</param>
		protected void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
		{
			this.Set(propertyName, value);
		}


		/// <summary>
		/// Sets the <paramref name="value"/> of the property with the given <paramref name="propertyName"/>.
		/// If not specified, takes the name of the Member (Property or Method) invoking the current function.
		/// </summary>
		/// <typeparam name="T">The type of the property value</typeparam>
		/// <param name="value">The value of the property</param>
		/// <param name="propertyName">The name of the property to set</param>
		protected void Set<T>(string propertyName, T value)
		{
			if (!this.preImage.Contains(propertyName))
			{
				this.target[propertyName] = value;
				return;
			}

			var preImageValue = this.preImage.GetAttributeValue<T>(propertyName);


			if (AreEqual(value, preImageValue))
			{
				this.target.Attributes.Remove(propertyName);
			}
			else
			{
				this.target[propertyName] = value;
			}
		}

		private static bool AreEqual<T>(T value, T preImageValue)
		{
			if (typeof(T) == typeof(EntityReference))
			{
				var a = value as EntityReference;
				var b = preImageValue as EntityReference;

				return Equals(a?.Id, b?.Id) && Equals(a?.LogicalName, b?.LogicalName);
			}
			if (typeof(T) == typeof(OptionSetValue))
			{
				var a = value as OptionSetValue;
				var b = preImageValue as OptionSetValue;

				return Equals(a?.Value, b?.Value);
			}
			if (typeof(T) == typeof(Money))
			{
				var a = value as Money;
				var b = preImageValue as Money;

				return Equals(a?.Value, b?.Value);
			}
			if (typeof(T) == typeof(string))
			{
				var a = value as string;
				var b = preImageValue as string;

				return string.Equals(a, b, StringComparison.Ordinal);
			}

			return Equals(value, preImageValue);
		}

		/// <summary>
		/// Saves or updates the current entity, committing the entity changes
		/// </summary>
		/// <param name="service">The organization service to be used to save or update the changes.</param>
		/// <returns>
		/// A value indicating whether the current entity has been correcly saved or updated.
		/// </returns>
		public virtual bool SaveOrUpdate(IOrganizationService service)
		{
			if (this.IsDeleted)
			{
				return false;
			}

			if (this.IsNew) // isNew
			{
				this.target.Id = service.Create(this.target);
				this.preImage.Id = this.target.Id;
			}
			else if (this.target.Attributes.Count > 0)
			{
				service.Update(this.target);
			}



			// merge
			foreach (var key in this.target.Attributes.Keys)
			{
				this.preImage[key] = this.target[key];
			}
			this.target.Attributes.Clear();
			return true;
		}

		/// <summary>
		/// Deletes the current entity
		/// </summary>
		/// <param name="service">The organization service to be used to save or update the changes.</param>
		public virtual void Delete(IOrganizationService service)
		{
			if (this.IsNew)
				return;

			service.Delete(this.ToEntityReference());
			this.IsDeleted = true;
		}

		/// <summary>
		/// Returns an entity reference to the current object
		/// </summary>
		/// <returns>
		/// An entity reference to the current object
		/// </returns>
		public EntityReference ToEntityReference()
		{
			return this.target.ToEntityReference();
		}


		/// <summary>
		/// Returns the target entity on the entity wrapper.
		/// </summary>
		/// <returns>The target entity.</returns>
		Entity IEntityWrapperInternal.GetTarget()
		{
			return this.target;
		}



		/// <summary>
		/// Returns the pre-image used by the entity wrapper.
		/// </summary>
		/// <returns>The pre-image entity.</returns>
		Entity IEntityWrapperInternal.GetPreImage()
		{
			return this.preImage;
		}



		/// <summary>
		/// Returns the merge between pre-image and target.
		/// </summary>
		/// <returns>The post-image entity.</returns>
		Entity IEntityWrapperInternal.GetPostImage()
		{
			return this.preImage.Merge(this.target);
		}


		/// <summary>
		/// Allows users to override the Id of the current record.
		/// </summary>
		/// <param name="newId">The new GUID for the current record.</param>
		void IEntityWrapperInternal.SetId(Guid newId)
		{
			this.target.Id = newId;
			this.preImage.Id = newId;
		}



		/// <summary>
		/// Creates a copy of the current record on a new entity wrapper, without cloning the ID
		/// </summary>
		/// <param name="other">The entitywrapper that will contain the data from the current one.</param>
		public void CopyTo(EntityWrapper other)
		{
			if (!string.Equals(other.EntityName, this.EntityName))
				throw new ArgumentException($"The other entity type doesn't match the type of entity you want to clone (current: {EntityName}, other: {other.EntityName}", nameof(other));

			other.preImage.Attributes.Clear();
			other.target.Attributes.Clear();

			var forbiddenAttributes = new string[0];

			foreach (var attribute in this.preImage.Attributes)
			{
				if (!CloneSettings.IsForbidden(this.preImage, attribute.Key, forbiddenAttributes))
					other.target[attribute.Key] = attribute.Value;

				//NOTA: copio tutto sul target dell'entità clonata, anche la preImage, perché così al save posso creare correttamente il record.
				// in questo modo, le variazioni risultano essere a tutti gli effetti delle modifiche.
			}
			foreach (var attribute in this.target.Attributes)
			{
				if (!CloneSettings.IsForbidden(this.target, attribute.Key, forbiddenAttributes))
					other.target[attribute.Key] = attribute.Value;
			}
		}
	}
}
