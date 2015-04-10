using System;

using Xamarin.Forms;
using Epitech.Intra.API.Data;
using System.Collections.Generic;

namespace Epitech.Intra.SharedApp
{
	public class HeaderPlaning : StackLayout
	{
		public HeaderPlaning (List<Calendar> calendar)
		{
			var registeredEvent = new List<Calendar> (calendar).FindAll (x => x.EventRegistered == "registered");
			var nextevent = new List<Calendar> (calendar).FindAll (x => x.EventRegistered == "registered").FindAll(x => x.Start < DateTime.Now.AddDays(7) && x.Start > DateTime.Now);
			nextevent.Sort ((x, y) => DateTime.Compare (y.Start, x.Start));
			registeredEvent.Sort ((x, y) => DateTime.Compare (y.Start, x.Start));

			if (registeredEvent.Count == 0)
				return;
			Label Name = new Label () {
				Text = "Prochain RDV : " + registeredEvent [0].ActiTitle,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
				TextColor = Color.White
			};
			Label Date = new Label () {
				Text = "Le : " + registeredEvent [0].Start.ToString (),
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
				TextColor = Color.White
			};
			TimeSpan logest = TimeSpan.Zero;
			foreach (var item in nextevent) {
				logest += (item.End - item.Start);
			}
			Label Log = new Label () {
				Text = "Log estimé cette semaine : " + ((int)logest.TotalHours).ToString () + ":" + logest.Minutes + "h / 70h",
				FontSize = Device.GetNamedSize (NamedSize.Micro, typeof(Label)),
				TextColor = Color.White
			};
			var progress = new ProgressBar () {
				Progress = logest.TotalHours / 70f,
				HorizontalOptions = LayoutOptions.Fill
			};

			Padding = new Thickness (10, 10, 10, 10);
			BackgroundColor = IntraColor.LightGray;
			Children.Add (Name);
			Children.Add (Date);
			Children.Add (Log);
			Children.Add (progress);
		}
	}
}


