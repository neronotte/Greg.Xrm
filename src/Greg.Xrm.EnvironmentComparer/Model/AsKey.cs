using Microsoft.Xrm.Sdk;
using System;
using System.Text;

namespace Greg.Xrm.EnvironmentComparer.Model
{
	public static class AsKey
	{
		public static IKeyProvider<Entity> UseGuid { get; } = new KeyProviderById();

		public static IKeyProvider<Entity> UseAttributes(params string[] attributeList)
		{
			return new KeyProviderByAttributeSet(attributeList);
		}


		class KeyProviderById : IKeyProvider<Entity>
		{
			public string Name => "Guid";

			public string GetKey(Entity item)
			{
				if (item == null) return null;
				return item.Id.ToString("D").ToLowerInvariant();
			}
		}


		class KeyProviderByAttributeSet : IKeyProvider<Entity>
		{
			private readonly string[] attributeList;

			public KeyProviderByAttributeSet(string[] attributeList)
			{
				this.attributeList = attributeList ?? throw new System.ArgumentNullException(nameof(attributeList));
				if (attributeList.Length == 0)
					throw new ArgumentException($"{nameof(attributeList)} cannot be empty!");
			}

			public string Name => string.Join(", ", attributeList);

			public string GetKey(Entity item)
			{
				if (item == null) return null;


				var sb = new StringBuilder();

				var i = 0;
				foreach (var attribute in attributeList)
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

					if (i < attributeList.Length)
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


	
}
