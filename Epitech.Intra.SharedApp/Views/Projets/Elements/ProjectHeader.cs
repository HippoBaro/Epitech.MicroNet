using System;

using Xamarin.Forms;
using Epitech.Intra.API.Data;

namespace Epitech.Intra.SharedApp
{
	public class ProjectHeader : StackLayout
	{
		public ProjectHeader (Project project)
		{

			StackLayout info = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness (5, 10, 5, 10)
			};

			info.Children.Add (new Label {
				Text = project.ProjectTitle,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label))
			});
			info.Children.Add (new Label {
				Text = project.ModuleTitle,
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label))
			});
			info.Children.Add (new Label {
				Text = project.End.ToString (),
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label))
			});

			Children.Add (info);

			if (project.Description != String.Empty) {
				StackLayout desc = new StackLayout {
					Orientation = StackOrientation.Vertical,
					Padding = new Thickness (0, 10, 0, 10),
					BackgroundColor = Color.Gray,
					Children = { new Label {
							Text = API.HTMLCleaner.Clean (project.Description),
							XAlign = TextAlignment.Center,
							HorizontalOptions = LayoutOptions.Fill,
							FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label))
						}
					}
				};

				Children.Add (desc);
			}
		}
	}
}


