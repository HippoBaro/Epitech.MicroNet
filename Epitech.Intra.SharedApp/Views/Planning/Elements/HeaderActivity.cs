using System;

using Xamarin.Forms;
using Epitech.Intra.API.Data;
using System.Collections.Generic;

namespace Epitech.Intra.SharedApp
{
	public class HeaderActivity : StackLayout
	{
		readonly Button ButtonRegister;

		public HeaderActivity (Calendar eventSelected)
		{
			ButtonRegister = new Button {
				HorizontalOptions = LayoutOptions.End,
				Style = (Style)Application.Current.Resources ["ButtonSkinned"],
				WidthRequest = 100
			};

			ButtonRegister.Text = eventSelected.EventRegistered != null ? "Se désinscrire" : "S'inscrire";

			ButtonRegister.IsEnabled &= eventSelected.AllowRegister && eventSelected.TypeCode != "rdv";

			if (eventSelected.TokenAsked) {
				ButtonRegister.IsEnabled = true;
				ButtonRegister.Text = "Entrer son token";
			}

			ButtonRegister.Clicked += (sender, e) => {
				if (eventSelected.TokenAsked) {
					PromptToken (eventSelected);
				} else {
					RegisterUnregister (eventSelected);
				}
			};

			StackLayout profs = new StackLayout {
				Orientation = StackOrientation.Horizontal
			};

			StackLayout info = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness (5, 10, 5, 10)
			};

			info.Children.Add (new Label {
				Text = eventSelected.ActiTitle,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label))
			});
			info.Children.Add (new Label {
				Text = "Du : " + eventSelected.Start,
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label))
			});
			info.Children.Add (new Label {
				Text = "Au : " + eventSelected.End,
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label))
			});
			info.Children.Add (new Label {
				Text = eventSelected.Room.Code,
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label))
			});

			Children.Add (info);
			Children.Add (new ScrollView {
				Content = profs,
				Orientation = ScrollOrientation.Horizontal,
				BackgroundColor = IntraColor.LightGray
			});
			if (eventSelected.ProfInst != null)
				foreach (var item in eventSelected.ProfInst) {
					profs.Children.Add (new UserBox (item.Title, item.Login, API.PictureHelper.GetUserPictureUri (item.Picture, item.Login, Epitech.Intra.API.PictureHelper.PictureSize.Light), true, Color.Transparent, Color.White) { VerticalOptions = LayoutOptions.Center });
				}
			Children.Add (new StackLayout { Children = { ButtonRegister }, Padding = new Thickness (10, 0, 10, 0) });
		}

		private async void PromptToken (Calendar eventSelected)
		{
			
		}

		private async void RegisterUnregister (Calendar eventSelected)
		{
			bool res;
			List<View> temp = new List<View> (Children);
			Children.Clear ();
			Children.Add (new ActivityIndicator {
				IsRunning = true,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			});
			if (eventSelected.EventRegistered != null) {
				res = await App.API.UnregistertoActivity (eventSelected);
				if (res) {
					ButtonRegister.Text = "S'inscrire";
					eventSelected.EventRegistered = null;
				}
			} else {
				res = await App.API.RegistertoActivity (eventSelected);
				if (res) {
					ButtonRegister.Text = "Se désinscrire";
					eventSelected.EventRegistered = "present";
				}
			}
			Children.Clear ();
			foreach (var item in temp) {
				Children.Add (item);
			}
		}
	}
}


