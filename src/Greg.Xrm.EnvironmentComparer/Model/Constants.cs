using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Model
{
	public static class Constants
	{
		public static readonly IReadOnlyCollection<string> AttributesToIgnore = new[] {
			"ownerid",
			"createdby",
			"createdonbehalfby",
			"createdon",
			"modifiedby",
			"modifiedon",
			"owninguser",
			"owningbusinessunit",
			"importsequencenumber",
			"modifiedonbehalfby",
			"overriddencreatedon",
			"timezoneruleversionnumber",
			"utcconversiontimezonecode",
			"versionnumber"
		};
	}
}
