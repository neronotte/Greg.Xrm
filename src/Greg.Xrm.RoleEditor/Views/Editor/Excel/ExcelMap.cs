using Greg.Xrm.RoleEditor.Model;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Views.Editor.Excel
{
	public class ExcelMap
	{
		private readonly object syncRoot = new object();
		private readonly Dictionary<string, Dictionary<PrivilegeType, Level>> tableMap = new Dictionary<string, Dictionary<PrivilegeType, Level>>();
		private readonly Dictionary<string, Level> miscMap = new Dictionary<string, Level>();

		/// <summary>
		/// Table related privileges
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="type"></param>
		/// <param name="value"></param>
		public void Add(string tableName, PrivilegeType type, int? value)
		{
			if (value == null) return;
			if (value < 0 || value > 4) 
				throw new System.ArgumentOutOfRangeException(nameof(value), $"Value of privilege {type} for table {tableName} must be between 0 and 4.");


			lock (this.syncRoot)
			{
				if (!this.tableMap.ContainsKey(tableName))
					this.tableMap[tableName] = new Dictionary<PrivilegeType, Level>();

				this.tableMap[tableName][type] = (Level)value.Value;
			}
		}


		/// <summary>
		/// Miscellaneous privilege
		/// </summary>
		/// <param name="privilegeName"></param>
		/// <param name="value"></param>
		public void Add(string privilegeName, int? value)
		{
			if (value == null) return;
			if (value < 0 || value > 4)
				throw new System.ArgumentOutOfRangeException(nameof(value), $"Value of privilege {privilegeName} must be between 0 and 4.");

			lock(this.syncRoot)
			{
				this.miscMap[privilegeName] = (Level)value.Value;
			}
		}

		/// <summary>
		/// Indicates if the map contains anything to do
		/// </summary>
		public bool HasSomethingToDo => this.tableMap.Count > 0 || this.miscMap.Count > 0;




		public void ApplyTo(RoleModel m)
		{
			var modelTableDict = m.TableGroups.SelectMany(x => x).ToDictionary(g => g.GetTableNameForExcelFile());

			foreach (var tableDef in this.tableMap)
			{
				var tableName = tableDef.Key;
				var privilegeDict = tableDef.Value;

				if (modelTableDict.TryGetValue(tableName, out var table))
				{
					foreach (var privilege in privilegeDict)
					{
						table[privilege.Key] = privilege.Value;
					}
				}
			}

			var modelMiscDict = m.MiscGroups.SelectMany(x => x).ToDictionary(g => g.Tooltip);

			foreach (var miscDef in this.miscMap)
			{
				var privilegeName = miscDef.Key;
				var level = miscDef.Value;

				if (modelMiscDict.TryGetValue(privilegeName, out var misc))
				{
					misc.Value = level;
				}
			}
		}
	}
}
