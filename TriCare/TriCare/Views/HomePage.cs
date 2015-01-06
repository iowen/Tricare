using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TriCare.Views
{
    public class HomePage : TabbedPage
    {
        public HomePage()
        {
			NavigationPage.SetHasBackButton (this, false);
			this.BackgroundImage = "tricareBG.png";
			App.EnableLogout ();
			var AddPatientButton = new Button { Text = "Add Patient" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
            AddPatientButton.Clicked += (sender, e) =>
            {
                this.Navigation.PushAsync(new CreatePatientPage());

            };

			var ManagePatientButton = new Button { Text = "Manage Patients", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White };
            ManagePatientButton.Clicked += (sender, e) =>
            {
                this.Navigation.PushAsync(new PatientListPage());

            };

			var AddPrescriptionButton = new Button { Text = "Add Prescription" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
            AddPrescriptionButton.Clicked += (sender, e) =>
            {
                this.Navigation.PushAsync(new PrescriptionNewORSelectPatientPage());

            };

			var ManagePrescriptionButton = new Button { Text = "Prescription History", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White };
            ManagePrescriptionButton.Clicked += (sender, e) =>
            {
                this.Navigation.PushAsync(new RegisterPage());

            };
			var EditProfileButton = new Button { Text = "Edit Profile", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White };
            EditProfileButton.Clicked += (sender, e) =>
            {
                this.Navigation.PushAsync(new PrescriberPage());

            };
            this.SetBinding(ContentPage.TitleProperty, "Home");
            this.Children.Add(new ContentPage
                {
                    Title = "Prescriptions",
					BackgroundImage = "tricareBG.png",
                    Content = new StackLayout
          {
              VerticalOptions = LayoutOptions.StartAndExpand,
              Padding = new Thickness(20),
              Children = {
					AddPrescriptionButton, ManagePrescriptionButton
              }
          },
                }
            );
            this.Children.Add(new ContentPage
            {
                Title = "Patients",
					BackgroundImage = "tricareBG.png",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Padding = new Thickness(20),
                    Children = {
					AddPatientButton, ManagePatientButton
              }
                }
            });
			this.Children.Add (new PrescriberPage ());
        }

    }
}
