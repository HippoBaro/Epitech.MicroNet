using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace Epitech.Intra.SharedApp
{
	public class MenuHeader : StackLayout
	{
		public MenuHeader ()
		{
			if (((App)Application.Current).User == null)
				return;

			CircleImage Profile = new CircleImage {
				Source = API.PictureHelper.GetUserPictureUri (((App)Application.Current).User.Picture, ((App)Application.Current).User.Login, Epitech.Intra.API.PictureHelper.PictureSize.Light),
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				Aspect = Aspect.Fill,
				HeightRequest = 120,
				WidthRequest = 120
			};

			Label Name = new Label {
				Text = ((App)Application.Current).User.Title,
				HorizontalOptions = LayoutOptions.Center,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				TextColor = Color.White,
			};

			BackgroundColor = Color.Transparent;
			Padding = new Thickness (0, 30, 0, 20);
			Children.Add (Profile);
			Children.Add (Name);
		}
	}
}


