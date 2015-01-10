using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Data;
using TriCare.Models;
using TriCare.Views;
using TriCare.Validators;
using Xamarin.Forms;

namespace TriCare
{
	public class EditPrescriberPage : ContentPage
	{
		public EditPrescriberPage ()
		{
			var pRepo = new PrescriberRepo();
			var p = pRepo.GetPrescriber(int.Parse(App.Token));

			this.BindingContext = p;
			this.BackgroundImage = "tricareBG.png";
			this.SetBinding(ContentPage.TitleProperty, "Edit Profile");

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


			var emailLabel = new Label { Text = "Email" };
			var emailEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			emailEntry.SetBinding(Entry.TextProperty, "Email");


			var NpiNumberLabel = new Label { Text = "NPI Number" };
			var NpiNumberEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			NpiNumberEntry.SetBinding(Entry.TextProperty, "NpiNumber");

			var LicenseNumberLabel = new Label { Text = "License Number" };
			var LicenseNumberEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			LicenseNumberEntry.SetBinding(Entry.TextProperty, "LicenseNumber");

			var DeaNumberLabel = new Label { Text = "DEA Number" };
			var DeaNumberEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			DeaNumberEntry.SetBinding(Entry.TextProperty, "DeaNumber");

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

			var FaxLabel = new Label { Text = "Fax" };
			var FaxEntry = new Entry()
			{
				BackgroundColor = Color.Transparent,
				TextColor = Color.White,
			};
			FaxEntry.SetBinding(Entry.TextProperty, "Fax");

			var registerButton = new Button { Text = "Register" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
			registerButton.Clicked += async (sender, e) =>
			{
//
//				int temp;
//				if (!int.TryParse(ZipEntry.Text,out temp))
//				{
//					await DisplayAlert("Error", "Invalid Zip Format", "OK", "");
//					return;
//				}
//				var prescriberItem = new Prescriber() { Address = AddressEntry.Text, City = CityEntry.Text, DeaNumber = DeaNumberEntry.Text, Email = emailEntry.Text, Fax = FaxEntry.Text, FirstName = firstNameEntry.Text, LastName = lastNameEntry.Text, LicenseNumber = LicenseNumberEntry.Text, NpiNumber = NpiNumberEntry.Text, Password = passwordEntry.Text, Phone = PhoneEntry.Text, State = StateEntry.Text, Zip = int.Parse(ZipEntry.Text) };
//				string msg;
//				var bR = PrescriberValidator.Validate(prescriberItem, out msg);
//				if(!bR)
//				{
//					await DisplayAlert("Error", msg, "OK", "");
//					return;
//				}
//
//				var prescriberRepo = new PrescriberRepo();
//				// send webservice request and so on
//				var res = await prescriberRepo.AddPrescriber(prescriberItem);
//				var resultInt = int.Parse(res.ToString());
//				if (resultInt > 0)
//				{
//					//await this.Navigation.PopAsync();
//					await this.Navigation.PushAsync(new HomePage());
//				}
//				else
//				{
//					await DisplayAlert("Error", "An Error Occured Please Try Again", "OK", "");
//				}
			};
			var scrollview = new ScrollView 
			{
				VerticalOptions = LayoutOptions.StartAndExpand,
				Content = new StackLayout 
				{
					VerticalOptions = LayoutOptions.StartAndExpand,
					Padding = new Thickness(20),
					Children={
						firstNameLabel, firstNameEntry, 
						lastNameLabel, lastNameEntry,
						emailLabel, emailEntry, 
						NpiNumberLabel, NpiNumberEntry, 
						LicenseNumberLabel, LicenseNumberEntry,
						DeaNumberLabel, DeaNumberEntry,
						AddressLabel, AddressEntry,
						CityLabel, CityEntry,
						StateLabel, StateEntry, 
						ZipLabel, ZipEntry, 
						PhoneLabel, PhoneEntry,
						FaxLabel, FaxEntry,
						registerButton
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