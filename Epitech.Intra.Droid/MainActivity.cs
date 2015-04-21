using Android.App;
using Android.Content.PM;
using Android.OS;
using Epitech.Intra.SharedApp;
using System.Net;
using Xamarin;

namespace Epitech.Intra.Droid
{
	[Activity (Label = "Epitech MicroNet", Icon = "@drawable/ic_launcher", MainLauncher = true, Theme = "@android:style/Theme.Holo.Light", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		private int ConvertPixelsToDp (float pixelValue)
		{
			var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
			return dp;
		}

		protected override void OnCreate (Bundle bundle)
		{
			ServicePointManager
				.ServerCertificateValidationCallback +=
					(sender, cert, chain, sslPolicyErrors) => true;

			Insights.HasPendingCrashReport += (sender, isStartupCrash) => {
				if (isStartupCrash) {
					Insights.PurgePendingCrashReports ().Wait ();
				}
			};
			Insights.Initialize ("d1b792655ba13200116b009b175104c32487cc25", ApplicationContext);

			base.OnCreate (bundle);

			var metrics = Resources.DisplayMetrics;
			App.ScreenWidth = ConvertPixelsToDp (metrics.WidthPixels);
			App.ScreenHeight = ConvertPixelsToDp (metrics.HeightPixels);

			OxyPlot.Xamarin.Forms.Platform.Android.Forms.Init ();
			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());
		}
	}
}

