using System;
using System.Collections.Generic;

using Foundation;
using UIKit;
using OxyPlot.Xamarin.Forms.Platform.iOS;
using Xamarin;
using Epitech.Intra.SharedApp;
using Epitech.Intra.API.Data;
using EventKit;
using Epitech.Intra.SharedApp.Views;

namespace Epitech.Intra.iOS
{
	[Register ("AppDelegate")]
	public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		App MainApp;

		public override bool FinishedLaunching (UIApplication uiApplication, NSDictionary launchOptions)
		{
			Forms.Init ();
			global::Xamarin.Forms.Forms.Init ();

			MainApp = new App ();

			LoadApplication (MainApp);

			EventManagerIOS.EventStore.RequestAccess (EKEntityType.Event, (granted, e) => {
				MainApp.HasAllowedEventKit = granted;
			});

			App.ScreenWidth = (int)UIScreen.MainScreen.Bounds.Width;
			App.ScreenHeight = (int)UIScreen.MainScreen.Bounds.Height;

			UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval (UIApplication.BackgroundFetchIntervalMinimum);
			UIApplication.SharedApplication.RegisterUserNotificationSettings (UIUserNotificationSettings.GetSettingsForTypes (UIUserNotificationType.Badge | UIUserNotificationType.Alert, null));

			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;

			if (launchOptions != null) {
				if (launchOptions.ContainsKey (UIApplication.LaunchOptionsLocalNotificationKey)) {
					var localNotification = launchOptions [UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
					if (localNotification != null) {
						OpenNotification (localNotification);
						UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
					}
				}
			}

			return base.FinishedLaunching (uiApplication, launchOptions);
		}

		void OpenNotification (UILocalNotification notif)
		{
			if (MainApp != null && MainApp.IsUserConnected) {
				if (notif.UserInfo != null) {
					List<Epitech.Intra.API.HTMLCleaner.LinkItem> Links = new List<Epitech.Intra.API.HTMLCleaner.LinkItem> ();
					foreach (var item in notif.UserInfo) {
						Links.Add (new Epitech.Intra.API.HTMLCleaner.LinkItem { Href = ((NSString)item.Value).ToString () });
					}
					((Notifications)MainApp.Root.JumpToPage<Notifications> ()).OpenNotification (Links);
				} else {
					MainApp.Root.JumpToPage<Notifications> ();
				}
			}
		}

		public override void ReceivedLocalNotification (UIApplication application, UILocalNotification notification)
		{
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
			OpenNotification (notification);
		}

		public override async void PerformFetch (UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
		{
			try {
				if (MainApp != null && MainApp.IsUserConnected) {
					var handle = Insights.TrackTime ("TimeToFetch");
					handle.Start ();
					foreach (var item in RootMaster.MenuTabs) {
						if (item.PageType == typeof(Profile) || item.PageType == typeof(ELearning) || item.PageType == typeof(Projets))
							continue;
						if (item.PageType == typeof(Notifications)) {
							await ((Notifications)item.Page).SilentUpdateForNotification (null);
							FireNotification (((Notifications)item.Page).News);
						} else
							await item.Page.SilentUpdate (null);
					}
					handle.Stop ();
					completionHandler (UIBackgroundFetchResult.NewData);
					return;
				}
			} catch (Exception ex) {
				Insights.Report (ex);
				completionHandler (UIBackgroundFetchResult.Failed);
			}
		}

		static void FireNotification (IList<Notification> news)
		{
			UILocalNotification notification = new UILocalNotification ();
			notification.FireDate = NSDate.Now.AddSeconds (10);
			notification.ApplicationIconBadgeNumber = UIApplication.SharedApplication.ApplicationIconBadgeNumber + news.Count;

			if (news.Count > 1) {
				notification.AlertAction = "Notification Intranet";
				notification.AlertBody = news.Count + " nouvelles notifications.";
			} else if (news.Count == 1) {
				notification.AlertAction = "Notification Intranet";
				notification.AlertBody = API.HTMLCleaner.RemoveLinksKeepText (news [0].Title);
				NSMutableDictionary LinkDic = new NSMutableDictionary ();
				int i = 0;
				foreach (var item in news[0].Links) {
					LinkDic.SetValueForKey (NSObject.FromObject (item.Href), new NSString ((i++).ToString ()));
				}
				notification.UserInfo = LinkDic;
			}

			UIApplication.SharedApplication.ScheduleLocalNotification (notification);
		}
	}
}

