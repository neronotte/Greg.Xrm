using Greg.Xrm.EnvironmentComparer.Model;
using Greg.Xrm.EnvironmentComparer.Model.Memento;

namespace Greg.Xrm.EnvironmentComparer
{
	public interface IEnvironmentComparerView
	{
		void CanOpenConfig(bool value);

		void SetConnectionNames(string env1name, string env2name);

		void ShowMemento(EngineMemento memento);

		void CanExecuteComparison(bool value);

		void ShowComparisonResult(CompareResult result);
		void CanLoadEntities(bool value);
	}
}
