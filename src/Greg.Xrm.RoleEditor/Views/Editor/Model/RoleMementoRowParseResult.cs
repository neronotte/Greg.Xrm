namespace Greg.Xrm.RoleEditor.Views.Editor
{
	/// <summary>
	/// Represents the result of parsing a single row in a memento.
	/// </summary>
	internal class RoleMementoRowParseResult
	{
		public bool Success { get; private set; }
		public bool Applied { get; private set; }
		public bool Skipped { get; private set; }
		public string ErrorMessage { get; private set; }
		public string WarningMessage { get; private set; }

		public static RoleMementoRowParseResult CreateSuccess(bool applied = true)
		{
			return new RoleMementoRowParseResult { Success = true, Applied = applied };
		}

		public static RoleMementoRowParseResult CreateSkipped(string warningMessage = null)
		{
			return new RoleMementoRowParseResult { Success = true, Skipped = true, WarningMessage = warningMessage };
		}

		public static RoleMementoRowParseResult CreateError(string errorMessage)
		{
			return new RoleMementoRowParseResult { Success = false, ErrorMessage = errorMessage };
		}
	}
}