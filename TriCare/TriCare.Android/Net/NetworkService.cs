using System;
using System.Threading.Tasks;
using Acr.XamForms.Mobile.Droid.Net;
using Acr.XamForms.Mobile.Net;
using App = Android.App.Application;
using Android.Net;
using Java.Net;
using Xamarin.Forms;


[assembly: Dependency(typeof(NetworkService))]


namespace Acr.XamForms.Mobile.Droid.Net {
    
    public class NetworkService : AbstractNetworkService {

        public NetworkService() {
            NetworkConnectionBroadcastReceiver.OnChange = this.SetFromInfo;
            var manager = (ConnectivityManager)Forms.Context.GetSystemService(App.ConnectivityService);
            this.SetFromInfo(manager.ActiveNetworkInfo);
        }


        private void SetFromInfo(NetworkInfo network) {
            //var active = NetworkInterface
            //    .GetAllNetworkInterfaces()
            //    .FirstOrDefault(x => 
            //        x.NetworkInterfaceType == NetworkInterfaceType.Ethernet || 
            //        x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
            //    );

            if (network == null || !network.IsConnected)
                this.IsConnected = false;
            else {
                this.IsConnected = true;
                this.IsRoaming = network.IsRoaming;
                this.IsWifi = (network.Type == ConnectivityType.Wifi);
                this.IsMobile = (network.Type == ConnectivityType.Mobile);
            }
            this.PostUpdateStates();
        }


        public override Task<bool> IsHostReachable(string host) {
            return Task<bool>.Run(() => {
                if (!this.IsConnected)
                    return false;

                try {
                    return InetAddress
                        .GetByName(host)
                        .IsReachable(5000);
                }
                catch {
                    return false;
                }
            });
        }
    }
}
