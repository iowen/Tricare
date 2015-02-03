using System;
using TriCare.iOS;
using TriCare.Data;
using Xamarin.Forms;
using System.IO;
using MonoTouch.NetworkExtension;
[assembly: Dependency (typeof (Network_iOS))]
namespace TriCare.iOS
{
	public class Network_iOS : INetwork
	{
		public Network_iOS ()
		{

		}
		public bool IsConnected ()
		{
			return Reachability.IsHostReachable ("www.google.com");
		}
	}
}

