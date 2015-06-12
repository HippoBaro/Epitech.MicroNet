using Xamarin.Forms;

using Epitech.Intra.SharedApp.Views;
using Epitech.Intra.API;
using Epitech.Intra.API.Data;

namespace Epitech.Intra.SharedApp
{
	public class App : Application
	{
		public static APIIndex API;

		public RootMaster Root;
		public bool IsUserConnected;
		public string UserLogin;
		public User User;

		public bool HasAllowedEventKit;
		public bool UserHasActivatedEventSync = Device.OnPlatform<bool> (true, false, false);

		public static int ScreenWidth;
		public static int ScreenHeight;

		public async void DisplayLoginScreen ()
		{
			Auth page = new Auth ();
			await Root.Navigation.PushModalAsync (new NavigationPage (page), true);
		}

		private static void SetGlobalStyles ()
		{
			Application.Current.Resources = new ResourceDictionary ();
			var NavBarStyle = new Style (typeof(NavigationPage)) {
				Setters = {
					new Setter { Property = NavigationPage.BarBackgroundColorProperty, Value = IntraColor.DarkGray },
					new Setter { Property = NavigationPage.BarTextColorProperty, Value = Color.White },
					new Setter { Property = NavigationPage.BackgroundColorProperty, Value = Color.White }
				}
			};
			Application.Current.Resources.Add (NavBarStyle);

			var ButtonStyle = new Style (typeof(Button)) {
				Setters = {
					new Setter { Property = VisualElement.BackgroundColorProperty, Value = IntraColor.LightBlue },
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
			MainPage = Root = new RootMaster ();
		}

		protected override void OnStart ()
		{
			DisplayLoginScreen ();
		}
	}
}

