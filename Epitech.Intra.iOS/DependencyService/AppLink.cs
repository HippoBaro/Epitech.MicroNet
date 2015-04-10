using System;
using Epitech.Intra.SharedApp.Views;
using Epitech.Intra.iOS;
using UIKit;
using Foundation;
using System.Linq;

[assembly: Xamarin.Forms.Dependency (typeof(AppLink_iOS))]
namespace Epitech.Intra.iOS
{
	public class AppLink_iOS : IAppLink
	{
		public bool OpenViaAppLink (string ressource)
		{
			if (UIApplication.SharedApplication.CanOpenUrl (new NSUrl ("vlc://" + new Uri (ressource).AbsoluteUri))) {
				UIApplication.SharedApplication.OpenUrl (new NSUrl ("vlc://" + new Uri (ressource).AbsoluteUri));
				return true;
			} else
				return false;
		}
	}
}

