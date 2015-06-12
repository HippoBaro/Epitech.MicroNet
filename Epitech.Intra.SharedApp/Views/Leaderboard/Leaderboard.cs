using Xamarin.Forms;
using Epitech.Intra.API.Data;
using Epitech.Intra.SharedApp.Views;
using System.Collections.Generic;
using System.Linq;
using System;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Xamarin.Forms;

namespace Epitech.Intra.SharedApp
{
	public class LeaderboardCell : ViewCell
	{
		private User user;

		Label Name;
		Image Pic;
		Label Desc;
		Label Num;

		protected override void OnBindingContextChanged ()
		{
			user = (User)BindingContext;
			if (user.Login == "AVG" || user.Login == "MED")
				DrawSeperator ();
			else
				DrawCell ();
			base.OnBindingContextChanged ();
		}

		private void DrawSeperator ()
		{
			string text;

			if (user.Login == "AVG")
				text = "GPA moyen : " + user.Title;
			else
				text = "GPA median : " + user.Title;

			View = new Label {
				Text = text,
				XAlign = TextAlignment.Center,
				TextColor = Color.White,
				BackgroundColor = IntraColor.LightGray
			};
		}

		private void DrawCell ()
		{
			Num = new Label {
				VerticalOptions = LayoutOptions.Center,
				WidthRequest = 50,
				Text = user.Index + ".",
				XAlign = TextAlignment.Center,
				YAlign = TextAlignment.Center,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				TextColor = IntraColor.DarkBlue
			};

			Pic = new Image {
				HeightRequest = 30,
				WidthRequest = 40,
				Source = (user.Picture != null) ? API.PictureHelper.GetUserPictureUri (user.Picture, user.Login, Epitech.Intra.API.PictureHelper.PictureSize.Light) : API.PictureHelper.PicturePlaceholder
			};

			Name = new Label {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Center,
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
				LineBreakMode = LineBreakMode.TailTruncation,
				Text = user.Title
			};

			Desc = new Label {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Micro, typeof(Label)),
				Text = "GPA : " + user.Gpa [0].GPA,
				FontAttributes = FontAttributes.Bold,
			};

			StackLayout info = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Children = { Name, Desc },
				Spacing = 0
			};

			StackLayout root = new StackLayout {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Horizontal,
				Children = { Num, Pic, info },
				Padding = new Thickness (10, 5, 5, 5)
			};

			if (user.Login == ((App)Application.Current).UserLogin)
				root.BackgroundColor = IntraColor.LightBlue;

			View = root;
		}
	}

	public sealed class Leaderboard : IntraPage
	{
		List<User> Profiles;

		public Leaderboard ()
		{
			Title = "Leaderboard";
		}

		protected override void OnAppearing ()
		{
			
			base.OnAppearing ();
			if (Profiles == null) {
				Profiles = new List<User> ();
				Content = new LoadingScreen ("L'intranet ne fournissant pas un classement, la construction du leaderboard peut prendre un peu de temps...", this);
				try {
					DisplayContentLocal ();
				} catch (Exception ex) {
					DisplayError (ex);
				}
			}
		}

		private PlotModel GetPieGraph ()
		{
			int under1Count = 0;
			int under2Count = 0;
			int under3Count = 0;
			int under4Count = 0;

			foreach (var item in Profiles) {
				if (item.Login == "AVG" || item.Login == "MED")
					continue;
				if (item.Gpa [0].GPA < 1)
					under1Count++;
				else if (item.Gpa [0].GPA < 2)
					under2Count++;
				else if (item.Gpa [0].GPA < 3)
					under3Count++;
				else if (item.Gpa [0].GPA < 4)
					under4Count++;
			}

			var model = new PlotModel ();

			var ps = new PieSeries {
				StrokeThickness = 2.0,
				InsideLabelPosition = 0.5,
				AngleSpan = 360,
				StartAngle = 0,
			};

			model.Series.Add (ps);
			ps.Slices.Add (new PieSlice ("GPA > 0", under1Count) {
				Fill = IntraColor.GraphColor.GPA0
			});
			ps.Slices.Add (new PieSlice ("GPA > 1", under2Count) {
				Fill = IntraColor.GraphColor.GPA1
			});
			ps.Slices.Add (new PieSlice ("GPA > 2", under3Count) {
				Fill = IntraColor.GraphColor.GPA2
			});
			ps.Slices.Add (new PieSlice ("GPA > 3", under4Count) {
				Fill = IntraColor.GraphColor.GPA3
			});

			ps.Slices [((int)((App)Application.Current).User.Gpa [0].GPA)].IsExploded = true;
			return model;
		}

		private async void DisplayContentLocal ()
		{
			TrombiFilter profiles = ((TrombiFilter)await App.API.GetUsersWithFilter (((App)Application.Current).User.Location, DateTime.Now.Year.ToString (), "Tek" + ((App)Application.Current).User.Studentyear)) ?? ((TrombiFilter)await App.API.GetUsersWithFilter (((App)Application.Current).User.Location, (DateTime.Now.Year - 1).ToString (), "Tek" + ((App)Application.Current).User.Studentyear));

			int i = 0;
			foreach (TrombiFilterItem item in profiles.Results) {
				i++;
				((LoadingScreen)Content).SetPercent ((((double)i) / ((double)(profiles.Results.Count))));
				var tmp = await App.API.GetUserShort (item.Login) as User;
				if (tmp == null)
					continue;
				Profiles.Add (tmp);
			}
			Profiles = Profiles.Where (x => !x.Close).ToList ();
			Profiles.Sort ((x, y) => y.Gpa [0].GPA.CompareTo (x.Gpa [0].GPA));
			InsertAverageAndMedian ();

			int foo = 1;
			foreach (User item in Profiles)
				item.Index = foo++;

			ListView list = new ListView {
				ItemTemplate = new DataTemplate (typeof(LeaderboardCell)),
				ItemsSource = Profiles,
				HasUnevenRows = true
			};

			list.ItemSelected += (sender, e) => {
				if (e.SelectedItem != null) {
					Navigation.PushAsync (new Profile (((User)e.SelectedItem).Login));
					((ListView)sender).SelectedItem = null;
				}
			};

			SearchBar search = new SearchBar {
				CancelButtonColor = IntraColor.LightBlue,
				Placeholder = "Cherchez dans le leaderboard"
			};

			search.TextChanged += (sender, e) => {
				if (e.NewTextValue == String.Empty || e.NewTextValue == null)
					list.ItemsSource = Profiles;
				else {
					List<User> newlist;
					newlist = Profiles.Where (x => x.Login.Contains (e.NewTextValue.ToLower ())).ToList ();
					newlist.AddRange (Profiles.Where (x => x.Title.ToLower ().Contains (e.NewTextValue.ToLower ())));
					newlist.Distinct ().ToList ();
					newlist.Sort ((x, y) => y.Gpa [0].GPA.CompareTo (x.Gpa [0].GPA));
					list.ItemsSource = newlist;
				}
			};

			list.Header = new StackLayout {
				Padding = new Thickness (0, 0, 0, 10),
				Children = {
					search, 
					new PlotView {
						Model = GetPieGraph (),
						HeightRequest = 200,
						VerticalOptions = LayoutOptions.Fill,
						HorizontalOptions = LayoutOptions.Fill,
						WidthRequest = App.ScreenWidth - 20,
					}
				} 
			};

			Content = list;
		}

		private void InsertAverageAndMedian ()
		{
			double i = 0;
			double avr;
			foreach (var item in Profiles) {
				i += item.Gpa [0].GPA;
			}
			avr = i / Profiles.Count;
			for (int b = 0; b < Profiles.Count - 1; b++) {
				if (Profiles [b].Gpa [0].GPA < avr) {
					Profiles.Insert (b, new User { Login = "AVG", Title = avr.ToString ("#.##") });
					break;
				}
			}
			Profiles.Insert (Profiles.Count / 2, new User {
				Login = "MED",
				Title = Profiles [Profiles.Count / 2].Gpa [0].GPA.ToString ()
			});
		}
	}
}


