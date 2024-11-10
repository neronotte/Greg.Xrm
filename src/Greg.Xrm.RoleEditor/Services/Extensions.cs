using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Services
{
	public static class Extensions
	{
		public static Dictionary<string, List<string>> CreateReverseMap(this Dictionary<string, string[]> map)
		{
			var reverseMap = new Dictionary<string, List<string>>();
			foreach (var key in map.Keys)
			{
				foreach (var value in map[key])
				{
					if (!reverseMap.ContainsKey(value))
					{
						reverseMap[value] = new List<string> { key };
					}
					else
					{
						var list = reverseMap[value];
						list.Add(key);
					}
				}
			}
			return reverseMap;
		}
	}
}
