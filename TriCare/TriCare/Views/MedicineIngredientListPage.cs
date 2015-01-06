using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Data;
using TriCare.Models;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Controls;

namespace TriCare.Views
{
    public class MedicineIngredientListPage : ContentPage
    {
		ListView listView;
		int medicineId;
		float addedPercentage;
		private ObservableCollection<object> ingredientList =
			new ObservableCollection<object>();
		public ObservableCollection<object> IngredientList
		{
			get { return ingredientList; }
			set
			{
				ingredientList = value;
			//	NotifyPropertyChanged(m => m.IngredientList);
			}
		}
		public MedicineIngredientListPage (int _medicineId)
		{
			this.BackgroundImage = "tricareBG.png";
			var iRepo = new IngredientRepo ();
			var ir = iRepo.GetAllIngredients ();
			foreach (var i in ir) {
				IngredientList.Add (i.Name.Trim ());
			}
			medicineId = _medicineId;
			Title = "Formula";
			var addIngredientButton = new Button { Text = "Add" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White,WidthRequest= 120 };
          
            //for version 2
            //addIngredientButton.Clicked += async (sender, e) =>
            //{

            //    await this.Navigation.PushModalAsync(new AddIngredients());

            //};

			var continueButton = new Button { Text = "Continue", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White ,WidthRequest= 120 };
			continueButton.Clicked += (sender, e) =>
			{
				//show add modal;
				App.CurrentPrescription.Medicine.Ingredients = new List<PrescriptionMedicineIngredientModel>();

				var en =listView.ItemsSource.GetEnumerator();
				while(en.MoveNext()){
					//get ingred. in list
					var it = (PrescriptionMedicineIngredient)en.Current;
					App.CurrentPrescription.Medicine.Ingredients.Add(new PrescriptionMedicineIngredientModel(){  Percentage = it.Percentage, IngredientId = it.IngredientId, Name = it.Name}); 
				}

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
			grid.Children.Add(addIngredientButton);
			var bv = new Label {
				Text = "Leftover space",
				TextColor = Color.Transparent,
				XAlign = TextAlignment.Center,
				YAlign = TextAlignment.Center,
				HeightRequest = 1,
				BackgroundColor = Color.Transparent
			};
			grid.Children.Add(bv, 0,0);
			grid.Children.Add(continueButton);

            // for version 2
		//	Grid.SetColumn (addIngredientButton, 0);
			Grid.SetColumn (bv, 1);
			Grid.SetColumn (continueButton, 2);
		
			listView = new ListView ();
			listView.BackgroundColor = Color.Transparent;
			listView.ItemTemplate = new DataTemplate 
					(typeof (IngredientCell));
			listView.ItemSelected += async (sender, e) => {
				var selected = (PrescriptionMedicineIngredient)e.SelectedItem;
				var action = await DisplayActionSheet("Menu","Cancel","Delete",new []{"Edit"});
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
	// for version 2
            //		layout.Children.Add (new Label { TextColor = Color.White, Text = "Tap an ingredient to edit." });
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
