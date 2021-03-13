using Greg.Xrm.Logging;

namespace Greg.Xrm.EnvironmentSolutionsComparer.Views.Solutions.ComponentResolution
{
	public class ResolverForRibbonCustomizations : ResolverCommon
	{
		public ResolverForRibbonCustomizations(ILog log)
		: base(log)
		{
			this.LabelExtractor = entity =>
			{
				var label = entity.GetAttributeValue<string>("entity");
				if (string.IsNullOrWhiteSpace(label))
				{
					label = "Application ribbon";
				}
				return label;
			};
		}

		public override SolutionComponentType ComponentType => SolutionComponentType.RibbonCustomization;

		public override string ComponentName => "ribboncustomization";

		public override string ComponentLabelField => "entity";
	}
}
