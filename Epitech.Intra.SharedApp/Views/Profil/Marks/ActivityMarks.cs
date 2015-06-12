using System;

using Xamarin.Forms;
using Epitech.Intra.API.Data.MarksJsonTypes;
using System.Collections.Generic;
using Epitech.Intra.API;

namespace Epitech.Intra.SharedApp
{
	public class MarkCell : ViewCell
	{
		private ActivityMark note;

		Label Name;
		Image Pic;
		Label Desc;

		protected override void OnBindingContextChanged ()
		{
			note = (ActivityMark)BindingContext;
			DrawCell ();
			base.OnBindingContextChanged ();
		}

		private void DrawCell ()
		{
			Pic = new Image {
				HeightRequest = 30,
				WidthRequest = 40,
				Source = (note.Picture != null) ? PictureHelper.GetUserPictureUri (note.Picture, note.Login, PictureHelper.PictureSize.VeryLight) : PictureHelper.PicturePlaceholder
			};

			Name = new Label {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				Text = note.UserTitle
			};

			Desc = new Label {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
				Text = "Note : " + note.Note,
				TextColor = IntraColor.DarkBlue,
				FontAttributes = FontAttributes.Bold
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

			if (((App)Application.Current).UserLogin == note.Login)
				root.BackgroundColor = IntraColor.LightBlue;

			View = root;
		}
	}

	public sealed class ActivityMarks : IntraPage
	{
		public ActivityMarks (Note note)
		{
			Content = new ActivityIndicator {
				IsRunning = true,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};
			try {
				DisplayContent (note);
			} catch (Exception ex) {
				DisplayError (ex);
			}
		}

		private async void DisplayContent (Note note)
		{
			List<ActivityMark> notes = (List<ActivityMark>)(await App.API.GetActivityMarks (note));

			notes.Sort ((x, y) => y.Note.CompareTo (x.Note));

			Label comment = new Label () {
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
			};

			Title = "Notes pour " + note.Title;
			StackLayout commentview = new StackLayout {
				BackgroundColor = Color.Gray,
				VerticalOptions = LayoutOptions.EndAndExpand,
				Padding = new Thickness (5, 5, 5, 5),
				Children = {
					new ScrollView { Content = comment },
				}
			};

			ListView list = new ListView {
				ItemTemplate = new DataTemplate (typeof(MarkCell)),
				ItemsSource = notes,
				HasUnevenRows = true
			};

			list.ItemSelected += (sender, e) => {
				if (e.SelectedItem != null) {
					commentview.IsVisible = true;
					comment.Text = ((ActivityMark)e.SelectedItem).Comment;
					((ListView)sender).SelectedItem = null;
				}
			};

			StackLayout root = new StackLayout {
				Children = { list, commentview }
			};

			commentview.IsVisible = false;

			Content = root;
		}
	}
}


