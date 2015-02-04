using System;
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
		private Button AddPatientButton; 
		private Button ManagePatientButton; 
		private Button AddPrescriptionButton; 
		private Button ManagePrescriptionButton; 
		private Button EditProfileButton; 
		private ActivityIndicator indi;
        public HomePage()
        {
			App.EnableLogout ();
			App.IsHome = true;
			App.IsLogin = false;
			indi = new ActivityIndicator ();
			indi.AnchorX = this.AnchorX / 2;
			NavigationPage.SetHasBackButton (this, false);
			var overlay = new AbsoluteLayout();
			var content = new StackLayout();
			this.BackgroundColor = Color.White;
			AddPatientButton = new Button { Text = "Add Patient" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
            AddPatientButton.Clicked += async (sender, e) =>
            {
				DisableButtons();
				if(!App.IsConnected())
				{
					await DisplayAlert ("Error", "Patients cannot be added without an internet connection.", "OK", "close");
					EnableButtons();
					return;
				}
				App.IsHome = false;
                await App.np.PushAsync(new CreatePatientPage());
				EnableButtons();

            };

			ManagePatientButton = new Button { Text = "Manage Patients", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White };
            ManagePatientButton.Clicked += async (sender, e) =>
            {
				DisableButtons();
				App.IsHome = false;
               await App.np.PushAsync(new PatientListPage());
				EnableButtons();
            };

			AddPrescriptionButton = new Button { Text = "Add Prescription" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
            AddPrescriptionButton.Clicked += async (sender, e) =>
            {
				DisableButtons();
				if(!App.IsConnected())
				{
					await DisplayAlert ("Error", "Prescriptions cannot be created without an internet connection.", "OK", "close");
					EnableButtons();
					return;
				}
				App.IsHome = false;

				await App.np.PushAsync(new PrescriptionNewORSelectPatientPage());
				EnableButtons();


            };

			ManagePrescriptionButton = new Button { Text = "Prescription History", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White };
            ManagePrescriptionButton.Clicked += async (sender, e) =>
            {
				DisableButtons();
				App.IsHome = false;
				await App.np.PushAsync(new PrescriptionListPage());
				EnableButtons();


            };
			EditProfileButton = new Button { Text = "Edit Profile", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White };
            EditProfileButton.Clicked += async (sender, e) =>
            {
				DisableButtons();
				App.IsHome = false;

				await  App.np.PushAsync(new PrescriberPage());
				EnableButtons();


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
					indi,AddPrescriptionButton, ManagePrescriptionButton
              }
					},
					Icon = "prescriptionIcon.png"
                });
            this.Children.Add(new ContentPage
            {
                Title = "Patients",
					BackgroundColor = Color.White,
                Content = new StackLayout
                {
						VerticalOptions = LayoutOptions.CenterAndExpand,
                    Padding = new Thickness(20),
                    Children = {
					indi,AddPatientButton, ManagePatientButton
              }
                },
					Icon = "patientIcon.png"
            });
			this.Children.Add (new PrescriberPage ());

        }
		private void  DisableButtons()
		{
			AddPatientButton.IsEnabled = false;
			ManagePatientButton.IsEnabled = false;
			AddPrescriptionButton.IsEnabled = false;
			AddPrescriptionButton.IsEnabled = false;
			ManagePrescriptionButton.IsEnabled = false;
			EditProfileButton.IsEnabled = false;
			indi.IsVisible = true;
			indi.IsRunning = true;

		}
		private void  EnableButtons()
		{
			AddPatientButton.IsEnabled = true;
			ManagePatientButton.IsEnabled = true;
			AddPrescriptionButton.IsEnabled = true;
			ManagePrescriptionButton.IsEnabled = true;
			EditProfileButton.IsEnabled = true;
			indi.IsVisible = false;
			indi.IsRunning = false;

		}
		protected override void OnCurrentPageChanged ()
		{
			if(Device.OS == TargetPlatform.iOS)
				Title = this.CurrentPage.Title;
			base.OnCurrentPageChanged ();
		}
		protected override void OnAppearing ()
		{
			App.IsHome = true;
			if(Device.OS == TargetPlatform.iOS)
			Title = this.CurrentPage.Title;

			base.OnAppearing ();
		}
		protected override bool OnBackButtonPressed ()
		{
			return false;
		}

    }
}
