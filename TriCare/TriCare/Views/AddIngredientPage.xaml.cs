using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using TriCare.Data;
namespace TriCare.Views
{	
	public partial class AddIngredientPage : ContentPage
	{	

		public AddIngredientPage ()
		{
			InitializeComponent ();
			var iRepo = new IngredientRepo ();
			this.BindingContext = iRepo.GetAllIngredients ();

		}
	}
}

