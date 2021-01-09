using Greg.Xrm.EnvironmentComparer.Actions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentComparer.Views.Actions
{
	public class ApplyActionsResult : List<ApplyActionResult>
	{
		public int SucceededCount => this.Count(_ => _.Succeeded);
		public int ErrorsCount => this.Count(_ => !_.Succeeded);



		public void Add(IAction action, Exception exception = null)
		{
			this.Add(new ApplyActionResult(action, exception));
		}
	}
}
