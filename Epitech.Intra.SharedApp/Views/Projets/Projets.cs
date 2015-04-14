using System;

using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Epitech.Intra.SharedApp.Views
{
	public class ProjectCell : ViewCell
	{
		private API.Data.Project proj;

		Label Name;
		Label Desc;

		protected override void OnBindingContextChanged ()
		{
			proj = (API.Data.Project)BindingContext;
			DrawCell ();
			base.OnBindingContextChanged ();
		}

		private void DrawCell ()
		{
			TimeSpan timerest = proj.End - DateTime.Now;

			Name = new Label {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				Text = proj.Title
			};

			Desc = new Label {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Micro, typeof(Label)),
				Text = "Debut : " + proj.Begin + Environment.NewLine
				+ "Fin :      " + proj.End
			};

			var start = proj.Begin;
			var end = proj.End;
			var total = (end - start).Ticks;
			var percentage = (DateTime.Now - start).Ticks * 100 / total;

			StackLayout time = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Children = {
					new ProgressBar {
						VerticalOptions = LayoutOptions.Center,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Progress = (double)percentage / 100f
					},
					new Label {
						HorizontalOptions = LayoutOptions.Start,
						VerticalOptions = LayoutOptions.CenterAndExpand,
						FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
						Text = (proj.End > DateTime.Now) ? "Temps restant : " + timerest.Days + " j, " + timerest.Hours + " h et " + timerest.Minutes + " mim" : "Projet terminé le : " + proj.Begin
					},
				}
			};

			StackLayout root = new StackLayout {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Vertical,
				Children = { Name, Desc, time },
				Padding = new Thickness (10, 5, 5, 10)
			};

			View = root;
		}
	}

	public class Projets : IntraPage
	{
		public Projets ()
		{
			InitIntraPage (typeof(Projets), App.API.GetProjects, new TimeSpan (1, 0, 0));
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();

			Title = "Projets";

			await RefreshData (false);
		}

		public override void DisplayContent (object data)
		{
			base.DisplayContent (data);

			List<API.Data.Project> projects = ((List<API.Data.Project>)data);

			var listView = new ListView {
				ItemTemplate = new DataTemplate (typeof(ProjectCell)),
				ItemsSource = projects.FindAll (x => x.UserProjectCode != null).OrderBy (x => x.End),
				HasUnevenRows = true,
				IsPullToRefreshEnabled = true
			};

			listView.ItemSelected += async delegate(object sender, SelectedItemChangedEventArgs e) {
				if (e.SelectedItem != null) {
					await Navigation.PushAsync (new Project ((API.Data.Project)e.SelectedItem));
					((ListView)sender).SelectedItem = null;
				}
			};

			listView.Refreshing += async (sender, e) => {
				await RefreshData (true);
				listView.IsRefreshing = false;
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { listView }
			};
		}
	}
}


