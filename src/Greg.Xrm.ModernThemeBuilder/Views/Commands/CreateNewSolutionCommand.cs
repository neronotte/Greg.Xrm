using Greg.Xrm.Views;

namespace Greg.Xrm.ModernThemeBuilder.Views.Commands
{
	public class CreateNewSolutionCommand : CommandBase
	{
		private readonly MainViewModel viewModel;

		public CreateNewSolutionCommand(MainViewModel viewModel)
        {
			this.viewModel = viewModel;
		}

		protected override void ExecuteInternal(object arg)
		{
			// todo
		}
	}
}
