using Greg.Xrm.EnvironmentComparer.Logging;
using Greg.Xrm.EnvironmentComparer.Model.Memento;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Greg.Xrm.EnvironmentComparer.Model
{
	public interface ICompareFluentInterface
	{
		ICompareFluentInterface Entity(string entityName, IKeyProvider<Entity> keyProvider);
		ICompareFluentInterface Entity(string entityName, IKeyProvider<Entity> keyProvider, bool onlyActiveRecords);
		ICompareFluentInterface Entity(string entityName, IKeyProvider<Entity> keyProvider, Action<QueryExpression> filterCriteria);
		ICompareFluentInterface Entity(string entityName, IKeyProvider<Entity> keyProvider, ISkipAttributeCriteria skipAttributeCriteria);
		ICompareFluentInterface Entity(string entityName, IKeyProvider<Entity> keyProvider, ISkipAttributeCriteria skipAttributeCriteria, bool onlyActiveRecords);
		ICompareFluentInterface ToMemento(out EngineMemento memento);
		ICompareEngine GetEngine(IOrganizationService crm1, IOrganizationService crm2, ILog log);		
	}
}
