using System;

using Xamarin.Forms;

namespace Epitech.Intra.SharedApp.Views
{
	public class LoadingScreen : StackLayout
	{
		public LoadingScreen ()
		{
			Padding = new Thickness (5, 20, 5, 0);
			Spacing = 10;
			HorizontalOptions = LayoutOptions.Center;
			VerticalOptions = LayoutOptions.Center;
			Children.Add (new ActivityIndicator () {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				IsVisible = true,
				IsRunning = true,
				IsEnabled = true,
			});
		}
	}
}


