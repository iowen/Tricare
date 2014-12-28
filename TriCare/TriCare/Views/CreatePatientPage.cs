using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Data;
using TriCare.Models;
using Xamarin.Forms;

namespace TriCare.Views
{
    public class CreatePatientPage : ContentPage
    {
        public CreatePatientPage(bool isDuringPrescription = false)
        {
            this.SetBinding(ContentPage.TitleProperty, "Add Patient");
			this.BackgroundImage = "tricareBG.png";

            var firstNameLabel = new Label { Text = "First Name" };
            var firstNameEntry = new Entry();
            firstNameEntry.SetBinding(Entry.TextProperty, "FirstName");

            var lastNameLabel = new Label { Text = "Last Name" };
            var lastNameEntry = new Entry();
            firstNameEntry.SetBinding(Entry.TextProperty, "LastName");


            var genderLabel = new Label { Text = "Gender" };
            var genderEntry = new Entry();
            genderEntry.SetBinding(Entry.TextProperty, "Gender");

            var birthDateLabel = new Label { Text = "Birth Date" };
            var birthDateEntry = new Entry();
            birthDateEntry.SetBinding(Entry.TextProperty, "BirthDate");


            var ssnLabel = new Label { Text = "Last 4 of SSN" };
            var ssnEntry = new Entry();
            ssnEntry.SetBinding(Entry.TextProperty, "SSN");

            var InsuranceCarrierLabel = new Label { Text = "Insurance Carrier" };
            var InsuranceCarrierEntry = new Entry();
            InsuranceCarrierEntry.SetBinding(Entry.TextProperty, "LicenseNumber");

            var InsuranceCarrierIdNumberLabel = new Label { Text = "Insurance Carrier Id Number" };
            var InsuranceCarrierIdNumberEntry = new Entry();
            InsuranceCarrierIdNumberEntry.SetBinding(Entry.TextProperty, "InsuranceCarrierIdNumber");

            var InsuranceGroupNumberLabel = new Label { Text = "Insurance Group Number" };
            var InsuranceGroupNumberEntry = new Entry();
            InsuranceGroupNumberEntry.SetBinding(Entry.TextProperty, "InsuranceGroupNumber");

            var InsurancePhoneLabel = new Label { Text = "Insurance Phone" };
            var InsurancePhoneEntry = new Entry();
            InsurancePhoneEntry.SetBinding(Entry.TextProperty, "InsurancePhone");

            var RxBinLabel = new Label { Text = "Rx Bin" };
            var RxBinEntry = new Entry();
            RxBinEntry.SetBinding(Entry.TextProperty, "RxBin");

            var RxPcnLabel = new Label { Text = "Rx Pcn" };
            var RxPcnEntry = new Entry();
            RxPcnEntry.SetBinding(Entry.TextProperty, "RxPcn");

            var AllergiesLabel = new Label { Text = "Allergies" };
            var AllergiesEntry = new Entry();
            AllergiesEntry.SetBinding(Entry.TextProperty, "Allergies");

            var DiagnosisLabel = new Label { Text = "Diagnosis" };
            var DiagnosisEntry = new Entry();
            RxPcnEntry.SetBinding(Entry.TextProperty, "Diagnosis");

            var AddressLabel = new Label { Text = "Address" };
            var AddressEntry = new Entry();
            AddressEntry.SetBinding(Entry.TextProperty, "Address");

            var CityLabel = new Label { Text = "City" };
            var CityEntry = new Entry();
            CityEntry.SetBinding(Entry.TextProperty, "City");

            var StateLabel = new Label { Text = "State" };
            var StateEntry = new Entry();
            StateEntry.SetBinding(Entry.TextProperty, "State");

            var ZipLabel = new Label { Text = "Zip" };
            var ZipEntry = new Entry();
            ZipEntry.SetBinding(Entry.TextProperty, "Zip");

            var PhoneLabel = new Label { Text = "Phone" };
            var PhoneEntry = new Entry();
            PhoneEntry.SetBinding(Entry.TextProperty, "Phone");

            var EmailLabel = new Label { Text = "Email" };
            var EmailEntry = new Entry();
            EmailEntry.SetBinding(Entry.TextProperty, "Email");

            var PaymentTypeLabel = new Label { Text = "Payment Time" };
            var PaymentTypeEntry = new Entry();
            PaymentTypeEntry.SetBinding(Entry.TextProperty, "PaymentType");

            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += async (sender, e) =>
            {
				var pId = int.Parse(App.Token);
				var patientItem = new Patient() { PrescriberId=pId, Address = AddressEntry.Text, City = CityEntry.Text, InsuranceCarrierIdNumber = InsuranceCarrierIdNumberEntry.Text, Gender = genderEntry.Text, Email = EmailEntry.Text, FirstName = firstNameEntry.Text, LastName = lastNameEntry.Text, InsuranceGroupNumber = InsuranceGroupNumberEntry.Text, SSN = int.Parse(ssnEntry.Text), Allergies = AllergiesEntry.Text, Phone = PhoneEntry.Text, State = StateEntry.Text, Zip = int.Parse(ZipEntry.Text), BirthDate = DateTime.Parse(birthDateEntry.Text), Diagnosis = DiagnosisEntry.Text, InsuranceCarrierId = 1, InsurancePhone = InsurancePhoneEntry.Text, PaymentType = PaymentTypeEntry.Text, RxBin = RxBinEntry.Text, RxPcn = RxPcnEntry.Text };
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
                        await this.Navigation.PushAsync(new PrescriptionSelectMedicinePage());

                    }
                }
                else
                {
                    await DisplayAlert("Error", "An Error Occured Please Try Again", "OK", "");
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

