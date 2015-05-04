using System.Threading.Tasks;
using Epitech.Intra.Droid;
using System.Runtime.Remoting.Contexts;
using Android.Content;
using Android.Preferences;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency (typeof(SecureEnclaveDroid))]
namespace Epitech.Intra.Droid
{
	public class SecureEnclaveDroid : Epitech.Intra.SharedApp.Security.ISecurity
	{
		private const string preferenceName = "Epitech.Intra.SharedPreferences";

		public async Task<bool> AddItemAsync (Epitech.Intra.SharedApp.Security.Credit credit)
		{
			return await Task.Run (() => {
//				try {
				ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences (Forms.Context); 
				ISharedPreferencesEditor editor = prefs.Edit ();
				editor.PutString ("Login", credit.Login);
				editor.PutString ("Password", credit.Password);
				return editor.Commit ();
//				} catch (System.Exception ex) {
//					return false;
//				}
			});
		}

		public async Task<Epitech.Intra.SharedApp.Security.Credit> GetItemAsync ()
		{
			return await Task.Run (() => {
				Epitech.Intra.SharedApp.Security.Credit credit = new Epitech.Intra.SharedApp.Security.Credit ();

				//				try {
				ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences (Forms.Context);
				credit.Login = prefs.GetString ("Login", string.Empty);
				credit.Password = prefs.GetString ("Password", string.Empty);
				if (credit.Login == string.Empty || credit.Password == string.Empty)
					return null;
				//				} catch (System.Exception ex) {
				//					return null;
				//				}
				return credit;
			});
		}

		public async Task<bool> UpdateItemAsync (Epitech.Intra.SharedApp.Security.Credit credit)
		{
			//return await Task.FromResult<bool> (UpdateItem (credit));
			return await Task.Run (() => true);
		}

		public async Task<bool> DeleteItemAsync ()
		{
			//return await Task.FromResult<bool> (DeleteItem ());
			return await Task.Run (() => {
				//				try {
				ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences (Forms.Context); 
				ISharedPreferencesEditor editor = prefs.Edit ();
				editor.Remove ("Login");
				editor.Remove ("Password");
				return editor.Commit ();
				//				} catch (System.Exception ex) {
				//					return false;
				//				}
			});
		}

		//		static bool AddItem (Epitech.Intra.SharedApp.Security.Credit credit)
		//		{
		//			var secObject = new SecAccessControl (SecAccessible.AfterFirstUnlockThisDeviceOnly);
		//
		//			if (secObject == null) {
		//				return false;
		//			}
		//
		//			var securityRecord = new SecRecord (SecKind.GenericPassword) {
		//				Service = "com.Epitech.uIntra",
		//				ValueData = new NSString (credit.Login + "|" + credit.Password).Encode (NSStringEncoding.UTF8),
		//				UseNoAuthenticationUI = true,
		//				AccessControl = secObject,
		//			};
		//
		//			SecStatusCode status = SecKeyChain.Add (securityRecord);
		//
		//			return status == SecStatusCode.Success;
		//		}
		//
		//		static Epitech.Intra.SharedApp.Security.Credit GetItem ()
		//		{
		//			var securityRecord = new SecRecord (SecKind.GenericPassword) {
		//				Service = "com.Epitech.uIntra",
		//				UseOperationPrompt = "Authentifier pour vous connecter à l'Intranet"
		//			};
		//
		//			SecStatusCode status;
		//			NSData resultData = null;
		//			UIApplication.SharedApplication.InvokeOnMainThread (() => {
		//				resultData = SecKeyChain.QueryAsData (securityRecord, false, out status);
		//			});
		//
		//			var result = resultData != null ? new NSString (resultData, NSStringEncoding.UTF8) : null;
		//
		//			if (result == null) {
		//				return null;
		//			} else {
		//				char[] sep = { '|' };
		//				string[] credit = ((string)result).Split (sep);
		//				return new Epitech.Intra.SharedApp.Security.Credit { Login = credit [0], Password = credit [1] };
		//			}
		//		}
		//
		//		static bool UpdateItem (Epitech.Intra.SharedApp.Security.Credit credit)
		//		{
		//			var securityRecord = new SecRecord (SecKind.GenericPassword) {
		//				Service = "com.Epitech.uIntra",
		//				UseOperationPrompt = "Authentifiez-vous pour mettre à jours vos identifiants"
		//			};
		//
		//			var recordUpdates = new SecRecord (SecKind.Identity) {
		//				ValueData = new NSString (credit.Login + "|" + credit.Password).Encode (NSStringEncoding.UTF8),
		//			};
		//
		//			SecStatusCode status = SecStatusCode.ItemNotFound;
		//			UIApplication.SharedApplication.InvokeOnMainThread (() => {
		//				status = SecKeyChain.Update (securityRecord, recordUpdates);
		//			});
		//
		//			return status == SecStatusCode.Success;
		//		}
		//
		//		static bool DeleteItem ()
		//		{
		//			var securityRecord = new SecRecord (SecKind.GenericPassword) {
		//				Service = "com.Epitech.uIntra"
		//			};
		//
		//			var status = SecKeyChain.Remove (securityRecord);
		//
		//			return status != SecStatusCode.Success;
		//		}
	}
}
