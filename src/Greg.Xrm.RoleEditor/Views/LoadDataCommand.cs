using Greg.Xrm.Core;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Services;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using System.Linq;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views
{
	public class LoadDataCommand : CommandBase
	{
		private readonly RoleTemplateBuilder roleTemplateBuilder;
		private readonly IRoleRepository roleRepository;
		private readonly IBusinessUnitRepository businessUnitRepository;
		private readonly ISystemUserRepository systemUserRepository;

		public LoadDataCommand(
			RoleTemplateBuilder roleTemplateBuilder, 
			IRoleRepository roleRepository, 
			IBusinessUnitRepository businessUnitRepository,
			ISystemUserRepository systemUserRepository)
        {
			this.roleTemplateBuilder = roleTemplateBuilder;
			this.roleRepository = roleRepository;
			this.businessUnitRepository = businessUnitRepository;
			this.systemUserRepository = systemUserRepository;

			this.WhenChanges(() => Context)
				.Execute(_ => this.CanExecute = this.Context != null);
		}


		public IXrmToolboxPluginContext Context
		{
			get => base.Get<IXrmToolboxPluginContext>();
			set => base.Set(value);
		}





        protected override void ExecuteInternal(object arg)
		{
			if (this.Context == null) return;

			var messenger = this.Context.Messenger;
			var log = this.Context.Log;


			messenger.Send(new WorkAsyncInfo
			{
				Message = "Loading tables and privileges...",
				Work = (worker, args) =>
				{
					messenger.Send<Freeze>();

					var template = this.roleTemplateBuilder.CreateTemplate(this.Context);


					var (businessUnit, buDict) = this.businessUnitRepository.GetTree(this.Context);




					using (log.Track("Retrieving roles..."))
					{
						var roleList = this.roleRepository.GetParentRoles(Context, template);
						log.Info($"Found {roleList.Count} roles");

						roleList.GroupBy(x => x.businessunitid.Id).ToList().ForEach(x => buDict[x.Key].AddRoles(x));
					}

					using(log.Track("Retrieving users..."))
					{
						var userList = this.systemUserRepository.GetActiveUsers(this.Context, template);
						log.Info($"Found {userList.Count} users");

						userList.GroupBy(x => x.businessunitid.Id).ToList().ForEach(x => buDict[x.Key].AddUsers(x));
					}
					


					var environment = new DataverseEnvironment(this.Context, template)
					{
						businessUnit
					};

					args.Result = environment;
				},
				PostWorkCallBack = e =>
				{
					messenger.Send<Unfreeze>();

					// do some work here
					if (!(e.Result is DataverseEnvironment result)) return;

					var roleList = result.GetAllRoles();

					messenger.Send(new RoleListLoaded { RoleList = roleList, Environment = result });
				}
			});
		}
	}
}
