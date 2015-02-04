using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Data;
using TriCare.Models;
using TriCare.Views;
using TriCare.Validators;
using Xamarin.Forms;
using System.Windows.Input;

namespace TriCare
{
	public class EditPrescriberPage : ContentPage
	{
		private List<State> stList;

		private ActivityIndicator indi;

		private List<object> stateList =
			new List<object>();

		public List<object> StateList
		{
			get { return stateList; }
		}
		private AutoCompleteView st;
		private string searchText;
		public string SearchText{ get{ return searchText;} set{searchText = value; OnPropertyChanged (); }}

		public ICommand SearchCommand { get; set; }
		public ICommand CellSelectedCommand { get; set; }
		public EditPrescriberPage ()
		{
			var pRepo = new PrescriberRepo();
			var p = pRepo.GetPrescriber(int.Parse(App.Token));
			var overlay = new AbsoluteLayout();
			var content = new StackLayout();
			indi = new ActivityIndicator();
			this.BindingContext = p;
			this.BackgroundColor = Color.White;
			this.Title = "Edit Profile";

			var firstNameLabel = new Label { Text = "First Name" , TextColor = Color.Navy};
			var firstNameEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			firstNameEntry.SetBinding(Entry.TextProperty, "FirstName");

			var lastNameLabel = new Label { Text = "Last Name" , TextColor = Color.Navy};
			var lastNameEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			lastNameEntry.SetBinding(Entry.TextProperty, "LastName");

			var NpiNumberLabel = new Label { Text = "NPI Number" , TextColor = Color.Navy};
			var NpiNumberEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			NpiNumberEntry.SetBinding(Entry.TextProperty, "NpiNumber");

			var LicenseNumberLabel = new Label { Text = "License Number", TextColor = Color.Navy };
			var LicenseNumberEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			LicenseNumberEntry.SetBinding(Entry.TextProperty, "LicenseNumber");

			var DeaNumberLabel = new Label { Text = "DEA Number", TextColor = Color.Navy };
			var DeaNumberEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			DeaNumberEntry.SetBinding(Entry.TextProperty, "DeaNumber");

			var AddressLabel = new Label { Text = "Address", TextColor = Color.Navy };
			var AddressEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};

			var CellSelectedCommands = new Command<State>((key) =>
				{
					// Add the key to the input string.
					this.Opacity = 50;
				});
			var s = new StateRepo ();
			stList = s.GetAllStates ();

			foreach (var sss in stList) {
				stateList.Add(sss);
			}
			st = new AutoCompleteView () {
				SearchBackgroundColor = Color.Transparent,
				ShowSearchButton = false,
				Suggestions = StateList,
				SearchCommand = SearchCommand,
				SelectedCommand = CellSelectedCommands,
				SuggestionBackgroundColor = Color.Gray,
				TextColor = Color.Black,
				SearchTextColor = Color.White,
				SuggestionItemDataTemplate =  new DataTemplate (typeof (acCell)),
			};

			SearchCommand = new Command((key) =>
				{
					DisplayAlert("Search",st.Suggestions.Count.ToString(),"close");
					//DisplayAlert("Search",a.Sugestions.Count.ToString(),"close");
					//DisplayAlert("Search",IngredientList.Count.ToString(),"close");
					// Add the key to the input string.
				});

			var seleLoc = stList.IndexOf (stList.First (x => x.Name.Trim() == p.State.Trim()));
			//Got This Working
			st.SetText (StateList.ElementAt (seleLoc).ToString ());
			st.ShowHideListbox (false);
			AddressEntry.SetBinding(Entry.TextProperty, "Address");

			var CityLabel = new Label { Text = "City" , TextColor = Color.Navy};
			var CityEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			CityEntry.SetBinding(Entry.TextProperty, "City");

			var StateLabel = new Label { Text = "State", TextColor = Color.Navy };
			var StateEntry = st;

			var ZipLabel = new Label { Text = "Zip", TextColor = Color.Navy };
			var ZipEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			ZipEntry.SetBinding(Entry.TextProperty, "Zip");

			var PhoneLabel = new Label { Text = "Phone" , TextColor = Color.Navy};
			var PhoneEntry = new PhoneNumberEntry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			PhoneEntry.SetBinding(Entry.TextProperty, "Phone");
			PhoneEntry.BindingContextChanged += (sender, e) => {
				base.OnBindingContextChanged();
				if(PhoneEntry.Text.Trim().Length == 10)
				{
					var pn = PhoneEntry.Text.Insert (3, "-").Insert (7, "-");
					PhoneEntry.Text = pn;
				}
			};
			PhoneEntry.TextChanged +=  (sender, e) => {
				PhoneEntry.Text = App.GetInputAsPhoneNumber(e.OldTextValue, e.NewTextValue);
			};
			var FaxLabel = new Label { Text = "Fax", TextColor = Color.Navy };
			var FaxEntry = new PhoneNumberEntry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			FaxEntry.SetBinding(Entry.TextProperty, "Fax");
			FaxEntry.BindingContextChanged += (sender, e) => {
				base.OnBindingContextChanged();
				if(FaxEntry.Text.Trim().Length == 10)
				{
					var pn = FaxEntry.Text.Insert (3, "-").Insert (7, "-");
					FaxEntry.Text = pn;
				}
			};
			PhoneEntry.TextChanged +=  (sender, e) => {
				PhoneEntry.Text = App.GetInputAsPhoneNumber(e.OldTextValue, e.NewTextValue);
			};
			var saveButton = new Button { Text = "Save" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
			saveButton.Clicked += async (sender, e) =>
			{
				indi.IsRunning = true;
				saveButton.IsEnabled = false;
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
				if(string.IsNullOrWhiteSpace(firstNameEntry.Text))
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a first name.", "OK");
					firstNameLabel.Text = "* First Name *";
					firstNameLabel.TextColor = Color.Red;
					firstNameLabel.Focus();
					firstNameEntry.Focus();
					return;
				}
				else if(string.IsNullOrWhiteSpace(lastNameEntry.Text))
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a last name.", "OK");
					lastNameLabel.Text = "* Last Name *";
					lastNameLabel.TextColor = Color.Red;
					lastNameLabel.Focus();
					lastNameEntry.Focus();
					return;
				}
				else if(string.IsNullOrWhiteSpace(NpiNumberEntry.Text))
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter an NPI number.", "OK");
					NpiNumberLabel.Text = "* NPI Number *";
					NpiNumberLabel.TextColor = Color.Red;
					NpiNumberLabel.Focus();
					NpiNumberEntry.Focus();
					return;
				}
				else if(string.IsNullOrWhiteSpace(LicenseNumberEntry.Text))
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a License number.", "OK");
					LicenseNumberLabel.Text = "* License Number *";
					LicenseNumberLabel.TextColor = Color.Red;
					LicenseNumberLabel.Focus();
					LicenseNumberEntry.Focus();
					return;
				}
				else if(string.IsNullOrWhiteSpace(DeaNumberEntry.Text))
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a DEA number.", "OK");
					DeaNumberLabel.Text = "* DEA Number *";
					DeaNumberLabel.TextColor = Color.Red;
					DeaNumberLabel.Focus();
					DeaNumberEntry.Focus();
					return;
				}
				else if(string.IsNullOrWhiteSpace(AddressEntry.Text))
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter an Address.", "OK");
					AddressLabel.Text = "* Address *";
					AddressLabel.TextColor = Color.Red;
					AddressLabel.Focus();
					AddressEntry.Focus();
					return;
				}
				else if(string.IsNullOrWhiteSpace(CityEntry.Text))
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a City.", "OK");
					CityLabel.Text = "* City *";
					CityLabel.TextColor = Color.Red;
					CityLabel.Focus();
					CityEntry.Focus();
					return;
				}
				else if(string.IsNullOrWhiteSpace(StateEntry.Text) || validState == null)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter a valid State.", "OK");
					StateLabel.Text = "* State *";
					StateLabel.TextColor = Color.Red;
					StateLabel.Focus();
					StateEntry.Focus();
					return;
				}
				else if (validZip < 10000)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					//must be atleast 5 digits
					await DisplayAlert("Error", "Invalid Zip Format, please enter 5 digits.", "OK");
					ZipLabel.Text = "* Zip *";
					ZipLabel.TextColor = Color.Red;
					ZipLabel.Focus();
					ZipEntry.Focus();
					return;
				}
				else if(validPhone < 1000000000)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					//must be atleast 10 digits
					await DisplayAlert("Error", "Please enter a valid Phone number.", "OK");
					PhoneLabel.Text = "* Phone *";
					PhoneLabel.TextColor = Color.Red;
					PhoneLabel.Focus();
					PhoneEntry.Focus();
					return;
				}
				else if(validFax < 1000000000)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					//must be atleast 10 digits
					await DisplayAlert("Error", "Please enter a valid fax number.", "OK");
					FaxLabel.Text = "* Fax *";
					FaxLabel.TextColor = Color.Red;
					FaxLabel.Focus();
					FaxEntry.Focus();
					return;
				}
				var prescriberItem = new Prescriber() { Address = AddressEntry.Text, City = CityEntry.Text, DeaNumber = DeaNumberEntry.Text, Email = p.Email.Trim(), Fax = fxTxt, FirstName = firstNameEntry.Text, LastName = lastNameEntry.Text, LicenseNumber = LicenseNumberEntry.Text, NpiNumber = NpiNumberEntry.Text, Password = p.Password.Trim(), Phone = phneTxt, State = StateEntry.Text, Zip = int.Parse(ZipEntry.Text) };
				string msg;
				var bR = PrescriberValidator.Validate(prescriberItem, out msg);
				if(!bR)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Error", msg, "OK");
					return;
				}
				#endregion

				Prescriber item = new Prescriber(){
					PrescriberId = p.PrescriberId,
					AccountId = p.AccountId,
					FirstName = firstNameEntry.Text.Trim(),
					LastName = lastNameEntry.Text.Trim(),
					NpiNumber = NpiNumberEntry.Text.Trim(),
					LicenseNumber = LicenseNumberEntry.Text.Trim(),	
					DeaNumber = DeaNumberEntry.Text.Trim(),	
					Address = AddressEntry.Text.Trim(),
					City = CityEntry.Text.Trim(),
					State = StateEntry.Text.Trim(),
					Zip = int.Parse(ZipEntry.Text),
					Phone = PhoneEntry.Text.Trim().Replace("-",""),
					Fax = FaxEntry.Text.Trim().Replace("-",""),
					Email = p.Email.Trim(),
					Password = p.Password.Trim(),
					LastUpdate = DateTime.Now
				};

				PrescriberRepo presRepo = new PrescriberRepo();
				var res = await presRepo.UpdatePrescriber(item);
				indi.IsRunning = false;
				if(res == true)
				{
					var Command = new Command(async o => {
						await App.np.PopToRootAsync(false);
						var pg = new HomePage();
						pg.CurrentPage = pg.Children.Last();
						await  App.np.PushAsync(pg,false);
					});
					Command.Execute(new []{"run"});

				}
			};
			content = new StackLayout 
				{
					VerticalOptions = LayoutOptions.FillAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Padding = new Thickness(20),
					Children={
						firstNameLabel, firstNameEntry, 
						lastNameLabel, lastNameEntry,
						NpiNumberLabel, NpiNumberEntry, 
						LicenseNumberLabel, LicenseNumberEntry,
						DeaNumberLabel, DeaNumberEntry,
						AddressLabel, AddressEntry,
						CityLabel, CityEntry,
						StateLabel, StateEntry, 
						ZipLabel, ZipEntry, 
						PhoneLabel, PhoneEntry,
						FaxLabel, FaxEntry,
						saveButton
					}
				
			};

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