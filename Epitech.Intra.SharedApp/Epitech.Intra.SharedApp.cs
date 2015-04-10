using System;

using Xamarin.Forms;

using Epitech.Intra.SharedApp.Views;
using Epitech.Intra.API;
using System.Threading.Tasks;
using Epitech.Intra.API.Data;

namespace Epitech.Intra.SharedApp
{
	public class App : Application
	{
		public static APIIndex API;

		public RootMaster root;
		public bool IsUserConnected = false;
		public string UserLogin;
		public User User;

		public bool HasAllowedEventKit;
		public bool UserHasActivatedEventSync = true;

		public static int ScreenWidth;
		public static int ScreenHeight;

		public async Task DisplayLoginScreen()
		{
			Auth page = new Auth();
			await root.Navigation.PushModalAsync(new NavigationPage(page), true);
		}

		private void SetGlobalStyles()
		{
			Application.Current.Resources = new ResourceDictionary ();
			var NavBarStyle = new Style (typeof(NavigationPage)) {
				Setters = {
					new Setter { Property = NavigationPage.BarBackgroundColorProperty, Value = IntraColor.DarkGray },
					new Setter { Property = NavigationPage.BarTextColorProperty, Value = Color.White },
				}
			};
			Application.Current.Resources.Add ( NavBarStyle);

			var ButtonStyle = new Style (typeof(Button)) {
				Setters = {
					new Setter { Property = Button.BackgroundColorProperty, Value = IntraColor.LightBlue },
					new Setter { Property = Button.TextColorProperty, Value = Color.White },
					new Setter { Property = Button.BorderRadiusProperty, Value = 1 },
				}
			};
			Application.Current.Resources.Add ("ButtonSkinned", ButtonStyle);
		}

		public App ()
		{
			API = new APIIndex ();
			SetGlobalStyles ();
			MainPage = root = new RootMaster();
		}

		protected override async void OnStart ()
		{
			await DisplayLoginScreen();
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

