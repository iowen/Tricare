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
		private static Patient _patient;
		public PatientPage(Patient p, bool isDuringPrescription = false)
		{

			var patientRepo  = new PatientRepo();
			_patient = patientRepo.GetPatient (p.PatientId);
			var insuranceCarrierRepo = new InsuranceCarrierRepo ();
			this.BackgroundColor = Color.White;
			if (!isDuringPrescription)
				this.Title = "View Patient";
			else
				this.Title = "Verify Patient";

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
			birthDateEntry.SetBinding(Label.TextProperty, "BirthDateFriendly");

			var ssnLabel = new Label { Text = "Id Number:",TextColor = Color.Navy };
			var ssnEntry = new Label { TextColor = Color.Black };
			ssnEntry.SetBinding(Label.TextProperty, "SSN");

			var InsuranceCarrierLabel = new Label { Text = "Insurance Carrier:", TextColor = Color.Navy };
			var InsuranceCarrierEntry = new Label { TextColor = Color.Black };
			//InsuranceCarrierEntry.SetBinding(Label.TextProperty, "LicenseNumber");
			InsuranceCarrierEntry.Text = insuranceCarrierRepo.GetInsuranceCarrier (_patient.InsuranceCarrierId).Name.Trim();

			var InsuranceCarrierIdNumberLabel = new Label { Text = "Insurance Carrier Id Number:", TextColor = Color.Navy};
			var InsuranceCarrierIdNumberEntry = new Label { TextColor = Color.Black };
			InsuranceCarrierIdNumberEntry.SetBinding(Label.TextProperty, "InsuranceCarrierIdNumber");
			InsuranceCarrierIdNumberEntry.BindingContextChanged += (sender, e) => {
				base.OnBindingContextChanged();

				var pn = InsuranceCarrierIdNumberEntry.Text.Trim();
				InsuranceCarrierIdNumberEntry.Text = pn;

			};
			var InsuranceGroupNumberLabel = new Label { Text = "Insurance Group Number:", TextColor = Color.Navy };
			var InsuranceGroupNumberEntry = new Label { TextColor = Color.Black };
			InsuranceGroupNumberEntry.SetBinding(Label.TextProperty, "InsuranceGroupNumber");

			var InsurancePhoneLabel = new Label { Text = "Insurance Phone:", TextColor = Color.Navy};
			var InsurancePhoneEntry = new Label { TextColor = Color.Black };
			InsurancePhoneEntry.SetBinding(Label.TextProperty, "InsurancePhone");
			InsurancePhoneEntry.BindingContextChanged += (sender, e) => {
				base.OnBindingContextChanged();
				if(InsurancePhoneEntry.Text.Trim().Length == 10)
				{
					var pn = InsurancePhoneEntry.Text.Insert (3, "-").Insert (7, "-");
					InsurancePhoneEntry.Text = pn;
				}
			};
			var RxBinLabel = new Label { Text = "Rx Bin:", TextColor = Color.Navy };
			var RxBinEntry = new Label { TextColor = Color.Black };
			RxBinEntry.SetBinding(Label.TextProperty, "RxBin");

			var RxPcnLabel = new Label { Text = "Rx Pcn:", TextColor = Color.Navy };
			var RxPcnEntry = new Label { TextColor = Color.Black };
			RxPcnEntry.SetBinding(Label.TextProperty, "RxPcn");

			var AllergiesLabel = new Label { Text = "Allergies:", TextColor = Color.Navy };
			var AllergiesEntry = new Label { TextColor = Color.Black };
			AllergiesEntry.SetBinding(Label.TextProperty, "Allergies");
			AllergiesEntry.BindingContextChanged+= (object sender, EventArgs e) => {AllergiesEntry.Text = AllergiesEntry.Text.Trim();};
			var DiagnosisLabel = new Label { Text = "Diagnosis:", TextColor = Color.Navy };
			var DiagnosisEntry = new Label { TextColor = Color.Black };
			DiagnosisEntry.SetBinding(Label.TextProperty, "Diagnosis");
			DiagnosisEntry.BindingContextChanged+= (object sender, EventArgs e) => {DiagnosisEntry.Text = DiagnosisEntry.Text.Trim();};

			var AddressLabel = new Label { Text = "Address:", TextColor = Color.Navy };
			var AddressEntry = new Label { TextColor = Color.Black };
			AddressEntry.SetBinding(Label.TextProperty, "Address");

			var CityLabel = new Label { Text = "City:", TextColor = Color.Navy};
			var CityEntry = new Label { TextColor = Color.Black };
			CityEntry.SetBinding(Label.TextProperty, "City");
			CityEntry.BindingContextChanged+= (object sender, EventArgs e) => {CityEntry.Text = CityEntry.Text.Trim();};

			var StateLabel = new Label { Text = "State:", TextColor = Color.Navy };
			var StateEntry = new Label { TextColor = Color.Black };
			StateEntry.SetBinding(Label.TextProperty, "State");
			StateEntry.BindingContextChanged+= (object sender, EventArgs e) => {StateEntry.Text = StateEntry.Text.Trim();};

			var ZipLabel = new Label { Text = "Zip:", TextColor = Color.Navy};
			var ZipEntry = new Label { TextColor = Color.Black };
			ZipEntry.SetBinding(Label.TextProperty, "Zip");

			var PhoneLabel = new Label { Text = "Phone:", TextColor = Color.Navy };
			var PhoneEntry = new Label { TextColor = Color.Black };
			PhoneEntry.SetBinding(Label.TextProperty, "Phone");
			PhoneEntry.BindingContextChanged += (sender, e) => {
				base.OnBindingContextChanged();
				if(PhoneEntry.Text.Trim().Length == 10)
				{
					var pn = PhoneEntry.Text.Insert (3, "-").Insert (7, "-");
					PhoneEntry.Text = pn;
				}
			};
			var EmailLabel = new Label { Text = "Email:", TextColor = Color.Navy };
			var EmailEntry = new Label { TextColor = Color.Black };
			EmailEntry.SetBinding(Label.TextProperty, "Email");

			var PaymentTypeLabel = new Label { Text = "Payment Type:", TextColor = Color.Navy };
			var PaymentTypeEntry = new Label { TextColor = Color.Black };
			PaymentTypeEntry.SetBinding(Label.TextProperty, "PaymentType");
			PaymentTypeEntry.BindingContextChanged+= (object sender, EventArgs e) => {PaymentTypeEntry.Text = PaymentTypeEntry.Text.Trim();};



			Grid grid = new Grid
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions = 
				{
					new RowDefinition { Height = new GridLength(40, GridUnitType.Absolute) },
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
			layout.BindingContext = _patient;
			//if (!isDuringPrescription) {
				var editButton = new Button {
					Text = "Edit" ,
					BackgroundColor = Color.FromRgba (128, 128, 128, 128),
					TextColor = Color.White
				};
				editButton.Clicked += async (sender, e) => {
				await App.np.PushAsync (new EditPatientPage (p,isDuringPrescription));

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
				App.CurrentPrescription.Patient =_patient;
				await App.np.PushAsync(new PrescriptionSelectMedicinePage());
				};
			grid.Children.Add(bv, 0,0);
			grid.Children.Add(editButton);
			grid.Children.Add(continueButton);

			Grid.SetColumn (editButton, 0);
			Grid.SetColumn (bv, 1);
			Grid.SetColumn (continueButton, 2);
				#region Layouts
			this.BindingContext = _patient;
			var buttonStack = new StackLayout();
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
					BackgroundColor = Color.White,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				StackLayout nameLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				nameLayout.Children.Add (fullNameLabel);
				nameLayout.Children.Add (fullNameEntry);

				StackLayout genderLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				genderLayout.Children.Add (genderLabel);
				genderLayout.Children.Add (genderEntry);

				StackLayout birthDateLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				birthDateLayout.Children.Add (birthDateLabel);
				birthDateLayout.Children.Add (birthDateEntry);

				StackLayout ssnLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				ssnLayout.Children.Add (ssnLabel);
				ssnLayout.Children.Add (ssnEntry);

				Label addressHeaderLabel = new Label {
					Text = "Address Information",
					Font = Font.SystemFontOfSize (NamedSize.Large),
					FontAttributes = FontAttributes.Bold,
					TextColor = Color.Black,
					BackgroundColor = Color.White,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				StackLayout addressLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				addressLayout.Children.Add (AddressLabel);
				addressLayout.Children.Add (AddressEntry);

				StackLayout cityLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				cityLayout.Children.Add (CityLabel);
				cityLayout.Children.Add (CityEntry);

				StackLayout stateLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				stateLayout.Children.Add (StateLabel);
				stateLayout.Children.Add (StateEntry);

				StackLayout zipLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				zipLayout.Children.Add (ZipLabel);
				zipLayout.Children.Add (ZipEntry);

				Label contactHeaderLabel = new Label {
					Text = "Contact Information",
					Font = Font.SystemFontOfSize (NamedSize.Large),
					FontAttributes = FontAttributes.Bold,
					TextColor = Color.Black,
					BackgroundColor = Color.White,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				StackLayout emailLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				emailLayout.Children.Add (EmailLabel);
				emailLayout.Children.Add (EmailEntry);

				StackLayout phoneLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				phoneLayout.Children.Add (PhoneLabel);
				phoneLayout.Children.Add (PhoneEntry);

				Label insuranceHeaderLabel = new Label {
					Text = "Insurance Information",
					Font = Font.SystemFontOfSize (NamedSize.Large),
					FontAttributes = FontAttributes.Bold,
					TextColor = Color.Black,
					BackgroundColor = Color.White,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				StackLayout insuranceCarrierLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				insuranceCarrierLayout.Children.Add (InsuranceCarrierLabel);
				insuranceCarrierLayout.Children.Add (InsuranceCarrierEntry);

				StackLayout insuranceCarrierIdNumberLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				insuranceCarrierIdNumberLayout.Children.Add (InsuranceCarrierIdNumberLabel);
				insuranceCarrierIdNumberLayout.Children.Add (InsuranceCarrierIdNumberEntry);

				StackLayout insuranceGroupNumberLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				insuranceGroupNumberLayout.Children.Add (InsuranceGroupNumberLabel);
				insuranceGroupNumberLayout.Children.Add (InsuranceGroupNumberEntry);

				StackLayout insurancePhoneLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				insurancePhoneLayout.Children.Add (InsurancePhoneLabel);
				insurancePhoneLayout.Children.Add (InsurancePhoneEntry);

				StackLayout paymentTypeLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				paymentTypeLayout.Children.Add (PaymentTypeLabel);
				paymentTypeLayout.Children.Add (PaymentTypeEntry);

				Label medicalHeaderLabel = new Label {
					Text = "Medical Information",
					Font = Font.SystemFontOfSize (NamedSize.Large),
					FontAttributes = FontAttributes.Bold,
					TextColor = Color.Black,
					BackgroundColor = Color.White,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				StackLayout rxBinLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				rxBinLayout.Children.Add (RxBinLabel);
				rxBinLayout.Children.Add (RxBinEntry);

				StackLayout rxPcnLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				rxPcnLayout.Children.Add (RxPcnLabel);
				rxPcnLayout.Children.Add (RxPcnEntry);

				StackLayout allergiesLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				allergiesLayout.Children.Add (AllergiesLabel);
				allergiesLayout.Children.Add (AllergiesEntry);

				StackLayout diagnosisLayout = new StackLayout {
					Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				};

				diagnosisLayout.Children.Add (DiagnosisLabel);
				diagnosisLayout.Children.Add (DiagnosisEntry);

				var scrollview = new ScrollView {
					VerticalOptions = LayoutOptions.StartAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
					Content = new StackLayout {
						VerticalOptions = LayoutOptions.StartAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
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
	
		protected override void OnAppearing ()
		{
			var patientRepo  = new PatientRepo();
			_patient = patientRepo.GetPatient (_patient.PatientId);
			OnBindingContextChanged ();
			base.OnAppearing ();

		}

		}
	//}
}