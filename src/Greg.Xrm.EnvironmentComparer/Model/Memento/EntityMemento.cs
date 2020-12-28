using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Greg.Xrm.EnvironmentComparer.Model.Memento
{
	public class EntityMemento : IValidatableObject
	{
		public EntityMemento()
		{
			this.OnlyActive = true;
		}

		public string EntityName { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public bool KeyUseGuid { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public List<string> KeyAttributeNames { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public bool OnlyActive { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public List<string> AttributesToSkip { get; set; }

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
