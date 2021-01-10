using Greg.Xrm.EnvironmentComparer.Engine.Config;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Engine
{
	public class EntityEqualityComparer : IExtendedEqualityComparer<Entity>
	{
		private readonly ISkipAttributeCriteria skipAttributeCriteria;

		public EntityEqualityComparer(ISkipAttributeCriteria skipAttributeCriteria = null)
		{
			this.skipAttributeCriteria = skipAttributeCriteria ?? Skip.Nothing;
		}


		public bool Equals(Entity x, Entity y, out List<Difference> differentProperties)
		{
			differentProperties = new List<Difference>();

			if (x == null && y == null) return true;
			if (x == null) return false;
			if (y == null) return false;
			if (!x.LogicalName.Equals(y.LogicalName)) return false;

			var keyPropertyName = x.LogicalName + "id";




			foreach (var attributeName in x.Attributes.Keys)
			{
				if (attributeName.Equals(keyPropertyName, System.StringComparison.OrdinalIgnoreCase))
					continue;
				if (this.skipAttributeCriteria.ShouldSkip(attributeName))
					continue;

				if (!y.Contains(attributeName))
				{
					differentProperties.Add(Difference.Create(attributeName, x, null));
					continue;
				}


				var xValue = x[attributeName];
				var yValue = y[attributeName];

				if (!CrmComparer.AreEqualCrmObjects(xValue, yValue))
				{
					differentProperties.Add(Difference.Create(attributeName, x, y));
					continue;
				}
			}
			foreach (var attributeName in y.Attributes.Keys)
			{
				if (attributeName.Equals(keyPropertyName, System.StringComparison.OrdinalIgnoreCase))
					continue;
				if (this.skipAttributeCriteria.ShouldSkip(attributeName))
					continue;

				if (!x.Contains(attributeName))
				{
					differentProperties.Add(Difference.Create(attributeName, null, y));
					continue;
				}
			}


			return differentProperties.Count == 0;
		}
	}
}
