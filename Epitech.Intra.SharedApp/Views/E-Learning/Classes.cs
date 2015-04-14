using System;

using Xamarin.Forms;
using Epitech.Intra.API.Data;

namespace Epitech.Intra.SharedApp
{
	public sealed class Classes : IntraPage
	{
		public Classes (ModuleElearning module)
		{
			try {
				DisplayContent (module);
			} catch (Exception ex) {
				DisplayError (ex);
			}
		}

		public void DisplayContent (ModuleElearning module)
		{
			base.DisplayContent (Data);

			Title = module.Title;
			ListView list = new ListView {
				ItemTemplate = new DataTemplate (typeof(TextCell)) {
					Bindings = {
						{ TextCell.TextProperty, new Binding ("Title") },
						{ TextCell.DetailProperty, new Binding ("InnerCount") { StringFormat = "{0} Ressourse(s)" } }
					}
				},
				ItemsSource = module.Classes
			};

			list.ItemSelected += async (sender, e) => {
				if (e.SelectedItem == null)
					return;
				if (((Class)e.SelectedItem).Steps.Count == 1) {
					string target = ((Class)e.SelectedItem).Steps [0].Step.Fullpath.Replace (" ", "%20");
					Steps.OpenRessource (this, target, ((Class)e.SelectedItem).Steps [0].Title);
					return;
				}
				await Navigation.PushAsync (new Steps (((Class)e.SelectedItem)));
				list.SelectedItem = null;
			};

			Content = list;
		}
	}
}


