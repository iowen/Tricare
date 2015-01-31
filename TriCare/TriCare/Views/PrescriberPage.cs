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
			Icon = "prescriberIcon.png";
			this.BackgroundColor = Color.White;
		//	App.EnableLogout ();
			var editButton = new Button { Text = "Edit" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
            editButton.Clicked += async (sender, e) =>
            {
				App.IsHome = false;
				await App.np.PushAsync(new EditPrescriberPage());
            };
			var buttonStack = new StackLayout();
			buttonStack.HorizontalOptions = LayoutOptions.FillAndExpand;

				buttonStack.Children.Add(editButton);

			var firstNameLabel = new Label { Text = "First Name", TextColor = Color.Navy  };
            var firstNameEntry = new Label();
            firstNameEntry.SetBinding(Label.TextProperty, "FirstName");

			var lastNameLabel = new Label { Text = "Last Name" , TextColor = Color.Navy };
            var lastNameEntry = new Label();
            lastNameEntry.SetBinding(Label.TextProperty, "LastName");

			var fullNameLabel = new Label{ Text = "Name:", TextColor = Color.Navy };
			var fullNameEntry = new Label { TextColor = Color.Black};
			fullNameEntry.SetBinding (Label.TextProperty, "NameFriendly");

			var emailLabel = new Label { Text = "Email:",  TextColor = Color.Navy };
			var emailEntry = new Label{ TextColor = Color.Black };
            emailEntry.SetBinding(Label.TextProperty, "Email");

//			var passwordLabel = new Label { Text = "Password:",  TextColor = Color.Navy  };
//			var passwordEntry = new Label{ TextColor = Color.Black };
//            passwordEntry.SetBinding(Label.TextProperty, "Password");

			var NpiNumberLabel = new Label { Text = "NPI Number:", TextColor = Color.Navy };
			var NpiNumberEntry = new Label{ TextColor = Color.Black };
            NpiNumberEntry.SetBinding(Label.TextProperty, "NpiNumber");

			var LicenseNumberLabel = new Label { Text = "License Number:", TextColor = Color.Navy };
			var LicenseNumberEntry = new Label{ TextColor = Color.Black };
            LicenseNumberEntry.SetBinding(Label.TextProperty, "LicenseNumber");

			var DeaNumberLabel = new Label { Text = "DEA Number:", TextColor = Color.Navy  };
			var DeaNumberEntry = new Label{ TextColor = Color.Black };
            DeaNumberEntry.SetBinding(Label.TextProperty, "DeaNumber");

			var AddressLabel = new Label { Text = "Address:", TextColor = Color.Navy  };
			var AddressEntry = new Label{ TextColor = Color.Black };
            AddressEntry.SetBinding(Label.TextProperty, "Address");

			var CityLabel = new Label { Text = "City:", TextColor = Color.Navy  };
			var CityEntry = new Label{ TextColor = Color.Black };
            CityEntry.SetBinding(Label.TextProperty, "City");
			CityEntry.BindingContextChanged+= (object sender, EventArgs e) => {CityEntry.Text = CityEntry.Text.Trim();};

			var StateLabel = new Label { Text = "State:", TextColor = Color.Navy  };
			var StateEntry = new Label{ TextColor = Color.Black };
            StateEntry.SetBinding(Label.TextProperty, "State");
			StateEntry.BindingContextChanged+= (object sender, EventArgs e) => {StateEntry.Text = StateEntry.Text.Trim();};

			var ZipLabel = new Label { Text = "Zip:", TextColor = Color.Navy };
			var ZipEntry = new Label{ TextColor = Color.Black };
            ZipEntry.SetBinding(Label.TextProperty, "Zip");

			var PhoneLabel = new Label { Text = "Phone:", TextColor = Color.Navy  };
			var PhoneEntry = new Label{ TextColor = Color.Black };
            PhoneEntry.SetBinding(Label.TextProperty, "Phone");
			PhoneEntry.BindingContextChanged += (sender, e) => {
				base.OnBindingContextChanged();
				if(PhoneEntry.Text.Length == 10)
				{
					var pn = PhoneEntry.Text.Insert (3, "-").Insert (7, "-");
					PhoneEntry.Text = pn;
				}
			};
			var FaxLabel = new Label { Text = "Fax:", TextColor = Color.Navy };
			var FaxEntry = new Label{ TextColor = Color.Black };
            FaxEntry.SetBinding(Label.TextProperty, "Fax");
			FaxEntry.BindingContextChanged += (sender, e) => {
				base.OnBindingContextChanged();
				if(FaxEntry.Text.Length == 10)
				{
					var fn = FaxEntry.Text.Insert (3, "-").Insert (7, "-");
					FaxEntry.Text = fn;
				}
			};
			#region LAYOUTS
			this.BindingContext = p;

			Label prescriberHeaderLabel = new Label{
				Text = "Prescriber Information",
				Font = Font.SystemFontOfSize (NamedSize.Large),
				FontAttributes = FontAttributes.Bold,
				TextColor = Color.Black,
				BackgroundColor = Color.White,
				HorizontalOptions = LayoutOptions.CenterAndExpand
	
				};

			StackLayout nameLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,

			};

			nameLayout.Children.Add (fullNameLabel);
			nameLayout.Children.Add (fullNameEntry);

			StackLayout passwordLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,

			};
//
//			passwordLayout.Children.Add (passwordLabel);
//			passwordLayout.Children.Add (passwordEntry);

			StackLayout npiLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,

			};

			npiLayout.Children.Add (NpiNumberLabel);
			npiLayout.Children.Add (NpiNumberEntry);

			StackLayout licenseLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,

			};

			licenseLayout.Children.Add (LicenseNumberLabel);
			licenseLayout.Children.Add (LicenseNumberEntry);

			StackLayout deaLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,

			};

			deaLayout.Children.Add (DeaNumberLabel);
			deaLayout.Children.Add (DeaNumberEntry);

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
				HorizontalOptions = LayoutOptions.CenterAndExpand,

			};

			emailLayout.Children.Add (emailLabel);
			emailLayout.Children.Add (emailEntry);

			StackLayout phoneLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,

			};

			phoneLayout.Children.Add (PhoneLabel);
			phoneLayout.Children.Add (PhoneEntry);

			StackLayout faxLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,

			};

			faxLayout.Children.Add (FaxLabel);
			faxLayout.Children.Add (FaxEntry);

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
				HorizontalOptions = LayoutOptions.CenterAndExpand,

			};

			addressLayout.Children.Add (AddressLabel);
			addressLayout.Children.Add (AddressEntry);

			StackLayout cityLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,

			};

			cityLayout.Children.Add (CityLabel);
			cityLayout.Children.Add (CityEntry);

			StackLayout stateLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,

			};

			stateLayout.Children.Add (StateLabel);
			stateLayout.Children.Add (StateEntry);

			StackLayout zipLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,

			};

			zipLayout.Children.Add (ZipLabel);
			zipLayout.Children.Add (ZipEntry);


			var scrollview = new ScrollView
			{
				VerticalOptions = LayoutOptions.StartAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.StartAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Padding = new Thickness(20),
					Children ={
						buttonStack,
						new Label(),
						prescriberHeaderLabel,
						nameLayout,
//						passwordLayout,
						npiLayout,
						licenseLayout,
						deaLayout,
						new Label(),
						contactHeaderLabel,
						emailLayout,
						phoneLayout,
						faxLayout,
						new Label(),
						addressHeaderLabel,
						addressLayout,
						cityLayout,
						stateLayout,
						zipLayout,
					}
				}
			};

			Content = scrollview; //scrollview;
			#endregion

        }

		protected override void OnAppearing ()
		{
			App.IsHome = true;
			base.OnAppearing ();
		}
    }
}
