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
			var addIngredientButton = new Button { Text = "Add Ingredient" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White,WidthRequest= 120 };
			addIngredientButton.Clicked += (sender, e) =>
			{
				//show add modal;


			};

			var continueButton = new Button { Text = "Continue", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White ,WidthRequest= 120 };
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
				HorizontalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions = 
				{
					new RowDefinition { Height = GridLength.Auto },
				},
				ColumnDefinitions = 
				{
					new ColumnDefinition { Width = new GridLength(120, GridUnitType.Absolute)},
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(120, GridUnitType.Absolute)}
				}
				};
			grid.Children.Add(addIngredientButton,0,0);
			var bv = new Label {
				Text = "Leftover space",
				TextColor = Color.Transparent,
				XAlign = TextAlignment.Center,
				YAlign = TextAlignment.Center,

				BackgroundColor = Color.Transparent
			};
			grid.Children.Add(bv, 0,0);
			grid.Children.Add(continueButton, 0,0);

			Grid.SetColumn (addIngredientButton, 0);
			Grid.SetColumn (bv, 1);
			Grid.SetColumn (continueButton, 2);
		
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
