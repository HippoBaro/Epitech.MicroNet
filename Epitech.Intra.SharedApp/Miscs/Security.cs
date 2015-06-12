using System.Threading.Tasks;
using Xamarin.Forms.Labs.Controls;

namespace Epitech.Intra.SharedApp
{
	[Preserve]
	public static class Security
	{
		public class Credit
		{
			public string Login;
			public string Password;
		}

		[Preserve]
		public interface ISecurity
		{
			Task<bool> AddItemAsync (Credit credit);

			Task<Credit> GetItemAsync ();

			Task<bool> UpdateItemAsync (Credit credit);

			Task<bool> DeleteItemAsync ();
		}
	}
}


