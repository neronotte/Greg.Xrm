using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Greg.Xrm.EnvironmentComparer.Engine.Memento
{
	public class EntityMemento : IValidatableObject, ICloneable
	{
		public EntityMemento()
		{
			this.KeyAttributeNames = new List<string>();
			this.AttributesToSkip = new List<string>();
			this.LookupMappings = new List<LookupMappingMemento>();
		}

		public string EntityName { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public bool IsManyToMany { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public bool KeyUseGuid { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public List<string> KeyAttributeNames { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public bool OnlyActive { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public List<string> AttributesToSkip { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public List<LookupMappingMemento> LookupMappings { get; set; }

		/// <summary>
		/// Optional FetchXML used to filter the records to compare (on both environments).
		/// </summary>
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string FetchXml { get; set; }

		public object Clone()
		{
			return new EntityMemento
			{
				EntityName = this.EntityName,
				IsManyToMany = this.IsManyToMany,
				KeyUseGuid = this.KeyUseGuid,
				KeyAttributeNames = this.KeyAttributeNames?.ToList() ?? new List<string>(),
				OnlyActive = this.OnlyActive,
				AttributesToSkip = this.AttributesToSkip?.ToList() ?? new List<string>(),
				LookupMappings = this.LookupMappings?.Select(x => (LookupMappingMemento)x.Clone()).ToList() ?? new List<LookupMappingMemento>(),
				FetchXml = this.FetchXml,
			};
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var index = (int)validationContext.Items["index"];

			if (string.IsNullOrWhiteSpace(EntityName))
			{
				yield return new ValidationResult($"[Entity {index}] EntityName should be not null.");
			}

			if (KeyUseGuid)
			{
				if (KeyAttributeNames != null && KeyAttributeNames.Count > 0)
				{
					yield return new ValidationResult($"[Entity {index}] KeyAttributeNames should not be specified if KeyUseGuid = true.");
				}
			}
			else
			{
				if (KeyAttributeNames == null || KeyAttributeNames.Count == 0)
				{
					yield return new ValidationResult($"[Entity {index}] KeyAttributeNames should be specified if KeyUseGuid = false.");
				}
			}

			var mappings = this.LookupMappings ?? new List<LookupMappingMemento>();
			for (var mappingIndex = 0; mappingIndex < mappings.Count; mappingIndex++)
			{
				var mapping = mappings[mappingIndex];
				if (mapping == null || string.IsNullOrWhiteSpace(mapping.LookupAttributeName) ||
					string.IsNullOrWhiteSpace(mapping.TargetEntityName) ||
					(!mapping.UseDestinationRootBusinessUnit && string.IsNullOrWhiteSpace(mapping.MatchAttributeName)))
				{
					yield return new ValidationResult($"[Entity {index}] LookupMappings[{mappingIndex}] is incomplete.");
				}
			}

			var validMappings = mappings.Where(x => x != null).ToList();
			if (validMappings.Any(x => x.UseDestinationRootBusinessUnit &&
				!string.Equals(x.TargetEntityName, "businessunit", StringComparison.OrdinalIgnoreCase)))
			{
				yield return new ValidationResult($"[Entity {index}] Destination root business unit can only be used for businessunit lookups.");
			}
			if (validMappings
				.GroupBy(x => x.LookupAttributeName + "|" + x.TargetEntityName, StringComparer.OrdinalIgnoreCase)
				.Any(x => x.Count() > 1))
			{
				yield return new ValidationResult($"[Entity {index}] LookupMappings contains duplicate lookup and target combinations.");
			}

			if (AttributesToSkip?.Any(x => validMappings.Any(mapping =>
				string.Equals(mapping.LookupAttributeName, x, StringComparison.OrdinalIgnoreCase))) == true)
			{
				yield return new ValidationResult($"[Entity {index}] Mapped lookup attributes cannot be specified in AttributesToSkip.");
			}

			if (!string.IsNullOrWhiteSpace(EntityName))
			{
				foreach (var error in FetchXmlFilter.Validate(FetchXml, EntityName))
				{
					yield return new ValidationResult($"[Entity {index}] Invalid FetchXml: {error}");
				}
			}
		}
	}

	public class LookupMappingMemento : ICloneable
	{
		public string LookupAttributeName { get; set; }

		public string TargetEntityName { get; set; }

		public string MatchAttributeName { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public bool UseDestinationRootBusinessUnit { get; set; }

		public object Clone()
		{
			return new LookupMappingMemento
			{
				LookupAttributeName = this.LookupAttributeName,
				TargetEntityName = this.TargetEntityName,
				MatchAttributeName = this.MatchAttributeName,
				UseDestinationRootBusinessUnit = this.UseDestinationRootBusinessUnit,
			};
		}
	}
}
