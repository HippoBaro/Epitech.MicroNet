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
			Children.Add (new ActivityIndicator () {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Color = Color.Navy,
				IsVisible = true,
				IsRunning = true,
				IsEnabled = true,
				Scale = 1
			});
		}
	}
}


