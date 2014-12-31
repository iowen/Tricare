using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Controls;
using System.Collections.ObjectModel;
using TriCare.Data;
using TriCare.Models;
using System.Windows.Input;
namespace TriCare.Views
{	
	public partial class AddIngredientPage : ContentPage
	{	
		private ObservableCollection<Ingredient> ingredientList =
			new ObservableCollection<Ingredient>();
		private ObservableCollection<Ingredient> ingredientList1 =
			new ObservableCollection<Ingredient>();
		public ICommand SearchCommand { get; set; }
		public ICommand CellSelectedCommand { get; set; }
		public ObservableCollection<Ingredient> IngredientList
		{
			get { return ingredientList; }
			set
			{
				ingredientList = value;
				OnPropertyChanged ();	
			}
		}
		private string searchText;
		public string SearchText{ get{ return searchText;} set{searchText = value; OnPropertyChanged (); }}
		public AddIngredientPage ()
		{
			var a = new AutoCompleteView ();
			InitializeComponent ();
			SearchText = "help";
			SearchCommand = new Command((key) =>
				{
					DisplayAlert("Search","scl","close");
					// Add the key to the input string.
				});

			CellSelectedCommand = new Command<Ingredient>((key) =>
				{
					// Add the key to the input string.
					this.Opacity = 50;
				});


			var iRepo = new IngredientRepo ();
			var t = iRepo.GetAllIngredients ();
			foreach (var i in t) {
				ingredientList.Add(i);
			}
			IngredientList = ingredientList1;
			//this.Content.BindingContext = t;

		}
	}
}

