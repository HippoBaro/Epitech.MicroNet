using System;

using Xamarin.Forms;

using System.Net;
using System.Collections.Generic;

namespace Epitech.Intra.SharedApp.Views
{

	public interface IAppLink
	{
		bool OpenViaAppLink (string ressource);
	}

	public class WebBrowser : IntraPage
	{
		public WebBrowser (string source, string Title)
		{
			this.Title = Title;
			Content = new LoadingScreen ();
			try {
				DisplayContent (source);
			} catch (Exception ex) {
				DisplayError (ex);
			}
		}

		private void DisplayContent(string source)
		{
			ToolbarItems.Add (new ToolbarItem ("OK", null, new Action (delegate() {
				Navigation.PopModalAsync (true);
			}), 0, 0));

			string tmp;
			string target;

			try {
				tmp = source.Substring (0, 8);
			} catch {
				throw new Exception ("Cette ressource est vide");
			}


			if (tmp != "https://")
				target = API.APIIndex.baseAPI + source;
			else
				target = source;

			WebView webview = new WebView () { Source = target };
			webview.Navigated += (object sender, WebNavigatedEventArgs e) => {
				Content = webview;
			};


			this.Content = webview;
		}
	}
}


