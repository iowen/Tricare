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
        public RegisterPage()
        {
			this.BackgroundColor = Color.White;
			App.DisableLogout ();
			prescriberRepo = new PrescriberRepo ();

			var overlay = new AbsoluteLayout (){ WidthRequest = this.Width };
			var content = new StackLayout();
			indi = new ActivityIndicator();
            this.SetBinding(ContentPage.TitleProperty, "Register");
			var a1 = new AutoCompleteView ();
			SearchCommand = new Command((key) =>
				{
					DisplayAlert("Search",st.Suggestions.Count.ToString(),"close");
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

			foreach (var sss in ss) {
				stateList.Add(sss);
			}
			st = new AutoCompleteView () {
				SearchBackgroundColor = Color.Transparent,
				ShowSearchButton = false,
				Suggestions = StateList,
				SearchCommand = SearchCommand,
				SelectedCommand = CellSelectedCommand,
				SuggestionBackgroundColor = Color.Gray,
				TextColor = Color.Black,
				SearchTextColor = Color.White,
				Placeholder = "",
			};
			var firstNameLabel = new Label { Text = "First Name" , TextColor = Color.Navy };
            var firstNameEntry = new Entry()
            {
				HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
                TextColor = Color.Black,
            };
            firstNameEntry.SetBinding(Entry.TextProperty, "FirstName");

			var lastNameLabel = new Label { Text = "Last Name" , TextColor = Color.Navy };
            var lastNameEntry = new Entry()
            {
				HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
                TextColor = Color.Black,
            };
            firstNameEntry.SetBinding(Entry.TextProperty, "LastName");


			var emailLabel = new Label { Text = "Email" , TextColor = Color.Navy };
            var emailEntry = new Entry()
            {
				HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
                TextColor = Color.Black,
            };
            emailEntry.SetBinding(Entry.TextProperty, "Email");

			var passwordLabel = new Label { Text = "Password" , TextColor = Color.Navy };
			var passwordEntry = new Entry(){
				IsPassword = true,
                BackgroundColor = Color.Transparent,
                TextColor = Color.Black
			}; 

			passwordEntry.SetBinding(Entry.TextProperty, "Password");

			var password2Label = new Label { Text = "Re-Enter Password" , TextColor = Color.Navy };
			var password2Entry = new Entry(){
				IsPassword = true,
                BackgroundColor = Color.Transparent,
                TextColor = Color.Black
			}; 


			var NpiNumberLabel = new Label { Text = "NPI Number" , TextColor = Color.Navy };
            var NpiNumberEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
            };
            NpiNumberEntry.SetBinding(Entry.TextProperty, "NpiNumber");

			var LicenseNumberLabel = new Label { Text = "License Number" , TextColor = Color.Navy };
            var LicenseNumberEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
            };
            LicenseNumberEntry.SetBinding(Entry.TextProperty, "LicenseNumber");

			var DeaNumberLabel = new Label { Text = "DEA Number" , TextColor = Color.Navy };
            var DeaNumberEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
            };
            DeaNumberEntry.SetBinding(Entry.TextProperty, "DeaNumber");

			var AddressLabel = new Label { Text = "Address" , TextColor = Color.Navy };
            var AddressEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
            };
            AddressEntry.SetBinding(Entry.TextProperty, "Address");

			var CityLabel = new Label { Text = "City", TextColor = Color.Navy  };
            var CityEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
            };
            CityEntry.SetBinding(Entry.TextProperty, "City");

			var StateLabel = new Label { Text = "State" , TextColor = Color.Navy };
			var StateEntry = st;

			var ZipLabel = new Label { Text = "Zip" , TextColor = Color.Navy };
            var ZipEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
            };
            ZipEntry.SetBinding(Entry.TextProperty, "Zip");

			var PhoneLabel = new Label { Text = "Phone" , TextColor = Color.Navy };
            var PhoneEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
            };
            PhoneEntry.SetBinding(Entry.TextProperty, "Phone");

			var FaxLabel = new Label { Text = "Fax" , TextColor = Color.Navy };
            var FaxEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
            };
            FaxEntry.SetBinding(Entry.TextProperty, "Fax");

			var registerButton = new Button { Text = "Register" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
            registerButton.Clicked += async (sender, e) =>
            {
				indi.IsRunning = true;
				registerButton.IsEnabled = false;
				#region Validation
				var validState = s.GetAllStates().Where(_st => _st.Name.Trim() == StateEntry.Text.Trim()).FirstOrDefault();
				int validZip = 0;
				int.TryParse(ZipEntry.Text,out validZip);
				long validPhone = 0;
				Int64.TryParse(PhoneEntry.Text,out validPhone);
				long validFax = 0;
				Int64.TryParse(FaxEntry.Text,out validFax);
				var prescriberList = prescriberRepo.GetAllPrescribers();
				var emailExists = prescriberList.Where(em => em.Email.Trim() == emailEntry.Text.Trim()).FirstOrDefault();
				if(string.IsNullOrWhiteSpace(firstNameEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a first name.", "OK");
					return;
				}
				else if(string.IsNullOrWhiteSpace(lastNameEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a last name.", "OK");
					return;
				}
				else if(string.IsNullOrWhiteSpace(emailEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter an email.", "OK");
					return;
				}
				else if(emailExists != null)
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "The email written already exists, please enter another.", "OK");
					return;
				}
				else if (passwordEntry.Text != password2Entry.Text)
                {
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
                    await DisplayAlert("Error", "Passwords must match.", "OK");
                    return;
                }
				else if (string.IsNullOrWhiteSpace(passwordEntry.Text))
                {
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
                    await DisplayAlert("Error", "Password is required", "OK");
                    return;
                }
				else if(string.IsNullOrWhiteSpace(NpiNumberEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter an NPI number.", "OK");
					return;
				}
				else if(string.IsNullOrWhiteSpace(LicenseNumberEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a License number.", "OK");
					return;
				}
				else if(string.IsNullOrWhiteSpace(DeaNumberEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a DEA number.", "OK");
					return;
				}
				else if(string.IsNullOrWhiteSpace(AddressEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter an Address.", "OK");
					return;
				}
				else if(string.IsNullOrWhiteSpace(CityEntry.Text))
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a City.", "OK");
					return;
				}
				else if(string.IsNullOrWhiteSpace(StateEntry.Text) || validState == null)
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a valid State.", "OK");
					return;
				}
				else if (validZip < 10000)
                {
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					//must be atleast 5 digits
                    await DisplayAlert("Error", "Invalid Zip Format, please enter 5 digits.", "OK");
                    return;
                }
				else if(validPhone < 1000000000)
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					//must be atleast 10 digits
					await DisplayAlert("Error", "Please enter a valid Phone number.", "OK");
					return;
				}
				else if(validFax < 1000000000)
				{
					indi.IsRunning = false;
					registerButton.IsEnabled = true;
					//must be atleast 10 digits
					await DisplayAlert("Error", "Please enter a valid fax number.", "OK");
					return;
				}
                var prescriberItem = new Prescriber() { Address = AddressEntry.Text, City = CityEntry.Text, DeaNumber = DeaNumberEntry.Text, Email = emailEntry.Text, Fax = FaxEntry.Text, FirstName = firstNameEntry.Text, LastName = lastNameEntry.Text, LicenseNumber = LicenseNumberEntry.Text, NpiNumber = NpiNumberEntry.Text, Password = passwordEntry.Text, Phone = PhoneEntry.Text, State = StateEntry.Text, Zip = int.Parse(ZipEntry.Text) };
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
    }
}
