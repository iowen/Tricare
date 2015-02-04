using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Data;
using TriCare.Models;
using TriCare.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Controls;

namespace TriCare.Views
{
    public class LoginPage : ContentPage
    {
		private ActivityIndicator indi;
		private AbsoluteLayout overlay;
		public LoginPage(bool isLogin = true)
        {
			App.IsHome = false;

			overlay = new AbsoluteLayout();
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
					if(!App.IsConnected())
					{
						await DisplayAlert ("Error", "Password request cannot be made without an internet connection.", "OK", "close");
						return;
					}
					loginButton.IsEnabled = false;
					registerButton.IsEnabled = false;
					forgotLabel.IsEnabled = false;
					await App.np.PushAsync(new ForgotPasswordPage());
					passwordEntry.Text = "";
					emailEntry.Text = "";
					loginButton.IsEnabled = true;
					registerButton.IsEnabled = true;
					forgotLabel.IsEnabled = true;
				};
				loginButton.Clicked += async (sender, e) => {
					loginButton.IsEnabled = false;
					registerButton.IsEnabled = false;
					forgotLabel.IsEnabled = false;
					var Command = new Command(async o => {
						indi.IsRunning = true;					
						var loginItem = new LoginModel () { Email = emailEntry.Text, Password = passwordEntry.Text };
						var prescriberRepo = new PrescriberRepo ();
						var loginState = await prescriberRepo.LoginPrescriber (loginItem);
						if (loginState.ToLower () == "success") {
							App.ClearCurrentPrescription ();

							passwordEntry.Text = "";
							emailEntry.Text = "";
							await App.np.PushAsync(new HomePage());
							loginButton.IsEnabled = true;
							registerButton.IsEnabled = true;
							forgotLabel.IsEnabled = true;
							indi.IsRunning = false;

						} else {
							loginButton.IsEnabled = true;
							registerButton.IsEnabled = true;
							forgotLabel.IsEnabled = true;
							indi.IsRunning = false;

							await DisplayAlert ("Error", "Invalid credentials provided", "OK", "close");

						}
					});
					Command.Execute(new []{"run"});



				};


				registerButton.Clicked += async (sender, e) => {
					if(!App.IsConnected())
					{
						await DisplayAlert ("Error", "Registration cannot be completed without an internet connection.", "OK", "close");
						return;
					}
					var Command = new Command(async o => {
						indi.IsRunning = true;					
							loginButton.IsEnabled = false;
							registerButton.IsEnabled = false;
						forgotLabel.IsEnabled = false;
							passwordEntry.Text = "";
							emailEntry.Text = "";
						indi.IsRunning = false;
							await App.np.PushAsync (new  RegisterPage());            
						loginButton.IsEnabled = true;
						registerButton.IsEnabled = true;
						forgotLabel.IsEnabled = true;
					});
					Command.Execute(new []{"run"});
				};

				content = new StackLayout {
					VerticalOptions = LayoutOptions.FillAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Padding = new Thickness (5),
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

				Content = new LoginScrollView () {
					Padding = new Thickness (20),
					VerticalOptions = LayoutOptions.StartAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Content = overlay

				};
			}
        }

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			if (!App.IsConnected ()) {
				DisplayAlert("Warning", "Functionality is limited because there is no internet connection.","OK");
			}
		}

    }
}
