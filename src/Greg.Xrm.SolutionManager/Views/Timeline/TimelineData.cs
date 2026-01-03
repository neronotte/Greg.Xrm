using System;

namespace Greg.Xrm.SolutionManager.Views.Timeline
{
	public class TimelineData
	{
		public int Iteration { get; set; }
		public TimeSpan Elapsed { get; set; }
		public TimeSpan Total { get; set; }
    }
}
