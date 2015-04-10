using System;

using Xamarin.Forms;
using System.Collections.Generic;
using Epitech.Intra.API.Data;
using System.Linq;

namespace Epitech.Intra.SharedApp.Views
{
	public class Modules : IntraPage
	{
		public Modules(ELearning Semester)
		{
			try {
				DisplayContent(Semester);
			} catch (Exception ex) {
				DisplayError (ex);
			}
		}

		public void DisplayContent (ELearning Semester)
		{
			base.DisplayContent (Data);

			Title = "Semestre " + Semester.semester.ToString();
			var modules = new List<ModuleElearning>(Semester.dic.Values);

			ListView list = new ListView {
				ItemTemplate = new DataTemplate (typeof(TextCell)) {
					Bindings = {
						{ TextCell.TextProperty, new Binding ("title") },
						{ TextCell.DetailProperty, new Binding ("InnerCount") { StringFormat = "{0} Cour(s) et TP(s)" } }
					}
				},
				ItemsSource = modules
			};

			list.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) => {
				if (e.SelectedItem == null)
					return;
				await Navigation.PushAsync (new Classes (((API.Data.ModuleElearning)e.SelectedItem)));
				list.SelectedItem = null;
			};

			Content = list;
		}
	}
}


