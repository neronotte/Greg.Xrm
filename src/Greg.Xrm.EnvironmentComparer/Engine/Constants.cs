﻿using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Engine
{
	public static class Constants
	{
		public static IReadOnlyCollection<string> AttributesToIgnore { get; } = new[] {
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
