using Greg.Xrm.EnvironmentComparer.Model;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Greg.Xrm.EnvironmentComparer.Views.Configurator
{
	public class EntityMetadataDto : IReadOnlyCollection<AttributeMetadataDto>
	{
		private readonly EntityMetadata entity;
		private readonly List<AttributeMetadataDto> attributeList;

		public EntityMetadataDto(EntityMetadata entity)
		{
			this.entity = entity ?? throw new ArgumentNullException(nameof(entity));
			this.attributeList = entity.Attributes
				.Where(x => !x.IsLogical.GetValueOrDefault())
				.Where(x => x.AttributeOf == null)
				.Where(x => !Engine.Constants.AttributesToIgnore.Contains(x.LogicalName))
				.Select(x => new AttributeMetadataDto(x))
				.OrderBy(x => x.Name)
				.ToList();
		}

		public string Name => this.entity.LogicalName;

		public int Count => this.attributeList.Count;

		public IEnumerator<AttributeMetadataDto> GetEnumerator()
		{
			return this.attributeList.GetEnumerator();
		}

		public override string ToString()
		{
			return Name;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
