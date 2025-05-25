using Greg.Xrm.Logging;
using System.Threading.Tasks;

namespace Greg.Xrm.ModernThemeBuilder.AIEngine
{
	public interface IImageAnalyzer
	{
		Task<string> AnalyzeImageAsync(ILog log, string imagePath, string prompt);
	}
}
