using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Greg.Xrm.EnvironmentComparer.Engine
{
    /// <summary>
    /// Helper class that handles the FetchXML used to filter the records of a given entity before the comparison.
    /// The FetchXML is used only to filter the records (filters and link-entities):
    /// the comparison is always performed on all the attributes of the entity.
    /// </summary>
    public static class FetchXmlFilter
    {
        public static string CreateTemplate(string entityName)
        {
            var document = new XDocument(
                new XElement("fetch",
                    new XElement("entity",
                        new XAttribute("name", entityName ?? string.Empty),
                        new XElement("filter",
                            new XAttribute("type", "and"),
                            string.Empty))));

            return Format(document);
        }

        public static bool HasTop(string fetchXml)
        {
            if (!TryParse(fetchXml, out var document, out _)) return false;

            return document.Root?.Attribute("top") != null;
        }

        public static string RemoveTop(string fetchXml)
        {
            if (!TryParse(fetchXml, out var document, out _)) return fetchXml;

            document.Root?.Attribute("top")?.Remove();
            return Format(document);
        }

        public static string Format(string fetchXml)
        {
            if (!TryParse(fetchXml, out var document, out _)) return fetchXml;

            return Format(document);
        }


        /// <summary>
        /// Validates the FetchXML against the given entity.
        /// </summary>
        /// <param name="fetchXml">The FetchXML to validate</param>
        /// <param name="entityName">The name of the entity the FetchXML should refer to</param>
        /// <returns>
        /// The list of validation errors. An empty list means that the FetchXML is valid.
        /// An empty FetchXML is considered valid (it means "no filter").
        /// </returns>
        public static IReadOnlyList<string> Validate(string fetchXml, string entityName)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(fetchXml)) return errors;

            if (!TryParse(fetchXml, out var document, out var parseError))
            {
                errors.Add($"The FetchXML is not a well-formed XML: {parseError}");
                return errors;
            }

            var root = document.Root;
            if (root == null || !string.Equals(root.Name.LocalName, "fetch", StringComparison.Ordinal))
            {
                errors.Add("The root node of the FetchXML must be <fetch>.");
                return errors;
            }

            if (string.Equals(root.Attribute("aggregate")?.Value, "true", StringComparison.OrdinalIgnoreCase))
            {
                errors.Add("Aggregate queries are not supported: remove the aggregate attribute from the <fetch> node.");
            }

            if (root.Attribute("top") != null)
            {
                errors.Add("The top attribute is not supported, because all the records matching the filter must be compared: remove the top attribute from the <fetch> node.");
            }

            var entityNodes = root.Elements("entity").ToList();
            if (entityNodes.Count != 1)
            {
                errors.Add("The FetchXML must contain exactly one <entity> node.");
                return errors;
            }

            var fetchEntityName = entityNodes[0].Attribute("name")?.Value;
            if (!string.Equals(fetchEntityName, entityName, StringComparison.OrdinalIgnoreCase))
            {
                errors.Add($"The FetchXML refers to entity <{fetchEntityName}>, while the configured entity is <{entityName}>.");
            }

            return errors;
        }

        private static bool TryParse(string fetchXml, out XDocument document, out string error)
        {
            document = null;
            error = null;

            if (string.IsNullOrWhiteSpace(fetchXml))
            {
                error = "The FetchXML is empty.";
                return false;
            }

            try
            {
                document = XDocument.Parse(fetchXml);
                return true;
            }
            catch (XmlException ex)
            {
                error = ex.Message;
                return false;
            }
        }

        private static string Format(XDocument document)
        {
            return document.Root?.ToString(SaveOptions.None) ?? string.Empty;
        }
    }
}
