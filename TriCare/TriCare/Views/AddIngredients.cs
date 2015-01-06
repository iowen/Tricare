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
	public class AddIngredients : ContentPage
	{
		private List<object> ingredientList =
			new List<object>();

		public ICommand SearchCommand { get; set; }
		public ICommand CellSelectedCommand { get; set; }
		public List<object> IngredientList
		{
			get { return ingredientList; }
			set
			{
				ingredientList = value;
				OnPropertyChanged ();	
			}
		}
		private AutoCompleteView a;
		private string searchText;
		public string SearchText{ get{ return searchText;} set{searchText = value; OnPropertyChanged (); }}
		public AddIngredients ()
		{
			var a1 = new AutoCompleteView ();
			SearchCommand = new Command((key) =>
				{
					//DisplayAlert("Search",a.AvailableSugestions.Count.ToString(),"close");
					DisplayAlert("Search",a.Sugestions.Count.ToString(),"close");
					//DisplayAlert("Search",IngredientList.Count.ToString(),"close");
					// Add the key to the input string.
				});

			CellSelectedCommand = new Command<Ingredient>((key) =>
				{
					// Add the key to the input string.
					this.Opacity = 50;
				});
			var iRepo = new IngredientRepo ();
			var t = iRepo.GetAllIngredients ();
	
			a = new AutoCompleteView () {
				SearchBackgroundColor = Color.Gray,
				ShowSearchButton = true,
				SearchCommand = SearchCommand,
				SelectedCommand = CellSelectedCommand,
				SugestionItemDataTemplate = new DataTemplate (typeof(IngCell)),
				BindingContext = t,
				SugestionBackgroundColor = Color.Blue,
				Placeholder = "Name",
			};

			foreach (var i in t) {
				ingredientList.Add(i);
			}

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(80),
				Children = { a }
			};
			//this.Content.BindingContext = t;
		}
	}
}

