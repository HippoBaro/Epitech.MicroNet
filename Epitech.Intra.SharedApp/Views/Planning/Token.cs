using Xamarin.Forms;
using Epitech.Intra.API.Data;
using System.Threading.Tasks;

namespace Epitech.Intra.SharedApp
{
	public class Token : IntraPage
	{
		readonly Label ratelabel;

		int rateshadow;

		int Rate {
			get {
				return rateshadow;
			}
			set {
				if (Rate != value) {
					ratelabel.Text = "Note : " + value + "/5";
					rateshadow = value;
				}
				return;
			}
		}

		public Token (Calendar activity)
		{
			Title = "Comfirmer sa présence";

			Entry tokenval = new Entry {
				Placeholder = "00000000",
				Keyboard = Keyboard.Numeric,
			};

			tokenval.TextChanged += (sender, e) => {
				if (e.NewTextValue.Length > 8)
					tokenval.Text = e.OldTextValue;
			};

			ratelabel = new Label { Text = "Note : 0/5" };

			Slider rate = new Slider {
				Minimum = 0,
				Maximum = 5,
				Value = 0,
			};

			rate.ValueChanged += (sender, e) => {
				Rate = (int)e.NewValue;
			};

			Label errorLabel = new Label {
				TextColor = Color.Red,
				XAlign = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.Center
			};

			Button abort = new Button {
				Text = "Annuler",
				Style = (Style)Application.Current.Resources ["ButtonSkinned"],
				WidthRequest = 120,
				HorizontalOptions = LayoutOptions.Start
			};

			abort.Clicked += (sender, e) => Navigation.PopModalAsync ();

			Button validate = new Button {
				Text = "Valider",
				Style = (Style)Application.Current.Resources ["ButtonSkinned"],
				WidthRequest = 120,
				HorizontalOptions = LayoutOptions.EndAndExpand
			};

			validate.Clicked += async (sender, e) => {
				if (tokenval.Text == null || tokenval.Text.Length != 8) {
					errorLabel.Text = "Merci de rentrer un token valide.";
					return;
				}
				TokenResponse res = await App.API.TryValidateToken (activity, new Epitech.Intra.API.Data.Token {
					TokenValue = tokenval.Text,
					Rate = this.Rate,
					Comment = string.Empty
				});

				if (res.Error != null)
					errorLabel.Text = res.Error;
				else {
					errorLabel.TextColor = Color.Green;
					errorLabel.Text = "Token comfirmé !";
					activity.EventRegistered = "present";
					await Task.Delay (500);
					await Navigation.PopModalAsync ();
				}
			};

			StackLayout buttonstack = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = {
					abort,
					validate
				}
			};

			StackLayout root = new StackLayout {
				Padding = new Thickness (20, 20, 20, 20),
				VerticalOptions = LayoutOptions.Center,
				Children = {
					new Label { Text = "Token : " },
					tokenval,
					ratelabel,
					rate,
					errorLabel,
					buttonstack
				}
			};

			Content = root;
		}
	}
}


