using System;

using Xamarin.Forms;
using Epitech.Intra.SharedApp.Views;

namespace Epitech.Intra.SharedApp
{
	public class UserBox : ContentView
	{
		public UserBox (string fullName, string login, string pictureUri, bool linktoProfile, Color background, Color textColor)
		{
			StackLayout root = new StackLayout {
				Padding = new Thickness (10, 10, 10, 10)
			};

			WidthRequest = 150;

			BackgroundColor = background;

			if (pictureUri == null)
				pictureUri = API.PictureHelper.PicturePlaceholder;

			Image pic = new Image {
				Source = new Uri (pictureUri),
				HeightRequest = 40,
				WidthRequest = 50
			};

			root.Children.Add (pic);
			root.Children.Add (new Label {
				Text = fullName,
				HorizontalOptions = LayoutOptions.Center,
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
				XAlign = TextAlignment.Center,
				TextColor = textColor
			});
			root.Children.Add (new Label {
				Text = login,
				HorizontalOptions = LayoutOptions.Center,
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
				TextColor = textColor
			});

			var tgr = new TapGestureRecognizer ();
			tgr.Tapped += (s, e) => Navigation.PushAsync (new Profile (login));

			if (linktoProfile)
				GestureRecognizers.Add (tgr);

			Content = root;
		}
	}
}


