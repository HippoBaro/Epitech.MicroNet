using System;
using Epitech.Intra.SharedApp.Views;
using Epitech.Intra.iOS;
using Foundation;
using Epitech.Intra.API.Data;
using System.Collections.Generic;
using EventKit;
using System.Threading.Tasks;
using Epitech.Intra.SharedApp;

[assembly: Xamarin.Forms.Dependency (typeof(EventManagerIOS))]
namespace Epitech.Intra.iOS
{
	public class EventManagerIOS : IEventManagerIOS
	{
		static protected EKEventStore eventStore;

		static public EKEventStore EventStore {
			get {
				if (eventStore == null)
					eventStore = new EKEventStore ();
				return eventStore; 
			}
		}

		const string CalendarIDKey = "CalendarIdForEventSynch";

		public static EKCalendar Calendar {
			get {
				string val = NSUserDefaults.StandardUserDefaults.StringForKey (CalendarIDKey); 
				if (val == null) {
					return CreateNewCalendar ();
				} else {
					foreach (var item in EventStore.GetCalendars(EKEntityType.Event)) {
						if (item.CalendarIdentifier == val)
							return item;
					}
					return CreateNewCalendar ();
				}
			}
		}

		static EKCalendar CreateNewCalendar ()
		{
			NSError err;
			EKCalendar cal = EKCalendar.Create (EKEntityType.Event, EventStore);
			cal.Title = "Evènements Epitech";
			foreach (EKSource s in EventStore.Sources) {
				if (s.SourceType == EKSourceType.CalDav) {
					cal.Source = s;
					break;
				}
			}
			if (cal.Source == null) {
				foreach (EKSource s in EventStore.Sources) {
					if (s.SourceType == EKSourceType.Local) {
						cal.Source = s;
						break;
					}
				}
			}
			bool didwell = EventStore.SaveCalendar (cal, true, out err);
			if (!didwell)
				throw new Exception ("SaveCalendar failed");
			NSUserDefaults.StandardUserDefaults.SetString (cal.CalendarIdentifier, CalendarIDKey);
			NSUserDefaults.StandardUserDefaults.Synchronize ();
			return cal;
		}

		public async void SynchrosizeCalendar (List<Calendar> events)
		{
			if ((((App)Xamarin.Forms.Application.Current).HasAllowedEventKit) && (((App)Xamarin.Forms.Application.Current).UserHasActivatedEventSync)) {
				await Task.Factory.StartNew (new Action (() => {
					try {
						SelectUnregisteredEvents (events);
						RegisterEvents (events);
						DeleteUnregisteredEvent (events);
					} catch (Exception ex) {
						Xamarin.Insights.Report (ex);
						return;
					}
				}));
			}
			return;
		}

		public static void RegisterEvents (List<Calendar> events)
		{
			foreach (var item in events) {
				if (!item.RegisterEventForStoring)
					continue;
				EKEvent newEvent = EKEvent.FromStore (EventStore);
				newEvent.StartDate = DateTimeToNSDate (item.Start).AddSeconds (-(3600 * 2));
				newEvent.EndDate = DateTimeToNSDate (item.End).AddSeconds (-(3600 * 2));
				newEvent.Title = item.ActiTitle;
				newEvent.Availability = EKEventAvailability.Busy;
				newEvent.Location = item.Room.Code ?? String.Empty;
				newEvent.Notes = "Type : " + item.TypeTitle + Environment.NewLine + "Module : " + item.Titlemodule;
				newEvent.Calendar = Calendar;
				NSError e;
				bool succes = EventStore.SaveEvent (newEvent, EKSpan.ThisEvent, out e);
				if (succes)
					item.EventKitID = newEvent.EventIdentifier;
				else
					throw new Exception ("RegisterEvent failed");
			}
		}

		static void DeleteUnregisteredEvent (List<Calendar> events)
		{
			NSDate startDate = DateTimeToNSDate (DateTime.Now).AddSeconds (-(3600 * 2));
			NSDate endDate = DateTimeToNSDate (DateTime.Now.AddMonths (1)).AddSeconds (-(3600 * 2));
			NSPredicate query = EventStore.PredicateForEvents (startDate, endDate, new [] { Calendar });
			EKEvent[] queryresult = EventStore.EventsMatching (query);
			if (queryresult == null)
				return;
			EKEvent[] todelete = Array.FindAll (queryresult, x => {
				foreach (var item in events) {
					if (item.EventKitID == x.EventIdentifier || item.Past)
						return false;
				}
				return true;
			});

			NSError err;
			bool succes;
			foreach (var item in todelete) {
				succes = EventStore.RemoveEvent (item, EKSpan.ThisEvent, true, out err);
				if (!succes)
					throw new Exception ("Delete failed");
			}
		}

		static void SelectUnregisteredEvents (List<Calendar> events)
		{
			foreach (var item in events) {
				NSDate startDate = DateTimeToNSDate (item.Start).AddSeconds (-(3600 * 2));
				NSDate endDate = DateTimeToNSDate (item.End).AddSeconds (-(3600 * 2));
				NSPredicate query = EventStore.PredicateForEvents (startDate, endDate, new [] { Calendar });
				EKEvent[] queryresult = EventStore.EventsMatching (query);
				bool exist = false;
				if (queryresult == null) {
					item.RegisterEventForStoring = true;
					continue;
				}
				foreach (var evt in queryresult) {
					if (evt.Location == (item.Room.Code ?? String.Empty) && evt.Title == item.ActiTitle) {
						exist = true;
						item.EventKitID = evt.EventIdentifier;
					}
				}
				item.RegisterEventForStoring |= !exist;
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
