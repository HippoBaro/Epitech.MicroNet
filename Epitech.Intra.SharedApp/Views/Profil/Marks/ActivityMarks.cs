using System;

using Xamarin.Forms;
using Epitech.Intra.API.Data.MarksJsonTypes;
using System.Collections.Generic;
using Epitech.Intra.API;

namespace Epitech.Intra.SharedApp
{
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

			notes.Sort ((x, y) => String.Compare (x.UserTitle, y.UserTitle, StringComparison.Ordinal));

			Label comment = new Label ();

			Title = "Notes pour " + note.Title;

			StackLayout commentview = new StackLayout {
				BackgroundColor = Color.Gray,
				Children = {
					new ScrollView { Content = comment  },
				}
			};

			ListView list = new ListView {
				ItemTemplate = new DataTemplate (typeof(TextCell)) {
					Bindings = {
						{ TextCell.TextProperty, new Binding ("UserTitle") },
						{ TextCell.DetailProperty, new Binding ("Note") }
					}
				},
				ItemsSource = notes
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


