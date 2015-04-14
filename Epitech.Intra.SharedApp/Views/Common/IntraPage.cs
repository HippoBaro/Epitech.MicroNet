using System;

using Xamarin.Forms;
using Epitech.Intra.SharedApp.Views;
using System.Threading.Tasks;
using Xamarin;
using System.Collections.Generic;
using Epitech.Intra.API.Data;

namespace Epitech.Intra.SharedApp
{
	public class IntraPage : ContentPage
	{
		private DateTime LastUpdate;
		private TimeSpan DataInvalidation;
		private Type Type;
		public object Data;

		bool FunctionHasParam;
		Func<string, Task<object>> FunctionWithParam;
		Func<Task<object>> Function;

		public bool IsDataInvalidated (DateTime time)
		{
			return DateTime.Compare (time + DataInvalidation, DateTime.Now) < 0;
		}

		public void InitIntraPage (Type type, Func<string, Task<object>> function, TimeSpan dataInvalidation, string dat)
		{
			DataInvalidation = dataInvalidation;
			Type = type;
			FunctionWithParam = function;
			FunctionHasParam = true;
		}

		public void InitIntraPage (Type type, Func<Task<object>> function, TimeSpan dataInvalidation)
		{
			DataInvalidation = dataInvalidation;
			Type = type;
			Function = function;
			FunctionHasParam = false;
		}

		public virtual async Task<object> SilentUpdate (string param)
		{
			if (Function != null || FunctionWithParam != null)
				try {
					if (FunctionHasParam) {
						if (param != null && FunctionWithParam != null) {
							Data = await FunctionWithParam (param);
							LastUpdate = DateTime.Now;
						}
					} else {
						if (Function != null) {
							Data = await Function ();
							LastUpdate = DateTime.Now;
						}
					}
					if (Type == typeof(Planning)) {
						DependencyService.Get<IEventManagerIOS> ().SynchrosizeCalendar (((List<Calendar>)Data).FindAll (x => !x.Past && x.EventRegistered != null));
					}
				} catch (Exception ex) {
					Insights.Report (ex);
					throw ex;
				}
			return Data;
		}

		public async Task RefreshData (bool forceFetch, string dat)
		{
			IsBusy = true;
			try {
				if (IsDataInvalidated (LastUpdate) || Data == null || forceFetch) {
					if (!forceFetch)
						Content = new LoadingScreen ();
					await SilentUpdate (dat);
					DisplayContent (Data);
				} else
					DisplayContent (Data);
			} catch (Exception ex) {
				DisplayError (ex);
			}
			IsBusy = false;
		}

		public async Task RefreshData (bool forceFetch)
		{
			IsBusy = true;
			try {
				if ((IsDataInvalidated (LastUpdate) || Data == null) || forceFetch) {
					if (!forceFetch)
						Content = new LoadingScreen ();
					await SilentUpdate (null);
					DisplayContent (Data);
				} else
					DisplayContent (Data);
			} catch (Exception ex) {
				DisplayError (ex);
			}
			IsBusy = false;
		}

		public virtual void DisplayContent (object data)
		{
			IsBusy = false;
		}

		public virtual void DisplayError (Exception ex)
		{
			Content = new StackLayout {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Children = {
					new Image {
						Source = ImageSource.FromFile ("404.jpg"),
						HorizontalOptions = LayoutOptions.Fill,
						VerticalOptions = LayoutOptions.Fill
					},
					new Label {
						Text = ex.Message,
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.Center,
						XAlign = TextAlignment.Center
					}
				}
			};
			Insights.Report (ex);
		}
	}
}


