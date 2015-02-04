using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TriCare.Views;
using Xamarin.Forms;
using TriCare.Models;
using TriCare.Data;
using System.Text.RegularExpressions;

namespace TriCare
{
    public class App
    {
		public static NavigationPage np;
		private static ToolbarItem logOutButton;
		public static int andCurr;
		public static bool showLogout;
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
			showLogout = true;
			logOutButton.Icon = "appMenu.png";
			logOutButton.Clicked += LogOutIOS;
			np.ToolbarItems.Add (logOutButton);


			if (andCurr > 0) {
				SaveToken (andCurr.ToString ());
				np.Navigation.PushAsync (new HomePage ());
			}
			var mainNav = np;
			mainNav.BarBackgroundColor = Color.FromRgb (52, 63, 169);
			mainNav.BarTextColor = Color.White;
            return mainNav;
        }
		public static  void LogOutClear()
		{
			ClearCurrentPrescription ();
			InvalidateToken ();
			andCurr = 0;
			IsHome = false;
		}
		public static async void LogOutTime()
		{
			ClearCurrentPrescription ();
			InvalidateToken ();
			andCurr = 0;
			IsHome = false;
			await np.PopToRootAsync ();
		}
			
		public static async void LogOut(Object e , EventArgs s)
		{
			ClearCurrentPrescription ();
			andCurr = 0;
			InvalidateToken ();
			IsHome = false;
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
				andCurr = 0;
				IsHome = false;
				await np.PopToRootAsync ();
			}
		}
        public static bool IsLoggedIn
        {
            get { return !string.IsNullOrWhiteSpace(_Token); }
        }
		public static void DisableLogout()
		{
			if (showLogout) {
				showLogout = false;
				logOutButton.Icon = "";
				logOutButton.Clicked -= LogOutIOS;
				logOutButton.Clicked += IgnoreLogOut;
			}
		}

		public static void EnableLogout()
		{
			if (!showLogout) {
				showLogout = true;
				logOutButton.Icon = "appMenu.png";
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

		public static string Encrypt(string plainText)
		{
			//Anything to process?
			var en = DependencyService.Get<IEncrypter> ();
			return en.GetEncryption (plainText);
		}
		public static string Decrypt(string plainText)
		{
			//Anything to process?
			var en = DependencyService.Get<IEncrypter> ();
			return en.GetDecryption (plainText);
		}

		public static string GetInputAsPhoneNumber(string oldText, string newText)
		{
			string phoneInput = newText;
			string phoneOutput = "";
			if ((newText.Length == 3 && newText.Length > oldText.Length) || (newText.Length == 4 && !newText.Contains ("-") && newText.Length > oldText.Length)) {
				phoneOutput = phoneInput.Insert (3, "-");
			} else if ((newText.Length == 7 && newText.Length > oldText.Length) || (newText.Length == 8 && !newText.Substring (5).Contains ("-") && newText.Length > oldText.Length)) {
				phoneOutput = phoneInput.Insert (7, "-");
			} else
				return phoneInput;
			return phoneOutput;
		}

		public static string ApiUrL = "http://teamsavagemma.com";

		public static bool ValidEmail(string email)
		{
			string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" 
				+ @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" 
				+ @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
			return regex.IsMatch (email);
		}

//		public static bool IsConnected()
//		{
//			return 
//		}
    }
}
