using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;
using System;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class SolutionComponent : EntityWrapper
	{
		public SolutionComponent(Entity entity) : base(entity)
		{
			//"componenttype", "rootsolutioncomponentid", "solutionid", "objectid"
		}

		public string ComponentTypeName => this.GetFormatted(nameof(componenttype));

#pragma warning disable IDE1006 // Naming Styles
		public OptionSetValue componenttype => this.Get<OptionSetValue>();

		public EntityReference solutionid => this.Get<EntityReference>();

		public Guid objectid => this.Get<Guid>();

		public bool ismetadata => this.Get<bool>();
#pragma warning restore IDE1006 // Naming Styles


		public override string ToString()
		{
			return $"{ComponentTypeName} {objectid}";
		}
	}
}
