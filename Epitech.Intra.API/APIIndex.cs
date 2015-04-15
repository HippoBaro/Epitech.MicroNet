using System;
using System.Collections.Generic;
using System.Linq;

using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Epitech.Intra.API.Data;
using Epitech.Intra.API.Data.ProjectJsonTypes;
using Epitech.Intra.API;
using Epitech.Intra.API.Data.WelcomeJsonTypes;

namespace Epitech.Intra.API
{
	public abstract class HTMLCleaner
	{
		public struct LinkItem
		{
			public string Href;
			public string Text;

			public override string ToString ()
			{
				return Href + "\n\t" + Text;
			}
		}

		public static List<LinkItem> GetLinks (string source)
		{
			List<LinkItem> list = new List<LinkItem> ();

			MatchCollection m1 = Regex.Matches (source, @"(<a.*?>.*?</a>)", RegexOptions.Singleline);

			foreach (Match m in m1) {
				string value = m.Groups [1].Value;
				LinkItem i = new LinkItem ();

				Match m2 = Regex.Match (value, @"href=\""(.*?)\""", RegexOptions.Singleline);
				if (m2.Success) {
					i.Href = m2.Groups [1].Value;
				}

				string t = Regex.Replace (value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
				i.Text = t;

				list.Add (i);
			}
			return list;
		}

		public static bool ContainsLink (string source)
		{
			return !String.Equals (source, HTMLCleaner.Clean (source));
		}

		public static string RemoveLinksKeepText (string source)
		{
			return Regex.Replace (source, @"<a [^>]+>(.*?)<\/a>", "$1");
		}

		public static string Clean (string source)
		{
			return Regex.Replace (source, "<.*?>", string.Empty);
		}
	}

	public abstract class PictureHelper
	{
		public const string PicturePlaceholder = "https://intra.epitech.eu/static7348/img/nopicture-profilview.png";

		public enum PictureSize
		{
			VeryLight,
			Light,
			Large
		}

		//
		// Main rule of the Universe : NEVER use Large for any Collection of object.
		// Your Customer's Device doesn't have unlimited memory nor bandwith and data plan.
		// ResponsePicture if for supporting placeholder.
		//
		public static string GetUserPictureUri (string responsePicture, string forLogin, PictureSize size)
		{
			//Large ~ 230Ko
			//Light ~ 5Ko
			//VeryLight ~ 2 Ko

			if (responsePicture == null || !(responsePicture.EndsWith (forLogin + ".bmp", StringComparison.Ordinal) || responsePicture.EndsWith (forLogin + ".jpg", StringComparison.Ordinal)))
				return PicturePlaceholder;

			if (size == PictureSize.Large)
				return "https://cdn.local.epitech.eu/userprofil/" + forLogin + ".bmp";
			if (size == PictureSize.Light)
				return "https://cdn.local.epitech.eu/userprofil/profilview/" + forLogin + ".jpg";
			return "https://cdn.local.epitech.eu/userprofil/commentview/" + forLogin + ".jpg";
		}

	}

	public class APIIndex
	{
		public const string BaseAPI = "https://intra.epitech.eu";
		public string Login;
		string password;

		static Uri buildUri (string target)
		{
			return new Uri (BaseAPI + target + "?format=json");
		}

		public void ForgetCredit ()
		{
			Login = null;
			password = null;
		}

		FormUrlEncodedContent GetHeader ()
		{
			var head = new List<KeyValuePair<string,string>> ();
			head.Add (new KeyValuePair<string, string> ("login", Login));
			head.Add (new KeyValuePair<string, string> ("password", password));
			head.Add (new KeyValuePair<string, string> ("rememberme", "on"));

			return new FormUrlEncodedContent (head);
		}

		/*
		 * Must be called upon connection. Called this on app startup to enable the api. 
		*/
		public async Task<bool> CreditialTest (string login, string password)
		{
			HttpClient client = new HttpClient ();

			Login = login;
			this.password = password;

			try {
				var result = await client.PostAsync (buildUri ("/"), GetHeader ());
				return result.IsSuccessStatusCode;
			} catch (Exception ex) {
				throw ex;
			}
		}

		public async Task<Welcome> GetWelcome ()
		{
			HttpClient client = new HttpClient ();

			try {
				var result = await client.PostAsync (buildUri ("/"), GetHeader ());
				return !result.IsSuccessStatusCode ? null : Newtonsoft.Json.JsonConvert.DeserializeObject<Welcome> (await result.Content.ReadAsStringAsync ());
			} catch (Exception e) {
				throw new Exception ("Erreur", e);
			}

		}

		public async Task<object> GetUser (string login)
		{
			HttpClient client = new HttpClient ();
			Data.User user;

			var result = await client.PostAsync (buildUri ("/user/" + login + "/"), GetHeader ());
			if (!result.IsSuccessStatusCode)
				return null;
			try {
				user = Newtonsoft.Json.JsonConvert.DeserializeObject<Data.User> (await result.Content.ReadAsStringAsync ());
			} catch {
				throw new Exception ("Impossible de récuperer les information utilisateur. Ce compte est peut-être un compte spécial pas encore supporté par l'application.");
			}

			if (!user.Close && user.Nsstat != null) {
				result = await client.PostAsync (buildUri ("/user/" + login + "/netsoul/"), GetHeader ());
				if (!result.IsSuccessStatusCode)
					return null;
				List<Double[]> test = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Double[]>> (await result.Content.ReadAsStringAsync ());
				user.Netsoul = new List<Data.UserJsonTypes.NetsoulRawData> ();
				foreach (var val in test) {
					Data.UserJsonTypes.NetsoulRawData item = new Data.UserJsonTypes.NetsoulRawData ();
					item.EpochTime = (int)val [0];
					item.TimeActiveScool = val [1];
					item.TimeIldleScool = val [2];
					item.TimeActiveOut = val [3];
					item.TimeIldleOut = val [4];
					if (val.Count () > 5)
						item.Average = val [5];
					user.Netsoul.Add (item);
				}
			}

			result = await client.PostAsync (buildUri ("/user/" + login + "/notes/"), GetHeader ());
			if (!result.IsSuccessStatusCode)
				return null;
			try {
				user.Marks = Newtonsoft.Json.JsonConvert.DeserializeObject<UserMarks> (await result.Content.ReadAsStringAsync ());
				Array.Reverse (user.Marks.Modules);
			} catch {
				user.Marks = null;
			}
			return user;
		}

		public async Task<TokenResponse> TryValidateToken (Calendar activity, Token token)
		{
			HttpClient client = new HttpClient ();

			try {
				var head = new List<KeyValuePair<string,string>> ();
				head.Add (new KeyValuePair<string, string> ("token", token.TokenValue));
				head.Add (new KeyValuePair<string, string> ("rate", token.Rate.ToString ()));
				head.Add (new KeyValuePair<string, string> ("comment", token.Comment));
				head.Add (new KeyValuePair<string, string> ("login", Login));
				head.Add (new KeyValuePair<string, string> ("password", password));
				head.Add (new KeyValuePair<string, string> ("rememberme", "on"));

				var result = await client.PostAsync (buildUri ("/module/" + activity.Scolaryear + "/" + activity.Codemodule + "/" + activity.Codeinstance + "/" + activity.Codeacti + "/" + activity.Codeevent + "/token"), new FormUrlEncodedContent (head));
				return Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse> (await result.Content.ReadAsStringAsync ());
			} catch (Exception ex) {
				throw ex;
			}
		}

		public async Task<object> GetCalendar ()
		{
			HttpClient client = new HttpClient ();
			List<Calendar> list;

			try {
				var result = await client.PostAsync (buildUri ("/planning/load"), GetHeader ());
				if (!result.IsSuccessStatusCode)
					return null;
				list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Calendar>> (await result.Content.ReadAsStringAsync ());
			} catch {
				throw new Exception ("Impossible de récuperer les informations du calendrier.");
			}
			return list;
		}

		public async Task<object> GetActivityRegisteredStudent (Calendar activity)
		{
			HttpClient client = new HttpClient ();

			try {
				var result = await client.PostAsync (buildUri ("/module/" + activity.Scolaryear + "/" + activity.Codemodule + "/" + activity.Codeinstance + "/" + activity.Codeacti + "/" + activity.Codeevent + "/registered"), GetHeader ());
				return !result.IsSuccessStatusCode ? null : Newtonsoft.Json.JsonConvert.DeserializeObject<List<RegisterStudent>> (await result.Content.ReadAsStringAsync ());
			} catch {
				return null;
			}
		}

		public async Task<bool> RegistertoActivity (Calendar activity)
		{
			HttpClient client = new HttpClient ();

			try {
				var result = await client.PostAsync (buildUri ("/module/" + activity.Scolaryear + "/" + activity.Codemodule + "/" + activity.Codeinstance + "/" + activity.Codeacti + "/" + activity.Codeevent + "/register"), GetHeader ());
				return result.IsSuccessStatusCode;
			} catch {
				throw new Exception ("Impossible de s'inscrire");
			}

		}

		public async Task<bool> UnregistertoActivity (Calendar activity)
		{
			HttpClient client = new HttpClient ();

			try {
				var result = await client.PostAsync (buildUri ("/module/" + activity.Scolaryear + "/" + activity.Codemodule + "/" + activity.Codeinstance + "/" + activity.Codeacti + "/" + activity.Codeevent + "/unregister"), GetHeader ());
				return result.IsSuccessStatusCode;
			} catch {
				throw new Exception ("Impossible de se désinscrire");
			}

		}

		public async Task<object> GetActivityMarks (Data.MarksJsonTypes.Note note)
		{
			HttpClient client = new HttpClient ();
			try {
				var result = await client.PostAsync (buildUri ("/module/" + note.Scolaryear + "/" + note.Codemodule + "/" + note.Codeinstance + "/" + note.Codeacti + "/note"), GetHeader ());
				return !result.IsSuccessStatusCode ? null : Newtonsoft.Json.JsonConvert.DeserializeObject<List<ActivityMark>> (await result.Content.ReadAsStringAsync ());
			} catch {
				return null;
			}
		}

		public async Task<object> GetProjects ()
		{
			HttpClient client = new HttpClient ();

			var result = await client.PostAsync (buildUri ("/"), GetHeader ());
			if (!result.IsSuccessStatusCode)
				return null;

			try {
				Projet[] temp = Newtonsoft.Json.JsonConvert.DeserializeObject<Welcome> (await result.Content.ReadAsStringAsync ()).Board.Projets;

				List<Project> projects = new List<Project> ();
				foreach (var item in temp) {
					result = await client.PostAsync (buildUri (item.TitleLink + "project"), GetHeader ());
					projects.Add (Newtonsoft.Json.JsonConvert.DeserializeObject<Project> (await result.Content.ReadAsStringAsync ()));
				}
				return projects;
			} catch {
				throw new Exception ("Impossible de récuperer les projets");
			}
		}

		public async Task<List<Trombi>> GetSearchResult (string search)
		{
			HttpClient client = new HttpClient ();

			if (search.Length < 3)
				return null;
			var result = await client.PostAsync (new Uri (BaseAPI + "/user/complete?format=json&contains=&search=" + search), GetHeader ());
			if (!result.IsSuccessStatusCode)
				return null;
			try {
				return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Trombi>> (await result.Content.ReadAsStringAsync ());
			} catch {
				throw new Exception ("Impossible de récuperer les profils");
			}

		}

		public async Task<List<Files>> GetProjectFiles (Project project)
		{
			HttpClient client = new HttpClient ();
			List<Files> resultlist = new List<Files> ();

			var result = await client.PostAsync (buildUri ("/module/" + project.Scolaryear + "/" + project.Codemodule + "/" + project.Codeinstance + "/" + project.Codeacti + "/project/file/"), GetHeader ());
			if (!result.IsSuccessStatusCode)
				return null;
			try {
				List<Files> root = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Files>> (await result.Content.ReadAsStringAsync ());
				resultlist.AddRange (root);
				foreach (var item in root) {
					if (item.Type == "d") {
						result = await client.PostAsync (buildUri ("/module/" + project.Scolaryear + "/" + project.Codemodule + "/" + project.Codeinstance + "/" + project.Codeacti + "/project/file/" + item.Slug + "/"), GetHeader ());
						List<Files> toAppend = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Files>> (await result.Content.ReadAsStringAsync ());
						resultlist.AddRange (toAppend);
					}
				}
				return resultlist.FindAll (x => x.TypeOfFile != "d");
			} catch {
				throw new Exception ("Impossible de récuperer les sujets");
			}
		}

		public async Task<object> GetELearning ()
		{
			HttpClient client = new HttpClient ();

			var result = await client.PostAsync (buildUri ("/e-learning/"), GetHeader ());
			if (!result.IsSuccessStatusCode)
				throw new Exception ("Impossible de récuperer les informations de l'e-learning");
			try {
				return Newtonsoft.Json.JsonConvert.DeserializeObject<List<ELearning>> (await result.Content.ReadAsStringAsync ());
			} catch {
				throw new Exception ("Impossible de récuperer les informations de l'e-learning");
			}
		}

		public async Task<object> GetNotifications ()
		{
			HttpClient client = new HttpClient ();

			var result = await client.PostAsync (buildUri ("/user/notification/message/"), GetHeader ());
			if (!result.IsSuccessStatusCode)
				throw new Exception ("Impossible de récuperer les notications");
			try {
				return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Notification>> (await result.Content.ReadAsStringAsync ());
			} catch {
				throw new Exception ("Impossible de récuperer les notifactions");
			}
		}
	}
}
