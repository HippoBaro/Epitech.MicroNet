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
			Children.Add (new ActivityIndicator {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				IsVisible = true,
				IsRunning = true,
				IsEnabled = true,
			});
		}

		public LoadingScreen (string message)
		{
			Padding = new Thickness (5, 20, 5, 0);
			Spacing = 10;
			HorizontalOptions = LayoutOptions.Center;
			VerticalOptions = LayoutOptions.Center;
			Children.Add (new ActivityIndicator {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				IsVisible = true,
				IsRunning = true,
				IsEnabled = true,
			});
			Children.Add (new Label {
				Text = message,
				XAlign = TextAlignment.Center,
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label))
			});
				
		}

		ProgressBar pgbar;

		public void SetPercent (double percent)
		{
			pgbar.Progress = percent;
		}

		public LoadingScreen (string message, Leaderboard ojb)
		{
			Padding = new Thickness (5, 20, 5, 0);
			Spacing = 10;
			HorizontalOptions = LayoutOptions.Center;
			VerticalOptions = LayoutOptions.Center;

			pgbar = new ProgressBar {
				WidthRequest = 100,
				Progress = 0,
			};

			Children.Add (pgbar);

			Children.Add (new Label {
				Text = message,
				XAlign = TextAlignment.Center,
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label))
			});
		}
	}

}
