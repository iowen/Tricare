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

    public class RegisterPage : ContentPage
    {
		private List<object> stateList =
			new List<object>();
		private ActivityIndicator indi;
		private PrescriberRepo prescriberRepo; 
		public ICommand SearchCommand { get; set; }
		public ICommand CellSelectedCommand { get; set; }

		public List<object> StateList
		{
			get { return stateList; }
		}
		private AutoCompleteView st;
		private string searchText;
		public string SearchText{ get{ return searchText;} set{searchText = value; OnPropertyChanged (); }}

		private List<Label> formLabelList;

        public RegisterPage()
        {
			this.BackgroundColor = Color.White;
			App.DisableLogout ();
			prescriberRepo = new PrescriberRepo ();
			formLabelList = new List<Label> ();
			var overlay = new AbsoluteLayout (){ WidthRequest = this.Width };
			var content = new StackLayout();
			indi = new ActivityIndicator();
            this.SetBinding(ContentPage.TitleProperty, "Register");
			var a1 = new AutoCompleteView ();
			SearchCommand = new Command((key) =>
				{
					//DisplayAlert("Search",st.Suggestions.Count.ToString(),"close");
					//DisplayAlert("Search",a.Sugestions.Count.ToString(),"close");
					//DisplayAlert("Search",IngredientList.Count.ToString(),"close");
					// Add the key to the input string.
				});
					
			var CellSelectedCommand = new Command<State>((key) =>
				{
					// Add the key to the input string.
					this.Opacity = 50;
				});
			var s = new StateRepo ();
			var ss = s.GetAllStates ();
			stateList.Clear ();
			foreach (var sss in ss) {
				stateList.Add(sss);
			}
			st = new AutoCompleteView () {
				SearchBackgroundColor = Color.Transparent,
				ShowSearchButton = false,
				Suggestions = StateList,
				SearchCommand = SearchCommand,
				SelectedCommand = CellSelectedCommand,
				SuggestionItemDataTemplate =  new DataTemplate (typeof (acCell)),
				SuggestionBackgroundColor = Color.Gray,
				TextColor = Color.Black,
				SearchTextColor = Color.White,
				Text = ""
			};

			var firstNameLabel = new Label { Text = "First Name" , TextColor = Color.Navy };
            var firstNameEntry = new Entry()
            {
				HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
                TextColor = Color.Black,
				Text = ""
            };
            firstNameEntry.SetBinding(Entry.TextProperty, "FirstName");
			formLabelList.Add (firstNameLabel);

			var lastNameLabel = new Label { Text = "Last Name" , TextColor = Color.Navy };
            var lastNameEntry = new Entry()
            {
				HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
                TextColor = Color.Black,
				Text = ""
            };
            lastNameEntry.SetBinding(Entry.TextProperty, "LastName");
			formLabelList.Add (lastNameLabel);

			var emailLabel = new Label { Text = "Email" , TextColor = Color.Navy };
            var emailEntry = new Entry()
            {
				HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
                TextColor = Color.Black,
				Text = ""
            };
            emailEntry.SetBinding(Entry.TextProperty, "Email");
			formLabelList.Add (emailLabel);

			var passwordLabel = new Label { Text = "Password" , TextColor = Color.Navy };
			var passwordEntry = new Entry(){
				IsPassword = true,
                BackgroundColor = Color.Transparent,
                TextColor = Color.Black,
				Text = ""
			}; 

			passwordEntry.SetBinding(Entry.TextProperty, "Password");
			formLabelList.Add (passwordLabel);

			var password2Label = new Label { Text = "Re-Enter Password" , TextColor = Color.Navy };
			var password2Entry = new Entry(){
				IsPassword = true,
                BackgroundColor = Color.Transparent,
                TextColor = Color.Black,
				Text = ""
			}; 


			var NpiNumberLabel = new Label { Text = "NPI Number" , TextColor = Color.Navy };
            var NpiNumberEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
				Text = ""
            };
            NpiNumberEntry.SetBinding(Entry.TextProperty, "NpiNumber");
			formLabelList.Add (NpiNumberLabel);

			var LicenseNumberLabel = new Label { Text = "License Number" , TextColor = Color.Navy };
            var LicenseNumberEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
				Text = ""
            };
            LicenseNumberEntry.SetBinding(Entry.TextProperty, "LicenseNumber");
			formLabelList.Add (LicenseNumberLabel);

			var DeaNumberLabel = new Label { Text = "DEA Number" , TextColor = Color.Navy };
            var DeaNumberEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
				Text = ""
            };
            DeaNumberEntry.SetBinding(Entry.TextProperty, "DeaNumber");
			formLabelList.Add (DeaNumberLabel);

			var AddressLabel = new Label { Text = "Address" , TextColor = Color.Navy };
            var AddressEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
				Text = ""
            };
            AddressEntry.SetBinding(Entry.TextProperty, "Address");
			formLabelList.Add (AddressLabel);

			var CityLabel = new Label { Text = "City", TextColor = Color.Navy  };
            var CityEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
				Text = ""
            };
            CityEntry.SetBinding(Entry.TextProperty, "City");
			formLabelList.Add (CityLabel);

			var StateLabel = new Label { Text = "State" , TextColor = Color.Navy };
			var StateEntry = st;
			formLabelList.Add (StateLabel);

			var ZipLabel = new Label { Text = "Zip" , TextColor = Color.Navy };
            var ZipEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
				Text = ""
            };
            ZipEntry.SetBinding(Entry.TextProperty, "Zip");
			formLabelList.Add (ZipLabel);

			var PhoneLabel = new Label { Text = "Phone" , TextColor = Color.Navy };
            var PhoneEntry = new PhoneNumberEntry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
				Text = ""
            };
			formLabelList.Add (PhoneLabel);

            PhoneEntry.SetBinding(Entry.TextProperty, "Phone");
			PhoneEntry.TextChanged +=  (sender, e) => {
				var pnum = sender as PhoneNumberEntry;
				pnum.Text =  App.GetInputAsPhoneNumber(e.OldTextValue, e.NewTextValue);
			};
			var FaxLabel = new Label { Text = "Fax" , TextColor = Color.Navy };
            var FaxEntry = new PhoneNumberEntry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
				Text = ""
            };
			formLabelList.Add (FaxLabel);

            FaxEntry.SetBinding(Entry.TextProperty, "Fax");
			FaxEntry.TextChanged +=  (sender, e) => {
				var fnum = sender as PhoneNumberEntry;
				fnum.Text =  App.GetInputAsPhoneNumber(e.OldTextValue, e.NewTextValue);
			};

			var registerButton = new Button { Text = "Register" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
            registerButton.Clicked += async (sender, e) =>
            {
				if(!App.IsConnected())
				{
					await DisplayAlert ("Error", "Registration cannot be completed without an internet connection.", "OK", "close");
					return;
				}
				indi.IsRunning = true;
				registerButton.IsEnabled = false;
				#region Validation
				var validState = s.GetAllStates().Where(_st => _st.Name.Trim() == StateEntry.Text.Trim()).FirstOrDefault();
				int validZip = 0;
				int.TryParse(ZipEntry.Text,out validZip);
				long validPhone = 0;
				var phneTxt = PhoneEntry.Text.Replace("-","");
				Int64.TryParse(phneTxt,out validPhone);
				long validFax = 0;
				var fxTxt = FaxEntry.Text.Replace("-","");

				Int64.TryParse(fxTxt,out validFax);
				var emailExists = await prescriberRepo.IsEmailTaken(emailEntry.Text.Trim());
				if(string.IsNullOrWhiteSpace(firstNameEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a first name.", "OK");
					firstNameLabel.Text = "* First Name *";
					firstNameLabel.TextColor = Color.Red;
					firstNameLabel.Focus();
					firstNameEntry.Focus();
					updateLabels(formLabelList,firstNameLabel);
					return;
				}
				else if(string.IsNullOrWhiteSpace(lastNameEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a last name.", "OK");
					lastNameLabel.Text = "* Last Name *";
					lastNameLabel.TextColor = Color.Red;
					lastNameLabel.Focus();
					lastNameEntry.Focus();
					updateLabels(formLabelList,lastNameLabel);
					return;
				}
				else if(string.IsNullOrWhiteSpace(emailEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter an email.", "OK");
					emailLabel.Text = "* Email *";
					emailLabel.TextColor = Color.Red;
					emailLabel.Focus();
					emailEntry.Focus();
					updateLabels(formLabelList,emailLabel);
					return;
				}
				else if (!App.ValidEmail(emailEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter an valid email.", "OK");
					emailLabel.Text = "* Email *";
					emailLabel.TextColor = Color.Red;
					emailLabel.Focus();
					emailEntry.Focus();
					updateLabels(formLabelList,emailLabel);
					return;
				}
				else if(emailExists)
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "The email written already exists, please enter another.", "OK");
					emailLabel.Text = "* Email *";
					emailLabel.TextColor = Color.Red;
					emailLabel.Focus();
					emailEntry.Focus();
					updateLabels(formLabelList,emailLabel);
					return;
				}
				else if (passwordEntry.Text != password2Entry.Text)
                {
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
                    await DisplayAlert("Error", "Passwords must match.", "OK");
					passwordLabel.Text = "* Password *";
					passwordLabel.TextColor = Color.Red;
					passwordLabel.Focus();
					passwordEntry.Focus();
					updateLabels(formLabelList,passwordLabel);
                    return;
                }
				else if (string.IsNullOrWhiteSpace(passwordEntry.Text))
                {
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
                    await DisplayAlert("Error", "Password is required", "OK");
					passwordLabel.Text = "* Password *";
					passwordLabel.TextColor = Color.Red;
					passwordLabel.Focus();
					passwordEntry.Focus();
					updateLabels(formLabelList,passwordLabel);
                    return;
                }
				else if(string.IsNullOrWhiteSpace(NpiNumberEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter an NPI number.", "OK");
					NpiNumberLabel.Text = "* NPI Number *";
					NpiNumberLabel.TextColor = Color.Red;
					NpiNumberLabel.Focus();
					NpiNumberEntry.Focus();
					updateLabels(formLabelList,NpiNumberLabel);
					return;
				}
				else if(string.IsNullOrWhiteSpace(LicenseNumberEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a License number.", "OK");
					LicenseNumberLabel.Text = "* License Number *";
					LicenseNumberLabel.TextColor = Color.Red;
					LicenseNumberLabel.Focus();
					LicenseNumberEntry.Focus();
					updateLabels(formLabelList,LicenseNumberLabel);
					return;
				}
				else if(string.IsNullOrWhiteSpace(DeaNumberEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a DEA number.", "OK");
					DeaNumberLabel.Text = "* DEA Number *";
					DeaNumberLabel.TextColor = Color.Red;
					DeaNumberLabel.Focus();
					DeaNumberEntry.Focus();
					updateLabels(formLabelList,DeaNumberLabel);
					return;
				}
				else if(string.IsNullOrWhiteSpace(AddressEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter an Address.", "OK");
					AddressLabel.Text = "* Address *";
					AddressLabel.TextColor = Color.Red;
					AddressLabel.Focus();
					AddressEntry.Focus();
					updateLabels(formLabelList,AddressLabel);
					return;
				}
				else if(string.IsNullOrWhiteSpace(CityEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a City.", "OK");
					CityLabel.Text = "* City *";
					CityLabel.TextColor = Color.Red;
					CityLabel.Focus();
					CityEntry.Focus();
					updateLabels(formLabelList,CityLabel);
					return;
				}
				else if(string.IsNullOrWhiteSpace(StateEntry.Text) || validState == null)
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a valid State.", "OK");
					StateLabel.Text = "* State *";
					StateLabel.TextColor = Color.Red;
					StateLabel.Focus();
					StateEntry.Focus();
					updateLabels(formLabelList,StateLabel);
					return;
				}
				else if (validZip < 10000)
                {
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					//must be atleast 5 digits
                    await DisplayAlert("Error", "Invalid Zip Format, please enter 5 digits.", "OK");
					ZipLabel.Text = "* Zip *";
					ZipLabel.TextColor = Color.Red;
					ZipLabel.Focus();
					ZipEntry.Focus();
					updateLabels(formLabelList,ZipLabel);
                    return;
                }
				else if(validPhone < 1000000000)
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					//must be atleast 10 digits
					await DisplayAlert("Error", "Please enter a valid Phone number.", "OK");
					PhoneLabel.Text = "* Phone *";
					PhoneLabel.TextColor = Color.Red;
					PhoneLabel.Focus();
					PhoneEntry.Focus();
					updateLabels(formLabelList,PhoneLabel);
					return;
				}
				else if(validFax < 1000000000)
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					//must be atleast 10 digits
					await DisplayAlert("Error", "Please enter a valid fax number.", "OK");
					FaxLabel.Text = "* Fax *";
					FaxLabel.TextColor = Color.Red;
					FaxLabel.Focus();
					FaxEntry.Focus();
					updateLabels(formLabelList,FaxLabel);
					return;
				}
				var prescriberItem = new Prescriber() { Address = AddressEntry.Text, City = CityEntry.Text, DeaNumber = DeaNumberEntry.Text, Email = emailEntry.Text, Fax = fxTxt, FirstName = firstNameEntry.Text, LastName = lastNameEntry.Text, LicenseNumber = LicenseNumberEntry.Text, NpiNumber = NpiNumberEntry.Text, Password = passwordEntry.Text, Phone = phneTxt, State = StateEntry.Text, Zip = int.Parse(ZipEntry.Text) };
                string msg;
                var bR = PrescriberValidator.Validate(prescriberItem, out msg);
                if(!bR)
                {
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
                    await DisplayAlert("Error", msg, "OK");
                    return;
                }
				#endregion

                //var prescriberRepo = new PrescriberRepo();
                // send webservice request and so on
                var res = await prescriberRepo.AddPrescriber(prescriberItem);
				indi.IsRunning = false;

				var resultInt = int.Parse(res.ToString());
				if (resultInt > 0)
                {
					var sRepo = new SyncRepo();
					var sModel = new SyncModel();
					sModel.SyncType = 'a';
					sModel.LastAppDataSync = sRepo.GetLastAppUpdate ();
					await sRepo.GetSyncData(sModel);
					App.SaveToken(resultInt.ToString());
					App.ClearCurrentPrescription ();
					var Command = new Command(async o => {
						await App.np.PopToRootAsync(false);
						var pg = new HomePage();
						pg.CurrentPage = pg.Children.Last();
						await  App.np.PushAsync(pg,false);
					});
					Command.Execute(new []{"run"});
                }
                else
                {
					registerButton.IsEnabled = true;
                    await DisplayAlert("Error", "An Error Occured Please Try Again", "OK");
                }
            };
	//		firstNameEntry.Placeholder = "                                                                ";
			content = new StackLayout 
				{
					VerticalOptions = LayoutOptions.FillAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Padding = new Thickness(20),
					Children={
					firstNameLabel, firstNameEntry, 
					lastNameLabel, lastNameEntry,
					emailLabel, emailEntry, 
						passwordLabel, passwordEntry,
						password2Label, password2Entry,
					NpiNumberLabel, NpiNumberEntry, 
					LicenseNumberLabel, LicenseNumberEntry,
					DeaNumberLabel, DeaNumberEntry,
						AddressLabel, AddressEntry,
					CityLabel, CityEntry,
					StateLabel, StateEntry, 
					ZipLabel, ZipEntry, 
					PhoneLabel, PhoneEntry,
					FaxLabel, FaxEntry,
					registerButton
				}

			};
//            content = new StackLayout
//            {
//				VerticalOptions = LayoutOptions.FillAndExpand,
//				HorizontalOptions = LayoutOptions.FillAndExpand,
//				Children = {
//               scrollview
//             		}
//            };
			//AbsoluteLayout.SetLayoutFlags(content, AbsoluteLayoutFlags.PositionProportional);

			//AbsoluteLayout.SetLayoutBounds(content,new Rectangle (0, 0, 1, 1), AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutFlags(indi, AbsoluteLayoutFlags.PositionProportional);
			AbsoluteLayout.SetLayoutBounds(indi, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			overlay.Children.Add(indi);
			overlay.Children.Add(content,new Rectangle (0, 0, 1, 1), AbsoluteLayoutFlags.All);
			Content = new ScrollView () {
				Content = overlay
			};

        }

		private void updateLabels(List<Label> formLabels, Label currentLabel)
		{
			/*
			 * if currentlabel is null, it means to reset all labels to original text 
			 * and remove red color signifying error has previously occurred.
			*/
			foreach (var label in formLabels) {
				if (currentLabel == null) {
					label.TextColor = Color.Black;
					label.Text = label.Text.Replace ("*", "").Trim ();
				}
				else if (label != currentLabel) {
					label.TextColor = Color.Black;
					label.Text = label.Text.Replace ("*", "").Trim ();
				}
			}
		}
    }
}
