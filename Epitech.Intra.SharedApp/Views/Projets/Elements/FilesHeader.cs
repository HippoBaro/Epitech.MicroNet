using System;

using Xamarin.Forms;
using Epitech.Intra.API.Data.ProjectJsonTypes;
using Epitech.Intra.SharedApp.Views;

namespace Epitech.Intra.SharedApp
{
	public class FilesHeader : ContentView
	{
		readonly Page handler;

		public FilesHeader (Page handler, Epitech.Intra.API.Data.Project project)
		{
			this.handler = handler;
			Content = new ActivityIndicator {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				IsRunning = true
			};
			handler.IsBusy = true;
			DisplayContent (project);
			handler.IsBusy = false;

		}

		public async void DisplayContent (Epitech.Intra.API.Data.Project project)
		{
			var elem = await App.API.GetProjectFiles (project);

			ListView listView = new ListView {
				ItemTemplate = new DataTemplate (typeof(TextCell)) {
					Bindings = {
						{ TextCell.TextProperty, new Binding ("Title") },
						{ TextCell.DetailProperty, new Binding ("TypeOfFile") }
					}
				},
				VerticalOptions = LayoutOptions.Start,
				ItemsSource = elem,
				Header = new StackLayout {
					Padding = new Thickness (10, 0, 0, 0),
					Children = { new  Label {
							Text = "Fichiers",
							FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label))
						}
					}
				}
			};

			if (elem != null)
				listView.HeightRequest = elem.Count * 45 + 30;
			else {
				listView.IsVisible = false;
			}

			listView.ItemSelected += async delegate(object sender, SelectedItemChangedEventArgs e) {

				if (e.SelectedItem != null) {
					if (((Files)e.SelectedItem).Mime == "application/pdf")
						await Navigation.PushModalAsync (new NavigationPage (new WebBrowser (((Files)e.SelectedItem).Fullpath, ((Files)e.SelectedItem).Title)));
					else {
						var answer = await handler.DisplayAlert ("Attention", "Ce fichier n'est pas garanti d'être affiché correctement. Continuer ?", "Oui", "Non");
						if (answer)
							await Navigation.PushModalAsync (new NavigationPage (new WebBrowser (((Files)e.SelectedItem).Fullpath, ((Files)e.SelectedItem).Title)));
					}
					((ListView)sender).SelectedItem = null;
				}
			};

			Content = new StackLayout {
				Children = { new ProjectHeader (project), listView }
			};

			if (project.Registered.Length != 0) {
				ContentView Footer = new ContentView {
					Padding = new Thickness (10, 0, 0, 10),
					Content = new Label {
						Text = "Groupes",
						FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label))
					}
				};
				((StackLayout)Content).Children.Add (Footer);
			}
		}
	}
}


