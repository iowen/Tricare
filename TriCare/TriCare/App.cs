using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TriCare.Views;
using Xamarin.Forms;

namespace TriCare
{
    public class App
    {
        public static Page GetMainPage()
        {
            var mainNav = new NavigationPage(new LoginPage());

            return mainNav;
        }

        public static bool IsLoggedIn
        {
            get { return !string.IsNullOrWhiteSpace(_Token); }
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
    }
}
