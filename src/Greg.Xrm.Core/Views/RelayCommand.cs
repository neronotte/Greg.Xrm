using System;

namespace Greg.Xrm.Views
{
	public class RelayCommand : CommandBase
	{
		private readonly Action delegateCommand;
		private readonly Func<bool> canExecuteCommand;

		public RelayCommand(Action delegateCommand, Func<bool> canExecuteCommand = null)
        {
			this.delegateCommand = delegateCommand ?? throw new ArgumentNullException(nameof(delegateCommand));
			this.canExecuteCommand = canExecuteCommand ?? new Func<bool>(() => true);
			this.CanExecute = this.canExecuteCommand();
		}

		public override void RefreshCanExecute()
		{
			this.CanExecute = this.canExecuteCommand();
		}

		protected override void ExecuteInternal(object arg)
		{
			this.delegateCommand();
		}
	}
}
