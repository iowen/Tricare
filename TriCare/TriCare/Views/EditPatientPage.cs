﻿using System;
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
			var firstNameLabel = new Label { Text = "First Name" };
			var firstNameEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			firstNameEntry.SetBinding(Entry.TextProperty, "FirstName");

			var lastNameLabel = new Label { Text = "Last Name" };
			var lastNameEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			lastNameEntry.SetBinding(Entry.TextProperty, "LastName");


			var genderLabel = new Label { Text = "Gender" };
			var genderEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			genderEntry.SetBinding(Entry.TextProperty, "Gender");

			var birthDateLabel = new Label { Text = "Birth Date" };
			var birthDateEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			birthDateEntry.SetBinding(Entry.TextProperty, "BirthDate");


			var ssnLabel = new Label { Text = "Last 4 of SSN" };
			var ssnEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			ssnEntry.SetBinding(Entry.TextProperty, "SSN");

			var InsuranceCarrierLabel = new Label { Text = "Insurance Carrier" };
			var InsuranceCarrierEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			InsuranceCarrierEntry.SetBinding(Entry.TextProperty, "LicenseNumber");

			var InsuranceCarrierIdNumberLabel = new Label { Text = "Insurance Carrier Id Number" };
			var InsuranceCarrierIdNumberEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			InsuranceCarrierIdNumberEntry.SetBinding(Entry.TextProperty, "InsuranceCarrierIdNumber");

			var InsuranceGroupNumberLabel = new Label { Text = "Insurance Group Number" };
			var InsuranceGroupNumberEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			InsuranceGroupNumberEntry.SetBinding(Entry.TextProperty, "InsuranceGroupNumber");

			var InsurancePhoneLabel = new Label { Text = "Insurance Phone" };
			var InsurancePhoneEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			InsurancePhoneEntry.SetBinding(Entry.TextProperty, "InsurancePhone");

			var RxBinLabel = new Label { Text = "Rx Bin" };
			var RxBinEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			RxBinEntry.SetBinding(Entry.TextProperty, "RxBin");

			var RxPcnLabel = new Label { Text = "Rx Pcn" };
			var RxPcnEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			RxPcnEntry.SetBinding(Entry.TextProperty, "RxPcn");

			var AllergiesLabel = new Label { Text = "Allergies" };
			var AllergiesEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			AllergiesEntry.SetBinding(Entry.TextProperty, "Allergies");

			var DiagnosisLabel = new Label { Text = "Diagnosis" };
			var DiagnosisEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			RxPcnEntry.SetBinding(Entry.TextProperty, "Diagnosis");

			var AddressLabel = new Label { Text = "Address" };
			var AddressEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			AddressEntry.SetBinding(Entry.TextProperty, "Address");

			var CityLabel = new Label { Text = "City" };
			var CityEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			CityEntry.SetBinding(Entry.TextProperty, "City");

			var StateLabel = new Label { Text = "State" };
			var StateEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			StateEntry.SetBinding(Entry.TextProperty, "State");

			var ZipLabel = new Label { Text = "Zip" };
			var ZipEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			ZipEntry.SetBinding(Entry.TextProperty, "Zip");

			var PhoneLabel = new Label { Text = "Phone" };
			var PhoneEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			PhoneEntry.SetBinding(Entry.TextProperty, "Phone");

			var EmailLabel = new Label { Text = "Email" };
			var EmailEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			EmailEntry.SetBinding(Entry.TextProperty, "Email");

			var PaymentTypeLabel = new Label { Text = "Payment Time" };
			var PaymentTypeEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
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
