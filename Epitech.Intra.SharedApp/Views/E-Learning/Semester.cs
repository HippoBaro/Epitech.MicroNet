using System;

using Xamarin.Forms;
using System.Collections.Generic;
using Epitech.Intra.SharedApp.Views;

namespace Epitech.Intra.SharedApp.Views
{
	public class Semester : IntraPage
	{
		public Semester ()
		{
			InitIntraPage (typeof(Semester), App.API.GetELearning, new TimeSpan(5, 0, 0, 0));
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();

			if (Content == null)
				await RefreshData (false);
		}

		public override void DisplayContent (object Data)
		{
			base.DisplayContent (Data);

			Title = "E-Learning";

			ListView list = new ListView {
				ItemTemplate = new DataTemplate (typeof(TextCell)) {
					Bindings = {
						{ TextCell.TextProperty, new Binding ("semester") { StringFormat = "Semestre {0}" } },
						{ TextCell.DetailProperty, new Binding ("InnerCount") { StringFormat = "{0} Modules disponible" } }
					}
				},
				ItemsSource = ((List<API.Data.ELearning>)Data),
				IsPullToRefreshEnabled = true
			};

			list.Refreshing += async (object sender, EventArgs e) => {
				await RefreshData (true);
				list.IsRefreshing = false;
			};

			list.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) => {
				if (e.SelectedItem == null)
					return;
				await Navigation.PushAsync (new Modules (((API.Data.ELearning)e.SelectedItem)));
				list.SelectedItem = null;
			};

			Content = list;
		}
	}
}


