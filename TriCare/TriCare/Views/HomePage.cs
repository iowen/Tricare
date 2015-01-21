﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Labs;
using Xamarin.Forms.Labs.Controls;

namespace TriCare.Views
{
    public class HomePage : ExtendedTabbedPage
    {
        public HomePage()
        {
			App.EnableLogout ();
			NavigationPage.SetHasBackButton (this, false);
			this.BackgroundColor = Color.White;
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
				this.Navigation.PushAsync(new PrescriptionListPage());

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
					BackgroundColor = Color.White,
                    Content = new StackLayout
          {
              VerticalOptions = LayoutOptions.CenterAndExpand,
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
					BackgroundColor = Color.White,
                Content = new StackLayout
                {
						VerticalOptions = LayoutOptions.CenterAndExpand,
                    Padding = new Thickness(20),
                    Children = {
					AddPatientButton, ManagePatientButton
              }
                }
            });
			this.Children.Add (new PrescriberPage ());
        }

		protected override bool OnBackButtonPressed ()
		{
			return false;
		}

    }
}
