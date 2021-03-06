﻿using System.Collections.Generic;

namespace Greg.Xrm.EnvironmentComparer.Engine
{
	public interface IExtendedEqualityComparer<T>
	{
		bool Equals(T item1, T item2, out List<Difference> differentProperties);
	}
}
