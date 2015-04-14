using System;
using System.Collections.Generic;
using System.Linq;

using Epitech.Intra.API.Data;
using System.Collections.ObjectModel;

namespace Epitech.Intra.SharedApp
{
	public class EventFilterOption
	{
		public bool FilderAscending { get; set; }

		public bool OnlyDisplayRegisteredEvent { get; set; }

		public bool OnlyDisplayEventToRegister { get; set; }

		public bool OnlyDisplayEventFromRegisteredModule { get; set; }

		public DateTime FilterStart { get; set; }

		public DateTime FilterEnd { get; set; }

		public bool OnlyDisplayEvent { get; set; }

		public bool DisplayPastEvent { get; set; }
	}

	public class EventGroup : ObservableCollection<Calendar>
	{
		public EventGroup (string grouptitle)
		{
			GroupTitle = grouptitle;
		}

		public string GroupTitle {
			get;
			private set;
		}
	}

	public class EventSorting
	{
		private EventFilterOption Option;
		private List<Calendar> List;

		public EventSorting (EventFilterOption option, List<Calendar> list)
		{
			Option = option;
			List = new List<Calendar> (list);
		}

		private List<Calendar> FilterAndSortList ()
		{
			if (!Option.FilderAscending)
				List.Sort ((a, b) => a.Start.CompareTo (b.Start));
			else
				List.Sort ((a, b) => b.Start.CompareTo (a.Start));

			if (!Option.DisplayPastEvent)
				List = List.FindAll ((x => x.End > DateTime.Now || x.TokenAsked));

			if (Option.OnlyDisplayRegisteredEvent)
				List = List.FindAll ((x => x.EventRegistered != null));
			
			if (Option.OnlyDisplayEventFromRegisteredModule)
				List = List.FindAll ((x => x.ModuleRegistered));

			if (Option.OnlyDisplayEventToRegister)
				List = List.FindAll ((x => x.AllowRegister || x.TokenAsked));

			return List;
		}

		public ObservableCollection<EventGroup> GetGroupedList ()
		{
			List = FilterAndSortList ();

			EventGroup group0 = new EventGroup ("Passés récemment");
			EventGroup grouptoken = new EventGroup ("Token à rentrer");
			EventGroup group1 = new EventGroup ("En ce moment");
			EventGroup group2 = new EventGroup ("Dans les 7 jours");
			EventGroup group3 = new EventGroup ("Inscription possible");
			EventGroup group4 = new EventGroup ("Suivant les 7 jours à venir");

			foreach (var item in List) {
				if (item.TokenAsked)
					grouptoken.Add (item);
				else if (DateTime.Now > item.End)
					group0.Add (item);
				else if (DateTime.Now >= item.Start && DateTime.Now < item.End)
					group1.Add (item);
				else if (DateTime.Now.AddDays (7) >= item.Start)
					group2.Add (item);
				else if (item.AllowRegister && item.EventRegistered == null)
					group3.Add (item);
				else if (DateTime.Now.AddDays (7) < item.Start)
					group4.Add (item);
			}

			ObservableCollection<EventGroup> groups = new ObservableCollection<EventGroup> ();
			if (grouptoken.Count != 0)
				groups.Add (grouptoken);
			if (group0.Count != 0)
				groups.Add (group0);
			if (group1.Count != 0)
				groups.Add (group1);
			if (group2.Count != 0)
				groups.Add (group2);
			if (group3.Count != 0)
				groups.Add (group3);
			if (group4.Count != 0)
				groups.Add (group4);
			return groups;
		}
	}
}

