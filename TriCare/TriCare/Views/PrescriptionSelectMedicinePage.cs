using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TriCare.Data;

namespace TriCare.Views
{
    public class PrescriptionSelectMedicinePage : ContentPage
    {
        public PrescriptionSelectMedicinePage()
        {
			this.BackgroundImage = "tricareBG.png";

			this.SetBinding(ContentPage.TitleProperty, "Select Medicine");
			var mRepo = new MedicineRepo ();
			var meds = mRepo.GetAllMedicines ();
			var myContent = new StackLayout
			{
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
			};
			foreach (var med in meds) {

				var newButton = new Button { Text = med.Name.Trim() , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
				newButton.Clicked += async (sender, e) => {
					App.CurrentPrescription.Medicine = new TriCare.Models.MedicineModelForPrescription();
					App.CurrentPrescription.Medicine .MedicineId = med.MedicineId;
					App.CurrentPrescription.Medicine .MedicineName = med.Name.Trim();
					await this.Navigation.PushAsync (new MedicineIngredientListPage (med.MedicineId));
				};
				myContent.Children.Add (newButton);
			}

			Content = myContent;


        }
    }
}
