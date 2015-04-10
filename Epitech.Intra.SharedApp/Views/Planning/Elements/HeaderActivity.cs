using System;

using Xamarin.Forms;
using Epitech.Intra.API.Data;
using System.Collections.Generic;

namespace Epitech.Intra.SharedApp
{
	public class HeaderActivity : StackLayout
	{
		public HeaderActivity (Calendar Event)
		{
			Button ButtonRegister = new Button () {
				HorizontalOptions = LayoutOptions.End,
				Style = (Style)App.Current.Resources["ButtonSkinned"],
				WidthRequest = 100
			};

			if (Event.EventRegistered != null)
				ButtonRegister.Text = "Se désinscrire";
			else
				ButtonRegister.Text = "S'inscrire";

			if (Event.AllowRegister == false || Event.TypeCode == "rdv")
				ButtonRegister.IsEnabled = false;

			ButtonRegister.Clicked += async (object sender, EventArgs e) => {
				bool res;
				List<View> temp = new List<View>(this.Children);
				Children.Clear ();
				Children.Add (new ActivityIndicator () {
					IsRunning = true,
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center
				});
				if (Event.EventRegistered != null) {
					res = await App.API.UnregistertoActivity (Event);
					if (res) {
						ButtonRegister.Text = "S'inscrire";
						Event.EventRegistered = null;
					}
				} else {
					res = await App.API.RegistertoActivity (Event);
					if (res) {
						ButtonRegister.Text = "Se désinscrire";
						Event.EventRegistered = "present";
					}
				}
				Children.Clear();
				foreach (var item in temp) {
					Children.Add (item);
				}
			};

			StackLayout profs = new StackLayout () {
				Orientation = StackOrientation.Horizontal
			};

			StackLayout info = new StackLayout () {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness (5, 10, 5, 10)
			};

			info.Children.Add (new Label { Text = Event.ActiTitle, FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)) });
			info.Children.Add (new Label {
				Text = "Du : " + Event.Start.ToString (),
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label))
			});
			info.Children.Add (new Label {
				Text = "Au : " + Event.End.ToString (),
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label))
			});
			info.Children.Add (new Label { Text = Event.Room.Code, FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)) });

			Children.Add(info);
			Children.Add (new ScrollView () { Content = profs, Orientation = ScrollOrientation.Horizontal, BackgroundColor = IntraColor.LightGray });
			if (Event.ProfInst != null)
				foreach (var item in Event.ProfInst) {
					profs.Children.Add (new UserBox (item.Title, item.Login, API.PictureHelper.GetUserPictureUri(item.Picture, item.Login, Epitech.Intra.API.PictureHelper.PictureSize.Light), true, Color.Transparent, Color.White) { VerticalOptions = LayoutOptions.Center } );
				}
			Children.Add (new StackLayout() { Children = { ButtonRegister }, Padding = new Thickness (10, 0, 10, 0) } );
		}
	}
}


