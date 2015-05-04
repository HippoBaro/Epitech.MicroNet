using Epitech.Intra.SharedApp.Views;
using System.Collections.Generic;
using Epitech.Intra.Droid;
using Epitech.Intra.API.Data;

[assembly: Xamarin.Forms.Dependency (typeof(EventManagerDroid))]
namespace Epitech.Intra.Droid
{
	public class EventManagerDroid : IEventManager
	{
		public async void SynchrosizeCalendar (List<Calendar> events)
		{
			return;
		}
	}
}
