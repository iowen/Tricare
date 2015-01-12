using System;
using System.Collections.Generic;
using Xamarin.Forms;
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
		}
		private AutoCompleteView a;
		private string searchText;
		public string SearchText{ get{ return searchText;} set{searchText = value; OnPropertyChanged (); }}
		public AddIngredients ()
		{

			var a1 = new AutoCompleteView ();
			SearchCommand = new Command((key) =>
				{
					DisplayAlert("Search",a.Suggestions.Count.ToString(),"close");
					//DisplayAlert("Search",a.Sugestions.Count.ToString(),"close");
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

			foreach (var i in t) {
				ingredientList.Add(i);
			}
			a = new AutoCompleteView () {
				SearchBackgroundColor = Color.Gray,
				ShowSearchButton = false,
				Suggestions = IngredientList,
				SearchCommand = SearchCommand,
				SelectedCommand = CellSelectedCommand,
				SuggestionItemDataTemplate = new DataTemplate (typeof(IngCell)),
				SuggestionBackgroundColor = Color.Blue,
				Placeholder = "Name",
			};
			var l = new Label {
				Text = "Rsdsd"
			};
			var l2 = new Label {
				Text = "dsdsdsd"
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(80),
				Children = { l, a, l2}
			};
			//this.Content.BindingContext = t;
		}
	}
}

