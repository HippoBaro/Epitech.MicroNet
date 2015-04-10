using System;

using Xamarin.Forms;
using Epitech.Intra.SharedApp.Views;

namespace Epitech.Intra.SharedApp
{
	public class UserBox : ContentView
	{
		public UserBox (string FullName, string Login, string PictureUri, bool LinktoProfile, Color Background, Color TextColor)
		{
			StackLayout root = new StackLayout () {
				Padding = new Thickness (10, 10, 10, 10)
			};

			this.WidthRequest = 150;

			this.BackgroundColor = Background;

			if (PictureUri == null)
				PictureUri = API.PictureHelper.PicturePlaceholder;

			Image pic = new Image () {
				Source = new Uri (PictureUri),
				HeightRequest = 40,
				WidthRequest = 50
			};

			root.Children.Add (pic);
			root.Children.Add (new Label () { Text = FullName, HorizontalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)), XAlign = TextAlignment.Center, TextColor = TextColor });
			root.Children.Add (new Label () { Text = Login, HorizontalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)), TextColor = TextColor });

			var tgr = new TapGestureRecognizer ();
			tgr.Tapped += (s, e) => { 
				Navigation.PushAsync (new Profile (Login));
			};

			if (LinktoProfile)
				this.GestureRecognizers.Add (tgr);

			Content = root;
		}
	}
}


