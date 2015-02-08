using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Data;
using TriCare.Models;
using Xamarin.Forms;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace TriCare.Views
{
	public class EditPatientPage : ContentPage
	{
		private List<InsuranceCarrier> inList;
		private List<State> stList;
		private List<object> insuranceList =
			new List<object>();
		public List<object> InsuranceList
		{
			get { return insuranceList; }
		}

		private List<object> stateList =
			new List<object>();
		private ActivityIndicator indi;

		public List<object> StateList
		{
			get { return stateList; }
		}
		private AutoCompleteView st;
		private string searchText;
		public string SearchText{ get{ return searchText;} set{searchText = value; OnPropertyChanged (); }}

		public ICommand SearchCommand { get; set; }
		public ICommand CellSelectedCommand { get; set; }
		private AutoCompleteView a;
		private int _insuranceCarrierId;

		private List<Label> formLabelList; 

		public EditPatientPage(Patient p, bool isDuringPrescription = false)
		{
			inList = new List<InsuranceCarrier> ();
			stList = new List<State> ();
			var overlay = new AbsoluteLayout();
			var content = new StackLayout();
			indi = new ActivityIndicator();
			this.BindingContext = p;
			_insuranceCarrierId = p.InsuranceCarrierId;
			formLabelList = new List<Label> ();

			var iRepo = new InsuranceCarrierRepo ();
			inList = iRepo.GetAllInsuranceCarriers ();

			foreach (var i in inList) {
				insuranceList.Add(i);
			}
			this.Title = "Edit Patient";
			this.BackgroundColor = Color.White;
			var firstNameLabel = new Label { Text = "First Name" , TextColor = Color.Navy};
			var firstNameEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			firstNameEntry.SetBinding(Entry.TextProperty, "FirstName");
			formLabelList.Add (firstNameLabel);

			var lastNameLabel = new Label { Text = "Last Name" , TextColor = Color.Navy};
			var lastNameEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			lastNameEntry.SetBinding(Entry.TextProperty, "LastName");
			formLabelList.Add (lastNameLabel);

			var genderLabel = new Label { Text = "Gender", TextColor = Color.Navy };
			var genderEntry = new Picker {
				Title="Select a Gender",
				BackgroundColor = Color.Transparent
			};
			genderEntry.Items.Add ("Male");
			genderEntry.Items.Add ("Female");
			genderEntry.SelectedIndex = p.Gender.Trim() == "Male" ? 0 : 1;
			//genderEntry.SetBinding (Picker.SelectedIndexProperty, "Gender");
			formLabelList.Add (genderLabel);

			var birthDateLabel = new Label { Text = "Birth Date" , TextColor = Color.Navy};
			DatePicker birthDateEntry = new DatePicker ();
			birthDateEntry.BackgroundColor = Color.Transparent;
			birthDateEntry.Date = p.BirthDate;
			formLabelList.Add (birthDateLabel);

			var ssnLabel = new Label { Text = "Id Number", TextColor = Color.Navy };
			var ssnEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			ssnEntry.SetBinding(Entry.TextProperty, "SSN");
			formLabelList.Add (ssnLabel);

			var CellSelectedCommandS = new Command<State>((key) =>
				{
					// Add the key to the input string.
					this.Opacity = 50;
				});
			CellSelectedCommand = new Command<InsuranceCarrier>((key) =>
				{
					// Add the key to the input string.
					this.Opacity = 50;
					_insuranceCarrierId = key.InsuranceCarrierId;
				});
			var a1 = new AutoCompleteView ();
			SearchCommand = new Command((key) =>
				{
					DisplayAlert("Search",a.Suggestions.Count.ToString(),"close");
					//DisplayAlert("Search",a.Sugestions.Count.ToString(),"close");
					//DisplayAlert("Search",IngredientList.Count.ToString(),"close");
					// Add the key to the input string.
				});
			a = new AutoCompleteView () {
				SearchBackgroundColor = Color.Transparent,
				ShowSearchButton = false,
				Suggestions = InsuranceList,
				SearchCommand = SearchCommand,
				SelectedCommand = CellSelectedCommand,
				SuggestionBackgroundColor = Color.Gray,
				TextColor = Color.Black,
				SearchTextColor = Color.White,
				SuggestionItemDataTemplate =  new DataTemplate (typeof (acCell)),

			};

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
				SelectedCommand = CellSelectedCommandS,
				SuggestionItemDataTemplate =  new DataTemplate (typeof (acCell)),
				SuggestionBackgroundColor = Color.Gray,
				TextColor = Color.Black,
				SearchTextColor = Color.White,
			};




			var InsuranceCarrierLabel = new Label { Text = "Insurance Carrier", TextColor = Color.Navy };
			var eleLoc = inList.IndexOf (inList.First (x => x.InsuranceCarrierId == p.InsuranceCarrierId));
			var seleLoc = stList.IndexOf (stList.First (x => x.Name.Trim() == p.State.Trim()));
			//Got This Working
			a.SetText (InsuranceList.ElementAt (eleLoc).ToString ());
			formLabelList.Add (InsuranceCarrierLabel);
			a.ShowHideListbox (false);

			st.SetText (StateList.ElementAt (seleLoc).ToString ());
			st.ShowHideListbox (false);
		//	firstNameEntry.Focus ();
			var InsuranceCarrierEntry = a; /*= new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Gray,
			};
			InsuranceCarrierEntry = a;*/
			//InsuranceCarrierEntry.SetBinding(Entry.TextProperty, "LicenseNumber");
			//InsuranceCarrierEntry.Text = iRepo.GetInsuranceCarrier (p.InsuranceCarrierId).Name;
			//var eleLoc = inList.IndexOf (inList.First (x => x.InsuranceCarrierId == p.InsuranceCarrierId));
			//InsuranceCarrierEntry.Text = insuranceList.ElementAt (eleLoc).ToString();

			var InsuranceCarrierIdNumberLabel = new Label { Text = "Insurance Carrier Id Number" , TextColor = Color.Navy};
			var InsuranceCarrierIdNumberEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			InsuranceCarrierIdNumberEntry.SetBinding(Entry.TextProperty, "InsuranceCarrierIdNumber");
			formLabelList.Add (InsuranceCarrierIdNumberLabel);

			var InsuranceGroupNumberLabel = new Label { Text = "Insurance Group Number" , TextColor = Color.Navy};
			var InsuranceGroupNumberEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			InsuranceGroupNumberEntry.SetBinding(Entry.TextProperty, "InsuranceGroupNumber");
			formLabelList.Add (InsuranceGroupNumberLabel);

			var InsurancePhoneLabel = new Label { Text = "Insurance Phone" , TextColor = Color.Navy};
			var InsurancePhoneEntry = new PhoneNumberEntry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			InsurancePhoneEntry.SetBinding(Entry.TextProperty, "InsurancePhone");
			InsurancePhoneEntry.BindingContextChanged += (sender, e) => {
				base.OnBindingContextChanged();
				if(InsurancePhoneEntry.Text.Trim().Length == 10)
				{
					var pn = InsurancePhoneEntry.Text.Insert (3, "-").Insert (7, "-");
					InsurancePhoneEntry.Text = pn;
				}
			};
			InsurancePhoneEntry.TextChanged +=  (sender, e) => {
				InsurancePhoneEntry.Text = App.GetInputAsPhoneNumber(e.OldTextValue, e.NewTextValue);
			};
			formLabelList.Add (InsurancePhoneLabel);

			var RxBinLabel = new Label { Text = "Rx Bin" , TextColor = Color.Navy};
			var RxBinEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			RxBinEntry.SetBinding(Entry.TextProperty, "RxBin");
			formLabelList.Add (RxBinLabel);

			var RxPcnLabel = new Label { Text = "Rx Pcn" , TextColor = Color.Navy};
			var RxPcnEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			RxPcnEntry.SetBinding(Entry.TextProperty, "RxPcn");
			formLabelList.Add (RxPcnLabel);

			var AllergiesLabel = new Label { Text = "Allergies", TextColor = Color.Navy };
			var AllergiesEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			AllergiesEntry.SetBinding(Entry.TextProperty, "Allergies");
			formLabelList.Add (AllergiesLabel);

			var DiagnosisLabel = new Label { Text = "Diagnosis" , TextColor = Color.Navy};
			var DiagnosisEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			DiagnosisEntry.SetBinding(Entry.TextProperty, "Diagnosis");
			formLabelList.Add (DiagnosisLabel);

			var AddressLabel = new Label { Text = "Address", TextColor = Color.Navy };
			var AddressEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			AddressEntry.SetBinding(Entry.TextProperty, "Address");
			formLabelList.Add (AddressLabel);

			var CityLabel = new Label { Text = "City" , TextColor = Color.Navy};
			var CityEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			CityEntry.SetBinding(Entry.TextProperty, "City");
			formLabelList.Add (CityLabel);

			var StateLabel = new Label { Text = "State" , TextColor = Color.Navy};
			var StateEntry = st;
			formLabelList.Add (StateLabel);

			var ZipLabel = new Label { Text = "Zip" , TextColor = Color.Navy};
			var ZipEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			ZipEntry.SetBinding(Entry.TextProperty, "Zip");
			formLabelList.Add (ZipLabel);

			var PhoneLabel = new Label { Text = "Phone" , TextColor = Color.Navy};
			var PhoneEntry = new PhoneNumberEntry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			formLabelList.Add (PhoneLabel);

			PhoneEntry.SetBinding(Entry.TextProperty, "Phone");
			PhoneEntry.BindingContextChanged += (sender, e) => {
				base.OnBindingContextChanged();
				if(PhoneEntry.Text.Trim().Length == 10)
				{
					var pn = PhoneEntry.Text.Insert (3, "-").Insert (7, "-");
					PhoneEntry.Text = pn;
				}
			};
			PhoneEntry.TextChanged +=  (sender, e) => {
				PhoneEntry.Text = App.GetInputAsPhoneNumber(e.OldTextValue, e.NewTextValue);
			};
			var EmailLabel = new Label { Text = "Email", TextColor = Color.Navy };
			var EmailEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			EmailEntry.SetBinding(Entry.TextProperty, "Email");
			formLabelList.Add (EmailLabel);

			var PaymentTypeLabel = new Label { Text = "Payment Type" , TextColor = Color.Navy};
			var PaymentTypeEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.Black,
			};
			PaymentTypeEntry.SetBinding(Entry.TextProperty, "PaymentType");
			formLabelList.Add (PaymentTypeLabel);
//			InsuranceCarrierEntry.TextChanged += (sender, e) => {
//				var ins = iRepo.GetAllInsuranceCarriers().Where(i => i.Name.Trim() == InsuranceCarrierEntry.Text.Trim()).FirstOrDefault();
//				if(ins != null)
//				{
//					InsuranceCarrierIdNumberEntry.Text = ins.InsuranceCarrierId.ToString();
//				}
//			};

			var saveButton = new Button { Text = "Save", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White };
			saveButton.Clicked += async (sender, e) =>
			{
				if(!App.IsConnected())
				{
					await DisplayAlert ("Error", "Patients cannot be modified without an internet connection.", "OK", "close");
					return;
				}
				indi.IsRunning = true;
				saveButton.IsEnabled = false;
				#region VALIDATE BEFORE SAVE
				var validInsCarrier = insuranceList.Where(i => i.ToString().Trim() == InsuranceCarrierEntry.Text.Trim()).FirstOrDefault();
				if(firstNameEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter a valid first name","OK");
					firstNameLabel.Text = "* First Name *";
					firstNameLabel.TextColor = Color.Red;
					firstNameLabel.Focus();
					firstNameEntry.Focus();
					updateLabels(formLabelList,firstNameLabel);
					return;
				}
				else if(lastNameEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter a valid last name","OK");
					lastNameLabel.Text = "* Last Name *";
					lastNameLabel.TextColor = Color.Red;
					lastNameLabel.Focus();
					lastNameEntry.Focus();
					updateLabels(formLabelList,lastNameLabel);
					return;
				}
				else if(genderEntry.SelectedIndex < 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please select a gender","OK");
					genderLabel.Text = "* Gender *";
					genderLabel.TextColor = Color.Red;
					genderLabel.Focus();
					genderEntry.Focus();
					updateLabels(formLabelList,genderLabel);
					return;
				}
				else if(ssnEntry.Text.Trim().Length != 4 || Regex.Matches(ssnEntry.Text,@"[a-zA-Z]").Count > 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please make sure SSN contains 4 digits","OK");
					ssnLabel.Text = "* Id Number *";
					ssnLabel.TextColor = Color.Red;
					ssnLabel.Focus();
					ssnEntry.Focus();
					updateLabels(formLabelList,ssnLabel);
					return;
				}
				else if(InsuranceCarrierEntry.Text.Trim().Length <= 0 || (validInsCarrier == null))
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please provide a valid Insurance Carrier","OK");
					InsuranceCarrierLabel.Text = "* Insurance Carrier *";
					InsuranceCarrierLabel.TextColor = Color.Red;
					InsuranceCarrierLabel.Focus();
					InsuranceCarrierEntry.Focus();
					updateLabels(formLabelList,InsuranceCarrierLabel);
					return;
				}
				else if(InsuranceCarrierIdNumberEntry.Text.Trim().Length <= 0 || Regex.Matches(InsuranceCarrierIdNumberEntry.Text,@"[a-zA-Z]").Count > 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter digits for the Insurance Carrier Id Number","OK");
					InsuranceCarrierIdNumberLabel.Text = "* Insurance Carrier Id Number *";
					InsuranceCarrierIdNumberLabel.TextColor = Color.Red;
					InsuranceCarrierIdNumberLabel.Focus();
					InsuranceCarrierIdNumberEntry.Focus();
					updateLabels(formLabelList,InsuranceCarrierIdNumberLabel);
					return;
				}
				else if(InsuranceGroupNumberEntry.Text.Trim().Length <= 0 || Regex.Matches(InsuranceGroupNumberEntry.Text,@"[a-zA-Z]").Count > 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter digits for the Insurance Group Number","OK");
					InsuranceGroupNumberLabel.Text = "* Insurance Group Number *";
					InsuranceGroupNumberLabel.TextColor = Color.Red;
					InsuranceGroupNumberLabel.Focus();
					InsuranceGroupNumberEntry.Focus();
					updateLabels(formLabelList,InsuranceGroupNumberLabel);
					return;
				}
				else if(InsurancePhoneEntry.Text.Trim().Replace("-","").Length != 10 || Regex.Matches(InsurancePhoneEntry.Text.Replace("-",""),@"[a-zA-Z]").Count > 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter 10 digits for the Insurance Phone Number","OK");
					InsurancePhoneLabel.Text = "* Insurance Phone *";
					InsurancePhoneLabel.TextColor = Color.Red;
					InsurancePhoneLabel.Focus();
					InsurancePhoneEntry.Focus();
					updateLabels(formLabelList,InsurancePhoneLabel);
					return;
				}
				else if(RxBinEntry.Text.Trim().Length <= 0 || Regex.Matches(RxBinEntry.Text,@"[a-zA-Z]").Count > 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter information for the RX Bin","OK");
					RxBinLabel.Text = "* Rx Bin *";
					RxBinLabel.TextColor = Color.Red;
					RxBinLabel.Focus();
					RxBinEntry.Focus();
					updateLabels(formLabelList,RxBinLabel);
					return;
				}
				else if(RxPcnEntry.Text.Trim().Length <= 0 || Regex.Matches(RxPcnEntry.Text,@"[a-zA-Z]").Count > 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter information for the RX Pcn","OK");
					RxPcnLabel.Text = "* Rx Pcn *";
					RxPcnLabel.TextColor = Color.Red;
					RxPcnLabel.Focus();
					RxPcnEntry.Focus();
					updateLabels(formLabelList,RxPcnLabel);
					return;
				}
				else if(AllergiesEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter information on Allergies","OK");
					AllergiesLabel.Text = "* Allergies *";
					AllergiesLabel.TextColor = Color.Red;
					AllergiesLabel.Focus();
					AllergiesEntry.Focus();
					updateLabels(formLabelList,AllergiesLabel);
					return;
				}
				else if(DiagnosisEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter information on Diagnosis","OK");
					DiagnosisLabel.Text = "* Diagnosis *";
					DiagnosisLabel.TextColor = Color.Red;
					DiagnosisLabel.Focus();
					DiagnosisEntry.Focus();
					updateLabels(formLabelList,DiagnosisLabel);
					return;
				}
				else if(AddressEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter an Address","OK");	
					AddressLabel.Text = "* Address *";
					AddressLabel.TextColor = Color.Red;
					AddressLabel.Focus();
					AddressEntry.Focus();
					updateLabels(formLabelList,AddressLabel);
					return;
				}
				else if(CityEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter a City","OK");
					CityLabel.Text = "* City *";
					CityLabel.TextColor = Color.Red;
					CityLabel.Focus();
					CityEntry.Focus();
					updateLabels(formLabelList,CityLabel);
					return;
				}
				else if(StateEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter an State","OK");
					StateLabel.Text = "* State *";
					StateLabel.TextColor = Color.Red;
					StateLabel.Focus();
					StateEntry.Focus();
					updateLabels(formLabelList,StateLabel);
					return;
				}
				else if(ZipEntry.Text.Trim().Length != 5 || Regex.Matches(ZipEntry.Text,@"[a-zA-Z]").Count > 0)
				{	
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter a Zip code with 5 digits","OK");
					ZipLabel.Text = "* Zip *";
					ZipLabel.TextColor = Color.Red;
					ZipLabel.Focus();
					ZipEntry.Focus();
					updateLabels(formLabelList,ZipLabel);
					return;
				}
				else if(PhoneEntry.Text.Trim().Replace("-","").Length != 10 || Regex.Matches(PhoneEntry.Text.Replace("-",""),@"[a-zA-Z]").Count > 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter 10 digits for the Phone Number","OK");
					PhoneLabel.Text = "* Phone *";
					PhoneLabel.TextColor = Color.Red;
					PhoneLabel.Focus();
					PhoneEntry.Focus();
					updateLabels(formLabelList,PhoneLabel);
					return;
				}
				else if(EmailEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter an Email","OK");
					EmailLabel.Text = "* Email *";
					EmailLabel.TextColor = Color.Red;
					EmailLabel.Focus();
					EmailEntry.Focus();
					updateLabels(formLabelList,EmailLabel);
					return;
				}
				else if (!App.ValidEmail(EmailEntry.Text))
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Error", "Please enter an valid email.", "OK");
					EmailLabel.Text = "* Email *";
					EmailLabel.TextColor = Color.Red;
					EmailLabel.Focus();
					EmailEntry.Focus();
					updateLabels(formLabelList,EmailLabel);
					return;
				}
				else if(PaymentTypeEntry.Text.Trim().Length <= 0)
				{
					indi.IsRunning = false;
					saveButton.IsEnabled = true;
					await DisplayAlert("Alert!","Please enter a Payment Type","OK");
					PaymentTypeLabel.Text = "* Payment Type *";
					PaymentTypeLabel.TextColor = Color.Red;
					PaymentTypeLabel.Focus();
					PaymentTypeEntry.Focus();
					updateLabels(formLabelList,PaymentTypeLabel);
					return;
				}
				#endregion

				var pId = int.Parse(App.Token);
				var patientItem = new Patient() { 
					PrescriberId=pId,
					PatientId = p.PatientId,
					Address = AddressEntry.Text, 
					City = CityEntry.Text, 
					InsuranceCarrierId = _insuranceCarrierId,
					InsuranceCarrierIdNumber = InsuranceCarrierIdNumberEntry.Text, 
					Gender = genderEntry.Items[genderEntry.SelectedIndex].ToString(),//genderEntry.Text, 
					Email = EmailEntry.Text, 
					FirstName = firstNameEntry.Text, 
					LastName = lastNameEntry.Text, 
					InsuranceGroupNumber = InsuranceGroupNumberEntry.Text, 
					SSN = int.Parse(ssnEntry.Text), 
					Allergies = AllergiesEntry.Text, 
					Phone = PhoneEntry.Text.Replace("-",""), 
					State = StateEntry.Text, 
					Zip = int.Parse(ZipEntry.Text), 
					BirthDate = birthDateEntry.Date, 
					Diagnosis = DiagnosisEntry.Text, 
					InsurancePhone = InsurancePhoneEntry.Text.Replace("-",""),
					PaymentType = PaymentTypeEntry.Text, 
					RxBin = RxBinEntry.Text, 
					RxPcn = RxPcnEntry.Text 
				};
				var patientRepo = new PatientRepo();
				// send webservice request and so on
				var res = await patientRepo.UpdatePatient(patientItem);
				indi.IsRunning = false;

				if (res == true)
				{
					if (!isDuringPrescription)
					{
						var Command = new Command(async o => {
							await App.np.PopAsync(false);
							await App.np.PopAsync(false);
							await App.np.PushAsync(new PatientPage(patientItem, isDuringPrescription));
						});
						Command.Execute(new []{"run"});
					}
					else
					{
						var Command = new Command(async o => {
							App.CurrentPrescription.Patient = patientItem;
							await App.np.PopAsync(false);
							await App.np.PushAsync(new PrescriptionSelectMedicineCategoryPage());
						});
						Command.Execute(new []{"run"});
					}
				}
				else
				{
					saveButton.IsEnabled = true;
					await DisplayAlert("Error", "An Error Occured Please Try Again", "OK", "");
				}
			};
		

			content = new StackLayout
				{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
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
			};

			AbsoluteLayout.SetLayoutFlags(indi, AbsoluteLayoutFlags.PositionProportional);
			AbsoluteLayout.SetLayoutBounds(indi, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			overlay.Children.Add(indi);
			overlay.Children.Add(content,new Rectangle (0, 0, 1, 1), AbsoluteLayoutFlags.All);
			Content = new ScrollView () {
				Content = overlay
			};
		}

		private void updateLabels(List<Label> formLabels, Label currentLabel)
		{
			/*
			 * if currentlabel is null, it means to reset all labels to original text 
			 * and remove red color signifying error has previously occurred.
			*/
			foreach (var label in formLabels) {
				if (currentLabel == null) {
					label.TextColor = Color.Black;
					label.Text = label.Text.Replace ("*", "").Trim ();
				}
				else if (label != currentLabel) {
					label.TextColor = Color.Black;
					label.Text = label.Text.Replace ("*", "").Trim ();
				}
			}
		}

	}
}

