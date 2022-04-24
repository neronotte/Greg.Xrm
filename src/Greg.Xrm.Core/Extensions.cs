using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Label = Microsoft.Xrm.Sdk.Label;

namespace Greg.Xrm
{
	/// <summary>
	/// Generic extension class
	/// </summary>
	public static partial class Extensions
	{

		/// <summary>
		/// Converts a source collection in a pool of collections, each one with maximum size of <paramref name="poolSize"/> elements.
		/// </summary>
		/// <param name="items">The source collection</param>
		/// <param name="poolSize">The maximum number of items in each collection of the pool</param>
		/// <typeparam name="T">The type of the items in the pool</typeparam>
		/// <returns>
		/// A pool of collections of type <typeparamref name="T"/>, each one containing at most <paramref name="poolSize"/> elements.
		/// </returns>
		public static List<List<T>> Pool<T>(this IEnumerable<T> items, int poolSize)
		{
			var poolContainer = new List<List<T>>();

			var pool = new List<T>();
			poolContainer.Add(pool);

			foreach (var item in items)
			{
				pool.Add(item);

				if (pool.Count >= poolSize)
				{
					pool = new List<T>();
					poolContainer.Add(pool);
				}
			}

			// removes the last if empty
			if (poolContainer[poolContainer.Count - 1].Count == 0)
			{
				poolContainer.RemoveAt(poolContainer.Count - 1);
			}

			return poolContainer;
		}


		/// <summary>
		/// Enumerates the collection tracking progress foreach specific item.
		/// </summary>
		/// <typeparam name="T">The type of the items of the collection</typeparam>
		/// <param name="items">The source collection of items to enumerate</param>
		/// <returns>A set of Progress objects, one foreach element of the source collection.</returns>
		public static IEnumerable<Progress<T>> Progress<T>(this ICollection<T> items)
		{
			var i = 0;
			foreach (var item in items)
			{
				i++;
				yield return new Progress<T>(item, i, items.Count);
			}
		}


		/// <summary>
		/// Pretty-prints an OrganizationServiceFault
		/// </summary>
		/// <param name="fault">The fault to print</param>
		/// <returns>A formatted representation of the fault</returns>
		public static string Print(this OrganizationServiceFault fault)
		{
			if (fault == null)
				throw new ArgumentNullException(nameof(fault));

			var sb = new StringBuilder();
			sb.Append("Message: ").Append(fault.Message).AppendLine();
			sb.Append("Error code: ").Append(fault.ErrorCode).AppendLine();
			sb.Append("Error details: ").AppendLine();
			foreach (var errorDetail in fault.ErrorDetails)
			{
				sb.Append("\t").Append(errorDetail.Key).Append(": ").Append(errorDetail.Value).AppendLine();
			}
			sb.Append("Trace:").Append(fault.TraceText).AppendLine();

			if (fault.InnerFault != null)
			{
				sb.AppendLine();
				sb.Append("--- INNER FAULT ").Append(new string('-', 50));
				sb.AppendLine(fault.InnerFault.Print());
			}
			return sb.ToString();
		}


		/// <summary>
		/// Returns <c>True</c> if the current entity contains at least one of the specified fields,
		/// <c>False</c> otherwise.
		/// </summary>
		/// <param name="entity">The current entity.</param>
		/// <param name="fieldNames">The list of fields to check for.</param>
		/// <returns>
		/// <c>True</c> if the current entity contains at least one of the specified fields,
		/// <c>False</c> otherwise.
		/// </returns>
		public static bool ContainsAny(this Entity entity, IEnumerable<string> fieldNames)
		{
			return fieldNames.Any(entity.Contains);
		}

		/// <summary>
		/// Returns <c>True</c> if the current entity contains at least one of the specified fields,
		/// <c>False</c> otherwise.
		/// </summary>
		/// <param name="entity">The current entity.</param>
		/// <param name="fieldNames">The list of fields to check for.</param>
		/// <returns>
		/// <c>True</c> if the current entity contains at least one of the specified fields,
		/// <c>False</c> otherwise.
		/// </returns>
		public static bool ContainsAny(this Entity entity, params string[] fieldNames)
		{
			return fieldNames.Any(entity.Contains);
		}

		/// <summary>
		/// Returns <c>True</c> if the current entity contains ALL the specified fields, <c>False</c> otherwise
		/// </summary>
		/// <param name="entity">The current entity.</param>
		/// <param name="fieldNames">The list of fields to check for.</param>
		/// <returns>
		/// <c>True</c> if the current entity contains ALL the specified fields,
		/// <c>False</c> otherwise.
		/// </returns>
		public static bool ContainsAll(this Entity entity, IEnumerable<string> fieldNames)
		{
			return fieldNames.All(entity.Contains);
		}

		/// <summary>
		/// Returns <c>True</c> if the current entity contains ALL the specified fields, <c>False</c> otherwise
		/// </summary>
		/// <param name="entity">The current entity.</param>
		/// <param name="fieldNames">The list of fields to check for.</param>
		/// <returns>
		/// <c>True</c> if the current entity contains ALL the specified fields,
		/// <c>False</c> otherwise.
		/// </returns>
		public static bool ContainsAll(this Entity entity, params string[] fieldNames)
		{
			return fieldNames.All(entity.Contains);
		}

		/// <summary>
		/// Joins the specified strings into one
		/// </summary>
		/// <param name="parts">The strings to join</param>
		/// <param name="separator">The separator that will be used while joining the strings</param>
		/// <returns>A single string.</returns>
		public static string Join(this IEnumerable<string> parts, string separator)
		{
			return string.Join(separator, parts);
		}


		/// <summary>
		/// Lazy accessor of property values. Quite similar to the ?. operator, but with a few differences.
		/// </summary>
		/// <typeparam name="T">The type of the source object</typeparam>
		/// <typeparam name="TProp">The type of the property to access</typeparam>
		/// <param name="item">The source object</param>
		/// <param name="propertyAccessor">A lambda expression that defines the property accessor</param>
		/// <param name="defaultValue">The default value to use in case of the source object is null</param>
		/// <returns>
		/// The value of the property, if the source object is not null. Otherwise, the default value specified.
		/// </returns>
		public static TProp Try<T, TProp>(this T item, Func<T, TProp> propertyAccessor, TProp defaultValue = default)
		{
			return Equals(item, default(T)) ? defaultValue : propertyAccessor(item);
		}


		/// <summary>
		/// Converts an EntityReference to an empty entity.
		/// </summary>
		/// <param name="reference">The entityReference to convert</param>
		/// <returns></returns>
		public static Entity ToEntity(this EntityReference reference)
		{
			if (reference == null)
				throw new ArgumentNullException(nameof(reference));

			return new Entity(reference.LogicalName) { Id = reference.Id };
		}

		/// <summary>
		/// Utility method to retrieve a specific entity from CRM
		/// </summary>
		/// <param name="service"></param>
		/// <param name="target"></param>
		/// <param name="columnSet"></param>
		/// <returns></returns>
		public static Entity Retrieve(this IOrganizationService service, EntityReference target, ColumnSet columnSet)
		{
			if (service == null)
				throw new ArgumentNullException(nameof(service));
			if (target == null)
				throw new ArgumentNullException(nameof(target));
			if (columnSet == null)
				throw new ArgumentNullException(nameof(columnSet));

			return service.Retrieve(target.LogicalName, target.Id, columnSet);
		}

		/// <summary>
		/// Utility method to retrieve a specific entity from CRM
		/// </summary>
		/// <param name="service"></param>
		/// <param name="target"></param>
		/// <param name="columns"></param>
		/// <returns></returns>
		public static Entity Retrieve(this IOrganizationService service, EntityReference target, params string[] columns)
		{
			if (service == null)
				throw new ArgumentNullException(nameof(service));
			if (target == null)
				throw new ArgumentNullException(nameof(target));

			var columnSet = columns != null && columns.Length > 0 ? new ColumnSet(columns) : new ColumnSet(false);
			return service.Retrieve(target.LogicalName, target.Id, columnSet);
		}

		/// <summary>
		/// [USE WITH CAUTION]
		/// Fetches all the entities returned by the specified query.
		/// </summary>
		/// <param name="service">The organization service use to query mscrm</param>
		/// <param name="query">The query that will be used to fetch the data.</param>
		/// <returns>
		/// ALL the entities returned by the specified query
		/// </returns>
		public static List<Entity> RetrieveAll(this IOrganizationService service, QueryExpression query)
		{
			return RetrieveAll(service, query, x => x);
		}

		/// <summary>
		/// [USE WITH CAUTION]
		/// Fetches all the entities returned by the specified query.
		/// </summary>
		/// <param name="service">The organization service use to query mscrm</param>
		/// <param name="query">The query that will be used to fetch the data.</param>
		/// <param name="projection">The projection function</param>
		/// <returns>
		/// ALL the entities returned by the specified query
		/// </returns>
		public static List<T> RetrieveAll<T>(this IOrganizationService service, QueryExpression query, Func<Entity, T> projection)
		{
			if (service == null)
				throw new ArgumentNullException(nameof(service));
			if (query == null)
				throw new ArgumentNullException(nameof(query));
			if (projection == null)
				throw new ArgumentNullException(nameof(projection));

			query.PageInfo.PageNumber = 1;

			EntityCollection entities;
			var list = new List<T>();

			do
			{
				entities = service.RetrieveMultiple(query);
				list.AddRange(entities.Entities.Select(projection));

				if (entities.MoreRecords)
				{
					query.PageInfo.PageNumber++;
					query.PageInfo.PagingCookie = entities.PagingCookie;
				}

			} while (entities.MoreRecords);

			return list;
		}


		/// <summary>
		/// Counts all the items of the specified type.
		/// </summary>
		/// <param name="service">The organization service to be used to connect to CRM</param>
		/// <param name="query">The query that will be used to fetch the records to count</param>
		/// <returns>
		/// The total number of items of the specified entity in CRM.
		/// </returns>
		public static int CountAll(this IOrganizationService service, QueryExpression query)
		{
			if (service == null)
				throw new ArgumentNullException(nameof(service));
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			// save the previous columns
			var oldColumns = query.ColumnSet;

			query.ColumnSet = new ColumnSet(false);
			query.PageInfo = new PagingInfo
			{
				PageNumber = 1,
				Count = 5000
			};


			var result = 0;
			EntityCollection entityCollection;
			do
			{
				entityCollection = service.RetrieveMultiple(query);
				if (entityCollection.MoreRecords)
				{
					query.PageInfo.PageNumber++;
					query.PageInfo.PagingCookie = entityCollection.PagingCookie;
				}

				result += entityCollection.Entities.Count;

			} while (entityCollection.MoreRecords);

			// restore the previous columns
			query.ColumnSet = oldColumns;

			return result;
		}


		/// <summary>
		/// Counts all the items of the specified type.
		/// </summary>
		/// <param name="service">The organization service to be used to connect to CRM</param>
		/// <param name="entityName">The name of the entity to count</param>
		/// <returns>
		/// The total number of items of the specified entity in CRM.
		/// </returns>
		public static int CountAll(this IOrganizationService service, string entityName)
		{
			var query = new QueryExpression(entityName)
			{
				ColumnSet = { AllColumns = false },
				PageInfo =
				{
					PageNumber = 1,
					Count = 5000
				}
			};


			var result = 0;
			EntityCollection entityCollection;
			do
			{
				entityCollection = service.RetrieveMultiple(query);
				if (entityCollection.MoreRecords)
				{
					query.PageInfo.PageNumber++;
					query.PageInfo.PagingCookie = entityCollection.PagingCookie;
				}

				result += entityCollection.Entities.Count;

			} while (entityCollection.MoreRecords);


			return result;
		}


		/// <summary>
		/// Returns a new entity created merging the two provided ones.
		/// </summary>
		/// <param name="main">The original entity</param>
		/// <param name="delta">The entity to overlap</param>
		/// <returns>A new entity merged from the previous two</returns>
		public static Entity Merge(this Entity main, Entity delta)
		{
			var entityName = main != null ? main.LogicalName : delta != null ? delta.LogicalName : string.Empty;
			var entityId = main != null ? main.Id : delta != null ? delta.Id : Guid.Empty;

			var entity = new Entity(entityName) { Id = entityId };

			if (main != null)
			{
				foreach (var attribute in main.Attributes)
				{
					entity[attribute.Key] = attribute.Value;
				}
			}

			if (delta != null)
			{
				foreach (var attribute in delta.Attributes)
				{
					entity[attribute.Key] = attribute.Value;
				}
			}

			return entity;
		}

		/// <summary>
		/// Returns an aliased value providing the required casts.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"></param>
		/// <param name="attributeLogicalName"></param>
		/// <returns></returns>
		public static T GetAliasedValue<T>(this Entity entity, string attributeLogicalName)
		{
			if (null == entity.Attributes)
			{
				entity.Attributes = new AttributeCollection();
			}

			var value = entity.GetAttributeValue<AliasedValue>(attributeLogicalName);
			if (value?.Value == null) return default;

			return (T)value.Value;
		}

		/// <summary>
		/// Returns an aliased value providing the required casts.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"></param>
		/// <param name="attributeLogicalName"></param>
		/// <param name="alias"></param>
		/// <returns></returns>
		public static T GetAliasedValue<T>(this Entity entity, string attributeLogicalName, string alias)
		{
			return GetAliasedValue<T>(entity, string.Format("{0}.{1}", alias, attributeLogicalName));
		}

		/// <summary>
		/// Clones a specific entity (excepts the standard attributes - createdon, ... - and the forbidden passed via parameter)
		/// </summary>
		/// <param name="original">The entity to clone</param>
		/// <param name="forbiddenAttributes">The attributes to not to clone</param>
		/// <returns>A new entity, cloned from the previous one</returns>
		public static Entity Clone(this Entity original, params string[] forbiddenAttributes)
		{
			var clone = new Entity(original.LogicalName);
			foreach (var attribute in original.Attributes)
			{
				if (!CloneSettings.IsForbidden(original, attribute.Key, forbiddenAttributes))
					clone[attribute.Key] = attribute.Value;
			}
			return clone;
		}

		/// <summary>
		/// Clones the current entity reference
		/// </summary>
		/// <param name="source">The EntityReference to clone</param>
		/// <returns></returns>
		public static EntityReference Clone(this EntityReference source)
		{
			return source == null ? null : new EntityReference(source.LogicalName, source.Id);
		}

		/// <summary>
		/// Clones the specified OptionSetValue
		/// </summary>
		/// <param name="source">The optionSetValue to clone</param>
		/// <returns></returns>
		public static OptionSetValue Clone(this OptionSetValue source)
		{
			return source == null ? null : new OptionSetValue(source.Value);
		}

		/// <summary>
		/// Clone current Entity setting only Id. Useful for update operations
		/// </summary>
		/// <param name="e">Entity to clone. Id is automatically populated.</param>
		/// <returns></returns>
		public static Entity CloneEmpty(this Entity e)
		{
			return new Entity(e.LogicalName) { Id = e.Id };
		}

		/// <summary>
		/// Equality operator between entity references
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool EqualsEntityReference(this EntityReference a, EntityReference b)
		{
			return CrmComparer.AreEqual(a, b);
		}

		/// <summary>
		/// Retrieves the label in the specified language code
		/// </summary>
		/// <param name="label"></param>
		/// <param name="languageCode"></param>
		/// <returns></returns>
		public static string GetLabelValue(this Label label, int languageCode)
		{
			var labelValue = label.LocalizedLabels.Where(x => x.LanguageCode == languageCode)
				.Select(x => x.Label)
				.FirstOrDefault();
			return string.IsNullOrEmpty(labelValue) ? string.Empty : labelValue;
		}

		/// <summary>
		/// Utility method to delete entities
		/// </summary>
		/// <param name="service">The service that can be used to delete the record</param>
		/// <param name="entityRef">A reference to the entity to delete</param>
		public static void Delete(this IOrganizationService service, EntityReference entityRef)
		{
			service.Delete(entityRef.LogicalName, entityRef.Id);
		}


		/// <summary>
		/// Trims the specified text to lenght characters.
		/// </summary>
		/// <param name="text">The text to trim</param>
		/// <param name="length">The number of characters.</param>
		/// <param name="ellipsis">Indicates if you should put the ellipsis at the end of the trimmed string</param>
		/// <returns>A trimmed text</returns>
		public static string TrimTo(this string text, int length, bool ellipsis = false)
		{
			if (string.IsNullOrWhiteSpace(text))
				return text;

			if (text.Length <= length)
				return text;

			var offset = 0;
			var ellipsisText = string.Empty;
			if (ellipsis)
			{
				offset = 3;
				ellipsisText = "...";
			}

			return text.Substring(0, length - offset) + ellipsisText;
		}

		/// <summary>
		/// Utility method that can be used to associate objects
		/// </summary>
		/// <param name="service"></param>
		/// <param name="target"></param>
		/// <param name="entityReferenceToAssociate"></param>
		/// <param name="relationshipName"></param>
		public static void Associate(this IOrganizationService service, EntityReference target, EntityReference entityReferenceToAssociate, string relationshipName)
		{
			service.Associate(target.LogicalName, target.Id, new Relationship(relationshipName), new EntityReferenceCollection { entityReferenceToAssociate });
		}

		/// <summary>
		/// Utility method that can be used to disassociate objects
		/// </summary>
		/// <param name="service"></param>
		/// <param name="target"></param>
		/// <param name="entityReferenceToDisssociate"></param>
		/// <param name="relationshipName"></param>
		public static void Disassociate(this IOrganizationService service, EntityReference target, EntityReference entityReferenceToDisssociate, string relationshipName)
		{
			service.Disassociate(target.LogicalName, target.Id, new Relationship(relationshipName), new EntityReferenceCollection { entityReferenceToDisssociate });
		}


		/// <summary>
		/// Build a string compose by the populated attribute name - attribute value of an entity
		/// </summary>
		/// <param name="entity"></param>
		/// <returns>message</returns>
		public static string GetLogMessageForEntity(this Entity entity)
		{
			var sb = new StringBuilder();

			foreach (var attribute in entity.Attributes)
			{
				if (attribute.Value == null)
				{
					sb.Append($"{attribute.Key} - null").AppendLine();
					continue;
				}

				if (attribute.Value is EntityReference entityReference)
				{
					sb.Append($"{attribute.Key} - {entityReference.Name ?? entityReference.Id.ToString()}").AppendLine();
					continue;
				}

				if (attribute.Value is Money money)
				{
					sb.Append($"{attribute.Key} - {money.Value}").AppendLine();
					continue;
				}

				if (attribute.Value is OptionSetValue optionSetValue)
				{
					sb.Append($"{attribute.Key} - {optionSetValue.Value}").AppendLine();
					continue;
				}


				sb.Append($"{attribute.Key} - {attribute.Value}").AppendLine();
			}

			return sb.ToString();
		}

		/// <summary>
		/// Returns the formatted value of a given entity attribute.
		/// </summary>
		/// <param name="entity">The entity</param>
		/// <param name="propertyName">The attribute to retrieve</param>
		/// <returns>The formatted value of the specified property</returns>
		public static string GetFormattedValue(this Entity entity, string propertyName)
		{
			return entity.FormattedValues.Contains(propertyName) ?
				entity.FormattedValues[propertyName] :
				entity.GetLiteralValue(propertyName);
		}

		/// <summary>
		/// Converts the value of a property to its string representation
		/// </summary>
		/// <param name="entity">The entity</param>
		/// <param name="propertyName">The attribute to retrieve</param>
		/// <returns>The string representation of the specified property</returns>
		public static string GetLiteralValue(this Entity entity, string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName)) return string.Empty;

			entity.Attributes.TryGetValue(propertyName, out object oValue);

			return StringFromObject(oValue);
		}

		private static string StringFromObject(object oValue)
		{
			if (oValue == null) return string.Empty;

			if (oValue is EntityReference entityReference)
				return entityReference.Name;

			if (oValue is OptionSetValue optionSetValue)
				return optionSetValue.Value.ToString();

			if (oValue is Money money)
				return money.Value.ToString(CultureInfo.CurrentUICulture);

			if (oValue is AliasedValue aliasedValue)
				return StringFromObject(aliasedValue.Value);

			return oValue.ToString();
		}



		/// <summary>
		/// Generic Get method for IServiceProvider instances.
		/// </summary>
		/// <typeparam name="T">The type of the object to retrieve</typeparam>
		/// <param name="serviceProvider">The service provider</param>
		/// <returns>An instance of type T</returns>
		public static T Get<T>(this IServiceProvider serviceProvider)
		{
			return (T)serviceProvider.GetService(typeof(T));
		}


		/// <summary>
		/// Returns the date of first day of the week containing the current date
		/// </summary>
		/// <param name="date">The current date</param>
		/// <param name="firstDayOfWeek">The day of the week to be considered as first day</param>
		/// <returns>The date of the first day of the week</returns>
		public static DateTime FirstDayOfWeek(this DateTime date, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
		{
			var currentDay = date.Date;

			while (currentDay.DayOfWeek != firstDayOfWeek)
			{
				currentDay = currentDay.AddDays(-1);
			}
			return currentDay;
		}

		/// <summary>
		/// Returns the date of last day of the week containing the current date
		/// </summary>
		/// <param name="date">The current date</param>
		/// <param name="firstDayOfWeek">The day of the week to be considered as first day</param>
		/// <returns>The date of the last day of the week</returns>
		public static DateTime LastDayOfWeek(this DateTime date, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
		{
			return date.FirstDayOfWeek(firstDayOfWeek).AddDays(6);
		}


		/// <summary>
		/// Indicates whether the type has a parameterless constructor
		/// </summary>
		/// <param name="type">Object type</param>
		/// <returns>
		/// <c>True</c> if the specified type has a parameterless constructor, <c>False</c> otherwise.
		/// </returns>
		public static bool HasDefaultConstructor(this Type type)
		{
			return type.IsValueType || type.GetConstructor(Type.EmptyTypes) != null;
		}

		public static int CountSubstring(this string text, string value)
		{
			int count = 0, minIndex = text.IndexOf(value, 0);
			while (minIndex != -1)
			{
				minIndex = text.IndexOf(value, minIndex + value.Length);
				count++;
			}
			return count;
		}


		/// <summary>
		/// Simplifies correctly calculating hash codes based upon
		/// Jon Skeet's answer here
		/// http://stackoverflow.com/a/263416
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="memberThunks">Thunks that return all the members upon which
		/// the hash code should depend.</param>
		/// <returns></returns>
		public static int CalculateHashCode(this object obj, params Func<object>[] memberThunks)
		{
			if (obj is null)
			{
				throw new ArgumentNullException(nameof(obj));
			}

			if (memberThunks is null)
			{
				throw new ArgumentNullException(nameof(memberThunks));
			}
			// Overflow is okay; just wrap around
			unchecked
			{
				int hash = 5;
				foreach (var member in memberThunks)
					hash = hash * 29 + member().GetHashCode();
				return hash;
			}
		}


		/// <summary>
		/// Via reflection, returns the name of the property wrapped in the expression function passed as parameter
		/// </summary>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="propertyLambda"></param>
		/// <returns></returns>
		public static string GetMemberName<TProperty>(this Expression<Func<TProperty>> propertyLambda)
		{
			if (!(propertyLambda.Body is MemberExpression member))
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a method, not a property.",
					propertyLambda.ToString()));

			var propInfo = member.Member as PropertyInfo;
			if (propInfo == null)
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a field, not a property.",
					propertyLambda.ToString()));

			return propInfo.Name;
		}
		
		
		/// <summary>
		 /// Via reflection, returns the name of the property wrapped in the expression function passed as parameter
		 /// </summary>
		 /// <typeparam name="TObject"></typeparam>
		 /// <typeparam name="TProperty"></typeparam>
		 /// <param name="propertyLambda"></param>
		 /// <returns></returns>
		public static string GetMemberName<TObject, TProperty>(this Expression<Func<TObject, TProperty>> propertyLambda)
		{
			if (!(propertyLambda.Body is MemberExpression member))
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a method, not a property.",
					propertyLambda.ToString()));

			var propInfo = member.Member as PropertyInfo;
			if (propInfo == null)
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a field, not a property.",
					propertyLambda.ToString()));

			return propInfo.Name;
		}


		public static string GetLocalized(this Label label, int? localeId = null)
		{
			if (!localeId.HasValue)
			{
				return label?.UserLocalizedLabel?.Label
					?? label?.LocalizedLabels?.FirstOrDefault()?.Label
					?? string.Empty;
			}

			return label?.LocalizedLabels.Where(x => x.LanguageCode == localeId.Value).Select(x => x.Label).FirstOrDefault()
				?? label?.UserLocalizedLabel?.Label
				?? label?.LocalizedLabels?.FirstOrDefault()?.Label ??
				string.Empty;
		}



		/// <summary>
		/// Converts the current object in a json string
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string ToJsonString(this object obj)
		{
			if (obj == null) return string.Empty;
			return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
		}
	}
}
