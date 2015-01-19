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
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace TriCare.Views
{
    public class CreatePatientPage : ContentPage
    {
		private List<object> insuranceList =
			new List<object>();

		private List<object> stateList =
			new List<object>();

		public ICommand SearchCommand { get; set; }
		public ICommand CellSelectedCommand { get; set; }
		public List<object> InsuranceList
		{
			get { return insuranceList; }
		}

		public List<object> StateList
		{
			get { return stateList; }
		}
		private AutoCompleteView a;
		private AutoCompleteView st;
		private string searchText;
		public string SearchText{ get{ return searchText;} set{searchText = value; OnPropertyChanged (); }}
        public CreatePatientPage(bool isDuringPrescription = false)
        {
            this.SetBinding(ContentPage.TitleProperty, "Add Patient");
			this.BackgroundColor = Color.White;
			App.EnableLogout ();
			var firstNameLabel = new Label { Text = "First Name" , TextColor = Color.Navy };
            var firstNameEntry = new Entry()
            {
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
            };
            firstNameEntry.SetBinding(Entry.TextProperty, "FirstName");

			var lastNameLabel = new Label { Text = "Last Name" , TextColor = Color.Navy };
            var lastNameEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
            };
            firstNameEntry.SetBinding(Entry.TextProperty, "LastName");

			var genderLabel = new Label { Text = "Gender" , TextColor = Color.Navy };
			var genderEntry = new Picker {
				Title="Select a Gender",
				BackgroundColor = Color.Transparent
			};
			genderEntry.Items.Add ("Male");
			genderEntry.Items.Add ("Female");
			genderEntry.SetBinding(Entry.TextProperty, "Gender");

			var birthDateLabel = new Label { Text = "Birth Date" , TextColor = Color.Navy };
			DatePicker birthDateEntry = new DatePicker ();
			birthDateEntry.BackgroundColor = Color.Transparent;

			birthDateEntry.SetBinding(Entry.TextProperty, "BirthDate");

			var ssnLabel = new Label { Text = "Last 4 of SSN" , TextColor = Color.Navy };
            var ssnEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
            };
            ssnEntry.SetBinding(Entry.TextProperty, "SSN");
			var a1 = new AutoCompleteView ();
			SearchCommand = new Command((key) =>
				{
					DisplayAlert("Search",a.Suggestions.Count.ToString(),"close");
					//DisplayAlert("Search",a.Sugestions.Count.ToString(),"close");
					//DisplayAlert("Search",IngredientList.Count.ToString(),"close");
					// Add the key to the input string.
				});

			CellSelectedCommand = new Command<InsuranceCarrier>((key) =>
				{
					// Add the key to the input string.
					this.Opacity = 50;
				});
			var CellSelectedCommandS = new Command<State>((key) =>
				{
					// Add the key to the input string.
					this.Opacity = 50;
				});
			var iRepo = new InsuranceCarrierRepo ();
			var t = iRepo.GetAllInsuranceCarriers ();

			foreach (var i in t) {
				insuranceList.Add(i);
			}

			var s = new StateRepo ();
			var ss = s.GetAllStates ();

			foreach (var sss in ss) {
				stateList.Add(sss);
			}
			a = new AutoCompleteView () {
				SearchBackgroundColor = Color.Transparent,
				ShowSearchButton = false,
				Suggestions = InsuranceList,
				SearchCommand = SearchCommand,
				SelectedCommand = CellSelectedCommand,
				SuggestionBackgroundColor = Color.Gray,
				Placeholder = "",
			};
	
			st = new AutoCompleteView () {
				SearchBackgroundColor = Color.Transparent,
				ShowSearchButton = false,
				Suggestions = StateList,
				SearchCommand = SearchCommand,
				SelectedCommand = CellSelectedCommandS,
				SuggestionBackgroundColor = Color.Gray,
				Placeholder = "",
			};

			var InsuranceCarrierLabel = new Label { Text = "Insurance Carrier" , TextColor = Color.Navy};
			var InsuranceCarrierEntry = a;

			var InsuranceCarrierIdNumberLabel = new Label { Text = "Insurance Carrier Id Number", TextColor = Color.Navy };
			var InsuranceCarrierIdNumberEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
            };
            InsuranceCarrierIdNumberEntry.SetBinding(Entry.TextProperty, "InsuranceCarrierIdNumber");

			var InsuranceGroupNumberLabel = new Label { Text = "Insurance Group Number" , TextColor = Color.Navy};
            var InsuranceGroupNumberEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
            };
            InsuranceGroupNumberEntry.SetBinding(Entry.TextProperty, "InsuranceGroupNumber");

			var InsurancePhoneLabel = new Label { Text = "Insurance Phone" , TextColor = Color.Navy};
            var InsurancePhoneEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
            };
            InsurancePhoneEntry.SetBinding(Entry.TextProperty, "InsurancePhone");

			var RxBinLabel = new Label { Text = "Rx Bin" , TextColor = Color.Navy};
            var RxBinEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
            };
            RxBinEntry.SetBinding(Entry.TextProperty, "RxBin");

			var RxPcnLabel = new Label { Text = "Rx Pcn" , TextColor = Color.Navy};
            var RxPcnEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
            };
            RxPcnEntry.SetBinding(Entry.TextProperty, "RxPcn");

			var AllergiesLabel = new Label { Text = "Allergies" , TextColor = Color.Navy};
            var AllergiesEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
            };
            AllergiesEntry.SetBinding(Entry.TextProperty, "Allergies");

			var DiagnosisLabel = new Label { Text = "Diagnosis", TextColor = Color.Navy };
            var DiagnosisEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
            };
            RxPcnEntry.SetBinding(Entry.TextProperty, "Diagnosis");

			var AddressLabel = new Label { Text = "Address" , TextColor = Color.Navy};
            var AddressEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
            };
            AddressEntry.SetBinding(Entry.TextProperty, "Address");

			var CityLabel = new Label { Text = "City", TextColor = Color.Navy };
            var CityEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
            };
            CityEntry.SetBinding(Entry.TextProperty, "City");

			var StateLabel = new Label { Text = "State" , TextColor = Color.Navy};
			var StateEntry = st;

			var ZipLabel = new Label { Text = "Zip", TextColor = Color.Navy };
            var ZipEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
            };
            ZipEntry.SetBinding(Entry.TextProperty, "Zip");

			var PhoneLabel = new Label { Text = "Phone", TextColor = Color.Navy };
            var PhoneEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
            };
            PhoneEntry.SetBinding(Entry.TextProperty, "Phone");

			var EmailLabel = new Label { Text = "Email" , TextColor = Color.Navy};
            var EmailEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
            };
            EmailEntry.SetBinding(Entry.TextProperty, "Email");

			var PaymentTypeLabel = new Label { Text = "Payment Type" , TextColor = Color.Navy};
            var PaymentTypeEntry = new Entry()
            {
                BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
            };
            PaymentTypeEntry.SetBinding(Entry.TextProperty, "PaymentType");

			var saveButton = new Button { Text = "Save", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White };
            saveButton.Clicked += async (sender, e) =>
			{
				#region VALIDATE BEFORE SAVE
				if(firstNameEntry.Text.Trim().Length <= 0)
				{
					await DisplayAlert("Alert!","Please enter a valid first name","OK");
					return;
				}
				else if(lastNameEntry.Text.Trim().Length <= 0)
				{
					await DisplayAlert("Alert!","Please enter a valid last name","OK");
					return;
				}
				else if(genderEntry.SelectedIndex < 0)
				{
					await DisplayAlert("Alert!","Please select a gender","OK");
					return;
				}
				else if(ssnEntry.Text.Trim().Length != 4 || Regex.Matches(ssnEntry.Text,@"[a-zA-Z]").Count > 0)
				{
					await DisplayAlert("Alert!","Please make sure SSN contains 4 digits","OK");
					return;
				}
				else if(InsuranceCarrierEntry.Text.Trim().Length <= 0)
				{
					await DisplayAlert("Alert!","Please provide the Insurance Carrier","OK");
					return;
				}
				else if(InsuranceCarrierIdNumberEntry.Text.Trim().Length <= 0 || Regex.Matches(InsuranceCarrierIdNumberEntry.Text,@"[a-zA-Z]").Count > 0)
				{
					await DisplayAlert("Alert!","Please enter digits for the Insurance Carrier Id Number","OK");
					return;
				}
				else if(InsuranceGroupNumberEntry.Text.Trim().Length <= 0 || Regex.Matches(InsuranceGroupNumberEntry.Text,@"[a-zA-Z]").Count > 0)
				{
					await DisplayAlert("Alert!","Please enter digits for the Insurance Group Number","OK");
					return;
				}
				else if(InsurancePhoneEntry.Text.Trim().Length != 10 || Regex.Matches(InsurancePhoneEntry.Text,@"[a-zA-Z]").Count > 0)
				{
					await DisplayAlert("Alert!","Please enter 10 digits for the Insurance Phone Number","OK");
					return;
				}
				else if(RxBinEntry.Text.Trim().Length <= 0 || Regex.Matches(RxBinEntry.Text,@"[a-zA-Z]").Count > 0)
				{
					await DisplayAlert("Alert!","Please enter digits for the RX Bin","OK");
					return;
				}
				else if(RxPcnEntry.Text.Trim().Length <= 0 || Regex.Matches(RxPcnEntry.Text,@"[a-zA-Z]").Count > 0)
				{
					await DisplayAlert("Alert!","Please enter only digits for the RX Pcn","OK");
					return;
				}
				else if(AllergiesEntry.Text.Trim().Length <= 0)
				{
					await DisplayAlert("Alert!","Please enter information on Allergies","OK");
					return;
				}
				else if(DiagnosisEntry.Text.Trim().Length <= 0)
				{
					await DisplayAlert("Alert!","Please enter information on Diagnosis","OK");
					return;
				}
				else if(AddressEntry.Text.Trim().Length <= 0)
				{
					await DisplayAlert("Alert!","Please enter an Address","OK");				
					return;
				}
				else if(CityEntry.Text.Trim().Length <= 0)
				{
					await DisplayAlert("Alert!","Please enter a City","OK");
					return;
				}
				else if(StateEntry.Text.Trim().Length <= 0)
				{
					await DisplayAlert("Alert!","Please enter an State","OK");
					return;
				}
				else if(ZipEntry.Text.Trim().Length != 5 || Regex.Matches(ZipEntry.Text,@"[a-zA-Z]").Count > 0)
				{				
					await DisplayAlert("Alert!","Please enter a Zip code with 5 digits","OK");
					return;
				}
				else if(PhoneEntry.Text.Trim().Length != 10 || Regex.Matches(PhoneEntry.Text,@"[a-zA-Z]").Count > 0)
				{
					await DisplayAlert("Alert!","Please enter 10 digits for the Phone Number","OK");
					return;
				}
				else if(EmailEntry.Text.Trim().Length <= 0)
				{
					await DisplayAlert("Alert!","Please enter an Email","OK");
					return;
				}
				else if(PaymentTypeEntry.Text.Trim().Length <= 0)
				{
					await DisplayAlert("Alert!","Please enter a Payment Type","OK");
					return;
				}
				#endregion

				var pId = int.Parse(App.Token);
				var patientItem = new Patient() { 
					PrescriberId=pId, 
					Address = AddressEntry.Text, 
					City = CityEntry.Text, 
					InsuranceCarrierIdNumber = InsuranceCarrierIdNumberEntry.Text, 
					Gender = genderEntry.Items[genderEntry.SelectedIndex].ToString(), 
					Email = EmailEntry.Text, 
					FirstName = firstNameEntry.Text, 
					LastName = lastNameEntry.Text, 
					InsuranceGroupNumber = InsuranceGroupNumberEntry.Text, 
					SSN = int.Parse(ssnEntry.Text), 
					Allergies = AllergiesEntry.Text, 
					Phone = PhoneEntry.Text, 
					State = StateEntry.Text, 
					Zip = int.Parse(ZipEntry.Text), 
					BirthDate = birthDateEntry.Date,//DateTime.Parse(birthDateEntry.Date.ToString("d")), 
					Diagnosis = DiagnosisEntry.Text, 
					InsuranceCarrierId = 1, 
					InsurancePhone = InsurancePhoneEntry.Text, 
					PaymentType = PaymentTypeEntry.Text,
					RxBin = RxBinEntry.Text, 
					RxPcn = RxPcnEntry.Text 
				};
                var patientRepo = new PatientRepo();
                // send webservice request and so on
                var res = await patientRepo.AddPatient(patientItem);
                if (!string.IsNullOrWhiteSpace(res))
                {
                    if (!isDuringPrescription)
                    {
                        await this.Navigation.PopAsync();
                        await this.Navigation.PushAsync(new HomePage());
                    }
                    else
                    {
						patientItem.PatientId = JsonConvert.DeserializeObject<int>(res);
						App.CurrentPrescription.Patient = patientItem;
                        await this.Navigation.PushAsync(new PrescriptionSelectMedicinePage());

                    }
                }
                else
                {
                    await DisplayAlert("Error", "An Error Occured Please Try Again", "OK", "Close");
                }
            };
            var scrollview = new ScrollView
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Padding = new Thickness(20),
                    Children ={
					firstNameLabel, firstNameEntry, 
					lastNameLabel, lastNameEntry,
					genderLabel, genderEntry, 
					birthDateLabel, birthDateEntry,
					ssnLabel, ssnEntry, 
					InsuranceCarrierLabel, InsuranceCarrierEntry,
					InsuranceCarrierIdNumberLabel, InsuranceCarrierIdNumberEntry, 
                    InsuranceGroupNumberLabel, InsuranceGroupNumberEntry,
                    InsurancePhoneLabel, InsurancePhoneEntry,
                    RxBinLabel, RxBinEntry,
                    RxPcnLabel, RxPcnEntry,
                    AllergiesLabel, AllergiesEntry,
                    DiagnosisLabel, DiagnosisEntry,
						AddressLabel, AddressEntry,
						CityLabel, CityEntry,
					StateLabel, StateEntry, 
					ZipLabel, ZipEntry, 
					PhoneLabel, PhoneEntry,
					EmailLabel, EmailEntry,
                    PaymentTypeLabel, PaymentTypeEntry,
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

