using System;

using Xamarin.Forms;
using System.Linq;
using Epitech.Intra.SharedApp;

namespace Epitech.Intra.SharedApp.Views
{
	public class TrombiCell : ViewCell
	{
		private API.Data.Trombi user;

		Label Name;
		Image Pic;
		Label Desc;

		protected override void OnBindingContextChanged ()
		{
			user = (API.Data.Trombi)BindingContext;
			DrawCell ();
			base.OnBindingContextChanged ();
		}

		private void DrawCell ()
		{
			string tmp = user.Login;
			if (user.Promo != null)
				tmp += " | Promotion " + user.Promo;

			Pic = new Image {
				HeightRequest = 30,
				WidthRequest = 40,
				Source = (user.Picture != null) ? API.PictureHelper.GetUserPictureUri (user.Picture, user.Login, Epitech.Intra.API.PictureHelper.PictureSize.Light) : API.PictureHelper.PicturePlaceholder
			};

			Name = new Label {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				Text = user.FullName
			};

			Desc = new Label {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
				Text = tmp
			};

			StackLayout info = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Children = { Name, Desc },
				Spacing = 0
			};

			StackLayout root = new StackLayout {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Horizontal,
				Children = { Pic, info },
				Padding = new Thickness (10, 5, 5, 10)
			};

			View = root;
		}
	}

	public sealed class Trombi : IntraPage
	{
		ListView list;

		public Trombi ()
		{
			Title = "Trombi";
			try {
				DisplayContent ();
			} catch (Exception ex) {
				DisplayError (ex);
			}
		}

		private void DisplayContent ()
		{
			list = new ListView {
				ItemTemplate = new DataTemplate (typeof(TrombiCell)),
				HasUnevenRows = true
			};

			list.ItemSelected += (sender, e) => {
				if (e.SelectedItem != null) {
					Navigation.PushAsync (new Profile (((API.Data.Trombi)e.SelectedItem).Login));
					((ListView)sender).SelectedItem = null;
				}

			};

			Content = new StackLayout {
				Children = {
					new SearchBox (this),
					list,
				}
			};
		}

		public async void Search (string searchval)
		{
			try {
				if (searchval.Length > 2) {
					IsBusy = true;
					list.ItemsSource = (await App.API.GetSearchResult (searchval)).OrderBy (q => q.FullName);
					IsBusy = false;
				} else
					list.ItemsSource = null;
			} catch {
				list.ItemsSource = null;
			}
		}
	}

}
