using System;

using Xamarin.Forms;
using Epitech.Intra.API.Data;
using Epitech.Intra.SharedApp.Views;

namespace Epitech.Intra.SharedApp
{
	public sealed class Steps : IntraPage
	{

		public static async void OpenRessource (Page handler, string target, string title)
		{
			if (target.EndsWith (".mp4", StringComparison.Ordinal)) {
				bool able = DependencyService.Get<IAppLink> ().OpenViaAppLink (target);
				if (able)
					return;
				await handler.DisplayAlert ("Information", "La vidéo va s'ouvrir dans une fentre web." + Environment.NewLine + "Astuce : Installez VLC et le fichier sera lu avec (VLC pourra lire le son. Et avec le son, c'est mieux...)", "Ok");
			}
			await handler.Navigation.PushModalAsync (new NavigationPage (new WebBrowser (target, title)));
		}

		public Steps (Class classElement)
		{
			try {
				DisplayContent (classElement);
			} catch (Exception ex) {
				DisplayError (ex);
			}
		}

		public void DisplayContent (Class classElement)
		{
			base.DisplayContent (Data);

			Title = classElement.Title;
			ListView list = new ListView {
				ItemTemplate = new DataTemplate (typeof(TextCell)) {
					Bindings = {
						{ TextCell.TextProperty, new Binding ("Title") },
						{ TextCell.DetailProperty, new Binding ("Type") }
					}
				},
				ItemsSource = classElement.Steps
			};

			list.ItemSelected += (sender, e) => {
				if (e.SelectedItem == null)
					return;
				string target = ((StepIndex)e.SelectedItem).Step.Fullpath.Replace (" ", "%20");
				list.SelectedItem = null;
				OpenRessource (this, target, ((StepIndex)e.SelectedItem).Title);
			};

			Content = list;
		}
	}
}


