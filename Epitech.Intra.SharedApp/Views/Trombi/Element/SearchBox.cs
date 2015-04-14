using Xamarin.Forms;
using Epitech.Intra.SharedApp.Views;

namespace Epitech.Intra.SharedApp
{
	public class SearchBox : StackLayout
	{
		public SearchBox (Trombi handler)
		{
			SearchBar bar = new SearchBar {
				Placeholder = "Chercher quelqu'un",
				HorizontalOptions = LayoutOptions.Fill,
			};
			bar.TextChanged += (sender, e) => handler.Search (e.NewTextValue);

			HorizontalOptions = LayoutOptions.Fill;
			Children.Add (bar);
		}
	}
}


