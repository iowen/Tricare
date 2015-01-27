using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Data;
using TriCare.Models;
using TriCare.Utilities;
using Xamarin.Forms;

namespace TriCare.Views
{
    public class LoginPage : ContentPage
    {
		private ActivityIndicator indi;
		public LoginPage(bool isLogin = true)
        {
			App.IsHome = false;

			var overlay = new AbsoluteLayout();
			var content = new StackLayout();
			 indi = new ActivityIndicator();
			if (isLogin) {
				this.BackgroundColor = Color.White;
				var logoCell = new Image () {
					Source = "tcareLoginLogo.png"
				};

				NavigationPage.SetHasNavigationBar (this, false);
				this.SetBinding (ContentPage.TitleProperty, "Login");
				var emailLabel = new Label { Text = "Email", TextColor = Color.Navy  };
				var emailEntry = new Entry () {
					BackgroundColor = Color.Transparent,
					TextColor = Color.Black,
				};
				var sr = new StateRepo();
				if (sr.InsertRecords()){
					var r = sr.GetStates();
				}
				emailEntry.SetBinding (Entry.TextProperty, "Email");

				var passwordLabel = new Label { Text = "Password", TextColor = Color.Navy   };
				var passwordEntry = new Entry () {
					BackgroundColor = Color.Transparent,
					TextColor = Color.Black,
					IsPassword = true
				};
				passwordEntry.SetBinding (Entry.TextProperty, "Password");

				var loginButton = new Button {
					Text = "Log In" ,
					BackgroundColor = Color.FromRgb (52, 63, 169),
					TextColor = Color.White
				};
				var registerButton = new Button {
					Text = "Register",
					BackgroundColor = Color.FromRgba (128, 128, 128, 128),
					TextColor = Color.White
				};
				var forgotLabel = new Button {
					Text = "Forgot Password?",
					TextColor = Color.Red,
					BackgroundColor = Color.Transparent
				};
				forgotLabel.Clicked += async (sender, e) => {
					await App.np.PushAsync(new ForgotPasswordPage());
				};
				loginButton.Clicked += async (sender, e) => {
					loginButton.IsEnabled = false;
					registerButton.IsEnabled = false;

					var Command = new Command(async o => {
						indi.IsRunning = true;					
						var loginItem = new LoginModel () { Email = emailEntry.Text, Password = passwordEntry.Text };
						var prescriberRepo = new PrescriberRepo ();
						var loginState = await prescriberRepo.LoginPrescriber (loginItem);
						indi.IsRunning = false;
						if (loginState.ToLower () == "success") {
							App.ClearCurrentPrescription ();
							loginButton.IsEnabled = true;
							registerButton.IsEnabled = true;
							passwordEntry.Text = "";
							emailEntry.Text = "";
							await App.np.PushAsync(new HomePage());
						} else {
							loginButton.IsEnabled = true;
							registerButton.IsEnabled = true;
							await DisplayAlert ("Error", "Invalid credentials provided", "OK", "close");

						}
					});
					Command.Execute(new []{"run"});



				};


				registerButton.Clicked += async (sender, e) => {
					await App.np.PushAsync (new  RegisterPage());            
				};

				content = new StackLayout {
					VerticalOptions = LayoutOptions.FillAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Padding = new Thickness (20),
					Children = {
						logoCell,
						emailLabel, emailEntry, 
						passwordLabel, passwordEntry,
						loginButton, registerButton, forgotLabel
					}
				};
//				AbsoluteLayout.SetLayoutFlags(content, AbsoluteLayoutFlags.PositionProportional);
//				AbsoluteLayout.SetLayoutBounds(content, new Rectangle(0f, 0f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
				AbsoluteLayout.SetLayoutFlags(indi, AbsoluteLayoutFlags.PositionProportional);
				AbsoluteLayout.SetLayoutBounds(indi, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
				overlay.Children.Add(content,new Rectangle (0, 0, 1, 1), AbsoluteLayoutFlags.All);
				overlay.Children.Add(indi);
				Content = new ScrollView () {
					VerticalOptions = LayoutOptions.CenterAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Content = overlay
				};

			}
        }
		public  void ReLoad()
		{
			this.Navigation.PushAsync (new LoginPage ());
		}
    }
}
