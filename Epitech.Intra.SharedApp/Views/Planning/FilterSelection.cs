using System;

using Xamarin.Forms;
using Epitech.Intra.SharedApp.Views;

namespace Epitech.Intra.SharedApp
{
	public class FilterSelection : ContentPage
	{
		public FilterSelection (Planning handle)
		{
			Title = "Filtres";
			ToolbarItems.Add (new ToolbarItem ("OK", null, new Action (delegate() {
				handle.DisplayContent(handle.Data);
				Navigation.PopModalAsync (true);
			}), 0, 0));

			SwitchCell subscribed = new SwitchCell () {
				Text = "Inscrit à l'évènement",
			};
			subscribed.SetBinding (SwitchCell.OnProperty, new Binding ("OnlyDisplayRegisteredEvent", BindingMode.TwoWay, null, null));

			SwitchCell tosubscribe = new SwitchCell () {
				Text = "Inscription possible",
			};
			tosubscribe.SetBinding (SwitchCell.OnProperty, new Binding ("OnlyDisplayEventToRegister", BindingMode.TwoWay, null, null));

			SwitchCell registeredmodule = new SwitchCell () {
				Text = "Inscrit au module",
			};
			registeredmodule.SetBinding (SwitchCell.OnProperty, new Binding ("OnlyDisplayEventFromRegisteredModule", BindingMode.TwoWay, null, null));

			SwitchCell pastevent = new SwitchCell () {
				Text = "Afficher évènement passés",
			};
			pastevent.SetBinding (SwitchCell.OnProperty, new Binding ("DisplayPastEvent", BindingMode.TwoWay, null, null));


			TableView tableView = new TableView {
				Intent = TableIntent.Form,
				Root = new TableRoot {
					new TableSection ("Filtres") {
						subscribed, tosubscribe, registeredmodule, pastevent
					}
				}
			};

			this.Content = new StackLayout {
				Children = {
					tableView
				}
			};
		}
	}
}


