using System;
using TriCare.Droid;
using TriCare.Data;
using Xamarin.Forms;
using System.IO;
using Android.App;
using Android.Net;
using Android.OS;
using Android.Widget;
using Android.Content;
[assembly: Dependency (typeof (Network_Android))]
namespace TriCare.Droid
{
	public class Network_Android : INetwork
	{
		private Context context; 
		public Network_Android ()
		{
			context = MainActivity.context;
		}		
		public bool IsConnected () {
			var connectivityManager = (ConnectivityManager)context.GetSystemService (Android.Content.Context.ConnectivityService);
			var activeConnection = connectivityManager.ActiveNetworkInfo;
			if ((activeConnection != null)  && activeConnection.IsConnected)
			{
				// we are connected to a network.
				return true;
			}
			return false;
		}
	}
}

