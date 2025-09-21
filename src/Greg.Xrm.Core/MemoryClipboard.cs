using System.Collections.Generic;

namespace Greg.Xrm.Core
{
	public static class MemoryClipboard
	{
		private static readonly object syncRoot = new object();
		private static readonly Dictionary<string, object> _data = new Dictionary<string, object>();

		public static void Store(string key, object value)
		{
			lock (syncRoot)
			{
				_data[key] = value;
			}
		}

		public static T Retrieve<T>(string key)
		{
			lock (syncRoot)
			{
				if (_data.TryGetValue(key, out var value) && value is T tValue)
				{
					return tValue;
				}
				return default;
			}
		}
	}
}
