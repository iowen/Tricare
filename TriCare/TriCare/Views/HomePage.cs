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
			NavigationPage.SetHasNavigationBar (this, false);
			this.BackgroundImage = "tricareBG.png";

            var AddPatientButton = new Button { Text = "Add Patient" };
            AddPatientButton.Clicked += (sender, e) =>
            {
                this.Navigation.PushAsync(new CreatePatientPage());

            };

            var ManagePatientButton = new Button { Text = "Manage Patients" };
            ManagePatientButton.Clicked += (sender, e) =>
            {
                this.Navigation.PushAsync(new PatientListPage());

            };

            var AddPrescriptionButton = new Button { Text = "Add Prescription" };
            AddPrescriptionButton.Clicked += (sender, e) =>
            {
                this.Navigation.PushAsync(new PrescriptionNewORSelectPatientPage());

            };

            var ManagePrescriptionButton = new Button { Text = "Prescription History" };
            ManagePrescriptionButton.Clicked += (sender, e) =>
            {
                this.Navigation.PushAsync(new RegisterPage());

            };
            var EditProfileButton = new Button { Text = "Edit Profile" };
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
            this.Children.Add(new ContentPage
            {
                Title = "Profile",
					BackgroundImage = "tricareBG.png",
                Content = new StackLayout
              {
                  VerticalOptions = LayoutOptions.StartAndExpand,
                  Padding = new Thickness(20),
                  Children = {
					EditProfileButton
              }
              }
            });
        }

    }
}
