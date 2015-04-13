using System;
using System.Net.Http;

using Xamarin.Forms;

using Epitech.Intra.API;
using System.Threading.Tasks;
using Xamarin;
using System.Collections.Generic;
using Epitech.Intra.API.Data;
using Xamarin.Forms.Labs.Controls;
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
				SelectLoginMethodAuto();
			} catch (Exception ex) {
				DisplayError (ex);
			}
		}

		private async void SelectLoginMethodAuto ()
		{
			Security.Credit Credit = await DependencyService.Get<Security.ISecurity> ().GetItemAsync ();

			if (Credit != null) {
				if (await TryConnect (Credit) == false)
					await DependencyService.Get<Security.ISecurity> ().DeleteItemAsync ();
			} else
				DisplayContent ();
		}

		private async Task<bool> TryConnect (Security.Credit Credit)
		{
			try {
				if (await InitIntra (Credit.login, Credit.password)) {
					await DependencyService.Get<Security.ISecurity> ().AddItemAsync (Credit);
					await this.Navigation.PopModalAsync (true);
					((MasterDetailPage)((App)App.Current).MainPage).IsPresented = true;
					return true;
				} else {
					await DisplayAlert ("Connexion", "Verifier vos identifiants", "Ok");
					DisplayContent ();
					return false;
				}
			} catch (Exception ex) {
				await DisplayAlert ("Connexion", ex.Message, "Ok");
				DisplayContent ();
				return false;
			}
		}

		private void DisplayContent ()
		{
			Entry login = new Entry {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Placeholder = "Login",
			};
			Entry pass = new Entry {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Placeholder = "Password Unix",
				IsPassword = true,
			};

			EventSync = new Switch () {
				IsToggled = ((App)App.Current).UserHasActivatedEventSync,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Start,
			};

			Label EventSyncText = new Label () {
				Text = "Activer la synchronisation des évènements",
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Start
			};

			StackLayout EventProp = new StackLayout() {
				Padding = new Thickness (5, 10, 5, 10),
				Orientation = StackOrientation.Horizontal,
				Children = { EventSync, EventSyncText }
			};

			login.Completed += (object sender, EventArgs e) => {
				pass.Focus();
			};

			pass.Completed += (object sender, EventArgs e) => {
				pass.Unfocus();
			};

			Button ok = new Button {
				Text = "Connexion",
				Style = (Style)App.Current.Resources ["ButtonSkinned"],
				HorizontalOptions = LayoutOptions.End,
				WidthRequest = 150
			};
			ok.Clicked += async (object sender, EventArgs e) => {
				((App)App.Current).UserHasActivatedEventSync = EventSync.IsToggled;
				await TryConnect (new Security.Credit () { login = login.Text, password = pass.Text });
			};
			Content = new StackLayout {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center,
				Padding = new Thickness (40, 0, 40, 20),
				Children = {
					login,
					pass,
					EventProp,
					ok
				}
			};
		}

		private async Task<bool> InitIntra (string login, string password)
		{
			Content = new ActivityIndicator () {
				IsRunning = true,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};

			bool res = await App.API.CreditialTest (login, password);
			if (!res)
				return res;

			var handle = Insights.TrackTime ("TimeToLogin");
			handle.Start ();

			var progressbar = new ProgressBar () { WidthRequest = App.ScreenWidth - 100 };
			var status = new Label () {
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				XAlign = TextAlignment.Center
			};
			var root = new StackLayout () {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Children = { status, progressbar }
			};

			Content = root;

			for (int i = 0; i < RootMaster.MenuTabs.Count; i++) {
				status.Text = RootMaster.MenuTabs [i].Name;
				progressbar.Progress = double.Parse ((i).ToString ()) / double.Parse (RootMaster.MenuTabs.Count.ToString ());
				if (i == 0) {
					((Profile)RootMaster.MenuTabs [i].Page).TargetUser = login;
					((App)App.Current).User = ((User)await RootMaster.MenuTabs [i].Page.SilentUpdate (login));
				} else {
					await RootMaster.MenuTabs [i].Page.SilentUpdate (null);
				}
			}

			((App)App.Current).root.DrawMenu ();

			progressbar.Progress = 1;
			status.Text = "On y est presque...";
			await Task.Delay (500);
			((Profile)RootMaster.MenuTabs [0].Page).DisplayContent (((Profile)RootMaster.MenuTabs [0].Page).Data);

			var user = ((API.Data.User)((((Profile)RootMaster.MenuTabs [0].Page).Data)));

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


