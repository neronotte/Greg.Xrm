﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Greg.Xrm.EnvironmentComparer.Model.Memento
{
	public class EngineMemento : IValidatableObject
	{
		public List<EntityMemento> Entities { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (Entities == null)
			{
				yield return new ValidationResult("Entities should not be null!");
				yield break;
			}
			if (Entities.Count == 0)
			{
				yield return new ValidationResult("Entities should contain at least one item!");
				yield break;
			}

			int index = 0;
			foreach (var entityMemento in Entities)
			{
				foreach(var result in entityMemento.Validate(new ValidationContext(entityMemento, new Dictionary<object, object> { { "index", index } })))
				{
					yield return result;
				}
				index++;
			}
		}
	}
}
