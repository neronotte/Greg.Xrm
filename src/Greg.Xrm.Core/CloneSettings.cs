using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm
{
	public static partial class Extensions
	{
		/// <summary>
		/// Settings that can be used to configure the clonation feature
		/// </summary>
		public static class CloneSettings
		{
			private static readonly List<string> ForbiddenAttributes = new List<string>();

			static CloneSettings()
			{
				ForbiddenAttributes.AddRange(new[]
				{
					"statecode",
					"statuscode",
					"ownerid",
					"owningbusinessunit",
					"owningteam",
					"owninguser",
					"createdon",
					"createdby",
					"modifiedon",
					"modifiedby"
				});
			}


			/// <summary>
			/// Indicates whether a property is forbidden or not for the clone.
			/// </summary>
			/// <param name="original">The entity that contains the property to clone</param>
			/// <param name="propertyName">The name of the property to clone</param>
			/// <param name="otherForbiddenAttributes">Forbidden attributes</param>
			/// <returns></returns>
			public static bool IsForbidden(Entity original, string propertyName, string[] otherForbiddenAttributes = null)
			{
				if (otherForbiddenAttributes == null) otherForbiddenAttributes = Array.Empty<string>();
				if (string.Equals(propertyName, original.LogicalName + "id", StringComparison.OrdinalIgnoreCase)) return true;
				if (ForbiddenAttributes.Any(x => string.Equals(x, propertyName, StringComparison.OrdinalIgnoreCase))) return true;
				if (otherForbiddenAttributes.Any(x => string.Equals(x, propertyName, StringComparison.OrdinalIgnoreCase))) return true;
				return false;
			}
		}
	}
}
