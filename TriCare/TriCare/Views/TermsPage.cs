using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using TriCare.Data;
using TriCare.Models;
using TriCare.Validators;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace TriCare.Views
{
	public class TermsPage : ContentPage
	{
		private ActivityIndicator indi;
		private AbsoluteLayout overlay;
		public TermsPage (Prescriber prescriber)
		{
			overlay = new AbsoluteLayout();
			var content = new StackLayout();
			indi = new ActivityIndicator();
			this.BackgroundColor = Color.White;
			App.IsHome = false;
			App.DisableLogout ();
			this.Title = "Terms & Aggrement";
			var logoCell = new Image () {
				Source = "tcareLoginLogo.png"
			};
			var privacyTitle = new Label () { Text = "Privacy Policy", FontAttributes = FontAttributes.Bold, TextColor = Color.Blue };
			var privacyText = new Label() {Text = "This Privacy Policy governs the manner in which the TriCareWellness App. collects, uses, maintains and discloses information collected from users (each, a \"User\") of the TriCare Wellness App. This privacy policy applies to the App and all products and services offered by the TriCare Wellness App."};
			
			var pidTitle = new Label () { Text = "Personal Identification Information", FontAttributes = FontAttributes.Bold, TextColor = Color.Blue };
			var pidText = new Label() {Text = "We may collect personal identification information from Users in a variety of ways, including, but not limited to, when Users open our app, subscribe to a newsletter, fill out a form, and in connection with other activities, services, features or resources we make available through our app. Users may be asked for, as appropriate, name, email address. We will collect personal identification information from Users only if they voluntarily submit such information to us. Users can always refuse to supply personally identification information, except that it may prevent them from engaging in certain app related activities."};

			var npidTitle = new Label () { Text = "Non-Personal Identification Information", FontAttributes = FontAttributes.Bold, TextColor = Color.Blue };
				var npidText = new Label() {Text = "We may collect non-personal identification information about Users whenever they interact with our app. Non-personal identification information may include the browser name, the type of computer and technical information about Users means of connection to our app, such as the operating system and the Internet service providers utilized and other similar information."};

			var protectTitle = new Label () { Text = "How we protect your information", FontAttributes = FontAttributes.Bold , TextColor = Color.Blue};
				var protectText = new Label() {Text = "We adopt appropriate data collection, storage and processing practices and security measures to protect against unauthorized access, alteration, disclosure or destruction of your personal information, username, password, transaction information and data stored on our app."};

			var sharingTitle = new Label () { Text = "Sharing your personal information", FontAttributes = FontAttributes.Bold, TextColor = Color.Blue };
				var sharingText = new Label() {Text = "We do not sell, trade, or rent Users personal identification information to others. We may share generic aggregated demographic information not linked to any personal identification information regarding visitors and users with our business partners, trusted affiliates and advertisers for the purposes outlined above.We may use third party service providers to help us operate our business and the app or administer activities on our behalf, such as sending out newsletters or surveys. We may share your information with these third parties for those limited purposes provided that you have given us your permission."};

			var changesTitle = new Label () { Text = "Changes to this privacy policy", FontAttributes = FontAttributes.Bold, TextColor = Color.Blue};
				var changesText = new Label() {Text = "TriCare Wellness App has the discretion to update this privacy policy at any time. When we do, we will send you an email. We encourage Users to frequently check this page for any changes to stay informed about how we are helping to protect the personal information we collect. You acknowledge and agree that it is your responsibility to review this privacy policy periodically and become aware of modifications."};

			var PrescriptionTitle = new Label () { Text = "Filling Prescriptions", FontAttributes = FontAttributes.Bold, TextColor = Color.Blue };
				var PrescriptionText = new Label() {Text = "By sending a signed prescription you agree to a one time processing of the insurance and other necessary billiing information by the pharmacy."};

			var scroll = new StackLayout() {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children= {new ScrollView (){
						Content=new StackLayout() {
						Children={privacyTitle,privacyText,pidTitle,pidText,npidTitle,npidText,protectTitle,protectText,sharingTitle,sharingText,changesTitle,changesText,PrescriptionTitle,PrescriptionText},
					VerticalOptions =LayoutOptions.FillAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand
						},					
						VerticalOptions =LayoutOptions.FillAndExpand,
						HorizontalOptions = LayoutOptions.FillAndExpand
					}},
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			var continueButton = new Button (){ Text = "Accept",
				BackgroundColor = Color.FromRgb (52, 63, 169),
				TextColor = Color.White};
			var cancelButton = new Button () { Text = "Cancel" ,
				BackgroundColor = Color.Gray,
				TextColor = Color.White};

			continueButton.Clicked += async (sender, e) => {
				indi.IsRunning = true;
				if(!App.IsConnected())
				{
					await DisplayAlert ("Error", "Terms cannot be agreed to without an internet connection.", "OK", "close");
					indi.IsRunning = false;
					cancelButton.IsEnabled = true;
					continueButton.IsEnabled = true;
					return;
				}
				continueButton.IsEnabled = false;
				cancelButton.IsEnabled = false;

				var prescriberRepo = new PrescriberRepo();
				var res = await prescriberRepo.AddPrescriber(prescriber);
				var resultInt = int.Parse(res.ToString());
				if (resultInt > 0)
				{
					var sRepo = new SyncRepo();
					var sModel = new SyncModel();
					sModel.SyncType = 'a';
					sModel.LastAppDataSync = sRepo.GetLastAppUpdate ();

					await sRepo.GetSyncData(sModel);
					App.np.IsBusy = false;
					await DisplayAlert ("Alert", "Please check your email for steps on verifying your account.", "OK", "close");
					await App.np.PopToRootAsync();
				}
				else
				{
					continueButton.IsEnabled = true;
					cancelButton.IsEnabled = true;
					App.np.IsBusy = false;
					await DisplayAlert("Error", "An Error Occured Please Try Again", "OK");
				}
			};

			cancelButton.Clicked += async (sender, e) => {

					await App.np.PopToRootAsync();

			};

			var sl = new StackLayout (){Padding = new Thickness (20)};
			sl.Children.Add (cancelButton);
			sl.Children.Add (logoCell);
			sl.Children.Add (scroll);
			sl.Children.Add (continueButton);
			sl.VerticalOptions = LayoutOptions.FillAndExpand;
			sl.HorizontalOptions = LayoutOptions.FillAndExpand;
			content = sl;
			AbsoluteLayout.SetLayoutFlags(indi, AbsoluteLayoutFlags.PositionProportional);
			AbsoluteLayout.SetLayoutBounds(indi, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			overlay.Children.Add(content,new Rectangle (0, 0, 1, 1), AbsoluteLayoutFlags.All);
			overlay.Children.Add(indi);
			Content = new StackLayout () {
				Padding = new Thickness (20),
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = {overlay}

			};
		}
	}
}

