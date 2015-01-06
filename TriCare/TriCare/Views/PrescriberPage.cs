using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Data;
using Xamarin.Forms;

namespace TriCare.Views
{
    public class PrescriberPage :ContentPage
    {
        public PrescriberPage()
        {
            var pRepo = new PrescriberRepo();
            var p = pRepo.GetPrescriber(int.Parse(App.Token));
			Title = "Profile";
			this.BackgroundImage = "tricareBG.png";
			App.EnableLogout ();
			var editButton = new Button { Text = "Edit" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
            editButton.Clicked += async (sender, e) =>
            {
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

            var firstNameLabel = new Label { Text = "First Name" };
            var firstNameEntry = new Label();
            firstNameEntry.SetBinding(Label.TextProperty, "FirstName");

            var lastNameLabel = new Label { Text = "Last Name" };
            var lastNameEntry = new Label();
            lastNameEntry.SetBinding(Label.TextProperty, "LastName");


            var emailLabel = new Label { Text = "Email" };
            var emailEntry = new Label();
            emailEntry.SetBinding(Label.TextProperty, "Email");

            var passwordLabel = new Label { Text = "Password" };
            var passwordEntry = new Label();

            passwordEntry.SetBinding(Label.TextProperty, "Password");


            var NpiNumberLabel = new Label { Text = "NPI Number" };
            var NpiNumberEntry = new Label();
            NpiNumberEntry.SetBinding(Label.TextProperty, "NpiNumber");

            var LicenseNumberLabel = new Label { Text = "License Number" };
            var LicenseNumberEntry = new Label();
            LicenseNumberEntry.SetBinding(Label.TextProperty, "LicenseNumber");

            var DeaNumberLabel = new Label { Text = "DEA Number" };
            var DeaNumberEntry = new Label();
            DeaNumberEntry.SetBinding(Label.TextProperty, "DeaNumber");

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

            var FaxLabel = new Label { Text = "Fax" };
            var FaxEntry = new Label();
            FaxEntry.SetBinding(Label.TextProperty, "Fax");

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
					emailLabel, emailEntry, 
						passwordLabel, passwordEntry,
					NpiNumberLabel, NpiNumberEntry, 
					LicenseNumberLabel, LicenseNumberEntry,
					DeaNumberLabel, DeaNumberEntry,
						AddressLabel, AddressEntry,
					CityLabel, CityEntry,
					StateLabel, StateEntry, 
					ZipLabel, ZipEntry, 
					PhoneLabel, PhoneEntry,
					FaxLabel, FaxEntry,
					}
                }
            };
            Content = new StackLayout
            {
                Children = {
                    editButton,
               scrollview
             		},
                    BindingContext = p,
				Padding = new Thickness(20)

            };

        }
    }
}
