using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.RoleEditor.Views.Messages;
using Greg.Xrm.Views;
using System;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace Greg.Xrm.RoleEditor.Views.Comparer
{
	public class CompareRolesCommand : CommandBase<(Role, Role)>
	{
		protected override void ExecuteInternal((Role, Role) arg)
		{
			var a = arg.Item1;
			var b = arg.Item2;


			var messenger = a.ExecutionContext.Messenger;


			messenger.Send<Freeze>();
			messenger.Send(new WorkAsyncInfo
			{
				Message = $"Comparing roles: {Environment.NewLine}- {a}{Environment.NewLine}- {b}",
				Work = (worker, args) =>
				{
					if (a.Privileges.Count == 0)
					{
						a.ReadPrivileges();
					}
					if (b.Privileges.Count == 0)
					{
						b.ReadPrivileges();
					}


					var comparer = new RoleComparer();
					var result = comparer.CompareRoles(a, b);
					args.Result = result;
				},
				PostWorkCallBack = (args) =>
				{

					messenger.Send<Unfreeze>();
					var result = (RoleComparisonResult)args.Result;
					if (result.Count == 0)
					{
						MessageBox.Show("The two roles are identical", "Comparison result", MessageBoxButtons.OK, MessageBoxIcon.Information);
						return;
					}

					messenger.Send(result);
				}
			});
		}
	}
}
