using Greg.Xrm.Core.Views.Help;
using Greg.Xrm.EnvironmentComparer.Engine;
using Greg.Xrm.EnvironmentComparer.Engine.Memento;
using Greg.Xrm.EnvironmentComparer.Help;
using Greg.Xrm.Messaging;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Greg.Xrm.EnvironmentComparer.Views.Configurator
{
	public partial class ConfiguratorDialog2 : Form
	{
		private readonly IMessenger messenger;
		private readonly IReadOnlyCollection<EntityMetadata> entityMetadataList;
		private readonly EntityMetadataDto[] dtoList;
		private readonly Microsoft.Xrm.Sdk.IOrganizationService crm1;
		private readonly Microsoft.Xrm.Sdk.IOrganizationService crm2;
		private List<LookupMappingMemento> lookupMappings = new List<LookupMappingMemento>();
		private bool loadingMemento;
		private string fetchXml;

		public ConfiguratorDialog2(
			IMessenger messenger,
			IReadOnlyCollection<EntityMetadata> entityMetadataList,
			Microsoft.Xrm.Sdk.IOrganizationService crm1,
			Microsoft.Xrm.Sdk.IOrganizationService crm2)
		{
			InitializeComponent();

			this.messenger = messenger;
			this.RegisterHelp(messenger, Topics.ConfiguratorDialog2);

			this.Memento = null;

			this.crm1 = crm1;
			this.crm2 = crm2;
			this.entityMetadataList = entityMetadataList ?? Array.Empty<EntityMetadata>();
			this.dtoList = this.entityMetadataList
				.Select(x => new EntityMetadataDto(x))
				.OrderBy(x => x.Name)
				.ToArray();

			this.lstEntities.Items.AddRange(dtoList);
			this.txtEntitySearch.AutoCompleteCustomSource.AddRange(dtoList.Select(x => x.Name).ToArray());

			this.rUseAttributes.CheckedChanged += (s, e) =>
			{
				this.rUseGuid.Checked = !this.rUseAttributes.Checked;
				this.chlKey.Enabled = this.rUseAttributes.Checked;
			};
			this.rUseGuid.CheckedChanged += (s, e) => { this.rUseAttributes.Checked = !this.rUseGuid.Checked; };
		}


		private EntityMemento memento;
		public EntityMemento Memento
		{
			get => this.memento;
			set
			{
				this.memento = (EntityMemento)value?.Clone() ?? new EntityMemento();
				this.OnMementoChanged();
			}
		}

		private void OnEntitySearchKeyUp(object sender, KeyEventArgs e)
		{
			var text = this.txtEntitySearch.Text;
			var itemsToShow = this.dtoList.Where(x => x.Name.StartsWith(text, StringComparison.OrdinalIgnoreCase)).ToArray();

			this.lstEntities.BeginUpdate();

			this.lstEntities.Items.Clear();
			this.lstEntities.Items.AddRange(itemsToShow);

			this.lstEntities.EndUpdate();

			if (lstEntities.Items.Count == 1)
			{
				lstEntities.SelectedIndex = 0;
			}
		}

		private void OnEntitySelectionChanged(object sender, EventArgs e)
		{
			if (!this.loadingMemento)
			{
				this.lookupMappings.Clear();
				UpdateLookupMappingsLabel();
			}

			this.chlKey.BeginUpdate();
			this.chlKey.Items.Clear();
			this.chlSkip.BeginUpdate();
			this.chlSkip.Items.Clear();

			if (lstEntities.SelectedItem is EntityMetadataDto dto)
			{
				var attributeList = dto
					.Where(a => !string.Equals(a.Name, dto.PrimaryIdAttribute, StringComparison.OrdinalIgnoreCase))
					.Cast<object>()
					.ToArray();

				this.chlKey.Items.AddRange(attributeList);
				this.chlSkip.Items.AddRange(attributeList);
			}

			this.chlKey.EndUpdate();
			this.chlSkip.EndUpdate();
		}




		private void OnCancelClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			Close();
		}



		private void OnMementoChanged()
		{
			this.loadingMemento = true;
			if (!string.IsNullOrWhiteSpace(memento.EntityName))
			{
				var entityMetadata = this.dtoList.FirstOrDefault(x => string.Equals(x.Name, memento.EntityName, StringComparison.OrdinalIgnoreCase));
				if (entityMetadata != null)
				{
					this.txtEntitySearch.Text = entityMetadata.Name;
					this.lstEntities.SelectedIndex = this.lstEntities.Items.IndexOf(entityMetadata);
				}
			}

			this.rUseGuid.Checked = memento.KeyUseGuid;
			this.rUseAttributes.Checked = !memento.KeyUseGuid;
			this.chlKey.Enabled = this.rUseAttributes.Checked;

			if (memento.KeyAttributeNames.Count > 0)
			{
				foreach (var key in this.chlKey.Items.Cast<AttributeMetadataDto>().ToArray())
				{
					if (memento.KeyAttributeNames.Contains(key.Name))
					{
						var index = this.chlKey.Items.IndexOf(key);
						this.chlKey.SetItemChecked(index, true);
					}
				}
			}

			if (memento.AttributesToSkip.Count > 0)
			{
				foreach (var key in this.chlSkip.Items.Cast<AttributeMetadataDto>().ToArray())
				{
					if (memento.AttributesToSkip.Contains(key.Name))
					{
						var index = this.chlSkip.Items.IndexOf(key);
						this.chlSkip.SetItemChecked(index, true);
					}
				}
			}


			this.chkOnlyActive.Checked = memento.OnlyActive;
			this.SetFetchXml(memento.FetchXml);
			this.lookupMappings = memento.LookupMappings?
				.Select(x => (LookupMappingMemento)x.Clone())
				.ToList() ?? new List<LookupMappingMemento>();
			this.loadingMemento = false;
			UpdateLookupMappingsLabel();
		}


		private void SetFetchXml(string value)
		{
			this.fetchXml = string.IsNullOrWhiteSpace(value) ? null : value;
			this.lblFilter.Text = this.fetchXml == null
				? "No filter: compare all records"
				: "Records filtered by FetchXML";
		}

		private void OnFilterClick(object sender, EventArgs e)
		{
			this.errorProvider.Clear();

			if (!(lstEntities.SelectedItem is EntityMetadataDto dto))
			{
				this.errorProvider.SetError(lstEntities, "Please select an entity before defining the filter");
				return;
			}

			using (var dialog = new FetchXmlFilterDialog(this.messenger, dto.Name, this.fetchXml))
			{
				if (dialog.ShowDialog(this) != DialogResult.OK) return;

				this.SetFetchXml(dialog.FetchXml);
			}
		}



		private void OnOKClick(object sender, EventArgs e)
		{
			this.errorProvider.Clear();

			if (!(lstEntities.SelectedItem is EntityMetadataDto dto))
			{
				this.errorProvider.SetError(lstEntities, "Please select an entity");
				return;
			}

			var useGuid = this.rUseGuid.Checked;
			var keyAttributeNames = Array.Empty<string>();
			if (!useGuid)
			{
				var keyAttributes = this.chlKey.CheckedItems.Cast<AttributeMetadataDto>().ToList();
				if (keyAttributes.Count == 0)
				{
					this.errorProvider.SetError(this.chlKey, "Please select the key attributes");
					return;
				}
				keyAttributeNames = keyAttributes.Select(x => x.Name).ToArray();
			}

			var skipAttributeNames = this.chlSkip.CheckedItems
				.Cast<AttributeMetadataDto>()
				.Select(x => x.Name)
				.ToArray();


			var intersection = keyAttributeNames.Intersect(skipAttributeNames).Join(", ");
			if (!string.IsNullOrWhiteSpace(intersection))
			{
				this.errorProvider.SetError(this.chlSkip, $"You cannot skip key attributes ({intersection})!");
				return;
			}

			var mappedIgnoredAttributes = this.lookupMappings
				.Select(x => x.LookupAttributeName)
				.Intersect(skipAttributeNames, StringComparer.OrdinalIgnoreCase)
				.Join(", ");
			if (!string.IsNullOrWhiteSpace(mappedIgnoredAttributes))
			{
				this.errorProvider.SetError(this.chlSkip, $"Lookup mappings cannot be configured for ignored attributes ({mappedIgnoredAttributes})!");
				return;
			}

			var onlyActive = this.chkOnlyActive.Checked;

			var fetchXmlErrors = FetchXmlFilter.Validate(this.fetchXml, dto.Name);
			if (fetchXmlErrors.Count > 0)
			{
				this.errorProvider.SetError(this.lblFilter, fetchXmlErrors.Join(Environment.NewLine));
				return;
			}

			this.Memento.EntityName = dto.Name;
			this.Memento.IsManyToMany = dto.IsManyToMany;
			this.Memento.KeyUseGuid = useGuid;
			this.Memento.OnlyActive = onlyActive;
			this.Memento.FetchXml = this.fetchXml;
			this.Memento.KeyAttributeNames.Clear();
			this.Memento.KeyAttributeNames.AddRange(keyAttributeNames);
			this.Memento.AttributesToSkip.Clear();
			this.Memento.AttributesToSkip.AddRange(skipAttributeNames);
			this.Memento.LookupMappings.Clear();
			this.Memento.LookupMappings.AddRange(this.lookupMappings.Select(x => (LookupMappingMemento)x.Clone()));

			this.DialogResult = DialogResult.OK;
			Close();
		}

		private void OnLookupMappingsClick(object sender, EventArgs e)
		{
			this.errorProvider.Clear();
			if (!(this.lstEntities.SelectedItem is EntityMetadataDto dto))
			{
				this.errorProvider.SetError(this.lstEntities, "Please select an entity before configuring lookup mappings");
				return;
			}
			if (!dto.Any(x => x.Targets.Count > 0))
			{
				this.errorProvider.SetError(this.bLookupMappings, "The selected table has no lookup fields");
				return;
			}
			if (this.crm1 == null || this.crm2 == null)
			{
				this.errorProvider.SetError(this.bLookupMappings, "Connect to both environments before configuring lookup mappings");
				return;
			}

			using (var dialog = new LookupMappingsDialog(dto, this.crm1, this.crm2, this.lookupMappings))
			{
				if (dialog.ShowDialog(this) != DialogResult.OK) return;
				this.lookupMappings = dialog.Mappings;
				UpdateLookupMappingsLabel();
			}
		}

		private void UpdateLookupMappingsLabel()
		{
			this.bLookupMappings.Text = this.lookupMappings.Count == 0
				? "Lookup mappings..."
				: $"Lookup mappings... ({this.lookupMappings.Count})";
		}

		private void OnKeySelectAll(object sender, EventArgs e)
		{
			var sb = new StringBuilder();
			sb.AppendLine("Selecting all attributes as a key means that a record is or not in a given environment.");
			sb.AppendLine("You won't have any partial match. Are you sure?");

			if (MessageBox.Show(sb.ToString(), "Select all as key", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				return;

			for (var i = 0; i < this.chlKey.Items.Count; i++)
				this.chlKey.SetItemChecked(i, true);
		}

		private void OnKeyUnselectAll(object sender, EventArgs e)
		{
			for (var i = 0; i < this.chlKey.Items.Count; i++)
				this.chlKey.SetItemChecked(i, false);
		}

		private void OnIgnoreSelectAll(object sender, EventArgs e)
		{
			var sb = new StringBuilder();
			sb.AppendLine("Marking all attributes \"to ignore\" means that you will match only record keys.");
			sb.AppendLine("Are you sure?");

			if (MessageBox.Show(sb.ToString(), "Select all to ignore", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				return;

			for (var i = 0; i < this.chlSkip.Items.Count; i++)
				this.chlSkip.SetItemChecked(i, true);
		}

		private void OnIgnoreUnselectAll(object sender, EventArgs e)
		{
			for (var i = 0; i < this.chlSkip.Items.Count; i++)
				this.chlSkip.SetItemChecked(i, false);
		}
	}

	public class LookupMappingsDialog : Form
	{
		private readonly EntityMetadataDto entity;
		private readonly Microsoft.Xrm.Sdk.IOrganizationService crm1;
		private readonly Microsoft.Xrm.Sdk.IOrganizationService crm2;
		private readonly IReadOnlyCollection<LookupMappingMemento> initialMappings;
		private readonly DataGridView grid;
		private readonly ErrorProvider errorProvider;

		public LookupMappingsDialog(
			EntityMetadataDto entity,
			Microsoft.Xrm.Sdk.IOrganizationService crm1,
			Microsoft.Xrm.Sdk.IOrganizationService crm2,
			IReadOnlyCollection<LookupMappingMemento> initialMappings)
		{
			this.entity = entity ?? throw new ArgumentNullException(nameof(entity));
			this.crm1 = crm1 ?? throw new ArgumentNullException(nameof(crm1));
			this.crm2 = crm2 ?? throw new ArgumentNullException(nameof(crm2));
			this.initialMappings = initialMappings ?? Array.Empty<LookupMappingMemento>();

			Text = "Lookup mappings - " + entity.Name;
			StartPosition = FormStartPosition.CenterParent;
			MinimizeBox = false;
			MaximizeBox = false;
			ShowIcon = false;
			ShowInTaskbar = false;
			Size = new System.Drawing.Size(760, 480);
			MinimumSize = new System.Drawing.Size(620, 360);

			this.errorProvider = new ErrorProvider { ContainerControl = this };
			var description = new Label
			{
				AutoSize = true,
				Location = new System.Drawing.Point(12, 12),
				Text = "Choose a field whose value identifies the referenced record in both environments.",
			};
			this.grid = new DataGridView
			{
				Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
				Location = new System.Drawing.Point(12, 42),
				Size = new System.Drawing.Size(720, 350),
				AllowUserToAddRows = false,
				AllowUserToDeleteRows = false,
				AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
				RowHeadersVisible = false,
				SelectionMode = DataGridViewSelectionMode.CellSelect,
			};
			grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Lookup field", ReadOnly = true, FillWeight = 25 });
			grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Target table", ReadOnly = true, FillWeight = 20 });
			grid.Columns.Add(new DataGridViewComboBoxColumn
			{
				HeaderText = "Matching field",
				FillWeight = 55,
				DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
			});
			grid.DataError += OnGridDataError;

			var cancelButton = new Button
			{
				Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
				DialogResult = DialogResult.Cancel,
				Location = new System.Drawing.Point(657, 404),
				Size = new System.Drawing.Size(75, 24),
				Text = "Cancel",
			};
			var okButton = new Button
			{
				Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
				Location = new System.Drawing.Point(576, 404),
				Size = new System.Drawing.Size(75, 24),
				Text = "OK",
			};
			okButton.Click += OnOkClick;

			AcceptButton = okButton;
			CancelButton = cancelButton;
			Controls.Add(description);
			Controls.Add(grid);
			Controls.Add(okButton);
			Controls.Add(cancelButton);
			Load += OnLoad;
		}

		public List<LookupMappingMemento> Mappings { get; private set; }

		private void OnLoad(object sender, EventArgs e)
		{
			try
			{
				PopulateRows();
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Load lookup mappings", MessageBoxButtons.OK, MessageBoxIcon.Error);
				DialogResult = DialogResult.Cancel;
				Close();
			}
		}

		private void PopulateRows()
		{
			var lookupAttributes = this.entity.Where(x => x.Targets.Count > 0).ToList();
			var metadataCache = new Dictionary<string, EntityKeyOption[]>(StringComparer.OrdinalIgnoreCase);

			foreach (var attribute in lookupAttributes)
			{
				foreach (var targetEntityName in attribute.Targets)
				{
					if (!metadataCache.TryGetValue(targetEntityName, out EntityKeyOption[] keyOptions))
					{
						keyOptions = GetCompatibleKeys(targetEntityName);
						metadataCache[targetEntityName] = keyOptions;
					}

					var index = grid.Rows.Add(attribute.Name, targetEntityName);
					var comboCell = (DataGridViewComboBoxCell)grid[2, index];
					comboCell.DisplayMember = nameof(EntityKeyOption.DisplayText);
					comboCell.ValueMember = nameof(EntityKeyOption.Name);
					comboCell.FlatStyle = FlatStyle.Flat;
					var options = new List<EntityKeyOption> { EntityKeyOption.None };
					if (string.Equals(targetEntityName, "businessunit", StringComparison.OrdinalIgnoreCase))
					{
						options.Add(EntityKeyOption.DestinationRootBusinessUnit);
					}
					options.AddRange(keyOptions);

					var configured = this.initialMappings.FirstOrDefault(x =>
						string.Equals(x.LookupAttributeName, attribute.Name, StringComparison.OrdinalIgnoreCase) &&
						string.Equals(x.TargetEntityName, targetEntityName, StringComparison.OrdinalIgnoreCase));

					if (configured != null)
					{
						var option = configured.UseDestinationRootBusinessUnit
							? options.FirstOrDefault(x => string.Equals(x.Name, EntityKeyOption.DestinationRootBusinessUnit.Name, StringComparison.OrdinalIgnoreCase))
							: options.FirstOrDefault(x => string.Equals(x.Name, configured.MatchAttributeName, StringComparison.OrdinalIgnoreCase));
						if (option == null)
						{
							option = EntityKeyOption.Unavailable(configured.MatchAttributeName);
							options.Add(option);
						}
						comboCell.DataSource = options;
						comboCell.Value = option.Name;
					}
					else
					{
						comboCell.DataSource = options;
						comboCell.Value = EntityKeyOption.None.Name;
					}
					grid.Rows[index].Cells[2].ToolTipText = string.Equals(targetEntityName, "businessunit", StringComparison.OrdinalIgnoreCase)
						? "DEFAULT resolves to the root business unit of the environment receiving the copy."
						: keyOptions.Length == 0
							? "No compatible scalar field is available in both environments."
							: "The selected value is resolved to the matching destination record when copied.";
				}
			}

			if (grid.Rows.Count == 0)
			{
				throw new InvalidOperationException("The selected table has no lookup fields.");
			}
		}

		private EntityKeyOption[] GetCompatibleKeys(string entityName)
		{
			var metadata1 = RetrieveEntityMetadata(this.crm1, entityName);
			var metadata2 = RetrieveEntityMetadata(this.crm2, entityName);

			return metadata1.Attributes
				.Where(x => LookupReferenceMapper.IsSupportedMatchAttribute(x))
				.Where(x => !string.Equals(x.LogicalName, metadata1.PrimaryIdAttribute, StringComparison.OrdinalIgnoreCase))
				.Where(x =>
				{
					var matching = metadata2.Attributes.FirstOrDefault(y => string.Equals(y.LogicalName, x.LogicalName, StringComparison.OrdinalIgnoreCase));
					return matching != null && matching.AttributeType == x.AttributeType;
				})
				.Select(x => new EntityKeyOption(x.LogicalName, x.DisplayName?.UserLocalizedLabel?.Label))
				.OrderBy(x => x.Name)
				.ToArray();
		}

		private static EntityMetadata RetrieveEntityMetadata(Microsoft.Xrm.Sdk.IOrganizationService crm, string entityName)
		{
			var response = (Microsoft.Xrm.Sdk.Messages.RetrieveEntityResponse)crm.Execute(new Microsoft.Xrm.Sdk.Messages.RetrieveEntityRequest
			{
				LogicalName = entityName,
				EntityFilters = EntityFilters.All,
				RetrieveAsIfPublished = true,
			});
			return response.EntityMetadata;
		}

		private void OnGridDataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.ThrowException = false;
			this.errorProvider.SetError(grid, "The selected matching field is not valid for this lookup target.");
		}

		private void OnOkClick(object sender, EventArgs e)
		{
			this.errorProvider.Clear();
			var mappings = new List<LookupMappingMemento>();
			foreach (DataGridViewRow row in grid.Rows)
			{
				var selectedName = row.Cells[2].Value as string;
				if (string.IsNullOrWhiteSpace(selectedName)) continue;
				var comboCell = (DataGridViewComboBoxCell)row.Cells[2];
				var option = ((IEnumerable<EntityKeyOption>)comboCell.DataSource)
					.FirstOrDefault(x => string.Equals(x.Name, selectedName, StringComparison.OrdinalIgnoreCase));
				if (option == null) continue;
				if (!option.IsAvailable)
				{
					this.errorProvider.SetError(grid, "One or more configured matching fields are no longer compatible in both environments.");
					grid.CurrentCell = row.Cells[2];
					grid.Focus();
					return;
				}

				mappings.Add(new LookupMappingMemento
				{
					LookupAttributeName = (string)row.Cells[0].Value,
					TargetEntityName = (string)row.Cells[1].Value,
					MatchAttributeName = option.IsDestinationRootBusinessUnit ? null : option.Name,
					UseDestinationRootBusinessUnit = option.IsDestinationRootBusinessUnit,
				});
			}

			this.Mappings = mappings;
			this.DialogResult = DialogResult.OK;
			Close();
		}

		private sealed class EntityKeyOption
		{
			public const string DestinationRootBusinessUnitName = "$default-root-businessunit";
			public static readonly EntityKeyOption None = new EntityKeyOption(string.Empty);
			public static readonly EntityKeyOption DestinationRootBusinessUnit = new EntityKeyOption(DestinationRootBusinessUnitName, "DEFAULT: destination root business unit");

			public EntityKeyOption(string name, string label = null, bool isAvailable = true)
			{
				this.Name = name;
				this.Label = label;
				this.IsAvailable = isAvailable;
			}

			public string Name { get; }
			public string Label { get; }
			public bool IsAvailable { get; }
			public bool IsDestinationRootBusinessUnit => string.Equals(this.Name, DestinationRootBusinessUnitName, StringComparison.OrdinalIgnoreCase);

			public string DisplayText => string.IsNullOrWhiteSpace(this.Name)
				? "No mapping (compare GUID)"
				: !this.IsAvailable
					? this.Name + " (unavailable)"
					: this.IsDestinationRootBusinessUnit ? this.Label : string.IsNullOrWhiteSpace(this.Label) ? this.Name : this.Label + " (" + this.Name + ")";

			public static EntityKeyOption Unavailable(string name)
			{
				return new EntityKeyOption(name, null, false);
			}

			public override string ToString()
			{
				return this.DisplayText;
			}
		}
	}
}
