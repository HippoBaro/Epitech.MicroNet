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
			InitIntraPage (typeof(Semester), App.API.GetELearning, new TimeSpan (5, 0, 0, 0));
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();

			if (Content == null)
				await RefreshData (false);
		}

		public override void DisplayContent (object data)
		{
			base.DisplayContent (data);

			Title = "E-Learning";

			ListView list = new ListView {
				ItemTemplate = new DataTemplate (typeof(TextCell)) {
					Bindings = {
						{ TextCell.TextProperty, new Binding ("Semester") { StringFormat = "Semestre {0}" } },
						{ TextCell.DetailProperty, new Binding ("InnerCount") { StringFormat = "{0} Modules disponible" } }
					}
				},
				ItemsSource = ((List<API.Data.ELearning>)data),
				IsPullToRefreshEnabled = true
			};

			list.Refreshing += async (sender, e) => {
				await RefreshData (true);
				list.IsRefreshing = false;
			};

			list.ItemSelected += async (sender, e) => {
				if (e.SelectedItem == null)
					return;
				await Navigation.PushAsync (new Modules (((API.Data.ELearning)e.SelectedItem)));
				list.SelectedItem = null;
			};

			Content = list;
		}
	}
}


