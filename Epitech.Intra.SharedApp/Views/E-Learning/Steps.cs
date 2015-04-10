using System;

using Xamarin.Forms;
using Epitech.Intra.API.Data;
using Epitech.Intra.SharedApp.Views;

namespace Epitech.Intra.SharedApp
{
	public class Steps : IntraPage
	{

		public static async void OpenRessource(Page handler, string target, string Title)
		{
			if (target.EndsWith (".mp4")) {
				bool able = DependencyService.Get<IAppLink> ().OpenViaAppLink (target);
				if (able)
					return;
				else
					await handler.DisplayAlert ("Information", "La vidéo va s'ouvrir dans une fentre web." + Environment.NewLine + "Astuce : Installez VLC et le fichier sera lu avec (VLC pourra lire le son. Et avec le son, c'est mieux...)", "Ok");
			}
			await handler.Navigation.PushModalAsync (new NavigationPage (new WebBrowser (target, Title)));
		}

		public Steps (Class Class)
		{
			try {
				DisplayContent (Class);
			} catch (Exception ex) {
				DisplayError (ex);
			}
		}

		public void DisplayContent (Class Class)
		{
			base.DisplayContent (Data);

			Title = Class.title;
			ListView list = new ListView {
				ItemTemplate = new DataTemplate (typeof(TextCell)) {
					Bindings = {
						{ TextCell.TextProperty, new Binding ("title") },
						{ TextCell.DetailProperty, new Binding ("Type") }
					}
				},
				ItemsSource = Class.steps
			};

			list.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
				if (e.SelectedItem == null)
					return;
				string target = ((API.Data.StepIndex)e.SelectedItem).step.fullpath.Replace (" ", "%20");
				list.SelectedItem = null;
				OpenRessource(this, target, ((API.Data.StepIndex)e.SelectedItem).title);
			};

			Content = list;
		}
	}
}


