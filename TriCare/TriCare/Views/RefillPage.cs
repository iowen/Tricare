using System;
using System.Collections.Generic;
using TriCare.Data;
using Xamarin.Forms;

namespace TriCare
{
	public class RefillPage :ContentPage
	{
		public RefillPage ()
		{
            var rRepo = new RefillRepo();
            Title = "Refills";

            var ra =rRepo.GetAllRefillAmounts();
			var refillAmountPicker = new Picker (); 
			var refillQuantPicker = new Picker ();
            foreach(var r in ra)
            {
				refillAmountPicker.Items.Add(r.Amount.ToString());
            }


            var qa =rRepo.GetAllRefillQuantities();
            foreach(var q in qa)
            {
				refillQuantPicker.Items.Add(q.Quantity.ToString());
            }


            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(20),
                Children = {
					refillAmountPicker, refillQuantPicker
                }
            };
		}
	}
}

