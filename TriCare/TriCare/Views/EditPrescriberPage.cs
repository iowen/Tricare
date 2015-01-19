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

			this.BindingContext = p;
			this.BackgroundColor = Color.White;
			this.SetBinding(ContentPage.TitleProperty, "Edit Profile");

			var firstNameLabel = new Label { Text = "First Name" , TextColor = Color.Navy};
			var firstNameEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			firstNameEntry.SetBinding(Entry.TextProperty, "FirstName");

			var lastNameLabel = new Label { Text = "Last Name" , TextColor = Color.Navy};
			var lastNameEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			lastNameEntry.SetBinding(Entry.TextProperty, "LastName");


			var emailLabel = new Label { Text = "Email" , TextColor = Color.Navy};
			var emailEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			emailEntry.SetBinding(Entry.TextProperty, "Email");


			var NpiNumberLabel = new Label { Text = "NPI Number" , TextColor = Color.Navy};
			var NpiNumberEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			NpiNumberEntry.SetBinding(Entry.TextProperty, "NpiNumber");

			var LicenseNumberLabel = new Label { Text = "License Number", TextColor = Color.Navy };
			var LicenseNumberEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			LicenseNumberEntry.SetBinding(Entry.TextProperty, "LicenseNumber");

			var DeaNumberLabel = new Label { Text = "DEA Number", TextColor = Color.Navy };
			var DeaNumberEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			DeaNumberEntry.SetBinding(Entry.TextProperty, "DeaNumber");

			var AddressLabel = new Label { Text = "Address", TextColor = Color.Navy };
			var AddressEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
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
				TextColor = Color.Gray,
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
				TextColor = Color.Gray,
			};
			CityEntry.SetBinding(Entry.TextProperty, "City");

			var StateLabel = new Label { Text = "State", TextColor = Color.Navy };
			var StateEntry = st;

			var ZipLabel = new Label { Text = "Zip", TextColor = Color.Navy };
			var ZipEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			ZipEntry.SetBinding(Entry.TextProperty, "Zip");

			var PhoneLabel = new Label { Text = "Phone" , TextColor = Color.Navy};
			var PhoneEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			PhoneEntry.SetBinding(Entry.TextProperty, "Phone");

			var FaxLabel = new Label { Text = "Fax", TextColor = Color.Navy };
			var FaxEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			FaxEntry.SetBinding(Entry.TextProperty, "Fax");

			var saveButton = new Button { Text = "Save" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
			saveButton.Clicked += async (sender, e) =>
			{
//
//				int temp;
//				if (!int.TryParse(ZipEntry.Text,out temp))
//				{
//					await DisplayAlert("Error", "Invalid Zip Format", "OK", "");
//					return;
//				}
//				var prescriberItem = new Prescriber() { Address = AddressEntry.Text, City = CityEntry.Text, DeaNumber = DeaNumberEntry.Text, Email = emailEntry.Text, Fax = FaxEntry.Text, FirstName = firstNameEntry.Text, LastName = lastNameEntry.Text, LicenseNumber = LicenseNumberEntry.Text, NpiNumber = NpiNumberEntry.Text, Password = passwordEntry.Text, Phone = PhoneEntry.Text, State = StateEntry.Text, Zip = int.Parse(ZipEntry.Text) };
//				string msg;
//				var bR = PrescriberValidator.Validate(prescriberItem, out msg);
//				if(!bR)
//				{
//					await DisplayAlert("Error", msg, "OK", "");
//					return;
//				}
//
//				var prescriberRepo = new PrescriberRepo();
//				// send webservice request and so on
//				var res = await prescriberRepo.AddPrescriber(prescriberItem);
//				var resultInt = int.Parse(res.ToString());
//				if (resultInt > 0)
//				{
//					//await this.Navigation.PopAsync();
//					await this.Navigation.PushAsync(new HomePage());
//				}
//				else
//				{
//					await DisplayAlert("Error", "An Error Occured Please Try Again", "OK", "");
//				}
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
			Content = new StackLayout
			{
				Children = {
					scrollview
				}
			};
		}
	}
}