using Greg.Xrm.RoleEditor.Model;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Greg.Xrm.RoleEditor.Views.Editor
{
	/// <summary>
	/// Provides functionality to create a memento string representation of a role's privilege configuration.
	/// </summary>
	public class RoleMementoProvider
	{
		private readonly RoleModel roleModel;

		public RoleMementoProvider(RoleModel roleModel)
		{
			this.roleModel = roleModel ?? throw new ArgumentNullException(nameof(roleModel));
		}

		/// <summary>
		/// Creates a memento string that represents the current privilege configuration.
		/// The string contains two sections separated by "---":
		/// 1. Table privileges: 8 privilege characters followed by pipe and table name (e.g., "12340000|account")
		/// 2. Miscellaneous privileges: Level character followed by pipe and privilege name (e.g., "4|prvCreateBusinessUnit")
		/// Values are encoded as: ' ' (no value), '0' (None), '1' (User), '2' (BusinessUnit), '3' (ParentChild), '4' (Organization)
		/// </summary>
		/// <returns>A memento string representing the role's privilege configuration</returns>
		public string CreateMemento()
		{
			var memento = new StringBuilder();

			// First section: Table privileges
			foreach (var tableGroup in roleModel.TableGroups.OrderBy(g => g.Name))
			{
				foreach (var table in tableGroup.OrderBy(t => t.Tooltip))
				{
					memento.AppendLine(CreateTableMementoRow(table));
				}
			}

			// Separator between sections
			memento.AppendLine("---");

			// Second section: Miscellaneous privileges
			foreach (var miscGroup in roleModel.MiscGroups.OrderBy(g => g.Name))
			{
				foreach (var misc in miscGroup.OrderBy(m => m.Tooltip))
				{
					memento.AppendLine(CreateMiscMementoRow(misc));
				}
			}

			return memento.ToString();
		}

		/// <summary>
		/// Parses and validates a memento string, then applies the privileges to the role model
		/// with merge logic (only applies if level is not null and greater than current value).
		/// </summary>
		/// <param name="mementoString">The memento string to parse and merge</param>
		/// <param name="overrideExisting">If true, applies memento values even when current values are greater</param>
		/// <returns>A MementoMergeResult containing detailed information about the merge operation</returns>
		public MementoMergeResult TryMergeMemento(string mementoString, bool overrideExisting = false)
		{
			if (string.IsNullOrWhiteSpace(mementoString))
				return MementoMergeResult.Failure("Memento string is null or empty.");

			var result = MementoMergeResult.Success();

			try
			{
				var lines = mementoString.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
				var separatorIndex = Array.IndexOf(lines, "---");

				if (separatorIndex == -1)
				{
					result.AddError("Invalid memento format: separator '---' not found.");
					return result;
				}

				// Parse table privileges section
				for (int i = 0; i < separatorIndex; i++)
				{
					var tableResult = TryParseAndApplyTableRow(lines[i], overrideExisting);
					if (!tableResult.Success)
					{
						result.AddError(string.Format("Error parsing table row {0}: {1}", i + 1, tableResult.ErrorMessage));
					}
					else if (tableResult.Applied)
					{
						result.TablePrivilegesApplied++;
					}
					else if (tableResult.Skipped)
					{
						result.TablePrivilegesSkipped++;
						if (!string.IsNullOrEmpty(tableResult.WarningMessage))
						{
							result.AddWarning(tableResult.WarningMessage);
						}
					}
				}

				// Parse miscellaneous privileges section
				for (int i = separatorIndex + 1; i < lines.Length; i++)
				{
					var miscResult = TryParseAndApplyMiscRow(lines[i], overrideExisting);
					if (!miscResult.Success)
					{
						result.AddError(string.Format("Error parsing misc row {0}: {1}", i - separatorIndex, miscResult.ErrorMessage));
					}
					else if (miscResult.Applied)
					{
						result.MiscPrivilegesApplied++;
					}
					else if (miscResult.Skipped)
					{
						result.MiscPrivilegesSkipped++;
						if (!string.IsNullOrEmpty(miscResult.WarningMessage))
						{
							result.AddWarning(miscResult.WarningMessage);
						}
					}
				}

				return result;
			}
			catch (System.Exception ex)
			{
				return MementoMergeResult.Failure(string.Format("Unexpected error during merge: {0}", ex.Message));
			}
		}

		/// <summary>
		/// Parses a single table privilege row from the memento and applies it to the role model.
		/// Required to handle the complex parsing of table names, privilege types, and validation.
		/// </summary>
		/// <param name="row">The table row in format "CCCCCCCC|tablename" where C represents privilege characters</param>
		/// <param name="overrideExisting">If true, applies values even when current values are greater</param>
		/// <returns>A RowParseResult indicating success, failure, or skip status</returns>
		private RoleMementoRowParseResult TryParseAndApplyTableRow(string row, bool overrideExisting)
		{
			if (string.IsNullOrWhiteSpace(row))
				return RoleMementoRowParseResult.CreateError("Row is null or empty.");

			var pipeIndex = row.IndexOf('|');
			if (pipeIndex == -1)
				return RoleMementoRowParseResult.CreateError("Invalid format: pipe separator '|' not found.");

			if (pipeIndex != 8)
				return RoleMementoRowParseResult.CreateError(string.Format("Invalid format: expected 8 privilege characters before pipe, found {0}.", pipeIndex));

			var privilegeChars = row.Substring(0, pipeIndex);
			var tableName = row.Substring(pipeIndex + 1);

			if (privilegeChars.Length != 8)
				return RoleMementoRowParseResult.CreateError(string.Format("Invalid privilege characters length: expected 8, got {0}.", privilegeChars.Length));

			if (string.IsNullOrWhiteSpace(tableName))
				return RoleMementoRowParseResult.CreateError("Table name is empty.");

			// Validate privilege characters
			for (int i = 0; i < privilegeChars.Length; i++)
			{
				var level = ConvertCharacterToLevel(privilegeChars[i]);
				if (privilegeChars[i] != ' ' && !level.HasValue)
				{
					return RoleMementoRowParseResult.CreateError(string.Format("Invalid privilege character '{0}' at position {1}.", privilegeChars[i], i + 1));
				}
			}

			// Find the table by tooltip (logical name)
			var table = FindTableByTooltip(tableName);
			if (table == null)
				return RoleMementoRowParseResult.CreateSkipped(string.Format("Unknown table '{0}' skipped.", tableName));

			var privilegeTypes = new[]
			{
				PrivilegeType.Create,
				PrivilegeType.Read,
				PrivilegeType.Write,
				PrivilegeType.Delete,
				PrivilegeType.Append,
				PrivilegeType.AppendTo,
				PrivilegeType.Assign,
				PrivilegeType.Share
			};

			bool anyApplied = false;
			for (int i = 0; i < privilegeTypes.Length && i < privilegeChars.Length; i++)
			{
				var level = ConvertCharacterToLevel(privilegeChars[i]);
				if (level.HasValue)
				{
					var applied = ApplyLevelToTable(table, privilegeTypes[i], level.Value, overrideExisting);
					if (applied)
						anyApplied = true;
				}
			}

			return RoleMementoRowParseResult.CreateSuccess(anyApplied);
		}

		/// <summary>
		/// Parses a single miscellaneous privilege row from the memento and applies it to the role model.
		/// Required to handle the parsing of privilege names and their associated levels.
		/// </summary>
		/// <param name="row">The misc row in format "C|privilegename" where C represents the privilege level character</param>
		/// <param name="overrideExisting">If true, applies values even when current values are greater</param>
		/// <returns>A RowParseResult indicating success, failure, or skip status</returns>
		private RoleMementoRowParseResult TryParseAndApplyMiscRow(string row, bool overrideExisting)
		{
			if (string.IsNullOrWhiteSpace(row))
				return RoleMementoRowParseResult.CreateError("Row is null or empty.");

			var pipeIndex = row.IndexOf('|');
			if (pipeIndex == -1)
				return RoleMementoRowParseResult.CreateError("Invalid format: pipe separator '|' not found.");

			if (pipeIndex != 1)
				return RoleMementoRowParseResult.CreateError(string.Format("Invalid format: expected 1 privilege character before pipe, found {0}.", pipeIndex));

			var levelChar = row.Substring(0, pipeIndex);
			var privilegeName = row.Substring(pipeIndex + 1);

			if (levelChar.Length != 1)
				return RoleMementoRowParseResult.CreateError(string.Format("Invalid level character length: expected 1, got {0}.", levelChar.Length));

			if (string.IsNullOrWhiteSpace(privilegeName))
				return RoleMementoRowParseResult.CreateError("Privilege name is empty.");

			var level = ConvertCharacterToLevel(levelChar[0]);
			if (levelChar[0] != ' ' && !level.HasValue)
				return RoleMementoRowParseResult.CreateError(string.Format("Invalid privilege character '{0}'.", levelChar[0]));

			if (!level.HasValue)
				return RoleMementoRowParseResult.CreateSuccess(false); // Skip null levels

			// Find the misc privilege by tooltip (privilege name)
			var misc = FindMiscByTooltip(privilegeName);
			if (misc == null)
				return RoleMementoRowParseResult.CreateSkipped(string.Format("Unknown privilege '{0}' skipped.", privilegeName));

			var applied = ApplyLevelToMisc(misc, level.Value, overrideExisting);
			return RoleMementoRowParseResult.CreateSuccess(applied);
		}

		/// <summary>
		/// Searches for a table model by its tooltip (logical name) across all table groups.
		/// Required because tables are organized in groups and we need to search across all of them
		/// to find the specific table referenced in the memento.
		/// </summary>
		/// <param name="tooltip">The logical name of the table to find</param>
		/// <returns>The TableModel if found, null otherwise</returns>
		private TableModel FindTableByTooltip(string tooltip)
		{
			foreach (var tableGroup in roleModel.TableGroups)
			{
				foreach (var table in tableGroup)
				{
					if (string.Equals(table.Tooltip, tooltip, StringComparison.OrdinalIgnoreCase))
						return table;
				}
			}
			return null;
		}

		/// <summary>
		/// Searches for a miscellaneous privilege model by its tooltip (privilege name) across all misc groups.
		/// Required because misc privileges are organized in groups and we need to search across all of them
		/// to find the specific privilege referenced in the memento.
		/// </summary>
		/// <param name="tooltip">The privilege name to find</param>
		/// <returns>The MiscModel if found, null otherwise</returns>
		private MiscModel FindMiscByTooltip(string tooltip)
		{
			foreach (var miscGroup in roleModel.MiscGroups)
			{
				foreach (var misc in miscGroup)
				{
					if (string.Equals(misc.Tooltip, tooltip, StringComparison.OrdinalIgnoreCase))
						return misc;
				}
			}
			return null;
		}

		/// <summary>
		/// Applies a privilege level to a specific table privilege type with merge logic.
		/// Required to encapsulate the comparison logic that determines whether to apply
		/// the new level based on current value and override settings.
		/// </summary>
		/// <param name="table">The table model to modify</param>
		/// <param name="privilegeType">The specific privilege type to update</param>
		/// <param name="newLevel">The new level to apply</param>
		/// <param name="overrideExisting">If true, applies the new level regardless of current value</param>
		/// <returns>True if the level was actually applied, false if it was skipped</returns>
		private bool ApplyLevelToTable(TableModel table, PrivilegeType privilegeType, Level newLevel, bool overrideExisting)
		{
			var currentLevel = table[privilegeType];
			
			// Apply if override is true, or if new level is greater than current level
			if (overrideExisting || !currentLevel.HasValue || newLevel > currentLevel.Value)
			{
				table[privilegeType] = newLevel;
				return true;
			}
			
			return false;
		}

		/// <summary>
		/// Applies a privilege level to a miscellaneous privilege with merge logic.
		/// Required to encapsulate the comparison logic that determines whether to apply
		/// the new level based on current value and override settings.
		/// </summary>
		/// <param name="misc">The miscellaneous privilege model to modify</param>
		/// <param name="newLevel">The new level to apply</param>
		/// <param name="overrideExisting">If true, applies the new level regardless of current value</param>
		/// <returns>True if the level was actually applied, false if it was skipped</returns>
		private bool ApplyLevelToMisc(MiscModel misc, Level newLevel, bool overrideExisting)
		{
			var currentLevel = misc.Value;
			
			// Apply if override is true, or if new level is greater than current level
			if (overrideExisting || newLevel > currentLevel)
			{
				misc.Value = newLevel;
				return true;
			}
			
			return false;
		}

		private static string CreateTableMementoRow(TableModel table)
		{
			var row = new StringBuilder();

			// Add the 8 privilege characters in the specified order: Create, Read, Write, Delete, Append, AppendTo, Assign, Share
			var privilegeTypes = new[]
			{
				PrivilegeType.Create,
				PrivilegeType.Read,
				PrivilegeType.Write,
				PrivilegeType.Delete,
				PrivilegeType.Append,
				PrivilegeType.AppendTo,
				PrivilegeType.Assign,
				PrivilegeType.Share
			};

			foreach (var privilegeType in privilegeTypes)
			{
				var level = table[privilegeType];
				row.Append(ConvertLevelToCharacter(level));
			}

			// Add pipe separator and tooltip
			row.Append('|');
			row.Append(table.Tooltip);

			return row.ToString();
		}

		private static string CreateMiscMementoRow(MiscModel misc)
		{
			return string.Format("{0}|{1}", ConvertLevelToCharacter(misc.Value), misc.Tooltip);
		}

		private static char ConvertLevelToCharacter(Level? level)
		{
			if (!level.HasValue)
				return ' ';

			if (level.Value == Level.None)
				return '0';
			if (level.Value == Level.User)
				return '1';
			if (level.Value == Level.BusinessUnit)
				return '2';
			if (level.Value == Level.ParentChild)
				return '3';
			if (level.Value == Level.Organization)
				return '4';
			
			return ' ';
		}

		/// <summary>
		/// Converts a character from the memento format back to a Level enum value.
		/// Required to translate the compact character representation used in mementos
		/// back to the strongly-typed Level enum used throughout the application.
		/// </summary>
		/// <param name="character">The character to convert (' ', '0', '1', '2', '3', '4')</param>
		/// <returns>The corresponding Level enum value, or null for space character</returns>
		private static Level? ConvertCharacterToLevel(char character)
		{
			if (character == ' ')
				return null;
			if (character == '0')
				return Level.None;
			if (character == '1')
				return Level.User;
			if (character == '2')
				return Level.BusinessUnit;
			if (character == '3')
				return Level.ParentChild;
			if (character == '4')
				return Level.Organization;
			
			return null;
		}
	}
}