﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Greg.Xrm.EnvironmentComparer.Engine
{
	[System.Serializable]
	public class CompareResultSet : Dictionary<string, CompareResultForEntity>
	{
		public CompareResultSet()
		{
		}

		protected CompareResultSet(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		public CompareResultSet Clone()
		{
			var clone = new CompareResultSet();

			foreach (var kvp in this)
			{
				clone[kvp.Key] = kvp.Value.Clone();
			}

			return clone;
		}
	}
}
