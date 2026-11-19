namespace Greg.Xrm.EnvironmentComparer.Messaging
{
    /// <summary>
    /// Requests to open FetchXML Builder to edit the given FetchXML.
    /// </summary>
    public class FetchXmlBuilderRequest
    {
        public FetchXmlBuilderRequest(string fetchXml)
        {
            this.FetchXml = fetchXml;
        }

        public string FetchXml { get; private set; }
    }
}
