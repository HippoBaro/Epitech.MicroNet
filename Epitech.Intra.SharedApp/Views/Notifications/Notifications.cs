using System;

using Xamarin.Forms;
using System.Collections.Generic;
using Epitech.Intra.API.Data;
using System.Threading.Tasks;
using Xamarin;

namespace Epitech.Intra.SharedApp.Views
{
	public class NotificationCell : ViewCell
	{
		private Notification noti;

		protected override void OnBindingContextChanged ()
		{
			noti = (Notification)BindingContext;
			DrawCell ();
			base.OnBindingContextChanged ();
		}

		private void DrawCell ()
		{
			var root = new StackLayout () {
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness (10, 5, 10, 5)
			};

			string userlogin;
			userlogin = noti.user.url.Remove (0, 6);
			userlogin = userlogin.Remove (userlogin.Length - 1, 1);

			Image Img = new Image () {
				Source = API.PictureHelper.GetUserPictureUri (noti.user.picture, userlogin, Epitech.Intra.API.PictureHelper.PictureSize.Light)
			};

			StackLayout Main = new StackLayout () {
				Padding = new Thickness (10, 0, 0, 0),
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Vertical,
			};

			Label name = new Label () {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
				Text = API.HTMLCleaner.RemoveLinksKeepText (noti.title),
			};

			Label desc = new Label () {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Micro, typeof(Label)),
				Text = API.HTMLCleaner.RemoveLinksKeepText (noti.content),
			};

			Main.Children.Add (name);
			if (!API.HTMLCleaner.ContainsLink (noti.content))
				Main.Children.Add (desc);
			root.Children.Add (Img);
			root.Children.Add (Main);

			if (noti.unread == true)
				root.BackgroundColor = IntraColor.LightBlue;

			View = root;
		}
	}

	public class Notifications : IntraPage
	{
		List<Notification> LastNotifications = null;
		public List<Notification> News =null;
		ListView listview;

		public Notifications ()
		{
			InitIntraPage (typeof(Notifications), App.API.GetNotifications, TimeSpan.Zero);
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();

			Title = "Notifications";
			await RefreshData (true);
		}
			

		public async Task<object> SilentUpdateForNotification (string param)
		{
			try {
				List<Notification> not = await base.SilentUpdate (param) as List<Notification>;
				News = GetAndMarkNewNotifications (not, true);
				return not;
			} catch (Exception ex) {
				Xamarin.Insights.Report (ex);
			}
			return null;
		}

		public List<Notification> GetAndMarkNewNotifications (List<Notification> Notifs, bool silenced)
		{
			DateTime LastNotificationDate;

			if (LastNotifications == null)
				LastNotifications = Notifs;

			foreach (var item in LastNotifications) {
				if (item.unread && !silenced) {
					continue;
				}
				if (LastNotificationDate < item.date)
					LastNotificationDate = item.date;
			}

			List<Notification> NewNotifications = Notifs.FindAll (x => x.date > LastNotificationDate);

			foreach (var item in NewNotifications) {
				item.unread = true;
			}

			LastNotifications = Notifs;
			return NewNotifications;
		}

		public override void DisplayContent (object Data)
		{
			base.DisplayContent (Data);
			List<Notification> notifications = (List<Notification>)Data;

			GetAndMarkNewNotifications(notifications, false);

			listview = new ListView () {
				ItemsSource = notifications,
				ItemTemplate = new DataTemplate (typeof(NotificationCell)),
				HasUnevenRows = true
			};

			listview.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) => {
				if (e.SelectedItem != null) {
					((Notification)e.SelectedItem).unread = false;
					string path = await ParseLink (((Notification)e.SelectedItem).Links);
					if (path != null) {
						Insights.Track ("UnknownNotification", new Dictionary <string,string> {
							{ "TargetLink", path },
							{ "Title", ((Notification)e.SelectedItem).title },
							{ "Content", ((Notification)e.SelectedItem).content }
						});
						await DisplayAlert ("Informations", "Cette notification ne peut pas être ouverte par l'application." + Environment.NewLine + "Elle va donc être ouverte sur le Web.", "Ok");
						await Navigation.PushModalAsync (new NavigationPage (new WebBrowser (API.APIIndex.baseAPI + path, "Intra Epitech")));
					}
					listview.SelectedItem = null;
				}
			};

			Content = listview;
		}

		public async void OpenNotification (List<Epitech.Intra.API.HTMLCleaner.LinkItem> Links)
		{
			string path = await ParseLink (Links);
			if (path != null) {
				Insights.Track ("UnknownNotification", new Dictionary <string,string> {
					{ "TargetLink", path }
				});
				await DisplayAlert ("Informations", "Cette notification ne peut pas être ouverte par l'application." + Environment.NewLine + "Elle va donc être ouverte sur le Web.", "Ok");
				await Navigation.PushModalAsync (new NavigationPage (new WebBrowser (API.APIIndex.baseAPI + path, "Intra Epitech")));
			}
		}

		private async Task<string> ParseLink (List<Epitech.Intra.API.HTMLCleaner.LinkItem> links)
		{
			foreach (var item in links) {
				int i = 0;
				string year;
				string module;
				string instance;
				string activity;
				bool isProject = false;

				string temp = item.Href;

				if (temp.EndsWith ("/project/"))
					isProject = true;
				if (!temp.StartsWith ("/module/"))
					continue;
				temp = temp.Remove (0, 8);
				year = temp.Substring (0, 4);
				temp = temp.Remove (0, 5);
				for (i = 0; i < temp.Length; i++) {
					if (temp [i] == '/')
						break;
				}
				module = temp.Substring (0, i);
				temp = temp.Remove (0, i + 1);
				for (i = 0; i < temp.Length; i++) {
					if (temp [i] == '/')
						break;
				}
				instance = temp.Substring (0, i);
				temp = temp.Remove (0, i + 1);
				for (i = 0; i < temp.Length; i++) {
					if (temp [i] == '/')
						break;
				}
				activity = temp.Substring (0, i);

				if (await FollowLink (year, module, instance, activity, isProject) == false)
					return item.Href;
				else
					return null;
			}
			return String.Empty;
		}

		private async Task<bool> FollowLink (string year, string module, string instance, string activity, bool isProject)
		{
			object target = null;

			if (!isProject) {
				foreach (var item in RootMaster.MenuTabs) {
					if (item.PageType == typeof(Planning)) {
						List<Calendar> Activities = ((List<Calendar>)item.Page.Data).FindAll (x => x.Scolaryear == year && x.Codemodule == module && x.Codeinstance == instance && x.Codeacti == activity);
						Activities = Activities.FindAll (x => x.EventRegistered != null);

						if (Activities.Count == 0)
							Activities = ((List<Calendar>)item.Page.Data).FindAll (x => x.Scolaryear == year && x.Codemodule == module && x.Codeinstance == instance && x.Codeacti == activity);

						if (Activities.Count == 0)
							return false;

						foreach (var acti in Activities) {
							if (acti.EventRegistered == "present")
								target = acti;
						}

						if (target == null) {
							foreach (var acti in Activities) {
								if (acti.EventRegistered != null)
									target = acti;
							}
						}

						if (target == null)
							target = Activities [0];
						break;
					}
				}
				await Navigation.PushAsync (new Activity ((Calendar)target));
			} else {
				foreach (var item in RootMaster.MenuTabs) {
					if (item.PageType == typeof(Projets)) {
						target = ((List<API.Data.Project>)item.Page.Data).Find (x => x.Scolaryear == year && x.Codemodule == module && x.Codeinstance == instance && x.Codeacti == activity);
						if (target == null)
							return false;
					}
				}
				await Navigation.PushAsync (new Project ((API.Data.Project)target));
			}
			return true;
		}
	}
}


