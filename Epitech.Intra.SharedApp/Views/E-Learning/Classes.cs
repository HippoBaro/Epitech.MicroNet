using System;

using Xamarin.Forms;
using Epitech.Intra.API.Data;
using System.Collections.Generic;

namespace Epitech.Intra.SharedApp
{
	public class Classes : IntraPage
	{
		public Classes (ModuleElearning Module)
		{
			try {
				DisplayContent (Module);
			} catch (Exception ex) {
				DisplayError (ex);
			}
		}

		public void DisplayContent (ModuleElearning Module)
		{
			base.DisplayContent (Data);

			Title = Module.title;
			ListView list = new ListView {
				ItemTemplate = new DataTemplate (typeof(TextCell)) {
					Bindings = {
						{ TextCell.TextProperty, new Binding ("title") },
						{ TextCell.DetailProperty, new Binding ("InnerCount") { StringFormat = "{0} Ressourse(s)" } }
					}
				},
				ItemsSource = Module.classes
			};

			list.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) => {
				if (e.SelectedItem == null)
					return;
				if (((API.Data.Class)e.SelectedItem).steps.Count == 1)
				{
					string target = ((API.Data.Class)e.SelectedItem).steps[0].step.fullpath.Replace (" ", "%20");
					Steps.OpenRessource(this, target, ((API.Data.Class)e.SelectedItem).steps[0].title);
					return;
				}
				await Navigation.PushAsync (new Steps (((API.Data.Class)e.SelectedItem)));
				list.SelectedItem = null;
			};

			Content = list;
		}
	}
}


