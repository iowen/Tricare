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
	public class EditPatientPage : ContentPage
	{
		public EditPatientPage(Patient p, bool isDuringPrescription = false)
		{
			this.BindingContext = p;

			this.SetBinding(ContentPage.TitleProperty, "Edit Patient");
			this.BackgroundImage = "tricareBG.png";
			App.EnableLogout ();
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


			var genderLabel = new Label { Text = "Gender", TextColor = Color.Navy };
			var genderEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			genderEntry.SetBinding(Entry.TextProperty, "Gender");

			var birthDateLabel = new Label { Text = "Birth Date" , TextColor = Color.Navy};
			var birthDateEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			birthDateEntry.SetBinding(Entry.TextProperty, "BirthDate");


			var ssnLabel = new Label { Text = "Last 4 of SSN", TextColor = Color.Navy };
			var ssnEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			ssnEntry.SetBinding(Entry.TextProperty, "SSN");

			var InsuranceCarrierLabel = new Label { Text = "Insurance Carrier", TextColor = Color.Navy };
			var InsuranceCarrierEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			InsuranceCarrierEntry.SetBinding(Entry.TextProperty, "LicenseNumber");

			var InsuranceCarrierIdNumberLabel = new Label { Text = "Insurance Carrier Id Number" , TextColor = Color.Navy};
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

			var AllergiesLabel = new Label { Text = "Allergies", TextColor = Color.Navy };
			var AllergiesEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			AllergiesEntry.SetBinding(Entry.TextProperty, "Allergies");

			var DiagnosisLabel = new Label { Text = "Diagnosis" , TextColor = Color.Navy};
			var DiagnosisEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			RxPcnEntry.SetBinding(Entry.TextProperty, "Diagnosis");

			var AddressLabel = new Label { Text = "Address", TextColor = Color.Navy };
			var AddressEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			AddressEntry.SetBinding(Entry.TextProperty, "Address");

			var CityLabel = new Label { Text = "City" , TextColor = Color.Navy};
			var CityEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			CityEntry.SetBinding(Entry.TextProperty, "City");

			var StateLabel = new Label { Text = "State" , TextColor = Color.Navy};
			var StateEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			StateEntry.SetBinding(Entry.TextProperty, "State");

			var ZipLabel = new Label { Text = "Zip" , TextColor = Color.Navy};
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

			var EmailLabel = new Label { Text = "Email", TextColor = Color.Navy };
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
		protected override void OnAppearing ()
		{
			base.OnAppearing ();
		}
	}
}

