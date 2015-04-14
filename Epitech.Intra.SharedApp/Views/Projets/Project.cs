using System;

using Xamarin.Forms;

using Epitech.Intra.API.Data.ProjectJsonTypes;
using System.Collections.Generic;

namespace Epitech.Intra.SharedApp.Views
{
	public class ProjectGroupCell : ViewCell
	{
		private Registered group;

		readonly StackLayout Main;
		readonly StackLayout members;
		readonly Label name;

		protected override void OnBindingContextChanged ()
		{
			group = (Registered)BindingContext;
			DrawCell ();
			base.OnBindingContextChanged ();
		}

		private void DrawCell ()
		{
			members.Children.Clear ();
			if (group.Master.Login == App.API.Login)
				View.BackgroundColor = Color.Gray;
			members.Children.Add (new UserBox (group.Master.Title, group.Master.Login, API.PictureHelper.GetUserPictureUri (group.Master.Picture, group.Master.Login, Epitech.Intra.API.PictureHelper.PictureSize.VeryLight), true, IntraColor.LightBlue, Color.White));
			foreach (var item in group.Members) {
				if (item.Login == App.API.Login) {
					View.BackgroundColor = IntraColor.LightGray;
					members.Children.Add (new UserBox (item.Title, item.Login, API.PictureHelper.GetUserPictureUri (item.Picture, item.Login, Epitech.Intra.API.PictureHelper.PictureSize.VeryLight), true, Color.Transparent, Color.White));
				} else
					members.Children.Add (new UserBox (item.Title, item.Login, API.PictureHelper.GetUserPictureUri (item.Picture, item.Login, Epitech.Intra.API.PictureHelper.PictureSize.VeryLight), true, Color.Transparent, Color.Black));
			}
			name.Text = group.Title;
		}

		public ProjectGroupCell ()
		{
			Main = new StackLayout {
				Padding = new Thickness (10, 5, 0, 0),
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Vertical,
			};

			members = new StackLayout {
				Orientation = StackOrientation.Horizontal
			};

			name = new Label {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
			};

			Main.Children.Add (name);
			Main.Children.Add (new ScrollView { Content = members, Orientation = ScrollOrientation.Horizontal });

			View = Main;
		}
	}

	public sealed class Project : IntraPage
	{
		public Project (API.Data.Project project)
		{
			try {
				DisplayContent (project);
			} catch (Exception ex) {
				DisplayError (ex);
			}
		}

		private void DisplayContent (API.Data.Project project)
		{
			Title = project.Title;
			Registered myproj = Array.Find<Registered> (project.Registered, x => x.Code == project.UserProjectCode);
			List<Registered> registeredStudent = new List<Registered> (project.Registered);
			registeredStudent.Remove (myproj);
			registeredStudent.Insert (0, myproj);

			if (!(registeredStudent.Count == 1 && registeredStudent [0] == null)) {
				ListView listView = new ListView {
					ItemTemplate = new DataTemplate (typeof(ProjectGroupCell)),
					Header = new FilesHeader (this, project),
					ItemsSource = registeredStudent,
					HasUnevenRows = true,
					VerticalOptions = LayoutOptions.FillAndExpand,
					IsEnabled = false,
				};
				listView.ItemSelected += (sender, e) => {
					if (e.SelectedItem != null)
						listView.SelectedItem = null;
				};
				Content = listView;
			} else {
				Content = new FilesHeader (this, project);
			}
		}
	}
}


