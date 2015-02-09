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
		public PrescriptionSelectMedicinePage(int categoryId)
        {
			this.BackgroundColor = Color.White;
			this.SetBinding(ContentPage.TitleProperty, "Select Medicine");
			var mRepo = new MedicineRepo ();
			var meds = mRepo.GetMedicinesForCategory(categoryId);
			Title = "Medicines";
			var myContent = new StackLayout
			{
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Padding = new Thickness(20),
			};
			foreach (var med in meds) {

				var newButton = new Button { Text = med.Name.Trim() , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
				newButton.Clicked += async (sender, e) => {
					App.CurrentPrescription.Medicine = new TriCare.Models.MedicineModelForPrescription();
					App.CurrentPrescription.Medicine .MedicineId = med.MedicineId;
					App.CurrentPrescription.Medicine .MedicineName = med.Name.Trim();
					App.CurrentPrescription.Medicine.Directions = med.Directions.Trim();
					App.CurrentPrescription.Medicine.MedicineDetail = med.MedicineDetail.Trim();
					await App.np.PushAsync (new MedicineIngredientListPage (med.MedicineId));
				};
				myContent.Children.Add (newButton);
			}

			Content = myContent;


        }
    }
}
