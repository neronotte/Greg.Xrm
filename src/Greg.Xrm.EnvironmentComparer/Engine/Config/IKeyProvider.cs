namespace Greg.Xrm.EnvironmentComparer.Engine.Config
{
	public interface IKeyProvider<T>
	{
		string Name { get; }
		string GetKey(T item);
	}
}