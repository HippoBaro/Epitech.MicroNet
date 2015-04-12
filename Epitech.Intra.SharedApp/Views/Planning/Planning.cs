using System;

using Xamarin.Forms;
using Epitech.Intra.API.Data;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Epitech.Intra.API;

namespace Epitech.Intra.SharedApp.Views
{
	public interface IEventManager_iOS
	{
		void SynchrosizeCalendar (List<Calendar> events);
	}

	public class Planning : IntraPage
	{
		EventFilterOption Filters = new EventFilterOption () { OnlyDisplayEventFromRegisteredModule = true };

		public Planning()
		{
			InitIntraPage (typeof(Planning), App.API.GetCalendar);
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();

			Title = "Planning";

			await RefreshData (false);
		}

		public override void DisplayContent (object Data)
		{
			base.DisplayContent (Data);

			ToolbarItems.Clear ();
			ToolbarItems.Add (new ToolbarItem ("Filtres", null, new Action (async delegate() {
				FilterSelection page = new FilterSelection (this) { BindingContext = Filters };
				await Navigation.PushModalAsync (new NavigationPage (page), true);
				DisplayContent (Data);
			}), 0, 0));

			List<Calendar> calendar = (List<Calendar>)Data;
			EventSorting sorted = new EventSorting (Filters, calendar);

			ListView listView = new ListView {
				IsPullToRefreshEnabled = true,
				ItemTemplate = new DataTemplate (typeof(TextCell)) {
					Bindings = {
						{ TextCell.TextProperty, new Binding ("ActiTitle") },
						{ TextCell.DetailProperty, new Binding ("Start") { StringFormat = "{0:dd/MM/yyyy - HH:mm}" } }
					}
				},
				GroupDisplayBinding = new Binding ("GroupTitle"),
				Header = new HeaderPlaning (calendar),
				IsGroupingEnabled = true,
				ItemsSource = sorted.GetGroupedList ()
			};
			listView.Refreshing += async (object sender, EventArgs e) => {
				await RefreshData (true);
				listView.IsRefreshing = false;
			};
			listView.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) => {
				if (e.SelectedItem != null) {
					await Navigation.PushAsync (new Activity ((Calendar)e.SelectedItem));
					((ListView)sender).SelectedItem = null;
				}
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { listView }
			};
		}
	}
}


