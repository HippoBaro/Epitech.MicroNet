using System;
using Epitech.Intra.SharedApp.Views;
using Epitech.Intra.iOS;
using UIKit;
using Foundation;
using System.Linq;
using Epitech.Intra.API.Data;
using System.Collections.Generic;
using EventKit;
using System.Threading.Tasks;
using Epitech.Intra.SharedApp;

[assembly: Xamarin.Forms.Dependency (typeof(EventManager_iOS))]
namespace Epitech.Intra.iOS
{
	public class EventManager_iOS : IEventManager_iOS
	{
		public static EKEventStore eventStore;
		const string CalendarIDKey = "CalendarIdForEventSynch";

		public static EKCalendar Calendar {
			get {
				string val = NSUserDefaults.StandardUserDefaults.StringForKey (CalendarIDKey); 
				if (val == null) {
					NSError err;
					EKCalendar cal = EventKit.EKCalendar.Create (EKEntityType.Event, eventStore);
					cal.Title = "Evènements Epitech";
					foreach (EKSource s in eventStore.Sources) {
						if (s.SourceType == EKSourceType.CalDav) {
							cal.Source = s;
							break;
						}
					}
					if (cal.Source == null) {
						foreach (EKSource s in eventStore.Sources) {
							if (s.SourceType == EKSourceType.Local) {
								cal.Source = s;
								break;
							}
						}
					}
					bool didwell = eventStore.SaveCalendar (cal, true, out err);
					if (!didwell)
						throw new Exception ("SaveCalendar failed");
					else {
						NSUserDefaults.StandardUserDefaults.SetString (cal.CalendarIdentifier, CalendarIDKey);
						NSUserDefaults.StandardUserDefaults.Synchronize ();
					}
					return cal;
				} else {
					foreach (var item in eventStore.Calendars) {
						if (item.CalendarIdentifier == val)
							return item;
					}
					throw new Exception ("Calendar not found");
				}
			}
		}

		public async Task SynchrosizeCalendar (List<Calendar> events)
		{
			try {
				await Task.Factory.StartNew (new Action (() => {
					if ((((App)App.Current).HasAllowedEventKit) || (((App)App.Current).UserHasActivatedEventSync)) {
						SelectUnregisteredEvents (events);
						RegisterEvents (events);
						DeleteUnregisteredEvent (events);
					}
				}));
			} catch (Exception ex) {
				Xamarin.Insights.Report (ex);
			}
		}

		public void RegisterEvents (List<Calendar> events)
		{
			foreach (var item in events) {
				if (!item.RegisterEventForStoring)
					continue;
				EKEvent newEvent = EKEvent.FromStore (eventStore);
				newEvent.StartDate = DateTimeToNSDate (item.Start).AddSeconds (-(3600 * 2));
				newEvent.EndDate = DateTimeToNSDate (item.End).AddSeconds (-(3600 * 2));
				newEvent.Title = item.ActiTitle;
				newEvent.Availability = EKEventAvailability.Busy;
				newEvent.Location = item.Room.Code;
				newEvent.Notes = "Type : " + item.TypeTitle + Environment.NewLine + "Module : " + item.Titlemodule;
				newEvent.Calendar = Calendar;
				NSError e;
				bool succes = eventStore.SaveEvent (newEvent, EKSpan.ThisEvent, out e);
				if (succes)
					item.EventKitID = newEvent.EventIdentifier;
				else
					throw new Exception ("RegisterEvent failed");
			}
		}

		private void DeleteUnregisteredEvent (List<Calendar> events)
		{
			NSDate startDate = DateTimeToNSDate (DateTime.Now).AddSeconds (-(3600 * 2));
			NSDate endDate = DateTimeToNSDate (DateTime.Now.AddMonths (1)).AddSeconds (-(3600 * 2));
			NSPredicate query = eventStore.PredicateForEvents (startDate, endDate, new EKCalendar[] { Calendar });
			EKEvent[] queryresult = eventStore.EventsMatching (query);
			if (queryresult == null)
				return;
			EKEvent[] todelete = Array.FindAll (queryresult, x => {
				foreach (var item in events) {
					if (item.EventKitID == x.EventIdentifier)
						return false;
				}
				return true;
			});

			NSError err;
			bool succes;
			foreach (var item in todelete) {
				succes = eventStore.RemoveEvent (item, EKSpan.ThisEvent, true, out err);
				if (!succes)
					throw new Exception ("Delete failed");
			}
		}

		private void SelectUnregisteredEvents (List<Calendar> events)
		{
			foreach (var item in events) {
				NSDate startDate = DateTimeToNSDate (item.Start).AddSeconds (-(3600 * 2));
				NSDate endDate = DateTimeToNSDate (item.End).AddSeconds (-(3600 * 2));
				NSPredicate query = eventStore.PredicateForEvents (startDate, endDate, new EKCalendar[] { Calendar });
				EKEvent[] queryresult = eventStore.EventsMatching (query);
				bool exist = false;
				if (queryresult == null) {
					item.RegisterEventForStoring = true;
					continue;
				}
				foreach (var evt in queryresult) {
					if (evt.Location == item.Room.Code && evt.Title == item.ActiTitle) {
						exist = true;
						item.EventKitID = evt.EventIdentifier;
					}
				}
				if (!exist)
					throw new Exception ("Select failed");
			}
		}

		public static DateTime NSDateToDateTime (NSDate date)
		{
			DateTime reference = new DateTime (2001, 1, 1, 0, 0, 0);
			return reference.AddSeconds (date.SecondsSinceReferenceDate);
		}

		public static NSDate DateTimeToNSDate (DateTime date)
		{
			DateTime reference = new DateTime (2001, 1, 1, 0, 0, 0);
			return NSDate.FromTimeIntervalSinceReferenceDate (
				(date - reference).TotalSeconds);
		}
	}
}
