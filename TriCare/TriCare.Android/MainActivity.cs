using System;
using System.Diagnostics;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics.Drawables;
using Android.Content;
using Xamarin.Forms.Platform.Android;

namespace TriCare.Droid
{
	[Activity (Label = "TriCare Wellness", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,Icon="@drawable/tricareIconA",Theme = "@style/TriCareSelect")]
    public class MainActivity : AndroidActivity
    {
		private static Stopwatch timer;
		public static Context context;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			try{
			if (timer.IsRunning) {
				timer.Stop ();
				if (timer.ElapsedMilliseconds > 600000) {
					App.LogOutClear();
				}
			}
			else
				timer = new Stopwatch ();
			}
			catch (Exception e) {
				timer = new Stopwatch ();
			}
            Xamarin.Forms.Forms.Init(this, bundle);
            SetPage(App.GetMainPage());
			context = this.BaseContext;
		}
		public override bool OnCreateOptionsMenu (IMenu menu)
		{

			var result = base.OnCreateOptionsMenu (menu);
			this.ActionBar.SetStackedBackgroundDrawable (new ColorDrawable(Xamarin.Forms.Color.FromRgb(52, 63, 169).ToAndroid()));
			return result;

		}

		protected override void OnStart()
		{
			base.OnStart();
		}
		protected override void OnResume()
		{
			base.OnResume();
			timer.Stop ();
			if (timer.ElapsedMilliseconds > 600000) {
					App.LogOutTime ();
			}
		}
		protected override void OnPause()
		{
			base.OnPause();
			timer.Stop ();
			timer.Reset ();
			timer.Start ();
		}
		protected override void OnStop()
		{
			base.OnStop();
			timer.Stop ();
			timer.Reset ();
			timer.Start ();	
		}

		public override void OnBackPressed ()
		{
//			if (App.IsHome) {
//				OnStop ();
//				return;
//			}
			if (!App.IsHome)
				base.OnBackPressed ();
			else
			{
				App.andCurr = int.Parse (App.Token);
				timer.Stop ();
				timer.Reset ();
				timer.Start ();
				Finish ();

			}
		}
    }
}

