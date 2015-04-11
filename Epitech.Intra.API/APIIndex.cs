using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
		public static string GetUserPictureUri (string ResponsePicture, string forLogin, PictureSize size)
		{
			//Large ~ 230Ko
			//Light ~ 5Ko
			//VeryLight ~ 2 Ko

			if (ResponsePicture == null || !(ResponsePicture.EndsWith (forLogin + ".bmp") || ResponsePicture.EndsWith (forLogin + ".jpg")))
				return PicturePlaceholder;

			if (size == PictureSize.Large)
				return "https://cdn.local.epitech.eu/userprofil/" + forLogin + ".bmp";
			else if (size == PictureSize.Light)
				return "https://cdn.local.epitech.eu/userprofil/profilview/" + forLogin + ".jpg";
			else
				return "https://cdn.local.epitech.eu/userprofil/commentview/" + forLogin + ".jpg";
		}

	}

	public class APIIndex
	{
		public const string baseAPI = "https://intra.epitech.eu";
		public string login;
		private string password;

		private Uri buildUri (string target)
		{
			return new Uri (baseAPI + target + "?format=json");
		}

		public void ForgetCredit ()
		{
			login = null;
			password = null;
		}

		private FormUrlEncodedContent GetHeader ()
		{
			List<KeyValuePair<string,string>> head = new List<KeyValuePair<string,string>> ();
			head.Add (new KeyValuePair<string, string> ("login", login));
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

			this.login = login;
			this.password = password;

			try {
				var result = await client.PostAsync (buildUri ("/"), GetHeader ());
				return result.IsSuccessStatusCode;
			} catch (Exception ex) {
				throw ex;
			}
		}

		public async Task<Data.Welcome> GetWelcome ()
		{
			HttpClient client = new HttpClient ();

			try {
				var result = await client.PostAsync (buildUri ("/"), GetHeader ());
				if (!result.IsSuccessStatusCode)
					return null;
				return Newtonsoft.Json.JsonConvert.DeserializeObject<Data.Welcome> (await result.Content.ReadAsStringAsync ());
			} catch {
				throw new Exception("Erreur");
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

			if (user.Close != true && user.Nsstat != null) {
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
				user.Marks = Newtonsoft.Json.JsonConvert.DeserializeObject<Data.UserMarks> (await result.Content.ReadAsStringAsync ());
				Array.Reverse (user.Marks.Modules);
			} catch {
				user.Marks = null;
			}
			return user;
		}

		private void DetectTokenAsked(List<Data.Calendar> list, Data.Welcome tokentest)
		{
			Activite[] token = Array.FindAll (tokentest.Board.Activites, x => x.Token != null);

			foreach (var item in token) {
				int i = 0;
				string year;
				string module;
				string instance;
				string activity;
				Calendar target = null;

				string temp = item.TokenLink;

				if (!temp.StartsWith ("/module/"))
					continue;
				temp = temp.Remove (0, 8);
				year = temp.Substring (0, 4);
				temp = temp.Remove (0, 5);
				for (i = 0; i < temp.Length; i++) {
					if (temp [i] == '/')
						break;
				}
				module = temp.Substring (0, i);
				temp = temp.Remove (0, i + 1);
				for (i = 0; i < temp.Length; i++) {
					if (temp [i] == '/')
						break;
				}
				instance = temp.Substring (0, i);
				temp = temp.Remove (0, i + 1);
				for (i = 0; i < temp.Length; i++) {
					if (temp [i] == '/')
						break;
				}
				activity = temp.Substring (0, i);

				List<Calendar> Activities = (list).FindAll (x => x.Scolaryear == year && x.Codemodule == module && x.Codeinstance == instance && x.Codeacti == activity);
				Activities = Activities.FindAll (x => x.EventRegistered != null);

				if (Activities.Count == 0)
					Activities = (list).FindAll (x => x.Scolaryear == year && x.Codemodule == module && x.Codeinstance == instance && x.Codeacti == activity);

				if (Activities.Count == 0)
					return;

				foreach (var acti in Activities) {
					if (acti.EventRegistered == "present")
						target = acti;
				}

				if (target == null) {
					foreach (var acti in Activities) {
						if (acti.EventRegistered != null)
							target = acti;
					}
				}
				target.TokenAsked = true;
			}
			return;
		}

		public async Task<object> GetCalendar ()
		{
			HttpClient client = new HttpClient ();
			List<Data.Calendar> list;

			try {
			var result = await client.PostAsync (buildUri ("/planning/load"), GetHeader ());
			if (!result.IsSuccessStatusCode)
				return null;
				list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Data.Calendar>> (await result.Content.ReadAsStringAsync ());

				result = await client.PostAsync (buildUri ("/"), GetHeader ());
				Data.Welcome tokentest = Newtonsoft.Json.JsonConvert.DeserializeObject<Data.Welcome> (await result.Content.ReadAsStringAsync ());
				DetectTokenAsked(list, tokentest);
			} catch {
				throw new Exception ("Impossible de récuperer les informations du calendrier.");
			}
			return list;
		}

		public async Task<object> GetActivityRegisteredStudent (Data.Calendar activity)
		{
			HttpClient client = new HttpClient ();

			try {
				var result = await client.PostAsync (buildUri ("/module/" + activity.Scolaryear + "/" + activity.Codemodule + "/" + activity.Codeinstance + "/" + activity.Codeacti + "/" + activity.Codeevent + "/registered"), GetHeader ());
				if (!result.IsSuccessStatusCode)
					return null;
				return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Data.RegisterStudent>> (await result.Content.ReadAsStringAsync ());
			} catch {
				return null;
			}
		}

		public async Task<bool> RegistertoActivity (Data.Calendar activity)
		{
			HttpClient client = new HttpClient ();

			try {
				var result = await client.PostAsync (buildUri ("/module/" + activity.Scolaryear + "/" + activity.Codemodule + "/" + activity.Codeinstance + "/" + activity.Codeacti + "/" + activity.Codeevent + "/register"), GetHeader ());
				return result.IsSuccessStatusCode;
			} catch {
				throw new Exception("Impossible de s'inscrire");
			}

		}

		public async Task<bool> UnregistertoActivity (Data.Calendar activity)
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
				if (!result.IsSuccessStatusCode)
					return null;
				return Newtonsoft.Json.JsonConvert.DeserializeObject<List<ActivityMark>> (await result.Content.ReadAsStringAsync ());
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
				Data.WelcomeJsonTypes.Projet[] temp = Newtonsoft.Json.JsonConvert.DeserializeObject<Data.Welcome> (await result.Content.ReadAsStringAsync ()).Board.Projets;

				List<Data.Project> projects = new List<Data.Project> ();
				foreach (var item in temp) {
					result = await client.PostAsync (buildUri (item.TitleLink + "project"), GetHeader ());
					projects.Add (Newtonsoft.Json.JsonConvert.DeserializeObject<Data.Project> (await result.Content.ReadAsStringAsync ()));
				}
				return projects;
			} catch {
				throw new Exception ("Impossible de récuperer les projets");
			}
		}

		public async Task<List<Data.Trombi>> GetSearchResult (string search)
		{
			HttpClient client = new HttpClient ();

			if (search.Length < 3)
				return null;
			var result = await client.PostAsync (new Uri (baseAPI + "/user/complete?format=json&contains=&search=" + search), GetHeader ());
			if (!result.IsSuccessStatusCode)
				return null;
			try {
				return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Data.Trombi>> (await result.Content.ReadAsStringAsync ());
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
					if (item.type == "d") {
						result = await client.PostAsync (buildUri ("/module/" + project.Scolaryear + "/" + project.Codemodule + "/" + project.Codeinstance + "/" + project.Codeacti + "/project/file/" + item.slug + "/"), GetHeader ());
						List<Files> toAppend = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Files>> (await result.Content.ReadAsStringAsync ());
						resultlist.AddRange (toAppend);
					}
				}
				return resultlist.FindAll (x => x.type != "d");
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
