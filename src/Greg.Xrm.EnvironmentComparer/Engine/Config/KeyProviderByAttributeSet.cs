using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace Greg.Xrm.EnvironmentComparer.Engine.Config
{
	public class KeyProviderByAttributeSet : IKeyProvider<Entity>
	{

		public KeyProviderByAttributeSet(string[] attributeList)
		{
			this.AttributeList = attributeList ?? throw new ArgumentNullException(nameof(attributeList));
			if (attributeList.Length == 0)
				throw new ArgumentException($"{nameof(attributeList)} cannot be empty!");
		}

		public string Name => string.Join(", ", AttributeList);
		public IReadOnlyCollection<string> AttributeList { get; }

		public string GetKey(Entity item)
		{
			if (item == null) return null;


			var sb = new StringBuilder();

			var i = 0;
			foreach (var attribute in AttributeList)
			{
				i++;

				if (item.Contains(attribute))
				{
					var attributeValue = item[attribute];
					var attributeValueFormatted = Format(attributeValue).Replace("\"", "\"\"");
					sb.Append("\"").Append(attributeValueFormatted).Append("\"");
				}
				else
				{
					sb.Append("\"null\"");
				}

				if (i < AttributeList.Count)
				{
					sb.Append(";");
				}
			}

			return sb.ToString();
		}

		private string Format(object attributeValue)
		{
			if (attributeValue == null) return "null";

			if (attributeValue is EntityReference entityRef)
			{
				return $"[{entityRef.LogicalName}={entityRef.Id},{entityRef.Name}]";
			}
			if (attributeValue is Money money)
			{
				return money.Value.ToString();
			}
			if (attributeValue is OptionSetValue optionSetValue)
			{
				return optionSetValue.Value.ToString();
			}

			return attributeValue.ToString();
		}
	}
}
