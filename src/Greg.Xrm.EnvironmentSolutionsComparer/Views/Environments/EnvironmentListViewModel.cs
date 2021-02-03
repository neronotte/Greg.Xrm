using Greg.Xrm.EnvironmentSolutionsComparer.Messaging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Environments
{
	public class EnvironmentListViewModel : ViewModel
	{
		private readonly IMessenger messenger;

		public EnvironmentListViewModel(IMessenger messenger)
		{
			this.messenger = messenger;

			this.AllowRequests = true;

			this.messenger.WhenObject<MainViewModel>()
				.ChangesProperty(_ => _.AllowRequests)
				.Execute(m => this.AllowRequests = (bool)m.NewValue);

			this.WhenChanges(() => AllowRequests)
				.ChangesAlso(() => CanAddEnvironment)
				.ChangesAlso(() => CanRemoveEnvironment);

			this.WhenChanges(() => SelectedModel)
				.ChangesAlso(() => CanRemoveEnvironment);

			this.EnvironmentList = new ObservableCollection<ConnectionModel>();
			this.messenger.Register<ConnectionAddedMessage>(m =>
			{
				this.EnvironmentList.Insert(m.Index, m.Model);
			});
			this.messenger.Register<ConnectionRemovedMessage>(m =>
			{
				this.EnvironmentList.Remove(m.Model);
			});
		}



		public ObservableCollection<ConnectionModel> EnvironmentList { get; }



		public bool AllowRequests
		{
			get => Get<bool>();
			private set => Set<bool>(value);
		}



		public bool CanAddEnvironment
		{
			get => this.AllowRequests;
		}

		public bool CanRemoveEnvironment
		{
			get => this.AllowRequests && this.SelectedModel != null && !this.SelectedModel.IsDefault;
		}


		public ConnectionModel SelectedModel
		{
			get => Get<ConnectionModel>();
			set => Set(value);
		}
	}
}
