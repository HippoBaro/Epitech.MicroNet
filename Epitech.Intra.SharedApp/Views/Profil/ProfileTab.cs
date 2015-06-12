using System;

using Xamarin.Forms;
using Epitech.Intra.SharedApp.Views;

namespace Epitech.Intra.SharedApp
{
	public class ProfileTab : TabbedPage
	{
		public static void ShowUserProfile (string user, Page handler)
		{
			handler.Navigation.PushAsync (new ProfileTab (user));
		}

		public ProfileTab (string user)
		{
			Children.Add (new Profile (user));
		}

		public ProfileTab (string user)
		{
			Children.Add (new Profile (user));
		}
	}
}


