using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Epitech.Intra.SharedApp.Views
{
	public class MenuCell : ViewCell
	{
		private Tab element;

		protected override void OnBindingContextChanged ()
		{
			element = (Tab)BindingContext;
			DrawCell ();
			base.OnBindingContextChanged ();
		}

		private void DrawCell ()
		{
			var root = new StackLayout () {
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness (10, 5, 10, 5)
			};

			Image Img = new Image () {
				Source = ImageSource.FromFile(element.Image),
				VerticalOptions = LayoutOptions.Center,
				HeightRequest = 40,
				WidthRequest = 40
			};

			StackLayout Main = new StackLayout () {
				Padding = new Thickness (10, 0, 0, 0),
				Spacing = -2,
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center,
				Orientation = StackOrientation.Vertical,
			};

			Label name = new Label () {
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

			Label desc = new Label () {
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

	public class Disconnection : IntraPage { }

	public class RootMaster : MasterDetailPage
	{
		public static List<Tab> MenuTabs = new List<Tab>();
		public ListView listView;

		public void CreateChidrens()
		{
			MenuTabs.Clear ();
			GC.Collect ();
			GC.WaitForPendingFinalizers ();
			MenuTabs.Add (new Tab ("Profil", typeof(Profile)) {
				Image = "MenuIcons/profile.png",
				Description = "Me, Myself and I"
			});
			MenuTabs.Add (new Tab ("Notifications", typeof(Notifications)) {
				Image = "MenuIcons/notifications.png",
				Description = "Quoi de neuf ?"
			});
			MenuTabs.Add (new Tab ("Planning", typeof(Planning)) {
				Image = "MenuIcons/calendar.png",
				Description = "Vos évènements"
			});
			MenuTabs.Add (new Tab ("Projets", typeof(Projets)) {
				Image = "MenuIcons/projects.png",
				Description = "Mes projets Epitech"
			});
			MenuTabs.Add (new Tab ("Trombi", typeof(Trombi)) {
				Image = "MenuIcons/crowd.png",
				Description = "Chercher des membres du réseau IONIS"
			});
			MenuTabs.Add (new Tab ("E-Learning", typeof(Semester)) {
				Image = "MenuIcons/elearning.png",
				Description = "« Mèdeis ageômetrètos eisitô mou tèn stegèn »"
			});
			MenuTabs.Add (new Tab ("A propos", typeof(Informations)) {
				Image = "MenuIcons/info.png",
				Description = "En savoir plus"
			});
			MenuTabs.Add (new Tab ("Déconnexion", typeof(Disconnection)) {
				Image = "MenuIcons/disconnect.png",
				Description = "Quelqu'un est jaloux et veux tester l'app ?"
			});
		}

		public RootMaster ()
		{
			this.IsPresented = false;
			if (Device.Idiom == TargetIdiom.Phone)
				this.MasterBehavior = MasterBehavior.SplitOnLandscape;
			else
				this.MasterBehavior = MasterBehavior.Split;

			this.Master = new ContentPage {
				BackgroundColor = IntraColor.DarkGray,
				Title = "Menu",
				Icon = new FileImageSource () { File = "menu24.png" }
			};
			this.Detail = new Page();
		}

		private async void Disconnect ()
		{
			string[] addbut = new string[1];
			string action = await DisplayActionSheet ("Voulez-vous vraiment vous déconnecter ?" + Environment.NewLine + "Les données ne seront plus synchronisés.", "Annuler", "Me déconnecter", addbut);
			if (action == "Me déconnecter") {
				this.IsPresented = false;
				App.API.ForgetCredit ();
				await DependencyService.Get<Security.ISecurity> ().DeleteItemAsync ();
				((App)App.Current).IsUserConnected = false;
				((App)App.Current).UserLogin = null;
				((App)App.Current).User = null;
				this.Detail = new Page ();
				GC.Collect ();
				((App)App.Current).DisplayLoginScreen();
			}
		}

		public void DrawMenu()
		{	
			listView = new ListView {
				ItemTemplate = new DataTemplate (typeof(MenuCell)),
				SeparatorVisibility = SeparatorVisibility.None,
				ItemsSource = MenuTabs,
				VerticalOptions = LayoutOptions.Start,
				BackgroundColor = Color.Transparent,
				HasUnevenRows = true,
				Header = new MenuHeader(),
			};

			listView.ItemSelected += (sender, args) => {
				if (args.SelectedItem == null)
					return;
				if (((Tab)listView.SelectedItem).PageType == typeof(Disconnection))
				{
					Disconnect();
					return;
				}
				this.Detail = ((Tab)listView.SelectedItem).Nav;
				listView.SelectedItem = null;
				if (Device.Idiom == TargetIdiom.Phone)
					this.IsPresented = false;
			};

			listView.SelectedItem = MenuTabs [0];

			this.Master = new ContentPage {
				BackgroundColor = IntraColor.DarkGray,
				Title = "Menu",
				Icon = new FileImageSource () { File = "menu24.png" },
				Content = listView
			};
		}

		public IntraPage JumpToPage(Type PageType)
		{
			foreach (var item in MenuTabs) {
				if (item.PageType == PageType) {
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
			this.Name = name;
			this.PageType = page;

			if (PageType == typeof(Profile)) {
				string[] profilearg = new string[1];
				profilearg [0] = App.API.login;
				Page = (IntraPage)Activator.CreateInstance (PageType, profilearg);
				Nav = new NavigationPage (Page);
				Nav.Icon = new FileImageSource () { File = "menu.png" };
			} else {
				Page = (IntraPage)Activator.CreateInstance (PageType);
				Nav = new NavigationPage (Page);
				Nav.Icon = new FileImageSource () { File = "menu.png" };
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
