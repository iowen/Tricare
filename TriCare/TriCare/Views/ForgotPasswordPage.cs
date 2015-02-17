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
    public class ForgotPasswordPage : ContentPage
    {
		private ActivityIndicator indi;
		private Entry emailEntry;
		public ForgotPasswordPage(bool isLogin = true)
		{
			App.IsHome = false;
			App.DisableLogout ();
			var overlay = new AbsoluteLayout ();
			var content = new StackLayout ();
			indi = new ActivityIndicator ();

			this.BackgroundColor = Color.White;
			var logoCell = new Image () {
				Source = "tcareLoginLogo.png"
			};

			this.SetBinding (ContentPage.TitleProperty, "Forgot Password");
			var emailLabel = new Label { Text = "Email", TextColor = Color.Navy  };
			emailEntry = new Entry () {
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
				Text=""
			};

			var forgotLabel = new Label {
				Text = "Please enter the email address you used to sign up.",
				TextColor = Color.Red,
				BackgroundColor = Color.Transparent
			};
			var submitButton = new Button {
				Text = "Submit",
				BackgroundColor = Color.FromRgba (128, 128, 128, 128),
				TextColor = Color.White
			};

			submitButton.Clicked += async (sender, e) => {
				if(!App.IsConnected())
				{
					await DisplayAlert ("Error", "Password request cannot be made without an internet connection.", "OK", "close");
					return;
				}
				var sRepo = new SyncRepo();
				submitButton.IsEnabled = false;
				indi.IsRunning = true;
				//send to api
				if(String.IsNullOrWhiteSpace(emailEntry.Text.Trim()))
					{
						var Command1 = new Command(async o => {
						indi.IsRunning = false;
							await DisplayAlert("Message","Please enter a valid email address.","Close");
						submitButton.IsEnabled = true;
						});
						Command1.Execute(new []{"run"});

					}
				else if (!App.ValidEmail(emailEntry.Text.Trim()))
				{
					var Command2 = new Command(async o => {
						indi.IsRunning = false;
						await DisplayAlert("Message","Please enter a valid email address.","Close");
						submitButton.IsEnabled = true;
					});
					Command2.Execute(new []{"run"});
				}
				else{
				var Command = new Command(async o => {
						var prescriberRepo = new PrescriberRepo();
						var emailExists = await prescriberRepo.IsEmailTaken(emailEntry.Text.Trim());
						if(!emailExists)
						{
							await DisplayAlert("Message","This email is not registered. Please try again.","Close");
							submitButton.IsEnabled = true;
							indi.IsRunning = false;

							return;
						}
						sRepo.SendForgotPassword(emailEntry.Text.Trim());
						indi.IsRunning = false;

					await DisplayAlert("Message","Please check email for instructions on obtaining your credentials","Close");
					await App.np.PopToRootAsync();
				});
				Command.Execute(new []{"run"});
				}

			};

			content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (20),
				Children = {
					logoCell, forgotLabel,
					emailLabel, emailEntry, 
					submitButton
				}
			};
//				AbsoluteLayout.SetLayoutFlags(content, AbsoluteLayoutFlags.PositionProportional);
//				AbsoluteLayout.SetLayoutBounds(content, new Rectangle(0f, 0f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			AbsoluteLayout.SetLayoutFlags (indi, AbsoluteLayoutFlags.PositionProportional);
			AbsoluteLayout.SetLayoutBounds (indi, new Rectangle (0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			overlay.Children.Add (content, new Rectangle (0, 0, 1, 1), AbsoluteLayoutFlags.All);
			overlay.Children.Add (indi);
			Content = new LoginScrollView () {
				VerticalOptions = LayoutOptions.StartAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Content = overlay
			};
		}
    }
}
