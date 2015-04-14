using System;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace Epitech.Intra.SharedApp
{
	public static class Security
	{
		public class Credit
		{
			public string Login;
			public string Password;
		}

		public interface ISecurity
		{
			Task<bool> AddItemAsync (Credit credit);

			Task<Credit> GetItemAsync ();

			Task<bool> UpdateItemAsync (Credit credit);

			Task<bool> DeleteItemAsync ();
		}
	}
}


