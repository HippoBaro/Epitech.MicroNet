using System;
using Epitech.Intra.SharedApp.Views;
using Epitech.Intra.SharedApp;
using UIKit;
using Foundation;
using System.Linq;
using Security;
using CoreFoundation;
using System.Threading.Tasks;
using Epitech.Intra.iOS;

[assembly: Xamarin.Forms.Dependency (typeof(SecureEnclave_iOS))]
namespace Epitech.Intra.iOS
{
	public class SecureEnclave_iOS : Epitech.Intra.SharedApp.Security.ISecurity
	{

		public async Task<bool> AddItemAsync (Epitech.Intra.SharedApp.Security.Credit credit)
		{
			return await Task.FromResult<bool> (AddItem (credit));
		}

		public async Task<Epitech.Intra.SharedApp.Security.Credit> GetItemAsync ()
		{
			return await Task.FromResult<Epitech.Intra.SharedApp.Security.Credit> (GetItem ());
		}

		public async Task<bool> UpdateItemAsync (Epitech.Intra.SharedApp.Security.Credit credit)
		{
			return await Task.FromResult<bool> (UpdateItem (credit));
		}

		public async Task<bool> DeleteItemAsync ()
		{
			return await Task.FromResult<bool> (DeleteItem ());
		}

		private bool AddItem (Epitech.Intra.SharedApp.Security.Credit credit)
		{
			var secObject = new SecAccessControl (SecAccessible.AfterFirstUnlockThisDeviceOnly, SecAccessControlCreateFlags.UserPresence);

			if (secObject == null) {
				return false;
			}
				
			var securityRecord = new SecRecord (SecKind.GenericPassword) {
				Service = "com.Epitech.uIntra",
				ValueData = new NSString (credit.login + "|" + credit.password).Encode (NSStringEncoding.UTF8),
				UseNoAuthenticationUI = true,
				AccessControl = secObject,
			};

			SecStatusCode status = SecKeyChain.Add (securityRecord);

			if (status != SecStatusCode.Success)
				return false;
			else
				return true;
		}

		private Epitech.Intra.SharedApp.Security.Credit GetItem ()
		{
			var securityRecord = new SecRecord (SecKind.GenericPassword) {
				Service = "com.Epitech.uIntra",
				UseOperationPrompt = "Authentifier pour vous connecter à l'Intranet"
			};
					
			SecStatusCode status;
			NSData resultData = null;
			UIApplication.SharedApplication.InvokeOnMainThread (() => {
				resultData = SecKeyChain.QueryAsData (securityRecord, false, out status);
			});

			var result = resultData != null ? new NSString (resultData, NSStringEncoding.UTF8) : null;

			if (result == null) {
				return null;
			} else {
				char[] sep = { '|' };
				string[] credit = ((string)result).Split (sep);
				return new Epitech.Intra.SharedApp.Security.Credit () { login = credit [0], password = credit [1] };
			}
		}

		private bool UpdateItem (Epitech.Intra.SharedApp.Security.Credit credit)
		{
			var securityRecord = new SecRecord (SecKind.GenericPassword) {
				Service = "com.Epitech.uIntra",
				UseOperationPrompt = "Authentifiez-vous pour mettre à jours vos identifiants"
			};

			var recordUpdates = new SecRecord (SecKind.Identity) {
				ValueData = new NSString (credit.login + "|" + credit.password).Encode (NSStringEncoding.UTF8),
			};

			SecStatusCode status = SecStatusCode.ItemNotFound;
			UIApplication.SharedApplication.InvokeOnMainThread (() => {
				status = SecKeyChain.Update (securityRecord, recordUpdates);
			});

			if (status != SecStatusCode.Success)
				return false;
			else
				return true;
		}

		private bool DeleteItem ()
		{
			var securityRecord = new SecRecord (SecKind.GenericPassword) {
				Service = "com.Epitech.uIntra"
			};

			var status = SecKeyChain.Remove (securityRecord);

			if (status != SecStatusCode.Success)
				return true;
			else
				return false;
		}
	}
}
