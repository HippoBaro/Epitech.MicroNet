using System;

using Xamarin.Forms;
using Epitech.Intra.API.Data;
using Epitech.Intra.API.Data.MarksJsonTypes;

namespace Epitech.Intra.SharedApp.Views
{
	public class Marks : ContentPage
	{
		public Marks (UserMarks marks, Module module)
		{
			Note[] notes = Array.FindAll (marks.Notes, x => x.Codemodule == module.Codemodule);
			Array.Sort (notes, (x, y) => String.Compare (x.Date, y.Date, StringComparison.Ordinal));

			Label comment = new Label ();

			Title = "Notes pour " + module.Title;

			Button but = new Button {
				Text = "Afficher les autres notes",
				TextColor = Color.White,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.End
			};

			StackLayout commentview = new StackLayout {
				BackgroundColor = Color.Gray,
				Children = {
					new ScrollView { Content = comment  },
					but
				}
			};
				
			ListView list = new ListView {
				ItemTemplate = new DataTemplate (typeof(TextCell)) {
					Bindings = {
						{ TextCell.TextProperty, new Binding ("Title") },
						{ TextCell.DetailProperty, new Binding ("FinalNote") }
					}
				},
				ItemsSource = notes
			};

			list.ItemSelected += (sender, e) => {
				if (e.SelectedItem != null) {
					commentview.IsVisible = true;
					comment.Text = ((Note)e.SelectedItem).Comment;
				}
			};

			but.Clicked += async (sender, e) => {
				await Navigation.PushAsync (new ActivityMarks ((Note)list.SelectedItem));
				list.SelectedItem = null;
			};

			StackLayout root = new StackLayout ();
			if (notes.Length != 0)
				root.Children.Add (list);
			else {
				root.Children.Add (new Label {
					Text = "Aucune note disponible pour ce module",
					HorizontalOptions = LayoutOptions.Center,
					XAlign = TextAlignment.Center
				});
				root.Padding = new Thickness (10, 20, 10, 10);
			}
			root.Children.Add (commentview);

			commentview.IsVisible = false;

			Content = root;
		}
	}
}


