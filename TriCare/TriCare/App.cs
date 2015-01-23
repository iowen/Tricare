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
		public static NavigationPage np;
		private static ToolbarItem logOutButton;

        public static Page GetMainPage()
		{
			logOutButton = new ToolbarItem ();
			np = new NavigationPage (new LoginPage ());

//			if (Device.OS != TargetPlatform.iOS) 
//			{
//				logOutButton.Order = ToolbarItemOrder.Secondary;
//				logOutButton.Text = "Log Out";
//				logOutButton.Clicked += LogOut;
//			} else 			{
//			}
			logOutButton.Text = ". . .";
			logOutButton.Clicked += LogOutIOS;
			np.ToolbarItems.Add (logOutButton);

			var mainNav = np;
			mainNav.BarBackgroundColor = Color.FromRgb (52, 63, 169);
			mainNav.BarTextColor = Color.White;
            return mainNav;
        }
		public static async void LogOutTime()
		{
			ClearCurrentPrescription ();
			InvalidateToken ();
			await np.PopToRootAsync ();
		}
			
		public static async void LogOut(Object e , EventArgs s)
		{
			ClearCurrentPrescription ();
			InvalidateToken ();
			await np.PopToRootAsync ();
		}

		public static async void IgnoreLogOut(Object e , EventArgs s)
		{
			return;
		}
		public static async void LogOutIOS(Object e , EventArgs s)
		{
			var action = await np.DisplayActionSheet("Menu", "Close","Log Out", new string[]{});
			if (action == "Log Out") {
				ClearCurrentPrescription ();
				InvalidateToken ();
				await np.PopToRootAsync ();
			}
		}
        public static bool IsLoggedIn
        {
            get { return !string.IsNullOrWhiteSpace(_Token); }
        }
		public static void DisableLogout()
		{
			if (logOutButton.Text == ". . .") {
				logOutButton.Text = "";
				logOutButton.Clicked -= LogOutIOS;
				logOutButton.Clicked += IgnoreLogOut;
			}
		}

		public static void EnableLogout()
		{
			if (logOutButton.Text == "") {
				logOutButton.Text = ". . .";
				logOutButton.Clicked -= IgnoreLogOut;
				logOutButton.Clicked += LogOutIOS;
			}

		}
		public static bool IsHome{ get; set; }
		public static bool IsLogin{ get; set; }
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
