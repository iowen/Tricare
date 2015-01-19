using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics.Drawables;

using Xamarin.Forms.Platform.Android;

namespace TriCare.Droid
{
	[Activity(Label = "TriCare", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Icon="@drawable/tricareIcon")]
    public class MainActivity : AndroidActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            SetPage(App.GetMainPage());
        }
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			if (this.ActionBar.TabCount > 0)
				this.ActionBar.SetStackedBackgroundDrawable (new ColorDrawable(Xamarin.Forms.Color.FromRgba(52, 63, 169, 128).ToAndroid()));
			return base.OnCreateOptionsMenu (menu);

		}
    }
}

