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
            Title = "Formula";
            var amounts  =new List<string>();

            var ra =rRepo.GetAllRefillAmounts();
            foreach(var r in ra)
            {
                amounts.Add(r.Amount.ToString());
            }

                        var quants  =new List<string>();

            var qa =rRepo.GetAllRefillQuantities();
            foreach(var q in qa)
            {
                quants.Add(q.Quantity.ToString());
            }
            var refillAmountPicker = new Picker { BindingContext = amounts };
            var refillQuantPicker = new Picker { BindingContext = quants };

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

