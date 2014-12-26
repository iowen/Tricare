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
    public class LoginPage : ContentPage
    {
        public LoginPage()
        {
            this.SetBinding(ContentPage.TitleProperty, "Login");

            var emailLabel = new Label { Text = "Email" };
            var emailEntry = new Entry();
            emailEntry.SetBinding(Entry.TextProperty, "Email");

            var passwordLabel = new Label { Text = "Password" };
			var passwordEntry = new Entry(){
				IsPassword = true
			};
            passwordEntry.SetBinding(Entry.TextProperty, "Password");

            var loginButton = new Button { Text = "Log In" };
            loginButton.Clicked += async (sender, e) =>
            {
				var iR = new InsuranceCarrierRepo();
				if (iR.InsertRecords())
				{
					var r = await iR.GetInsuranceCarriers();
				}
				var iG = new IngredientRepo();
				if (iG.InsertRecords())
				{
					var r = await iG.GetIngredients();
				}
				var med = new MedicineRepo();
				if (med.InsertRecords())
				{
					var r = await med.GetMedicines();
				}
				var medR = new MedicineIngredientRepo();
				if (medR.InsertRecords())
				{
					var r = await medR.GetMedicineIngredients();
				}
				var loginItem = new LoginModel() {Email = emailEntry.Text, Password = passwordEntry.Text};
                var prescriberRepo = new PrescriberRepo();
				var loginState = await prescriberRepo.LoginPrescriber(loginItem);
				if (loginState.ToLower() == "success")
                {
                    await this.Navigation.PushAsync(new HomePage());
                }
                else{
                    await DisplayAlert("Error", "Invalid credentials provided", "OK","");
                }
            };

            var registerButton = new Button { Text = "Register" };
            registerButton.Clicked += (sender, e) =>
            {
                this.Navigation.PushAsync(new RegisterPage());
                
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(20),
                Children = {
					emailLabel, emailEntry, 
					passwordLabel, passwordEntry,
					loginButton, registerButton
                }
            };


        }
    }
}
