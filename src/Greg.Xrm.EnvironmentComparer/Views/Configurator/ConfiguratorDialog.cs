﻿using Greg.Xrm.EnvironmentComparer.Model.Memento;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Greg.Xrm.EnvironmentComparer.Views.Configurator
{
	public partial class ConfiguratorDialog : Form
	{
		private readonly IReadOnlyCollection<EntityMetadata> entityMetadataList;
		private readonly EntityMetadataDto[] dtoList;

		public ConfiguratorDialog(IReadOnlyCollection<EntityMetadata> entityMetadataList)
		{
			InitializeComponent();

			this.Memento = null;

			this.entityMetadataList = entityMetadataList ?? Array.Empty<EntityMetadata>();
			this.dtoList = this.entityMetadataList
				.Select(x => new EntityMetadataDto(x))
				.OrderBy(x => x.Name)
				.ToArray();

			this.lstEntities.Items.AddRange(dtoList);
			this.txtEntitySearch.AutoCompleteCustomSource.AddRange(dtoList.Select(x => x.Name).ToArray());

			this.rUseAttributes.CheckedChanged += (s, e) => { 
				this.rUseGuid.Checked = !this.rUseAttributes.Checked; 
				this.chlKey.Enabled = this.rUseAttributes.Checked; 
			};
			this.rUseGuid.CheckedChanged += (s, e) => { this.rUseAttributes.Checked = !this.rUseGuid.Checked;  };
		}


		public EntityMemento Memento { get; private set; }

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
			this.chlKey.BeginUpdate();
			this.chlKey.Items.Clear();
			this.chlSkip.BeginUpdate();
			this.chlSkip.Items.Clear();

			if (lstEntities.SelectedItem is EntityMetadataDto dto)
			{
				var attributeList = dto.Cast<object>().ToArray();

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

		private void OnOKClick(object sender, EventArgs e)
		{
			if (!(lstEntities.SelectedItem is EntityMetadataDto dto))
			{
				this.errorProvider.SetError(lstEntities, "Please select an entity");
				return;
			}

			var useGuid = this.rUseGuid.Checked;
			var  keyAttributeNames = Array.Empty<string>();
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

			var onlyActive = this.chkOnlyActive.Checked;

			var entityMemento = new EntityMemento
			{
				EntityName = dto.Name,
				KeyUseGuid = useGuid,
				OnlyActive = onlyActive
			};
			entityMemento.KeyAttributeNames.AddRange(keyAttributeNames);
			entityMemento.AttributesToSkip.AddRange(skipAttributeNames);

			this.Memento = entityMemento;
			this.DialogResult = DialogResult.OK;
			Close();
		}
	}
}