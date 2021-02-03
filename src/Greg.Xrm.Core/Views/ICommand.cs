using System.ComponentModel;

namespace Greg.Xrm.Views
{
	/// <summary>
	/// Represents a generic command that can be performed by the UI
	/// </summary>
	public interface ICommand : INotifyPropertyChanged
	{
		/// <summary>
		/// Indicates whether the command fullfills the requirements to be executed
		/// </summary>
		bool CanExecute { get; }

		/// <summary>
		/// Executes the command
		/// </summary>
		/// <param name="arg">An argument that is passed to the command.</param>
		void Execute(object arg);
	}

	/// <summary>
	/// A <see cref="ICommand"/> that accepts arguments of a specific type
	/// </summary>
	/// <typeparam name="T">The type of argument accepted by the command</typeparam>
	public interface ICommand<T> : ICommand
	{
		/// <summary>
		/// Executes the command
		/// </summary>
		/// <param name="arg">An argument that is passed to the command.</param>
		void Execute(T arg);
	}
}
