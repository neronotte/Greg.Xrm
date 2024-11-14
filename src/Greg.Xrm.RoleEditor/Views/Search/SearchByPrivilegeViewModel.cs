using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Model;
using Greg.Xrm.Views;
using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.RoleEditor.Views.Search
{
	public class SearchByPrivilegeViewModel : ViewModel
	{
		private readonly Dictionary<string, object> privilegeByNameDict = new Dictionary<string, object>();


		public SearchByPrivilegeViewModel(
			DataverseEnvironment environment,
			IRoleRepository roleRepository)
		{
			var template = environment.Template;

			var tablePrivileges = template.Tables.Values.SelectMany(x => x.GetAllPrivileges());
			var miscPrivileges = template.Misc.Values.ToList();


			foreach (var privilege in tablePrivileges)
			{
				this.privilegeByNameDict[privilege.Name] = privilege;
			}
			foreach (var privilege in miscPrivileges)
			{
				this.privilegeByNameDict[privilege.PrivilegeName] = privilege;
			}

			var tables = template.Tables.Select(x => new TableDto(x)).OrderBy(x => x.Key).ToList();
			tables.Insert(0, new TableDto("(select a table)", null));
			this.Tables = tables;
			this.Misc = miscPrivileges;
			this.PrivilegeNames = this.privilegeByNameDict.Keys.ToArray();
			this.IsSearchByName = true;
			this.IsTablePrivilegeTypesEnabled = false;

			this.SearchCommand = new SearchByPrivilegeCommand(this, environment, roleRepository);

			this.WhenChanges(() => IsSearchByName)
				.ChangesAlso(() => IsSearchByLabel)
				.Execute(_ =>
				{
					if (this.IsSearchByName)
					{
						this.SelectedTable = null;
					}
					else
					{
						this.SelectedPrivilegeName = null;
					}
				});

			this.WhenChanges(() => SelectedTable)
				.Execute(_ =>
				{
					if (this.SelectedTable != null && this.SelectedTable.Value != null)
					{
						var privileges = this.SelectedTable.Value.GetAllPrivileges()
							.Select(x => x.PrivilegeType)
							.Where(x => x != Microsoft.Xrm.Sdk.Metadata.PrivilegeType.None)
							.OrderBy(x => x)
							.ToArray();

						this.TablePrivilegeTypes = privileges;
					}
					else
					{
						this.TablePrivilegeTypes = new PrivilegeType[0];
					}
					this.IsTablePrivilegeTypesEnabled = this.TablePrivilegeTypes.Count > 0;
				});

			this.WhenChanges(() => SelectedPrivilegeType)
				.Execute(_ =>
				{
					if (this.SelectedPrivilegeType != null)
					{
						this.SelecteMiscPrivilege = null;
					}
				});

			this.WhenChanges(() => SelecteMiscPrivilege)
				.Execute(_ =>
				{
						if (this.SelecteMiscPrivilege != null)
					{
						this.SelectedTable = null;
					}
				});
		}


		public string[] PrivilegeNames { get; }

		public bool IsSearchByName
		{
			get => this.Get<bool>();
			set => this.Set(value);
		}

		public bool IsSearchByLabel => !this.IsSearchByName;

		public string SelectedPrivilegeName
		{
			get => this.Get<string>();
			set => this.Set(value);
		}




		public IReadOnlyList<TableDto> Tables { get; }
		public IReadOnlyList<TemplateForGenericPrivilege> Misc { get; }



		public TableDto SelectedTable
		{
			get => this.Get<TableDto>();
			set => this.Set(value);
		}

		public bool IsTablePrivilegeTypesEnabled
		{
			get => this.Get<bool>();
			private set => this.Set(value);
		}

		public IReadOnlyList<PrivilegeType> TablePrivilegeTypes
		{
			get => this.Get<IReadOnlyList<PrivilegeType>>();
			private set => this.Set(value);
		}

		public PrivilegeType? SelectedPrivilegeType
		{
			get => this.Get<PrivilegeType?>();
			set => this.Set(value);
		}

		public TemplateForGenericPrivilege SelecteMiscPrivilege
		{
			get => this.Get<TemplateForGenericPrivilege>();
			set => this.Set(value);
		}

		public class TableDto
		{
			public TableDto(KeyValuePair<string, ITemplateForTable> pair)
				: this(pair.Key, pair.Value)
			{
			}


			public TableDto(string key, ITemplateForTable value)
			{
				Key = key;
				Value = value;
			}


			public string Key { get; }

			public ITemplateForTable Value { get; }
		}


		public ICommand SearchCommand { get; }
	}
}
