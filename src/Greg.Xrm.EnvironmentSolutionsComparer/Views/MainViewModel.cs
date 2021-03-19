using Greg.Xrm.EnvironmentSolutionsComparer.Messaging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views
{
	public class MainViewModel : PluginViewModelBase
	{
		private readonly IMessenger messenger;

		public MainViewModel(IMessenger messenger)
		{
			this.EnvironmentList = new ObservableCollection<ConnectionModel>();
			this.messenger = messenger;

			this.AllowRequests = true;
			this.WhenChanges(() => AllowRequests).NotifyOthers(messenger);
		}


		public ObservableCollection<ConnectionModel> EnvironmentList { get; }
		

		public void AddDefaultEnvironment(ConnectionDetail connectionDetail, IOrganizationService crm)
		{
			// if there is a default environment. Remove it
			var oldDefaultEnvironment = this.EnvironmentList.FirstOrDefault(_ => _.IsDefault);
			if (oldDefaultEnvironment != null)
			{
				this.EnvironmentList.Remove(oldDefaultEnvironment);
				this.messenger.Send(new ConnectionRemovedMessage(oldDefaultEnvironment));
			}


			// if the current environment is still there, removeit
			var oldCurrentEnvironment = this.EnvironmentList.FirstOrDefault(_ => string.Equals( _.Detail.ConnectionId, connectionDetail.ConnectionId));
			if (oldCurrentEnvironment != null)
			{
				this.EnvironmentList.Remove(oldCurrentEnvironment);
				this.messenger.Send(new ConnectionRemovedMessage(oldCurrentEnvironment));
			}


			var model = new ConnectionModel(connectionDetail, true, crm);
			this.EnvironmentList.Insert(0, model);

			this.messenger.Send(new ConnectionAddedMessage(0, model));
		}



		public void AddEnvironment(ConnectionDetail connectionDetail, IOrganizationService crm = null)
		{
			var oldCurrentEnvironment = this.EnvironmentList.FirstOrDefault(_ => string.Equals(_.Detail.ConnectionId, connectionDetail.ConnectionId));
			if (oldCurrentEnvironment != null)
			{
				this.EnvironmentList.Remove(oldCurrentEnvironment);
				this.messenger.Send(new ConnectionRemovedMessage(oldCurrentEnvironment));
			}

			var model = new ConnectionModel(connectionDetail, false, crm);
			this.EnvironmentList.Add(model);

			this.messenger.Send(new ConnectionAddedMessage(this.EnvironmentList.IndexOf(model), model));
		}



		public void RemoveEnvironment(IList<ConnectionDetail> oldItems)
		{
			foreach (var oldItem in oldItems)
			{
				var model = this.EnvironmentList.FirstOrDefault(_ => _.Detail == oldItem);
				if (model != null)
				{
					this.EnvironmentList.Remove(model);
					this.messenger.Send(new ConnectionRemovedMessage(model));
				}
			}
		}
	}
}
