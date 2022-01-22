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

		public object Clone()
		{
			return new EntityMemento
			{
				EntityName = this.EntityName,
				IsManyToMany = this.IsManyToMany,
				KeyUseGuid = this.KeyUseGuid,
				KeyAttributeNames = this.KeyAttributeNames.ToList(),
				OnlyActive = this.OnlyActive,
				AttributesToSkip = this.AttributesToSkip.ToList(),
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
				if (KeyAttributeNames !=null && KeyAttributeNames.Count > 0)
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
		}
	}
}
