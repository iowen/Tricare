using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Data;
using TriCare.Models;
using TriCare.Utilities;
using Xamarin.Forms;

namespace TriCare.Views
{
    public class LoginPage : ContentPage
    {
        public LoginPage()
        {
			this.BackgroundImage = "tricareBG.png";
			NavigationPage.SetHasNavigationBar (this, false);
            this.SetBinding(ContentPage.TitleProperty, "Login");
			var emailLabel = new Label { Text = "Email", TextColor = Color.White  };
			var emailEntry = new Entry(){
				BackgroundColor = Color.Transparent,
				TextColor = Color.White ,

				};
            emailEntry.SetBinding(Entry.TextProperty, "Email");

			var passwordLabel = new Label { Text = "Password", TextColor = Color.White   };
			var passwordEntry = new Entry(){
				BackgroundColor = Color.Transparent,
				TextColor = Color.White ,
				IsPassword = true
			};
            passwordEntry.SetBinding(Entry.TextProperty, "Password");

			var loginButton = new Button { Text = "Log In" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White };
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
                var rR = new RefillRepo();
                if (rR.InsertRecords())
                {
                    var r = await rR.GetRefillAmounts();
                    var rq = await rR.GetRefillQuantities();
                }
				var loginItem = new LoginModel() {Email = emailEntry.Text, Password = passwordEntry.Text};
                var prescriberRepo = new PrescriberRepo();
				var loginState = await prescriberRepo.LoginPrescriber(loginItem);
				if (loginState.ToLower() == "success")
                {
					App.ClearCurrentPrescription();
                    await this.Navigation.PushAsync(new HomePage());
                }
                else{
                    await DisplayAlert("Error", "Invalid credentials provided", "OK","close");
                }
            };

			var registerButton = new Button { Text = "Register", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White  };
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
