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
			this.SetBinding(ContentPage.TitleProperty, "Edit Profile");

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


			var emailLabel = new Label { Text = "Email" , TextColor = Color.Navy};
			var emailEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			emailEntry.SetBinding(Entry.TextProperty, "Email");


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
			var PhoneEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			PhoneEntry.SetBinding(Entry.TextProperty, "Phone");

			var FaxLabel = new Label { Text = "Fax", TextColor = Color.Navy };
			var FaxEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			FaxEntry.SetBinding(Entry.TextProperty, "Fax");

			var saveButton = new Button { Text = "Save" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
			saveButton.Clicked += async (sender, e) =>
			{
				indi.IsRunning = true;
				saveButton.IsEnabled = false;
				#region Validation 
				int zipval = 0;
				int.TryParse(ZipEntry.Text.Trim(),out zipval);
				long phoneval = 0;
				long faxval = 0;
				Int64.TryParse(FaxEntry.Text.Trim(), out faxval);
				Int64.TryParse(PhoneEntry.Text.Trim(), out phoneval);
				if(firstNameEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					await DisplayAlert("Alert!","Please enter a first name","Ok");
					return;
				}
				else if(lastNameEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter a last name","Ok");
					return;
				}
				else if(emailEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter an email address","Ok");
					return;
				}
				else if(NpiNumberEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter an NPI number","Ok");
					return;
				}
				else if(LicenseNumberEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter an License Number","Ok");
					return;
				}
				else if(DeaNumberEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter an DEA Number","Ok");
					return;
				}
				else if(AddressEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter an Address","Ok");
					return;
				}
				else if(CityEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter a City","Ok");
					return;
				}
				else if(StateEntry.Text.Trim().Length <= 0 || stList.Where(_st => _st.Name.Trim() == StateEntry.Text.Trim()).FirstOrDefault() == null)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter a valid State","Ok");
					return;
				}
				else if(zipval < 10000)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter a vlaid ZIP Code","Ok");
					return;
				}
				else if(phoneval < 1000000000)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter 10 digits for Phone Number","Ok");
					return;
				}
				else if(faxval < 1000000000)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter 10 digits for Fax Number","Ok");
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
					Phone = PhoneEntry.Text.Trim(),
					Fax = FaxEntry.Text.Trim(),
					Email = emailEntry.Text.Trim(),
					Password = p.Password.Trim(),
					LastUpdate = DateTime.Now
				};

				PrescriberRepo presRepo = new PrescriberRepo();
				var res = await presRepo.UpdatePrescriber(item);
				indi.IsRunning = false;
				if(res == true)
				{
					await App.np.PopAsync();

				}
			};
			var scrollview = new ScrollView 
			{
				VerticalOptions = LayoutOptions.StartAndExpand,
				Content = new StackLayout 
				{
					VerticalOptions = LayoutOptions.StartAndExpand,
					Padding = new Thickness(20),
					Children={
						firstNameLabel, firstNameEntry, 
						lastNameLabel, lastNameEntry,
						emailLabel, emailEntry, 
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
				}
			};
			content = new StackLayout
			{
				Children = {
					scrollview
				}
			};
			AbsoluteLayout.SetLayoutFlags(content, AbsoluteLayoutFlags.PositionProportional);
			AbsoluteLayout.SetLayoutBounds(content, new Rectangle(0f, 0f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			AbsoluteLayout.SetLayoutFlags(indi, AbsoluteLayoutFlags.PositionProportional);
			AbsoluteLayout.SetLayoutBounds(indi, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			overlay.Children.Add(content);
			overlay.Children.Add(indi);
			Content = new ScrollView () {
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Content = overlay
			};

		}
	}
}