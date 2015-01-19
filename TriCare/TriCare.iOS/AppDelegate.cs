using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Forms;

namespace TriCare.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
		private Stopwatch timer;
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
			timer = new Stopwatch ();
            window = new UIWindow(UIScreen.MainScreen.Bounds);

            window.RootViewController = App.GetMainPage().CreateViewController();

            window.MakeKeyAndVisible();

            return true;
        }

		public override void DidEnterBackground (UIApplication application)
		{
			// NOTE: Don't call the base implementation on a Model class
			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
			timer.Stop ();
			timer.Reset ();
			timer.Start ();
		}

		public override void WillEnterForeground (UIApplication application)
		{
			// NOTE: Don't call the base implementation on a Model class
			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
			timer.Stop ();
			if (timer.ElapsedMilliseconds > 600000) {
				if (App.IsLoggedIn)
					App.LogOutTime ();
			}
		}
    }
}
