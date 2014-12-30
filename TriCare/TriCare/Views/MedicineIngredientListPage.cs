using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Data;
using TriCare.Models;
using Xamarin.Forms;

namespace TriCare.Views
{
    public class MedicineIngredientListPage : ContentPage
    {
		ListView listView;
		int medicineId;
		public MedicineIngredientListPage (int _medicineId)
		{
			this.BackgroundImage = "tricareBG.png";

			medicineId = _medicineId;
			Title = "Formula";
			var addIngredientButton = new Button { Text = "Add Ingredient" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
			addIngredientButton.Clicked += (sender, e) =>
			{
				//show add modal;

			};

			var continueButton = new Button { Text = "Continue", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White };
			continueButton.Clicked += (sender, e) =>
			{
				//show add modal;
				var en =listView.ItemsSource.GetEnumerator();
				do{
					//get ingred. in list
				}while(en.MoveNext());

                Navigation.PushAsync(new RefillPage());
			};
			Grid grid = new Grid
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions = 
				{
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
				},
				ColumnDefinitions = 
				{
					new ColumnDefinition { Width = GridLength.Auto },
				}
				};
			grid.Children.Add(addIngredientButton,0,0);

			grid.Children.Add(continueButton, 20,0);

		
		
			listView = new ListView ();
			listView.BackgroundColor = Color.Transparent;
			listView.ItemTemplate = new DataTemplate 
					(typeof (IngredientCell));
			listView.ItemSelected += (sender, e) => {
				var selected = (PrescriptionMedicineIngredient)e.SelectedItem;
   
//var patientPage = new PatientPage(selected);
  //                  Navigation.PushAsync(patientPage);

			};



			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
			var layout = new StackLayout();
			if (Device.OS == TargetPlatform.WinPhone) { // WinPhone doesn't have the title showing
				layout.Children.Add(new Label{
					Text="Formula", 
					Font=Font.SystemFontOfSize (NamedSize.Large)});
			}
			layout.Children.Add (grid);
			layout.Children.Add (	new ScrollView
				{
					Content = listView,
					VerticalOptions = LayoutOptions.FillAndExpand
				}
				);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;


		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			MedicineIngredientRepo mRepo = new MedicineIngredientRepo ();
            listView.ItemsSource = mRepo.ConvertIngreditentsToPrescriptionIngredients(mRepo.GetIngredientsForMedicine(medicineId));
		}

    }
}
