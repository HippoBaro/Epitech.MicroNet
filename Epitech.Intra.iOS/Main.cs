using UIKit;
using Xamarin;

namespace Epitech.Intra.iOS
{
	public class Application
	{
		static void Main (string[] args)
		{
			Insights.HasPendingCrashReport += (sender, isStartupCrash) => {
				if (isStartupCrash) {
					Insights.PurgePendingCrashReports ().Wait ();
				}
			};
			Insights.Initialize ("d1b792655ba13200116b009b175104c32487cc25");
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}

