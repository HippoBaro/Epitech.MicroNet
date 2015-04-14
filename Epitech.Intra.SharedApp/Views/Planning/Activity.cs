using System;

using Xamarin.Forms;
using Epitech.Intra.API.Data;
using System.Collections.Generic;
using Epitech.Intra.SharedApp.Views;

namespace Epitech.Intra.SharedApp.Views
{
	public class PresenceCell : ViewCell
	{
		private RegisterStudent student;

		protected override void OnBindingContextChanged ()
		{
			student = (RegisterStudent)BindingContext;
			DrawCell ();
			base.OnBindingContextChanged ();
		}

		private void DrawCell ()
		{
			var root = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness (10, 5, 10, 5)
			};
					
			Image Img = new Image {
				Source = API.PictureHelper.GetUserPictureUri (student.Picture, student.Login, Epitech.Intra.API.PictureHelper.PictureSize.VeryLight),
				HeightRequest = 40,
				WidthRequest = 30
			};

			StackLayout Main = new StackLayout {
				Padding = new Thickness (10, 0, 0, 0),
				Spacing = -3,
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center,
				Orientation = StackOrientation.Vertical,
			};

			Label name = new Label {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				Text = student.Title,
			};

			Label desc = new Label {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Micro, typeof(Label)),
				Text = student.Login,
			};

			Main.Children.Add (name);
			Main.Children.Add (desc);
			root.Children.Add (Img);
			root.Children.Add (Main);

			View = root;

		}
	}

	public sealed class Activity : IntraPage
	{
		public Activity (Calendar activity)
		{
			Title = activity.ActiTitle;
			Content = new ActivityIndicator {
				IsRunning = true,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};
			try {
				DisplayContent (activity);
			} catch (Exception ex) {
				DisplayError (ex);
			}
		}

		private async void DisplayContent (Calendar activity)
		{
			List<RegisterStudent> registeredStud = (List<RegisterStudent>)(await App.API.GetActivityRegisteredStudent (activity));

			StackLayout root = new StackLayout ();

			if (activity.TypeCode != "rdv") {
				var list = new ListView {
					ItemTemplate = new DataTemplate (typeof(PresenceCell)),
					ItemsSource = registeredStud,
					Header = new HeaderActivity (activity),
					HasUnevenRows = true,
				};

				list.ItemSelected += (sender, e) => {
					if (e.SelectedItem != null) {
						Navigation.PushAsync (new Profile (((RegisterStudent)e.SelectedItem).Login));
						((ListView)sender).SelectedItem = null;
					}
				};

				root.Children.Add (list);
			} else {
				var message = new StackLayout {
					Padding = new Thickness (10, 10, 10, 10),
					Children = { new Label {
							Text = "Cette activité comporte un système d'inscription par slot." + Environment.NewLine + "Rendez-vous sur l'intranet pour réserver un slot.",
							XAlign = TextAlignment.Center
						}
					}
				};
				root.Children.Add (message);
			}
				
			Content = root;
		}
	}
}


