using System;
using System.Collections.Generic;
using System.Text;

namespace Greg.Xrm.RoleEditor.Services
{
	/// <summary>
	/// Thrown when a set of requests executed one by one (without transaction) fails halfway.
	/// </summary>
	public class PartialSaveException : Exception
	{
		public PartialSaveException(IReadOnlyList<string> applied, string failed, IReadOnlyList<string> notExecuted, Exception innerException)
			: base($"{applied.Count} of {applied.Count + 1 + notExecuted.Count} operation(s) applied before {failed} failed: {innerException.Message}", innerException)
		{
			this.Applied = applied;
			this.Failed = failed;
			this.NotExecuted = notExecuted;
		}

		public IReadOnlyList<string> Applied { get; }

		public string Failed { get; }

		public IReadOnlyList<string> NotExecuted { get; }



		public string GetDetails()
		{
			var sb = new StringBuilder();
			sb.AppendLine("Save failed in fallback mode (no transaction), changes have been partially applied:");
			foreach (var item in this.Applied)
			{
				sb.Append("  [applied]      ").AppendLine(item);
			}
			sb.Append("  [failed]       ").AppendLine(this.Failed);
			foreach (var item in this.NotExecuted)
			{
				sb.Append("  [not executed] ").AppendLine(item);
			}
			return sb.ToString();
		}
	}
}
