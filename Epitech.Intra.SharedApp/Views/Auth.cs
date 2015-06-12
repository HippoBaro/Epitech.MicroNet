using System;

using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin;
using System.Collections.Generic;
using Epitech.Intra.API.Data;
using Epitech.Intra.SharedApp;

namespace Epitech.Intra.SharedApp.Views
{
	public class Auth : IntraPage
	{
		Switch EventSync;

		public Auth ()
		{
			Title = "Connexion à l'intranet";
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			try {
				SelectLoginMethodAuto ();
			} catch (Exception ex) {
				DisplayError (ex);
			}
		}

		async void SelectLoginMethodAuto ()
		{
			Security.Credit Credit = await DependencyService.Get<Security.ISecurity> ().GetItemAsync ();

			if (Credit != null) {
				if (!await TryConnect (Credit))
					await DependencyService.Get<Security.ISecurity> ().DeleteItemAsync ();
			} else
				DisplayContent (string.Empty);
		}

		async Task<bool> TryConnect (Security.Credit credit)
		{
			try {
				if (await InitIntra (credit.Login, credit.Password)) {
					await DependencyService.Get<Security.ISecurity> ().AddItemAsync (credit);
					await Navigation.PopModalAsync (true);
					((MasterDetailPage)Application.Current.MainPage).IsPresented = true;
					return true;
				}
				await DisplayAlert ("Connexion", "Verifier vos identifiants", "Ok");
				DisplayContent (credit.Login);
				return false;
			} catch (Exception ex) {
				await DisplayAlert ("Connexion", ex.Message, "Ok");
				DisplayContent (credit.Login);
				return false;
			}
		}

		void DisplayContent (string user)
		{
			Entry login = new Entry {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Placeholder = "Login",
				Text = user
			};
			Entry pass = new Entry {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Placeholder = "Password Unix",
				IsPassword = true,
			};

			EventSync = new Switch {
				IsToggled = ((App)Application.Current).UserHasActivatedEventSync,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Start,
			};

			Label EventSyncText = new Label {
				Text = "Activer la synchronisation des évènements",
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Start
			};
					
			StackLayout EventProp = new StackLayout {
				Padding = new Thickness (5, 10, 5, 10),
				Orientation = StackOrientation.Horizontal,
				Children = { EventSync, EventSyncText }
			};

			login.Completed += (sender, e) => pass.Focus ();

			pass.Completed += (sender, e) => pass.Unfocus ();

			Button ok = new Button {
				Text = "Connexion",
				Style = (Style)Application.Current.Resources ["ButtonSkinned"],
				HorizontalOptions = LayoutOptions.End,
				WidthRequest = 150
			};
			ok.Clicked += async (sender, e) => {
				if (login.Text == null || pass.Text == null)
					return;
				((App)Application.Current).UserHasActivatedEventSync = EventSync.IsToggled;
				await TryConnect (new Security.Credit {
					Login = login.Text.Replace (" ", String.Empty),
					Password = pass.Text.Replace (" ", String.Empty)
				});
			};
			Content = new StackLayout {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center,
				Padding = new Thickness (40, 0, 40, 20),
				Children = {
					login,
					pass
				}
			};

			if (Device.OS == TargetPlatform.iOS)
				((StackLayout)Content).Children.Add (EventProp);
			((StackLayout)Content).Children.Add (ok);
		}

		async Task<bool> InitIntra (string login, string password)
		{
			Content = new ActivityIndicator {
				IsRunning = true,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};

			bool res = await App.API.CreditialTest (login, password);
			if (!res)
				return res;

			RootMaster.CreateChidrens ();
			var handle = Insights.TrackTime ("TimeToLogin");
			handle.Start ();

			var progressbar = new ActivityIndicator ();
			var status = new Label {
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				XAlign = TextAlignment.Center
			};
			var root = new StackLayout {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Children = { status, progressbar }
			};

			Content = root;

			progressbar.IsRunning = true;
			status.Text = "On y est presque...";
			((App)Application.Current).User = ((User)await RootMaster.MenuTabs [0].Page.SilentUpdate (login));
			((Profile)RootMaster.MenuTabs [0].Page).DisplayContent (RootMaster.MenuTabs [0].Page.Data);
			((Profile)RootMaster.MenuTabs [0].Page).TargetUser = login;
			((App)Application.Current).Root.DrawMenu ();
			var user = ((User)((RootMaster.MenuTabs [0].Page.Data)));

			var traits = new Dictionary<string, string> {
				{ Insights.Traits.Email, user.InternalEmail },
				{ Insights.Traits.Name, user.Firstname + " " + user.Lastname },
				{ Insights.Traits.FirstName, user.Firstname },
				{ Insights.Traits.LastName, user.Lastname }
			};
			traits.Add ("promotion", user.Promo.ToString ());
			traits.Add ("location", user.Location);
			traits.Add ("school", user.SchoolTitle);
			traits.Add ("GPA", user.Gpa [user.Gpa.Length - 1].GPA.ToString ());
			traits.Add ("cycle", user.Gpa [user.Gpa.Length - 1].Cycle);
			Insights.Identify (user.Login, traits);

			handle.Stop ();

			((App)Application.Current).IsUserConnected = true;
			((App)Application.Current).UserLogin = login;

			return true;
		}
	}
}


