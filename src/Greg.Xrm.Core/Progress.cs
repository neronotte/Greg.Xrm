using System;

namespace Greg.Xrm
{
	/// <summary>
	/// Represents a wrapper around a specific collection item, that can be used to track progression while enumerating the collection
	/// </summary>
	/// <typeparam name="T">The type of the items within the collection.</typeparam>
	public class Progress<T>
	{
		/// <summary>
		/// Creates a new Progress&lt;T&gt; instance.
		/// </summary>
		/// <param name="item">The item wrapped by the current object</param>
		/// <param name="index">The index of the item within the source collection</param>
		/// <param name="total">The total number of items in the collection</param>
		internal Progress(T item, int index, int total)
		{
			this.Item = item;
			this.Index = index;
			this.Total = total;
			this.Percent = Convert.ToDecimal(index) / total;
		}


		/// <summary>
		/// Gets the progress percentage
		/// </summary>
		public decimal Percent { get; }

		/// <summary>
		/// Gets the total number of items in the source collection
		/// </summary>
		public int Total { get; }

		/// <summary>
		/// Gets the index of the current object within the source collection 
		/// </summary>
		public int Index { get; }

		/// <summary>
		/// Gets the item wrapped by the current instance
		/// </summary>
		public T Item { get; }

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return $"{this.Index}/{this.Total} ({this.Percent:P2})";
		}
	}
}
