using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TriCare.Views;
using Xamarin.Forms;
using TriCare.Models;

namespace TriCare
{
    public class App
    {
		private static NavigationPage np;
		private static ToolbarItem logOutButton;

        public static Page GetMainPage()
		{
			logOutButton = new ToolbarItem ();
			if (Device.OS != TargetPlatform.iOS) 
			{
				logOutButton.Order = ToolbarItemOrder.Secondary;
				logOutButton.Text = "Log Out";
				logOutButton.Clicked += LogOut;
				np = new NavigationPage (new LoginPage ());
				np.ToolbarItems.Add (logOutButton);
			} else 			{
				logOutButton.Text = ". . .";
				logOutButton.Clicked += LogOutIOS;
				np = new NavigationPage (new LoginPage ());
				np.ToolbarItems.Add (logOutButton);
			}
			var mainNav = np;
			mainNav.BarBackgroundColor = Color.FromRgba(119,164,214,128);
			mainNav.BarTextColor = Color.White;
            return mainNav;
        }
		public static void LogOut(Object e , EventArgs s)
		{
			ClearCurrentPrescription ();
			InvalidateToken ();
			np.Navigation.PushAsync (new LoginPage ());
		}
		public static async void LogOutIOS(Object e , EventArgs s)
		{
			var action = await np.DisplayActionSheet("Menu", "Close","Log Out", new string[]{});
			if (action == "Log Out") {
				ClearCurrentPrescription ();
				InvalidateToken ();
				np.Navigation.PushAsync (new LoginPage ());
			}
		}
        public static bool IsLoggedIn
        {
            get { return !string.IsNullOrWhiteSpace(_Token); }
        }
		public static void DisableLogout()
		{
			np.ToolbarItems.Clear ();
		}

		public static void EnableLogout()
		{
			if (np.ToolbarItems.Count == 0)
			{
				if (Device.OS != TargetPlatform.iOS) 
				{
					logOutButton = new ToolbarItem ();

					logOutButton.Order = ToolbarItemOrder.Secondary;
					logOutButton.Text = "Log Out";
					logOutButton.Clicked += LogOut;
					np = new NavigationPage (new LoginPage ());
					np.ToolbarItems.Add (logOutButton);
				} else 			{
					logOutButton = new ToolbarItem ();

					logOutButton.Text = ". . .";
					logOutButton.Clicked += LogOutIOS;
					np = new NavigationPage (new LoginPage ());
					np.ToolbarItems.Add (logOutButton);
				}
			}
		}
        static string _Token;
        public static string Token
        {
            get { return _Token; }
        }
        public static void InvalidateToken()
        {
            _Token = " ";
        }
        public static void SaveToken(string token)
        {
            _Token = token;
        }

        public static Action SuccessfulLoginAction
        {
            get
            {
                return new Action(() =>
                {
                    new HomePage().Navigation.PopModalAsync();
                });
            }
        }
		static PrescriptionModel _currentPrescription;
		public static PrescriptionModel  CurrentPrescription
		{
			get{ return _currentPrescription; }
		}
		public static void ClearCurrentPrescription()
		{
			_currentPrescription = new PrescriptionModel ();
		}
    }
}
