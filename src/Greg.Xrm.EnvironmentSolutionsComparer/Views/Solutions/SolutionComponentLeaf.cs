using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions
{
	public class SolutionComponentLeaf : SolutionComponentNode
	{
		private readonly SolutionComponent component;


		public SolutionComponentLeaf(SolutionComponentNode parent, SolutionComponent component)
			: base(component.objectid.ToString(), parent)
		{
			this.Environments = new Dictionary<ConnectionModel, SolutionComponent>();
			this.component = component;
			this.Label = component.Label;
		}

		public Guid ObjectId => component.objectid;

		public string ComponentTypeName => this.component.ComponentTypeName ?? this.component.SolutionComponentDefinitionName;

		public int ComponentType => this.component.componenttype?.Value ?? 0;


		public Dictionary<ConnectionModel, SolutionComponent> Environments { get; }




		public override string ToString()
		{
			return Label;
		}
	}



}
