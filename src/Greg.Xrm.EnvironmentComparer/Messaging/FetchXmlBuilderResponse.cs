namespace Greg.Xrm.EnvironmentComparer.Messaging
{
    /// <summary>
    /// Carries the FetchXML returned by FetchXML Builder.
    /// </summary>
    public class FetchXmlBuilderResponse
    {
        public FetchXmlBuilderResponse(string fetchXml)
        {
            this.FetchXml = fetchXml;
        }

        public string FetchXml { get; private set; }
    }
}
