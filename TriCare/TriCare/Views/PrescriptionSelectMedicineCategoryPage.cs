using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TriCare.Data;

namespace TriCare.Views
{
    public class PrescriptionSelectMedicineCategoryPage : ContentPage
    {
        public PrescriptionSelectMedicineCategoryPage()
        {
			this.BackgroundColor = Color.White;
			this.SetBinding(ContentPage.TitleProperty, "Select Medicine Category");
			var mRepo = new MedicineCategoryRepo ();
			var meds = mRepo.GetAllMedicineCategories ();
			Title = "Medicine Categories";
			var myContent = new StackLayout
			{
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Padding = new Thickness(20),
			};
			foreach (var med in meds) {

				var newButton = new Button { Text = med.Name.Trim() , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
				newButton.Clicked += async (sender, e) => {
					App.CurrentPrescription.MedicineCategoryId = med.MedicineCategoryId;
					await App.np.PushAsync (new PrescriptionSelectMedicinePage (med.MedicineCategoryId));
				};
				myContent.Children.Add (newButton);
			}

			Content = myContent;


        }
    }
}
