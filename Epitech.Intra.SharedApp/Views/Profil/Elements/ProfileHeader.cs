using System;

using Xamarin.Forms;

using Epitech.Intra.API.Data;

namespace Epitech.Intra.SharedApp.Views
{
	public class ProfileHeader : StackLayout
	{
		public ProfileHeader (User user)
		{
			if (user.Picture == null)
				user.Picture = API.PictureHelper.PicturePlaceholder;

			Padding = new Thickness (10, 0, 10, 0);
			Orientation = StackOrientation.Horizontal;
			VerticalOptions = LayoutOptions.Start;
			HorizontalOptions = LayoutOptions.Fill;
			BackgroundColor = IntraColor.LightGray;
			Spacing = 10;
			Children.Add (new ContentView {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Center,
				Content = new Image {
					Source = API.PictureHelper.GetUserPictureUri (user.Picture, user.Login, Epitech.Intra.API.PictureHelper.PictureSize.Light),
					HorizontalOptions = LayoutOptions.Fill,
					VerticalOptions = LayoutOptions.Center,
					HeightRequest = 150,
					WidthRequest = 120,
					Aspect = Aspect.AspectFit
				}
			});
			Children.Add (new StackLayout {
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.Center,
				Children = {
					new Label {
						Text = user.Title,
						FontSize = 20,
						TextColor = Color.White,
						FontAttributes = FontAttributes.Bold
					},
					new Label {
						Text = user.Login + Environment.NewLine + ((user.Location == null) ? "" : user.Location + Environment.NewLine) + "Promotion " + user.Promo + Environment.NewLine + "Semestre " + user.Semester,
						HorizontalOptions = LayoutOptions.Start,
						TextColor = Color.White
					},
				}
			});
		}
	}
}


