using Greg.Xrm.Model;
using Greg.Xrm.RoleEditor.Views.Common;
using System.Collections;
using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Views.BulkEditor.Model
{
	public class BulkGroupModel<T> : ViewModel, IReadOnlyList<T>, IReadOnlyList
		where T : IEditorChild
	{
		private readonly List<T> tables = new List<T>();

        public BulkGroupModel(string name)
        {
			this.Name = name;
		}


		/// <summary>
		/// Name of the group
		/// </summary>
		public string Name { get; }


		public bool IsDirty => this.tables.Exists(x => x.IsDirty);


		public void Add(T table)
		{
			this.tables.Add(table);
			table.Parent = this;
			table.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(IsDirty))
					OnPropertyChanged(nameof(IsDirty), IsDirty);
			};
		}


		#region IReadOnlyList<BulkTableModel> implementation

		/// <summary>
		/// Get the table at the given index
		/// </summary>
		/// <param name="index">The index of the table to retreive</param>
		/// <returns></returns>
		public T this[int index] => this.tables[index];

		/// <summary>
		/// Get the number of tables in the group
		/// </summary>
		public int Count => this.tables.Count;

		/// <summary>
		/// Enumerate the tables in the group
		/// </summary>
		/// <returns></returns>
		public IEnumerator<T> GetEnumerator()
		{
			return this.tables.GetEnumerator();
		}

		/// <summary>
		/// Enumerate the tables in the group
		/// </summary>
		/// <returns></returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}
