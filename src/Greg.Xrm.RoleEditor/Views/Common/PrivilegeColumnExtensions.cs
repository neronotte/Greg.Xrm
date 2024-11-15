using BrightIdeasSoftware;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Greg.Xrm.RoleEditor.Views.Common
{
	public static class PrivilegeColumnExtensions
	{
		public static OLVColumn SetPrivilegeColumn(this OLVColumn column, PrivilegeType privilegeType)
		{
			if (column == null) throw new ArgumentNullException(nameof(column));

			column.Width = 60;
			column.TextAlign = HorizontalAlignment.Center;
			column.AspectToStringConverter = x => string.Empty;
			column.Sortable = false;
			column.Tag = new PrivilegeColumnTag(privilegeType);
			return column;
		}





		public static OLVColumn SetImageGetter(this OLVColumn column, ImageGetterDelegate action)
		{
			if (column == null)
				throw new ArgumentNullException(nameof(column));

			column.ImageGetter = action ?? throw new ArgumentNullException(nameof(action));
			return column;
		}



		public static T UseLightSelection<T>(this T listView)
			where T : ObjectListView
		{
			if (listView == null)
				throw new ArgumentNullException(nameof(listView));

			listView.UseCustomSelectionColors = true;
			listView.HighlightBackgroundColor = Color.FromArgb(240, 240, 240);
			listView.HighlightForegroundColor = Color.Black;

			return listView;
		}
	}
}
