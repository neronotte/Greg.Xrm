using Greg.Xrm.Model;
using Microsoft.Xrm.Sdk;

namespace Greg.Xrm.ConstantsExtractor.Model
{
	public class Solution : EntityWrapper
	{
		public Solution(Entity entity) : base(entity)
		{
		}

		public string uniquename => Get<string>();
		public string friendlyname => Get<string>();


		public override string ToString()
		{
			return this.friendlyname;
		}

	}
}
