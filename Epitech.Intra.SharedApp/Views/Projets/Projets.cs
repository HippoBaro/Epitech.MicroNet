﻿using System;

using Xamarin.Forms;
using Epitech.Intra.API.Data.WelcomeJsonTypes;
using System.Collections.Generic;
using Epitech.Intra.API.Data;
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

			Name = new Label () {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				Text = proj.Title
			};

			Desc = new Label () {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Micro, typeof(Label)),
				Text = "Debut : " + proj.Begin.ToString () + Environment.NewLine
				     + "Fin :      " + proj.End.ToString ()
			};

			var start = proj.Begin;
			var end = proj.End;
			var total = (end - start).Ticks;
			var percentage = (DateTime.Now - start).Ticks * 100 / total;

			StackLayout time = new StackLayout () {
				Orientation = StackOrientation.Vertical,
				Children = {
					new ProgressBar () {
						VerticalOptions = LayoutOptions.Center,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Progress = (double)percentage / 100f
					},
					new Label () {
						HorizontalOptions = LayoutOptions.Start,
						VerticalOptions = LayoutOptions.CenterAndExpand,
						FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
						Text = "Temps restant : " + timerest.Days + " j, " + timerest.Hours + " h et " + timerest.Minutes + " mim"
					},
				}
			};

			StackLayout root = new StackLayout () {
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
		public Projets()
		{
			InitIntraPage (typeof(Projets), App.API.GetProjects);
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();

			Title = "Projets";

			await RefreshData (false);
		}

		public override void DisplayContent (object Data)
		{
			base.DisplayContent (Data);

			List<API.Data.Project> projects = ((List<API.Data.Project>)Data);

			var listView = new ListView {
				ItemTemplate = new DataTemplate (typeof(ProjectCell)),
				ItemsSource = projects.FindAll(x => x.UserProjectCode != null).OrderBy (x => x.End),
				HasUnevenRows = true,
				IsPullToRefreshEnabled = true
			};

			listView.ItemSelected += async delegate(object sender, SelectedItemChangedEventArgs e) {
				if (e.SelectedItem != null) {
					await Navigation.PushAsync (new Project ((API.Data.Project)e.SelectedItem));
					((ListView)sender).SelectedItem = null;
				}
			};

			listView.Refreshing += async (object sender, EventArgs e) => {
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


