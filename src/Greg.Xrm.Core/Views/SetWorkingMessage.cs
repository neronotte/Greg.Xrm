namespace Greg.Xrm.Core.Views
{
	public class SetWorkingMessage
	{
		public SetWorkingMessage(string message, int width = 340, int heigth = 150)
		{
			Message = message;
			Width = width;
			Heigth = heigth;
		}

		public string Message { get; }
		public int Width { get; }
		public int Heigth { get; }
	}
}
