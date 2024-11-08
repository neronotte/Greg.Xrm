namespace Greg.Xrm.RoleEditor.Model
{
	public static class RoleName
	{
		private static int index = 0;

		public static string GetNewName()
		{
			index++;
			return $"New Role {index}";
		}
	}
}
