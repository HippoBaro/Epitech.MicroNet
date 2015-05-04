using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Epitech.Intra.SharedApp.Views
{
	public class MenuCell : ViewCell
	{
		Tab element;

		protected override void OnBindingContextChanged ()
		{
			element = (Tab)BindingContext;
			DrawCell ();
			base.OnBindingContextChanged ();
		}

		void DrawCell ()
		{
			var root = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness (10, 5, 10, 5)
			};

			Image Img = new Image {
				Source = ImageSource.FromFile (element.Image),
				VerticalOptions = LayoutOptions.Center,
				HeightRequest = 40,
				WidthRequest = 40
			};

			StackLayout Main = new StackLayout {
				Padding = new Thickness (10, 0, 0, 0),
				Spacing = -2,
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center,
				Orientation = StackOrientation.Vertical,
			};

			Label name = new Label {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
				TextColor = IntraColor.LightBlue,
				Text = element.Name,
			};

			if (name.Text == "Déconnexion") {
				name.TextColor = Color.FromHex ("FF8080");
			}

			Label desc = new Label {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize (NamedSize.Micro, typeof(Label)),
				FontAttributes = FontAttributes.Italic,
				TextColor = Color.White,
				Text = element.Description,
			};

			root.Padding = new Thickness (10, 5, 0, 5);
			Main.Children.Add (name);
			Main.Children.Add (desc);
			root.Children.Add (Img);
			root.Children.Add (Main);

			View = root;

		}
	}

	public class Disconnection : IntraPage
	{

	}

	public class RootMaster : MasterDetailPage
	{
		public static List<Tab> MenuTabs = new List<Tab> ();
		public ListView ListView;

		public static void CreateChidrens ()
		{
			MenuTabs.Clear ();
			GC.Collect ();
			GC.WaitForPendingFinalizers ();
			MenuTabs.Add (new Tab ("Profil", typeof(Profile)) {
				Image = "menuiconsprofile.png",
				Description = "Me, Myself and I"
			});
			MenuTabs.Add (new Tab ("Notifications", typeof(Notifications)) {
				Image = "menuiconsnotifications.png",
				Description = "Quoi de neuf ?"
			});
			MenuTabs.Add (new Tab ("Planning", typeof(Planning)) {
				Image = "menuiconscalendar.png",
				Description = "Vos évènements"
			});
			MenuTabs.Add (new Tab ("Projets", typeof(Projets)) {
				Image = "menuiconsprojects.png",
				Description = "Mes projets Epitech"
			});
			MenuTabs.Add (new Tab ("Trombi", typeof(Trombi)) {
				Image = "menuiconscrowd.png",
				Description = "Chercher des membres du réseau IONIS"
			});
			MenuTabs.Add (new Tab ("E-Learning", typeof(Semester)) {
				Image = "menuiconselearning.png",
				Description = "« Mèdeis ageômetrètos eisitô mou tèn stegèn »"
			});
			MenuTabs.Add (new Tab ("A propos", typeof(Informations)) {
				Image = "menuiconsinfo.png",
				Description = "En savoir plus"
			});
			MenuTabs.Add (new Tab ("Déconnexion", typeof(Disconnection)) {
				Image = "menuiconsdisconnect.png",
				Description = "Quelqu'un est jaloux et veux tester l'app ?"
			});
		}

		public RootMaster ()
		{
			IsPresented = false;
			MasterBehavior = Device.Idiom == TargetIdiom.Phone ? MasterBehavior.SplitOnLandscape : MasterBehavior.Split;

			Master = new ContentPage {
				BackgroundColor = IntraColor.DarkGray,
				Title = "Menu",
				Icon = new FileImageSource { File = "menu24.png" }
			};
			Detail = new Page ();
		}

		async void Disconnect ()
		{
			var action = await DisplayAlert ("Deconnexion", "Voulez-vous vraiment vous déconnecter ?" + Environment.NewLine + "Les données ne seront plus synchronisés.", "Me deconnecter", "Annuler");
			if (action) {
				IsPresented = false;
				App.API.ForgetCredit ();
				await DependencyService.Get<Security.ISecurity> ().DeleteItemAsync ();
				((App)Application.Current).IsUserConnected = false;
				((App)Application.Current).UserLogin = null;
				((App)Application.Current).User = null;
				Detail = new Page ();
				GC.Collect ();
				((App)Application.Current).DisplayLoginScreen ();
			}
		}

		public void DrawMenu ()
		{	
			ListView = new ListView {
				ItemTemplate = new DataTemplate (typeof(MenuCell)),
				SeparatorVisibility = SeparatorVisibility.None,
				ItemsSource = MenuTabs,
				VerticalOptions = LayoutOptions.Start,
				BackgroundColor = Color.Transparent,
				HasUnevenRows = true,
				Header = new MenuHeader (),
			};

			ListView.HorizontalOptions = LayoutOptions.Fill;
			ListView.VerticalOptions = LayoutOptions.Fill;

			ListView.ItemSelected += (sender, args) => {
				if (args.SelectedItem == null)
					return;
				Xamarin.Insights.Track ("Moved to " + ((Tab)ListView.SelectedItem).Name, null);
				if (((Tab)ListView.SelectedItem).PageType == typeof(Disconnection)) {
					Disconnect ();
					return;
				}
				Detail = ((Tab)ListView.SelectedItem).Nav;
				ListView.SelectedItem = null;
				IsPresented &= Device.Idiom != TargetIdiom.Phone;
			};

			ListView.SelectedItem = MenuTabs [0];

			Master = new ContentPage {
				BackgroundColor = IntraColor.DarkGray,
				Title = "Menu",
				Icon = new FileImageSource { File = "menu24.png" },
				Content = ListView
			};
		}

		public IntraPage JumpToPage (Type pageType)
		{
			foreach (var item in MenuTabs) {
				if (item.PageType == pageType) {
					Detail = new NavigationPage (item.Page);
					return item.Page;
				}
			}
			return (null);
		}
	}

	public class Tab
	{
		public Tab (string name, Type page)
		{
			Name = name;
			PageType = page;

			if (PageType == typeof(Profile)) {
				string[] profilearg = new string[1];
				profilearg [0] = App.API.Login;
				Page = (IntraPage)Activator.CreateInstance (PageType, profilearg);
				Nav = new NavigationPage (Page);
				Nav.Icon = new FileImageSource { File = "menu.png" };
			} else {
				Page = (IntraPage)Activator.CreateInstance (PageType);
				Nav = new NavigationPage (Page);
				Nav.Icon = new FileImageSource { File = "menu.png" };
			}
		}

		public string Name { private set; get; }

		public string Description { set; get; }

		public string Image { set; get; }

		public NavigationPage Nav { private set; get; }

		public Type PageType { private set; get; }

		public IntraPage Page { private set; get; }

		public override string ToString ()
		{
			return Name;
		}
	}
}
