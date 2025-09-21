using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	/// <summary>
	/// Represents the result of a memento merge operation with detailed validation information.
	/// </summary>
	public class MementoMergeResult
	{
		private readonly List<string> errors = new List<string>();
		private readonly List<string> warnings = new List<string>();

		/// <summary>
		/// Indicates whether the merge operation was successful (no errors).
		/// </summary>
		public bool IsSuccess => !errors.Any();

		/// <summary>
		/// Gets the list of validation errors that occurred during the merge.
		/// </summary>
		public IReadOnlyList<string> Errors => errors;

		/// <summary>
		/// Gets the list of warnings that occurred during the merge.
		/// </summary>
		public IReadOnlyList<string> Warnings => warnings;

		/// <summary>
		/// Gets the number of table privileges that were successfully applied.
		/// </summary>
		public int TablePrivilegesApplied { get; internal set; }

		/// <summary>
		/// Gets the number of miscellaneous privileges that were successfully applied.
		/// </summary>
		public int MiscPrivilegesApplied { get; internal set; }

		/// <summary>
		/// Gets the number of table privileges that were skipped (unknown tables).
		/// </summary>
		public int TablePrivilegesSkipped { get; internal set; }

		/// <summary>
		/// Gets the number of miscellaneous privileges that were skipped (unknown privileges).
		/// </summary>
		public int MiscPrivilegesSkipped { get; internal set; }

		/// <summary>
		/// Adds an error message to the result.
		/// </summary>
		/// <param name="message">The error message to add</param>
		internal void AddError(string message)
		{
			errors.Add(message);
		}

		/// <summary>
		/// Adds a warning message to the result.
		/// </summary>
		/// <param name="message">The warning message to add</param>
		internal void AddWarning(string message)
		{
			warnings.Add(message);
		}

		/// <summary>
		/// Creates a successful result with no errors.
		/// </summary>
		/// <returns>A successful MementoMergeResult</returns>
		public static MementoMergeResult Success()
		{
			return new MementoMergeResult();
		}

		/// <summary>
		/// Creates a failed result with the specified error message.
		/// </summary>
		/// <param name="errorMessage">The error message</param>
		/// <returns>A failed MementoMergeResult</returns>
		public static MementoMergeResult Failure(string errorMessage)
		{
			var result = new MementoMergeResult();
			result.AddError(errorMessage);
			return result;
		}

		/// <summary>
		/// Gets a summary message describing the merge operation result.
		/// </summary>
		/// <returns>A formatted summary message</returns>
		public string GetSummaryMessage()
		{
			if (!IsSuccess)
			{
				return string.Format("Merge failed with {0} error(s): {1}", 
					Errors.Count, 
					string.Join("; ", Errors));
			}

			var message = string.Format("Merge completed successfully. Applied {0} table privilege(s) and {1} miscellaneous privilege(s).", 
				TablePrivilegesApplied, 
				MiscPrivilegesApplied);

			if (TablePrivilegesSkipped > 0 || MiscPrivilegesSkipped > 0)
			{
				message += string.Format(" Skipped {0} unknown table(s) and {1} unknown privilege(s).", 
					TablePrivilegesSkipped, 
					MiscPrivilegesSkipped);
			}

			if (Warnings.Any())
			{
				message += string.Format(" {0} warning(s): {1}", 
					Warnings.Count, 
					string.Join("; ", Warnings));
			}

			return message;
		}
	}
}