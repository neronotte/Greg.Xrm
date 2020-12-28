using XrmToolBox.Extensibility;

namespace Greg.Xrm.Async
{
	public interface IAsyncJobScheduler
	{
		void Enqueue(WorkAsyncInfo work);
	}
}
