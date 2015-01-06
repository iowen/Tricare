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
			this.BackgroundImage = "tricareBG.png";
			App.EnableLogout ();
			var pRepo = new PrescriberRepo ();
			var pId = int.Parse(App.Token);
			var presc = pRepo.GetPrescriber (pId);
			App.ClearCurrentPrescription ();
			App.CurrentPrescription.Prescriber = presc;
            this.SetBinding(ContentPage.TitleProperty, "Select Patient");

			var newPatientButton = new Button { Text = "New Patient" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
            newPatientButton.Clicked += async (sender, e) =>
            {
                    await this.Navigation.PushAsync(new CreatePatientPage(true));
            };

			var existingPatientButton = new Button { Text = "Existing Patient", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White };
            existingPatientButton.Clicked += (sender, e) =>
            {
                this.Navigation.PushAsync(new PatientListPage(true));

            };
			var orLabel = new Label (){ Text = "- OR -",HorizontalOptions =LayoutOptions.Center, TextColor = Color.White };
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(20),
                Children = {
					newPatientButton,orLabel, existingPatientButton
                }
            };


        }
    }
}
