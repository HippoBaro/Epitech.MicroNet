using Xamarin.Forms;
using Epitech.Intra.API.Data;

namespace Epitech.Intra.SharedApp
{
	public class CreditHeader : StackLayout
	{
		public CreditHeader (User user)
		{
			if (user.Gpa == null) {
				return;
			}
				
			StackLayout GPA = new StackLayout {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center,
				BackgroundColor = IntraColor.LightGray,
				Padding = new Thickness (10, 0, 10, 0),
				Children = {
					new Label {
						Text = "GPA :",
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.Center,
						FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
						TextColor = Color.White,
					},
					new Label {
						Text = user.Gpa [user.Gpa.Length - 1].GPA.ToString (),
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.Center,
						FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
						FontAttributes = FontAttributes.Bold,
						TextColor = Color.White,
					},
					new Label {
						Text = "Moyenne : " + user.AverageGPA [user.Gpa.Length - 1].GpaAverage,
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.Center,
						FontSize = Device.GetNamedSize (NamedSize.Micro, typeof(Label)),
						XAlign = TextAlignment.Center,
						TextColor = Color.White,
					}
				}
			};

			StackLayout Credit = new StackLayout {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Center,
				Children = {
					new Label {
						XAlign = TextAlignment.Center,
						Text = "Crédit acquis : " + user.Credits,
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.Center,
						FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
						TextColor = Color.White,
					},
					new Label {
						XAlign = TextAlignment.Center,
						Text = "En cours d'acquisition : " + CalcCreditGoal (user),
						FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.Center,
						TextColor = Color.White,
					}
				}
			};

			BackgroundColor = IntraColor.LightBlue;
			Padding = new Thickness (10, 0, 10, 0);
			Children.Add (GPA);
			Children.Add (Credit);
			Orientation = StackOrientation.Horizontal;
			HorizontalOptions = LayoutOptions.Fill;
		}

		static int CalcCreditGoal (User user)
		{
			int result = 0;

			if (user.Marks == null)
				return 0;
			
			foreach (var item in user.Marks.Modules) {
				if (item.Grade == "-")
					result += item.Credits;
			}
			return result;
		}
	}
}


