using System;

using Xamarin.Forms;

namespace Epitech.Intra.SharedApp.Views
{

	public interface IAppLink
	{
		bool OpenViaAppLink (string ressource);
	}

	public sealed class WebBrowser : IntraPage
	{
		public WebBrowser (string source, string title)
		{
			Title = title;
			Content = new LoadingScreen ();
			try {
				DisplayContent (source);
			} catch (Exception ex) {
				DisplayError (ex);
			}
		}

		void DisplayContent (string source)
		{
			ToolbarItems.Add (new ToolbarItem ("OK", null, new Action (() => Navigation.PopModalAsync (true))));

			string tmp;
			string target;

			try {
				tmp = source.Substring (0, 8);
			} catch {
				throw new Exception ("Cette ressource est vide");
			}


			if (tmp != "https://")
				target = API.APIIndex.BaseAPI + source;
			else
				target = source;

			WebView webview = new WebView { Source = target };
			webview.Navigated += (sender, e) => {
				Content = webview;
			};
				
			Content = webview;
		}
	}
}


