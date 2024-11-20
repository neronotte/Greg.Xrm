using Greg.Xrm.Core;
using Greg.Xrm.Core.Views;
using Greg.Xrm.Logging;
using Greg.Xrm.Messaging;
using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.UsageInspector
{
	public class UsageInspectorViewModel : ViewModel, INotificationProvider, IDisposable
	{
		private readonly ILog log;
		private readonly IScopedMessenger messenger;
		private readonly IDependencyRepository dependencyRepository;
		private readonly Role role;
		private bool disposedValue;

		public event EventHandler<NotificationEventArgs> Notify;

		public UsageInspectorViewModel(IDependencyRepository dependencyRepository, Role role)
        {
			this.dependencyRepository = dependencyRepository;
			this.role = role;
			this.messenger = role.ExecutionContext.Messenger.CreateScope();
			this.log = role.ExecutionContext.Log;
			this.IsEnabled = true;

			this.StartInspectionCommand = new RelayCommand(StartInspection, CanStartInspection);

			this.messenger.Register<Freeze>(m => this.IsEnabled = false);
			this.messenger.Register<Unfreeze>(m => this.IsEnabled = true);


			this.WhenChanges(() => IsEnabled).Refresh(StartInspectionCommand);
		}

		public bool IsEnabled
		{
			get => Get<bool>();
			private set => Set(value);
		}
		
		public string Output
		{
			get => Get<string>();
			private set => Set(value);
		}




		public RelayCommand StartInspectionCommand { get; }


		private bool CanStartInspection()
		{
			return this.IsEnabled;
		}

		private void StartInspection()
		{
			this.messenger.Send<Freeze>();
			this.Output = "Inspection started, please wait...";

			this.messenger.Send(new WorkAsyncInfo
			{
				Message = "Inspection started, please wait...",
				Work = (worker, args) =>
				{
					var crm = this.role.ExecutionContext;
					// Do the work here

					List<Entity> systemUserList, teamList;
					DependencyList otherDependencies;
					List<Guid> roleIdList;


					using(this.log.Track("Retrieving hierarchy of roles"))
					{
						roleIdList = ReadRoleRecursive(crm, this.role.Id);
					}


					using (this.log.Track($"Fetching active users with role {this.role.name}"))
					{
						var query = new QueryExpression("systemuser");
						query.ColumnSet.AddColumns("fullname", "domainname");
						query.Criteria.AddCondition("isdisabled", ConditionOperator.Equal, false);

						var link = query.AddLink("systemuserroles", "systemuserid", "systemuserid");
						var roleLink = link.AddLink("role", "roleid", "roleid");

						roleLink.LinkCriteria.FilterOperator = LogicalOperator.Or;
						roleLink.LinkCriteria.AddCondition("roleid", ConditionOperator.In, roleIdList.Cast<object>().ToArray());

						systemUserList = crm.RetrieveAll(query);
					}
					

					using(this.log.Track($"Fetching teams with role {this.role.name}"))
					{
						var query = new QueryExpression("team");
						query.ColumnSet.AddColumns("name");

						var link = query.AddLink("teamroles", "teamid", "teamid");
						var roleLink = link.AddLink("role", "roleid", "roleid");

						roleLink.LinkCriteria.FilterOperator = LogicalOperator.Or;
						roleLink.LinkCriteria.AddCondition("roleid", ConditionOperator.In, roleIdList.Cast<object>().ToArray());

						teamList = crm.RetrieveAll(query);
					}


					using(this.log.Track($"Retrieving other dependencies for role {this.role.name}"))
					{
						otherDependencies = this.dependencyRepository.GetRoleDependencies(crm, role);
					}



					var sb = new StringBuilder();

					var header = $"Role: {this.role.name}";
					var headerLen = Math.Min(header.Length + 2, 80);

					sb.Append("+").Append('-', headerLen).Append("+").AppendLine();
					sb.Append($"| ").Append(header.PadRight(headerLen-1)).AppendLine("|");
					sb.Append("+").Append('-', headerLen).Append("+").AppendLine();
					sb.AppendLine();

					if (systemUserList.Count == 0)
					{
						sb.AppendLine("No active users found with this role.");
						sb.AppendLine();
					}
					else
					{
						sb.Append("Found ").Append(systemUserList.Count).Append(" active user")
						.Append(systemUserList.Count > 1 ? "s" : "")
						.Append(" with this role:").AppendLine();

						foreach (var entity in systemUserList)
						{
							sb.Append(" - ")
								.Append(entity.GetAttributeValue<string>("fullname"))
								.Append(", ")
								.Append(entity.GetAttributeValue<string>("domainname"))
								.Append(" (")
								.Append(entity.Id)
								.AppendLine(")");
						}
						sb.AppendLine();
					}


					if (teamList.Count == 0)
					{
						sb.AppendLine("No teams found with this role.");
						sb.AppendLine();
					}
					else
					{
						sb.Append("Found ").Append(teamList.Count).Append(" team")
						.Append(teamList.Count > 1 ? "s" : "")
						.Append(" with this role:").AppendLine();

						foreach (var entity in teamList)
						{
							sb.Append(" - ")
								.Append(entity.GetAttributeValue<string>("name"))
								.Append(" (")
								.Append(entity.Id)
								.AppendLine(")");
						}
						sb.AppendLine();
					}




					if (otherDependencies.Count == 0)
					{
						sb.AppendLine("No other dependencies found for this role.");
						sb.AppendLine();
					}
					else
					{
						var dependencyGroups = otherDependencies.GroupBy(x => x.dependentcomponenttype.Value)
							.OrderBy(x => x.First().DependentComponentTypeFormatted)
							.ToArray();

						foreach (var dependencyGroup in dependencyGroups)
						{
							var componentTypeName = dependencyGroup.First().DependentComponentTypeFormatted;

							sb.Append(componentTypeName)
								.Append(" (typeCode: ")
								.Append(dependencyGroup.First().dependentcomponenttype.Value)
								.Append(", count: ")
								.Append(dependencyGroup.Count())
								.Append(")")
								.AppendLine();

							foreach (var dependency in dependencyGroup.OrderBy(x => x.dependentcomponentobjectid))
							{
								sb
									.Append(dependency.dependentcomponentobjectid)
									.Append(" | ")
									.Append(dependency.DependentComponentLabel);

								if (!string.IsNullOrWhiteSpace(dependency.DependencyTypeFormatted))
								{
									sb
										.Append(" (")
										.Append(dependency.DependencyTypeFormatted)
										.Append(")");
								}
								sb.AppendLine();
							}
							sb.AppendLine();
						}
					}


					args.Result = sb.ToString();
				},
				PostWorkCallBack = (args) =>
				{
					this.messenger.Send<Unfreeze>();
					if (args.Error != null)
					{
						this.Output = "Error during inspection: " + args.Error.Message;
						this.SendNotification(NotificationType.Error, args.Error.Message);
						return;
					}

					this.Output = args.Result?.ToString();
				},

			});
		}

		private List<Guid> ReadRoleRecursive(IXrmToolboxPluginContext crm, params Guid[] idList)
		{
			var query = new QueryExpression("role");

			if (idList.Length == 1)
			{
				query.Criteria.AddCondition("parentroleid", ConditionOperator.Equal, idList[0]);
			}
			else
			{
				query.Criteria.AddCondition("parentroleid", ConditionOperator.In, idList.Cast<object>().ToArray());
			}

			var nextLevelOfIds = crm.RetrieveMultiple(query).Entities.Select(x => x.Id).ToList();
			if (nextLevelOfIds.Count == 0)
			{
				return idList.ToList();
			}

			var recursiveIds = ReadRoleRecursive(crm, nextLevelOfIds.ToArray());
			return idList.Concat(recursiveIds).ToList();
		}

		public void SendNotification(NotificationType type, string message, int? timerInSeconds = null)
		{
			this.Notify?.Invoke(this, new NotificationEventArgs(type, message, timerInSeconds));
		}



		#region IDisposable Implementation
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					this.messenger.Dispose();
				}
				disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
