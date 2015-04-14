using System;
using Epitech.Intra.SharedApp.Views;
using UIKit;
using Foundation;
using Epitech.Intra.iOS;

[assembly: Xamarin.Forms.Dependency (typeof(AppLinkIOS))]
namespace Epitech.Intra.iOS
{
	public class AppLinkIOS : IAppLink
	{
		public bool OpenViaAppLink (string ressource)
		{
			if (UIApplication.SharedApplication.CanOpenUrl (new NSUrl ("vlc://" + new Uri (ressource).AbsoluteUri))) {
				UIApplication.SharedApplication.OpenUrl (new NSUrl ("vlc://" + new Uri (ressource).AbsoluteUri));
				return true;
			}
			return false;
		}
	}
}

