using System;

using Xamarin.Forms;
using System.Collections.Generic;
using Epitech.Intra.API.Data;

namespace Epitech.Intra.SharedApp.Views
{
	public sealed class Modules : IntraPage
	{
		public Modules (ELearning semester)
		{
			try {
				DisplayContent (semester);
			} catch (Exception ex) {
				DisplayError (ex);
			}
		}

		public void DisplayContent (ELearning semester)
		{
			base.DisplayContent (Data);

			Title = "Semestre " + semester.Semester;
			var modules = new List<ModuleElearning> (semester.Dic.Values);

			ListView list = new ListView {
				ItemTemplate = new DataTemplate (typeof(TextCell)) {
					Bindings = {
						{ TextCell.TextProperty, new Binding ("Title") },
						{ TextCell.DetailProperty, new Binding ("InnerCount") { StringFormat = "{0} Cour(s) et TP(s)" } }
					}
				},
				ItemsSource = modules
			};

			list.ItemSelected += async (sender, e) => {
				if (e.SelectedItem == null)
					return;
				await Navigation.PushAsync (new Classes (((ModuleElearning)e.SelectedItem)));
				list.SelectedItem = null;
			};

			Content = list;
		}
	}
}


