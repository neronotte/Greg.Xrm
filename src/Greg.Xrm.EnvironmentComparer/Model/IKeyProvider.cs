namespace Greg.Xrm.EnvironmentComparer.Model
{
	public interface IKeyProvider<T>
	{
		string Name { get; }
		string GetKey(T item);
	}
}