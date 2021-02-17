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

		public OptionSetValue componenttype => this.Get<OptionSetValue>();

		public EntityReference solutionid => this.Get<EntityReference>();

		public Guid objectid => this.Get<Guid>();

		public bool ismetadata => this.Get<bool>();


		public override string ToString()
		{
			return $"{ComponentTypeName} {objectid}";
		}
	}
}
