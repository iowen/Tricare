using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Models;
using Xamarin.Forms;

namespace TriCare.Views
{
    public class PatientPage : ContentPage
    {
		public PatientPage(Patient p, bool isDuringPrescription = false)
        {
			this.BackgroundImage = "tricareBG.png";
			App.EnableLogout ();
			if (!isDuringPrescription)
            this.SetBinding(ContentPage.TitleProperty, "View Patient");
			else
				this.SetBinding(ContentPage.TitleProperty, "Verify Patient");

            var firstNameLabel = new Label { Text = "First Name" };
            var firstNameEntry = new Label( );
            firstNameEntry.SetBinding(Label.TextProperty, "FirstName");

            var lastNameLabel = new Label { Text = "Last Name" };
            var lastNameEntry = new Label();
            lastNameEntry.SetBinding(Label.TextProperty, "LastName");


            var genderLabel = new Label { Text = "Gender" };
            var genderEntry = new Label();
            genderEntry.SetBinding(Label.TextProperty, "Gender");

            var birthDateLabel = new Label { Text = "Birth Date" };
            var birthDateEntry = new Label();
            birthDateEntry.SetBinding(Label.TextProperty, "BirthDate");


            var ssnLabel = new Label { Text = "Last 4 of SSN" };
            var ssnEntry = new Label();
            ssnEntry.SetBinding(Label.TextProperty, "SSN");

            var InsuranceCarrierLabel = new Label { Text = "Insurance Carrier" };
            var InsuranceCarrierEntry = new Label();
            InsuranceCarrierEntry.SetBinding(Label.TextProperty, "LicenseNumber");

            var InsuranceCarrierIdNumberLabel = new Label { Text = "Insurance Carrier Id Number" };
            var InsuranceCarrierIdNumberEntry = new Label();
            InsuranceCarrierIdNumberEntry.SetBinding(Label.TextProperty, "InsuranceCarrierIdNumber");

            var InsuranceGroupNumberLabel = new Label { Text = "Insurance Group Number" };
            var InsuranceGroupNumberEntry = new Label();
            InsuranceGroupNumberEntry.SetBinding(Label.TextProperty, "InsuranceGroupNumber");

            var InsurancePhoneLabel = new Label { Text = "Insurance Phone" };
            var InsurancePhoneEntry = new Label();
            InsurancePhoneEntry.SetBinding(Label.TextProperty, "InsurancePhone");

            var RxBinLabel = new Label { Text = "Rx Bin" };
            var RxBinEntry = new Label();
            RxBinEntry.SetBinding(Label.TextProperty, "RxBin");

            var RxPcnLabel = new Label { Text = "Rx Pcn" };
            var RxPcnEntry = new Label();
            RxPcnEntry.SetBinding(Label.TextProperty, "RxPcn");

            var AllergiesLabel = new Label { Text = "Allergies" };
            var AllergiesEntry = new Label();
            AllergiesEntry.SetBinding(Label.TextProperty, "Allergies");

            var DiagnosisLabel = new Label { Text = "Diagnosis" };
            var DiagnosisEntry = new Label();
            RxPcnEntry.SetBinding(Label.TextProperty, "Diagnosis");

            var AddressLabel = new Label { Text = "Address" };
            var AddressEntry = new Label();
            AddressEntry.SetBinding(Label.TextProperty, "Address");

            var CityLabel = new Label { Text = "City" };
            var CityEntry = new Label();
            CityEntry.SetBinding(Label.TextProperty, "City");

            var StateLabel = new Label { Text = "State" };
            var StateEntry = new Label();
            StateEntry.SetBinding(Label.TextProperty, "State");

            var ZipLabel = new Label { Text = "Zip" };
            var ZipEntry = new Label();
            ZipEntry.SetBinding(Label.TextProperty, "Zip");

            var PhoneLabel = new Label { Text = "Phone" };
            var PhoneEntry = new Label();
            PhoneEntry.SetBinding(Label.TextProperty, "Phone");

            var EmailLabel = new Label { Text = "Email" };
            var EmailEntry = new Label();
            EmailEntry.SetBinding(Label.TextProperty, "Email");

            var PaymentTypeLabel = new Label { Text = "Payment Time" };
            var PaymentTypeEntry = new Label();
            PaymentTypeEntry.SetBinding(Label.TextProperty, "PaymentType");
			var layout = new StackLayout();
			layout.BindingContext = p;
			if (!isDuringPrescription) {
				var editButton = new Button { Text = "Edit" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
				editButton.Clicked += async (sender, e) => {
					 this.Navigation.PushAsync(new EditPatientPage(p));

				};
				var deleteButton = new Button { Text = "Delete", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White };
				deleteButton.Clicked += async (sender, e) => {
					//var pId = int.Parse(App.Token);
					//var patientItem = new Patient() { PrescriberId = pId, Address = AddressEntry.Text, City = CityEntry.Text, InsuranceCarrierIdNumber = InsuranceCarrierIdNumberEntry.Text, Gender = genderEntry.Text, Email = EmailEntry.Text, FirstName = firstNameEntry.Text, LastName = lastNameEntry.Text, InsuranceGroupNumber = InsuranceGroupNumberEntry.Text, SSN = int.Parse(ssnEntry.Text), Allergies = AllergiesEntry.Text, Phone = PhoneEntry.Text, State = StateEntry.Text, Zip = int.Parse(ZipEntry.Text), BirthDate = DateTime.Parse(birthDateEntry.Text), Diagnosis = DiagnosisEntry.Text, InsuranceCarrierId = 1, InsurancePhone = InsurancePhoneEntry.Text, PaymentType = PaymentTypeEntry.Text, RxBin = RxBinEntry.Text, RxPcn = RxPcnEntry.Text };
					//var patientRepo = new PatientRepo();
					//// send webservice request and so on
					//var res = await patientRepo.AddPatient(patientItem);
					//if (!string.IsNullOrWhiteSpace(res))
					//{
					//    await this.Navigation.PopAsync();
					//    await this.Navigation.PushAsync(new HomePage());
					//}
					//else
					//{
					//    await DisplayAlert("Error", "An Error Occured Please Try Again", "OK", "");
					//}
				};
				Grid grid = new Grid
				{
					VerticalOptions = LayoutOptions.FillAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					RowDefinitions = 
					{
						new RowDefinition { Height = GridLength.Auto },
					},
					ColumnDefinitions = 
					{
						new ColumnDefinition { Width = new GridLength(120, GridUnitType.Absolute)},
						new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
						new ColumnDefinition { Width = new GridLength(120, GridUnitType.Absolute)}
					}
					};
				grid.Children.Add(editButton);
				var bv = new Label {
					Text = "Leftover space",
					TextColor = Color.Transparent,
					XAlign = TextAlignment.Center,
					YAlign = TextAlignment.Center,

					BackgroundColor = Color.Transparent
				};
				grid.Children.Add(bv, 0,0);
				//grid.Children.Add(deleteButton);

				Grid.SetColumn (editButton, 0);
				Grid.SetColumn (bv, 1);
			//	Grid.SetColumn (deleteButton, 2);


				layout.Children.Add(grid);
			}
			else {
				var continueButton = new Button { Text = "Continue" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White,WidthRequest= 120};
				continueButton.Clicked += async (sender, e) => {
					var pId = int.Parse(App.Token);
					var patientItem = new Patient() {PatientId = p.PatientId, PrescriberId = pId, Address = AddressEntry.Text, City = CityEntry.Text, InsuranceCarrierIdNumber = InsuranceCarrierIdNumberEntry.Text, Gender = genderEntry.Text, Email = EmailEntry.Text, FirstName = firstNameEntry.Text, LastName = lastNameEntry.Text, InsuranceGroupNumber = InsuranceGroupNumberEntry.Text, SSN = int.Parse(ssnEntry.Text), Allergies = AllergiesEntry.Text, Phone = PhoneEntry.Text, State = StateEntry.Text, Zip = int.Parse(ZipEntry.Text), BirthDate = DateTime.Parse(birthDateEntry.Text), Diagnosis = DiagnosisEntry.Text, InsuranceCarrierId = 1, InsurancePhone = InsurancePhoneEntry.Text, PaymentType = PaymentTypeEntry.Text, RxBin = RxBinEntry.Text, RxPcn = RxPcnEntry.Text };
					//var patientRepo = new PatientRepo();
					//// send webservice request and so on
					//var res = await patientRepo.AddPatient(patientItem);
					//if (!string.IsNullOrWhiteSpace(res))
					//{
					//    await this.Navigation.PopAsync();
					App.CurrentPrescription.Patient = patientItem;
					await this.Navigation.PushAsync(new PrescriptionSelectMedicinePage());
					//}
					//else
					//{
					//    await DisplayAlert("Error", "An Error Occured Please Try Again", "OK", "");
					//}
				};

				var editButton = new Button { Text = "Edit" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White,WidthRequest= 120};
				editButton.Clicked += async (sender, e) => {
					var pId = int.Parse(App.Token);
					var patientItem = new Patient() {PatientId = p.PatientId,  PrescriberId = pId, Address = AddressEntry.Text, City = CityEntry.Text, InsuranceCarrierIdNumber = InsuranceCarrierIdNumberEntry.Text, Gender = genderEntry.Text, Email = EmailEntry.Text, FirstName = firstNameEntry.Text, LastName = lastNameEntry.Text, InsuranceGroupNumber = InsuranceGroupNumberEntry.Text, SSN = int.Parse(ssnEntry.Text), Allergies = AllergiesEntry.Text, Phone = PhoneEntry.Text, State = StateEntry.Text, Zip = int.Parse(ZipEntry.Text), BirthDate = DateTime.Parse(birthDateEntry.Text), Diagnosis = DiagnosisEntry.Text, InsuranceCarrierId = 1, InsurancePhone = InsurancePhoneEntry.Text, PaymentType = PaymentTypeEntry.Text, RxBin = RxBinEntry.Text, RxPcn = RxPcnEntry.Text };
					//var patientRepo = new PatientRepo();
					//// send webservice request and so on
					//var res = await patientRepo.AddPatient(patientItem);
					//if (!string.IsNullOrWhiteSpace(res))
					//{
					//    await this.Navigation.PopAsync();
					App.CurrentPrescription.Patient = patientItem;
					await this.Navigation.PushAsync(new PrescriptionSelectMedicinePage());
					//}
					//else
					//{
					//    await DisplayAlert("Error", "An Error Occured Please Try Again", "OK", "");
					//}
				};

				Grid grid = new Grid
				{
					VerticalOptions = LayoutOptions.FillAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					RowDefinitions = 
					{
						new RowDefinition { Height = GridLength.Auto },
					},
					ColumnDefinitions = 
					{
						new ColumnDefinition { Width = new GridLength(120, GridUnitType.Absolute)},
						new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
						new ColumnDefinition { Width = new GridLength(120, GridUnitType.Absolute)}
					}
					};
				grid.Children.Add(continueButton);
				var bv = new Label {
					Text = "Leftover space",
					TextColor = Color.Transparent,
					XAlign = TextAlignment.Center,
					YAlign = TextAlignment.Center,

					BackgroundColor = Color.Transparent
				};
				grid.Children.Add(bv, 0,0);
				grid.Children.Add(editButton);

				Grid.SetColumn (editButton, 0);
				Grid.SetColumn (bv, 1);
				Grid.SetColumn (continueButton, 2);


				layout.Children.Add(grid);
			}

            layout.Children.Add(new ScrollView()
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
                    PaymentTypeLabel, PaymentTypeEntry
					
					}
                }
            }
                );
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            Content = layout;
        }
    }
}
