using System;

using Xamarin.Forms;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Forms;
using System.Collections.Generic;
using Epitech.Intra.API.Data;
using Epitech.Intra.API.Data.MarksJsonTypes;
using Epitech.Intra.API;

namespace Epitech.Intra.SharedApp.Views
{

	public class ModuleCell : ViewCell
	{
		private Module module;

		protected override void OnBindingContextChanged ()
		{
			module = (Module)BindingContext;
			DrawCell ();
			base.OnBindingContextChanged ();
		}

		private void DrawCell ()
		{
			StackLayout Main = new StackLayout () {
				Padding = new Thickness (10, 0, 0, 0),
				Spacing = -10,
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Vertical,
			};

			Label name = new Label () {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				LineBreakMode = LineBreakMode.TailTruncation,
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				Text = module.Title,
			};

			Label desc = new Label () {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
				Text = getdesc (),
			};

			Main.Children.Add (name);
			Main.Children.Add (desc);

			View = Main;

		}

		private string getdesc ()
		{
			string result = string.Empty;

			result += "Crédit : " + module.Credits.ToString () + ".";
			if (module.Grade == "-")
				result += " En cours d'acquisition.";
			else
				result += " Module terminé : " + module.Grade;
			return result;
		}
	}

	public class Profile : IntraPage
	{
		static private PlotView plotview = new PlotView ();
		NetsoulGraph graph;
		public string TargetUser;

		public Profile (string user)
		{
			TargetUser = (user == null) ? App.API.login : user;
			InitIntraPage (typeof(Profile), App.API.GetUser, new TimeSpan(1, 0, 0), TargetUser);
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
			plotview.Model = null;

			TargetUser = (TargetUser == null) ? App.API.login : TargetUser;
			if (TargetUser != null) {
				InitIntraPage (typeof(Profile), App.API.GetUser, new TimeSpan(1, 0, 0), TargetUser);
				await RefreshData (false, TargetUser);
			}
		}

		public override void DisplayContent (object Data)
		{
			base.DisplayContent (Data);

			Title = ((User)Data).Title;

			if (TargetUser != App.API.login) {
				ToolbarItems.Clear ();
				ToolbarItems.Add (new ToolbarItem ("Email", null, new Action (delegate {
					Device.OpenUri (new Uri ("mailto:" + ((User)Data).InternalEmail));
				}), 0, 0));
			}

			if (graph == null)
				graph = new NetsoulGraph ((User)Data, plotview);

			StackLayout root = new StackLayout () {
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.Fill,
				Children = {
					new ProfileHeader ((User)Data),
					graph,
					new CreditHeader ((User)Data)
				}
			};

			if (((User)Data).Marks != null && ((User)Data).Marks.Modules.Length != 0) {
				ListView listView = new ListView {
					VerticalOptions = LayoutOptions.FillAndExpand,
					ItemTemplate = new DataTemplate (typeof(ModuleCell)),
					ItemsSource = ((User)Data).Marks.Modules,
					Header = new ContentView () {
						Padding = new Thickness (10, 0, 0, 0),
						Content = new Label () {
							Text = "Modules / Notes",
							FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
							HorizontalOptions = LayoutOptions.Center
						}
					},
				};
				listView.HeightRequest = ((User)Data).Marks.Modules.Length * 45 + 10;
				root.Children.Add (listView);

				listView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
					if (e.SelectedItem != null) {
						Navigation.PushAsync (new Marks (((User)Data).Marks, (Module)(e.SelectedItem)));
						((ListView)sender).SelectedItem = null;
					}
				};
			}

			GC.Collect ();
			Content = new ScrollView () {
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.Fill,
				Content = root
			};
		}
	}
}


