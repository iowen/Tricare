using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TriCare.Data;
namespace TriCare.Views
{
    public class PrescriptionNewORSelectPatientPage : ContentPage
    {
        public PrescriptionNewORSelectPatientPage()
        {
			this.BackgroundColor = Color.White;
			var pRepo = new PrescriberRepo ();
			var pId = int.Parse(App.Token);
			var presc = pRepo.GetPrescriber (pId);
			App.ClearCurrentPrescription ();
			App.CurrentPrescription.Prescriber = presc;
			this.Title = "Select Patient";

			var newPatientButton = new Button { Text = "New Patient" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
            newPatientButton.Clicked += async (sender, e) =>
            {
				await App.np.PushAsync(new CreatePatientPage(true));
            };

			var existingPatientButton = new Button { Text = "Existing Patient", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White };
            existingPatientButton.Clicked += async (sender, e) =>
            {
				await App.np.PushAsync(new PatientListPage(true));

            };
			var orLabel = new Label (){ Text = "- OR -",HorizontalOptions =LayoutOptions.Center, TextColor = Color.Gray };
            Content = new StackLayout
            {
				VerticalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(20),
                Children = {
					newPatientButton,orLabel, existingPatientButton
                }
            };


        }
    }
}
