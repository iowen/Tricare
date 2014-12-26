using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TriCare.Views
{
    public class PrescriptionNewORSelectPatientPage : ContentPage
    {
        public PrescriptionNewORSelectPatientPage()
        {
            this.SetBinding(ContentPage.TitleProperty, "Select Patient");

            var newPatientButton = new Button { Text = "New Patient" };
            newPatientButton.Clicked += async (sender, e) =>
            {
                    await this.Navigation.PushAsync(new CreatePatientPage(true));
            };

            var existingPatientButton = new Button { Text = "Existing Patient" };
            existingPatientButton.Clicked += (sender, e) =>
            {
                this.Navigation.PushAsync(new PatientListPage(true));

            };
			var orLabel = new Label (){ Text = "- OR -" };
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
