using System;
using Android.App;
using Android.Content.PM;
using Android.Content;
using Android.OS;


using Xamarin.Forms.Platform.Android;

namespace TriCare.Droid
{
	[Activity(Label = "TriCare Wellness", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Icon="@drawable/tricareIconA",  NoHistory = true,Theme = "@layout/SplashTheme")]

	public class SplashScreen : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			var intent = new Intent (this, typeof(MainActivity));
			StartActivity(intent);
			Finish();
		}
	}
}

