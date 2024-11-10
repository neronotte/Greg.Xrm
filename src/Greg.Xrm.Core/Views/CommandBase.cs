using Greg.Xrm.Model;
using System;

namespace Greg.Xrm.Views
{
	/// <summary>
	/// Base class for command implementations
	/// </summary>
	public abstract class CommandBase : ViewModel, ICommand
	{
		protected CommandBase()
		{
			this.CanExecute = true;
		}

		public virtual bool CanExecute
		{
			get => this.Get<bool>();
			protected set => this.Set(value);
		}


		public virtual void Execute(object arg)
		{
			ExecuteInternal(arg);
		}

		protected abstract void ExecuteInternal(object arg);


		public virtual void RefreshCanExecute()
		{
			this.OnPropertyChanged(nameof(CanExecute), CanExecute);
		}
	}




	/// <summary>
	/// Base class for generic commands
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class CommandBase<T> : ViewModel, ICommand<T>
	{
		protected CommandBase()
		{
			this.CanExecute = true;
		}

		public virtual bool CanExecute
		{
			get => this.Get<bool>();
			protected set => this.Set(value);
		}


		public virtual void Execute(object arg)
		{
			if (arg != null && !(arg is T))
			{
				throw new ArgumentException($"Invalid type for the given argument. Expected <{typeof(T).FullName}>, actual <{arg.GetType().FullName}>", nameof(arg));
			}

			ExecuteInternal((T)arg);
		}


		public virtual void Execute(T arg)
		{
			ExecuteInternal(arg);
		}


		protected abstract void ExecuteInternal(T arg);
	}
}
