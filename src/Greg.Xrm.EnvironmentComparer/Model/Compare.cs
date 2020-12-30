using Greg.Xrm.EnvironmentComparer.Logging;
using Greg.Xrm.EnvironmentComparer.Model.Memento;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Greg.Xrm.EnvironmentComparer.Model
{
	public static class Compare
	{
		public static ICompareFluentInterface Entity(string entityName, IKeyProvider<Entity> keyProvider)
		{
			var f = new FluentInterface();
			return f.Entity(entityName, keyProvider);
		}

		public static ICompareFluentInterface Entity(string entityName, IKeyProvider<Entity> keyProvider, bool onlyActiveRecords)
		{
			var f = new FluentInterface();
			return f.Entity(entityName, keyProvider, onlyActiveRecords);
		}

		public static ICompareFluentInterface Entity(string entityName, IKeyProvider<Entity> keyProvider, Action<QueryExpression> filterCriteria)
		{
			var f = new FluentInterface();
			return f.Entity(entityName, keyProvider, filterCriteria);
		}

		public static ICompareFluentInterface Entity(string entityName, IKeyProvider<Entity> keyProvider, ISkipAttributeCriteria skipAttributeCriteria)
		{
			var f = new FluentInterface();
			return f.Entity(entityName, keyProvider, skipAttributeCriteria);
		}

		public static ICompareFluentInterface Entity(string entityName, IKeyProvider<Entity> keyProvider, ISkipAttributeCriteria skipAttributeCriteria, bool onlyActiveRecords)
		{
			var f = new FluentInterface();
			return f.Entity(entityName, keyProvider, skipAttributeCriteria, onlyActiveRecords);
		}

		public static ICompareFluentInterface FromMemento(string mementoFileName, out EngineMemento memento)
		{
			if (string.IsNullOrWhiteSpace(mementoFileName)) 
				throw new ArgumentNullException(nameof(mementoFileName));


			var jsonData = File.ReadAllText(mementoFileName);
			memento = JsonConvert.DeserializeObject<EngineMemento>(jsonData);
			if (memento == null)
				throw new ArgumentException("Cannot deserialize memento from file " + mementoFileName);

			return FromMemento(memento);
		}

		public static ICompareFluentInterface FromMemento(EngineMemento memento)
		{
			if (memento == null) throw new ArgumentNullException(nameof(memento));


			var validationResults = new List<ValidationResult>();
			if (!Validator.TryValidateObject(memento, new ValidationContext(memento), validationResults))
			{
				throw new ExtendedValidationException("memento", memento, validationResults);
			}


			var f = new FluentInterface();

			if (memento.Entities == null) return f;
			if (memento.Entities.Count == 0) return f;

			foreach (var entityMemento in memento.Entities)
			{
				IKeyProvider<Entity> keyProvider;
				if (entityMemento.KeyUseGuid)
				{
					keyProvider = AsKey.UseGuid;
				}
				else
				{
					keyProvider = AsKey.UseAttributes(entityMemento.KeyAttributeNames.ToArray());
				}

				if (entityMemento.AttributesToSkip?.Count > 0)
				{
					var skipAttributeCriteria = Skip.Attributes(entityMemento.AttributesToSkip.ToArray());
					f.Entity(entityMemento.EntityName, keyProvider, skipAttributeCriteria, entityMemento.OnlyActive);
				}
				else
				{
					f.Entity(entityMemento.EntityName, keyProvider, entityMemento.OnlyActive);
				}
			}

			return f;
		}


		private class FluentInterface : ICompareFluentInterface
		{
			private readonly List<EntityComparer> comparerList = new List<EntityComparer>();


			public ICompareFluentInterface Entity(string entityName, IKeyProvider<Entity> keyProvider)
			{
				this.comparerList.Add(new EntityComparer(entityName, keyProvider, true));
				return this;
			}

			public ICompareFluentInterface Entity(string entityName, IKeyProvider<Entity> keyProvider, bool onlyActiveRecords)
			{
				this.comparerList.Add(new EntityComparer(entityName, keyProvider, onlyActiveRecords));
				return this;
			}

			public ICompareFluentInterface Entity(string entityName, IKeyProvider<Entity> keyProvider, Action<QueryExpression> filterCriteria)
			{
				this.comparerList.Add(new EntityComparer(entityName, keyProvider, filterCriteria));
				return this;
			}

			public ICompareFluentInterface Entity(string entityName, IKeyProvider<Entity> keyProvider, ISkipAttributeCriteria skipAttributeCriteria)
			{
				this.comparerList.Add(new EntityComparer(entityName, keyProvider, null, skipAttributeCriteria));
				return this;
			}

			public ICompareFluentInterface Entity(string entityName, IKeyProvider<Entity> keyProvider, ISkipAttributeCriteria skipAttributeCriteria, bool onlyActiveRecords)
			{
				this.comparerList.Add(new EntityComparer(entityName, keyProvider, skipAttributeCriteria, onlyActiveRecords));
				return this;
			}

			public ICompareEngine GetEngine(IOrganizationService crm1, IOrganizationService crm2, ILog log)
			{
				return new CompareEngine(log, crm1, crm2, comparerList);
			}


			public ICompareFluentInterface ToMemento(out EngineMemento memento)
			{
				memento = new EngineMemento();
				foreach (var comparer in this.comparerList)
				{
					memento.Entities.Add(comparer.ToEntityMemento());
				}
				return this;
			}
		}
	}
}
