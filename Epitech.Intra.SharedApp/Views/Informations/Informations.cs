using System;

using Xamarin.Forms;

namespace Epitech.Intra.SharedApp
{
	public class Informations : IntraPage
	{
		public override async System.Threading.Tasks.Task<object> SilentUpdate (string param)
		{
			// Do nothing.
			return null;
		}

		public Informations ()
		{
			Title = "A propos";

			Image Applogo = new Image () {
				Source = ImageSource.FromFile ("logo_blue.png"),
				HeightRequest = 100,
				WidthRequest = 100
			};

			Label InfoContact = new Label () {
				Text = "Vous avez trouvé un bug ?" + Environment.NewLine + "Vous souhaitez contribuer au développement de l'app ? Rendez-vous sur Github." + Environment.NewLine + Environment.NewLine + "Les contributions sont les bienvenues.",
				XAlign = TextAlignment.Center,
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
				HorizontalOptions = LayoutOptions.Center
			};

			StackLayout bug = new StackLayout () {
				Spacing = 20,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = { Applogo, InfoContact }
			};

			Image Gitlogo = new Image () {
				Source = ImageSource.FromFile ("git_logo.png"),
				VerticalOptions = LayoutOptions.Center,
				HeightRequest = 40,
				WidthRequest = 40
			};

			Button GitLink = new Button () {
				Text = "HippoBaro/Epitech.MicroNet",
			};

			GitLink.Clicked += (object sender, EventArgs e) => 
			{
				Device.OpenUri(new Uri ("https://github.com/HippoBaro/Epitech.MicroNet"));
			};

			StackLayout gitinfo = new StackLayout () {
				HorizontalOptions = LayoutOptions.Center,
				Orientation = StackOrientation.Horizontal,
				Children = { Gitlogo, GitLink }
			};

			Label credit = new Label () {
				Text = "Application développée par Hippolyte Barraud. Avec la participation initiale de Christian Diaconu",
				FontSize = Device.GetNamedSize (NamedSize.Micro, typeof(Label)),
				XAlign = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.EndAndExpand
			};

			StackLayout root = new StackLayout () {
				Padding = new Thickness (10, 20, 20, 20),
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.Fill,
				Children = { bug, gitinfo, credit }
			};

			Content = root;

		}
	}
}


