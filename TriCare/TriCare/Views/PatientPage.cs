using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Models;
using TriCare.Data;
using Xamarin.Forms;

namespace TriCare.Views
{
	public class PatientPage : ContentPage
	{
		public PatientPage(Patient p, bool isDuringPrescription = false)
		{
			var insuranceCarrierRepo = new InsuranceCarrierRepo ();
			this.BackgroundColor = Color.White;
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

			var fullNameLabel = new Label{ Text = "Name:", TextColor = Color.Navy };
			var fullNameEntry = new Label { TextColor = Color.Black };
			fullNameEntry.SetBinding (Label.TextProperty, "NameFriendly");

			var genderLabel = new Label { Text = "Gender:", TextColor = Color.Navy };
			var genderEntry = new Label { TextColor = Color.Black };
			genderEntry.SetBinding(Label.TextProperty, "Gender");

			var birthDateLabel = new Label { Text = "Birth Date:", TextColor = Color.Navy };
			var birthDateEntry = new Label { TextColor = Color.Black };
			birthDateEntry.SetBinding(Label.TextProperty, "BirthDate");

			var ssnLabel = new Label { Text = "Last 4 of SSN:",TextColor = Color.Navy };
			var ssnEntry = new Label { TextColor = Color.Black };
			ssnEntry.SetBinding(Label.TextProperty, "SSN");

			var InsuranceCarrierLabel = new Label { Text = "Insurance Carrier:", TextColor = Color.Navy };
			var InsuranceCarrierEntry = new Label { TextColor = Color.Black };
			//InsuranceCarrierEntry.SetBinding(Label.TextProperty, "LicenseNumber");
			InsuranceCarrierEntry.Text = insuranceCarrierRepo.GetInsuranceCarrier (p.InsuranceCarrierId).Name;

			var InsuranceCarrierIdNumberLabel = new Label { Text = "Insurance Carrier Id Number:", TextColor = Color.Navy};
			var InsuranceCarrierIdNumberEntry = new Label { TextColor = Color.Black };
			InsuranceCarrierIdNumberEntry.SetBinding(Label.TextProperty, "InsuranceCarrierIdNumber");

			var InsuranceGroupNumberLabel = new Label { Text = "Insurance Group Number:", TextColor = Color.Navy };
			var InsuranceGroupNumberEntry = new Label { TextColor = Color.Black };
			InsuranceGroupNumberEntry.SetBinding(Label.TextProperty, "InsuranceGroupNumber");

			var InsurancePhoneLabel = new Label { Text = "Insurance Phone:", TextColor = Color.Navy};
			var InsurancePhoneEntry = new Label { TextColor = Color.Black };
			InsurancePhoneEntry.SetBinding(Label.TextProperty, "InsurancePhone");

			var RxBinLabel = new Label { Text = "Rx Bin:", TextColor = Color.Navy };
			var RxBinEntry = new Label { TextColor = Color.Black };
			RxBinEntry.SetBinding(Label.TextProperty, "RxBin");

			var RxPcnLabel = new Label { Text = "Rx Pcn:", TextColor = Color.Navy };
			var RxPcnEntry = new Label { TextColor = Color.Black };
			RxPcnEntry.SetBinding(Label.TextProperty, "RxPcn");

			var AllergiesLabel = new Label { Text = "Allergies:", TextColor = Color.Navy };
			var AllergiesEntry = new Label { TextColor = Color.Black };
			AllergiesEntry.SetBinding(Label.TextProperty, "Allergies");

			var DiagnosisLabel = new Label { Text = "Diagnosis:", TextColor = Color.Navy };
			var DiagnosisEntry = new Label { TextColor = Color.Black };
			RxPcnEntry.SetBinding(Label.TextProperty, "Diagnosis");

			var AddressLabel = new Label { Text = "Address:", TextColor = Color.Navy };
			var AddressEntry = new Label { TextColor = Color.Black };
			AddressEntry.SetBinding(Label.TextProperty, "Address");

			var CityLabel = new Label { Text = "City:", TextColor = Color.Navy};
			var CityEntry = new Label { TextColor = Color.Black };
			CityEntry.SetBinding(Label.TextProperty, "City");

			var StateLabel = new Label { Text = "State:", TextColor = Color.Navy };
			var StateEntry = new Label { TextColor = Color.Black };
			StateEntry.SetBinding(Label.TextProperty, "State");

			var ZipLabel = new Label { Text = "Zip:", TextColor = Color.Navy};
			var ZipEntry = new Label { TextColor = Color.Black };
			ZipEntry.SetBinding(Label.TextProperty, "Zip");

			var PhoneLabel = new Label { Text = "Phone:", TextColor = Color.Navy };
			var PhoneEntry = new Label { TextColor = Color.Black };
			PhoneEntry.SetBinding(Label.TextProperty, "Phone");

			var EmailLabel = new Label { Text = "Email:", TextColor = Color.Navy };
			var EmailEntry = new Label { TextColor = Color.Black };
			EmailEntry.SetBinding(Label.TextProperty, "Email");

			var PaymentTypeLabel = new Label { Text = "Payment Type:", TextColor = Color.Navy };
			var PaymentTypeEntry = new Label { TextColor = Color.Black };
			PaymentTypeEntry.SetBinding(Label.TextProperty, "PaymentType");



			Grid grid = new Grid
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions = 
				{
					new RowDefinition { Height = new GridLength(80, GridUnitType.Absolute) },
				},
				ColumnDefinitions = 
				{
					new ColumnDefinition { Width = new GridLength(120, GridUnitType.Absolute)},
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(120, GridUnitType.Absolute)}
				}
			};
			var bv = new Label {
				Text = "Leftover space",
				TextColor = Color.Transparent,
				XAlign = TextAlignment.Center,
				YAlign = TextAlignment.Center,

				BackgroundColor = Color.Transparent
			};






			var layout = new StackLayout();
			layout.BindingContext = p;
			//if (!isDuringPrescription) {
				var editButton = new Button {
					Text = "Edit" ,
					BackgroundColor = Color.FromRgba (128, 128, 128, 128),
					TextColor = Color.White
				};
				editButton.Clicked += async (sender, e) => {
					this.Navigation.PushAsync (new EditPatientPage (p));

				};
				var continueButton = new Button {
					Text = "Continue",
					BackgroundColor = Color.FromRgba (128, 128, 128, 128),
					TextColor = Color.White
				};
				continueButton.Clicked += async (sender, e) => {
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
					App.CurrentPrescription.Patient =p;
					await this.Navigation.PushAsync(new PrescriptionSelectMedicinePage());
				};
			grid.Children.Add(bv, 0,0);
			grid.Children.Add(editButton);
			grid.Children.Add(continueButton);

			Grid.SetColumn (editButton, 0);
			Grid.SetColumn (bv, 1);
			Grid.SetColumn (continueButton, 2);
				#region Layouts
				this.BindingContext = p;
			var buttonStack = new StackLayout();
			buttonStack.VerticalOptions = LayoutOptions.FillAndExpand;
			if(isDuringPrescription)
			{
				buttonStack.Children.Add(grid);
			}
			else{
				buttonStack.Children.Add(editButton);
			}
				Label patientHeaderLabel = new Label {
					Text = "Patient Information",
					Font = Font.SystemFontOfSize (NamedSize.Large),
					FontAttributes = FontAttributes.Bold,
					TextColor = Color.Black,
					BackgroundColor = Color.White
				};

				StackLayout nameLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				nameLayout.Children.Add (fullNameLabel);
				nameLayout.Children.Add (fullNameEntry);

				StackLayout genderLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				genderLayout.Children.Add (genderLabel);
				genderLayout.Children.Add (genderEntry);

				StackLayout birthDateLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				birthDateLayout.Children.Add (birthDateLabel);
				birthDateLayout.Children.Add (birthDateEntry);

				StackLayout ssnLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				ssnLayout.Children.Add (ssnLabel);
				ssnLayout.Children.Add (ssnEntry);

				Label addressHeaderLabel = new Label {
					Text = "Address Information",
					Font = Font.SystemFontOfSize (NamedSize.Large),
					FontAttributes = FontAttributes.Bold,
					TextColor = Color.Black,
					BackgroundColor = Color.White
				};

				StackLayout addressLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				addressLayout.Children.Add (AddressLabel);
				addressLayout.Children.Add (AddressEntry);

				StackLayout cityLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				cityLayout.Children.Add (CityLabel);
				cityLayout.Children.Add (CityEntry);

				StackLayout stateLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				stateLayout.Children.Add (StateLabel);
				stateLayout.Children.Add (StateEntry);

				StackLayout zipLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				zipLayout.Children.Add (ZipLabel);
				zipLayout.Children.Add (ZipEntry);

				Label contactHeaderLabel = new Label {
					Text = "Contact Information",
					Font = Font.SystemFontOfSize (NamedSize.Large),
					FontAttributes = FontAttributes.Bold,
					TextColor = Color.Black,
					BackgroundColor = Color.White
				};

				StackLayout emailLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				emailLayout.Children.Add (EmailLabel);
				emailLayout.Children.Add (EmailEntry);

				StackLayout phoneLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				phoneLayout.Children.Add (PhoneLabel);
				phoneLayout.Children.Add (PhoneEntry);

				Label insuranceHeaderLabel = new Label {
					Text = "Insurance Information",
					Font = Font.SystemFontOfSize (NamedSize.Large),
					FontAttributes = FontAttributes.Bold,
					TextColor = Color.Black,
					BackgroundColor = Color.White
				};

				StackLayout insuranceCarrierLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				insuranceCarrierLayout.Children.Add (InsuranceCarrierLabel);
				insuranceCarrierLayout.Children.Add (InsuranceCarrierEntry);

				StackLayout insuranceCarrierIdNumberLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				insuranceCarrierIdNumberLayout.Children.Add (InsuranceCarrierIdNumberLabel);
				insuranceCarrierIdNumberLayout.Children.Add (InsuranceCarrierIdNumberEntry);

				StackLayout insuranceGroupNumberLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				insuranceGroupNumberLayout.Children.Add (InsuranceGroupNumberLabel);
				insuranceGroupNumberLayout.Children.Add (InsuranceGroupNumberEntry);

				StackLayout insurancePhoneLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				insurancePhoneLayout.Children.Add (InsurancePhoneLabel);
				insurancePhoneLayout.Children.Add (InsurancePhoneEntry);

				StackLayout paymentTypeLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				paymentTypeLayout.Children.Add (PaymentTypeLabel);
				paymentTypeLayout.Children.Add (PaymentTypeEntry);

				Label medicalHeaderLabel = new Label {
					Text = "Medical Information",
					Font = Font.SystemFontOfSize (NamedSize.Large),
					FontAttributes = FontAttributes.Bold,
					TextColor = Color.Black,
					BackgroundColor = Color.White
				};

				StackLayout rxBinLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				rxBinLayout.Children.Add (RxBinLabel);
				rxBinLayout.Children.Add (RxBinEntry);

				StackLayout rxPcnLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				rxPcnLayout.Children.Add (RxPcnLabel);
				rxPcnLayout.Children.Add (RxPcnEntry);

				StackLayout allergiesLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				allergiesLayout.Children.Add (AllergiesLabel);
				allergiesLayout.Children.Add (AllergiesEntry);

				StackLayout diagnosisLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				};

				diagnosisLayout.Children.Add (DiagnosisLabel);
				diagnosisLayout.Children.Add (DiagnosisEntry);

				var scrollview = new ScrollView {
					VerticalOptions = LayoutOptions.StartAndExpand,
					Content = new StackLayout {
						VerticalOptions = LayoutOptions.StartAndExpand,
						Padding = new Thickness (20),
						Children = {
						buttonStack,
							new Label (),
							patientHeaderLabel,
							nameLayout,
							genderLayout,
							birthDateLayout,
							ssnLayout,
							new Label (),
							contactHeaderLabel,
							emailLayout,
							phoneLayout,
							new Label (),
							addressHeaderLabel,
							addressLayout,
							cityLayout,
							stateLayout,
							zipLayout,
							new Label (),
							insuranceHeaderLabel,
							insuranceCarrierLayout,
							insuranceCarrierIdNumberLayout,
							insuranceGroupNumberLayout,
							insurancePhoneLayout,
							paymentTypeLayout,
							new Label (),
							medicalHeaderLabel,
							rxBinLayout,
							rxPcnLayout,
							allergiesLayout,
							diagnosisLayout
						}
					}
				};

				Content = scrollview;
				#endregion
			}

		}
	//}
}