using System;

namespace Greg.Xrm.EnvironmentComparer.Logging
{
	public interface ILog
	{
		void Debug(string message);
		void Info(string message);
		void Warn(string message);
		void Error(string message);
		void Error(string message, Exception ex);
		void Fatal(string message, Exception ex);

		IDisposable Track(string message);
	}
}
