using System.Collections;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Views.AddUserRoles
{
	public abstract class TreeNodeList<T> : TreeNode, IList<T>
		where T : TreeNode
	{
		private readonly List<T> _innerList = new List<T>();

		// IList<TreeNodeUser> implementation
		public int IndexOf(T item) => _innerList.IndexOf(item);

		public void Insert(int index, T item) => _innerList.Insert(index, item);

		public void RemoveAt(int index) => _innerList.RemoveAt(index);

		public T this[int index]
		{
			get => _innerList[index];
			set => _innerList[index] = value;
		}

		// ICollection<TreeNodeUser> implementation
		public virtual void Add(T item)
		{
			_innerList.Add(item);
		}

		public void AddRange(IEnumerable<T> items)
		{
			foreach (var item in items)
			{
				this.Add(item);
			}
		}

		public void Clear() => _innerList.Clear();

		public bool Contains(T item) => _innerList.Contains(item);

		public void CopyTo(T[] array, int arrayIndex) => _innerList.CopyTo(array, arrayIndex);

		public bool Remove(T item) => _innerList.Remove(item);

		public void RemoveAll(IEnumerable<T> items)
		{
			foreach (var item in items)
			{
				this.Remove(item);
			}
		}

		public int Count => _innerList.Count;

		public bool IsReadOnly => false;

		// IEnumerable<TreeNodeUser> implementation
		public IEnumerator<T> GetEnumerator() => _innerList.GetEnumerator();

		// IEnumerable implementation
		IEnumerator IEnumerable.GetEnumerator() => _innerList.GetEnumerator();
	}
}
