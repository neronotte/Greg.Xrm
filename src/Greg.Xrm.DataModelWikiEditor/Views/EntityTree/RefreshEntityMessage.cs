using System;

namespace Greg.Xrm.DataModelWikiEditor.Views.EntityTree
{
	public class RefreshEntityMessage
	{
		public RefreshEntityMessage(NodeForEntity nodeForEntity)
		{
			this.NodeForEntity = nodeForEntity ?? throw new ArgumentNullException(nameof(nodeForEntity));
		}

		public NodeForEntity NodeForEntity { get; set; }
	}
}
