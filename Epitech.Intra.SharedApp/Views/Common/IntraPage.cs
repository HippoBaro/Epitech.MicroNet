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
		private const int DataInvalidationinHour = 1;
		private DateTime LastUpdate;
		private Type Type;
		public object Data;

		bool FunctionHasParam;
		Func<string, Task<object>> FunctionWithParam;
		Func<Task<object>> Function;

		public bool IsDataInvalidated (DateTime time)
		{
			if (DateTime.Compare (time.AddHours (DataInvalidationinHour), DateTime.Now) < 0)
				return true;
			else
				return false;
		}

		public void InitIntraPage (Type Type, Func<string, Task<object>> function, string dat)
		{
			this.Type = Type;
			FunctionWithParam = function;
			FunctionHasParam = true;
		}

		public void InitIntraPage (Type Type, Func<Task<object>> function)
		{
			this.Type = Type;
			Function = function;
			FunctionHasParam = false;
		}

		public virtual async Task<object> SilentUpdate (string param)
		{
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
				if (Type == typeof(Planning))
					DependencyService.Get<IEventManager_iOS> ().SynchrosizeCalendar (((List<Calendar>)Data).FindAll (x => x.Past == false && x.EventRegistered != null));
			} catch (Exception ex) {
				Insights.Report (ex);
				throw ex;
			}
			return Data;
		}

		public async Task RefreshData (bool ForceFetch, string dat)
		{
			this.IsBusy = true;
			try {
				if (IsDataInvalidated (LastUpdate) || Data == null || ForceFetch) {
					if (!ForceFetch)
						Content = new LoadingScreen ();
					await SilentUpdate (dat);
					DisplayContent (Data);
				} else
					DisplayContent (Data);
			} catch (Exception ex) {
				DisplayError (ex);
			}
			this.IsBusy = false;
		}

		public async Task RefreshData (bool ForceFetch)
		{
			this.IsBusy = true;
			try {
				if ((IsDataInvalidated (LastUpdate) || Data == null) || ForceFetch) {
					if (!ForceFetch)
						Content = new LoadingScreen ();
					await SilentUpdate (null);
					DisplayContent (Data);
				} else
					DisplayContent (Data);
			} catch (Exception ex) {
				DisplayError (ex);
			}
			this.IsBusy = false;
		}

		public virtual void DisplayContent (object Data)
		{
			this.IsBusy = false;
		}

		public virtual void DisplayError (Exception ex)
		{
			Content = new StackLayout () {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Children = {
					new Image () { Source = ImageSource.FromFile("404.jpg"), HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill },
					new Label () { Text = ex.Message, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, XAlign = TextAlignment.Center }
				}
			};
			Insights.Report (ex);
		}
	}
}


